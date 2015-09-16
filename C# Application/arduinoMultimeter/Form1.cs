using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;        // added for Serial port functions
using System.Data.SqlClient;    // to connect with database
using ZedGraph;                  // added as reference. Added .NET Framework component to Toolbox


namespace arduinoMultimeter
{
    public partial class testForm : Form
    {
        // ---- INITIALIZE GLOBAL VARIABLES ----

        // ---- DataBase variables        
        private SqlConnection myConnection;

        string wheelSide;
        string normalCurrentR, normalRPMrightCCW, normalRPMrightCW;
        string stallCurrentR, stallRPMright;
        string switchTestR;
        string finalTestResultR;
        bool testRcomplete = false;


        string normalCurrentL, normalRPMleftCCW, normalRPMleftCW;
        string stallCurrentL, stallRPMleft;
        string switchTestL;
        string finalTestResultL;
        bool testLcomplete = false;


        //string SessionID = dataContainer.SessionID;             // used in both forms, initialized in LogIN.cs

        // --- for ZedGraph. RIGHT Wheel
        RollingPointPairList listPointsOneRight = new RollingPointPairList(110);
        RollingPointPairList listPointsTwoRight = new RollingPointPairList(110);
        GraphPane ampGraphPaneRight = new GraphPane();
        GraphPane rpmGraphPaneRight = new GraphPane();
        LineItem myCurveOneRight;
        LineItem myCurveTwoRight;
        int counterRight =0;                                              // for x-axis

        // --- for ZedGraph. LEFT Wheel
        RollingPointPairList listPointsOneLeft = new RollingPointPairList(110);
        RollingPointPairList listPointsTwoLeft = new RollingPointPairList(110);
        GraphPane ampGraphPaneLeft = new GraphPane();
        GraphPane rpmGraphPaneLeft = new GraphPane();
        LineItem myCurveOneLeft;
        LineItem myCurveTwoLeft;
        int counterLeft = 0;      


        // --- Serial port data from Arduino: [AmpsR(double), RPMright(float),  microswitchR , presenceR,   physicalButton  , AmpsL, RPMleft, switchL, presenceL ] : active low.
        string output;                           // too store all values as string, comma delimited
        int pFrom, pTo;


        double currentRight,     currentLeft;
        double rightWheelRPM,    leftWheelRPM;
        int switchStatusR,       switchStatusL;
        int wheelPresentR,       wheelPresentL;
        int physicalButton;
        string unitID;

        // --- variables for keeping track of things
        int switchcounterRight = 0, switchCounterLeft = 0;      // tracks how many times switch was pressed
        int sameWheelR = 0, sameWheelL = 0;
        int nextTestReadyR = 0, nextTestReadyL = 0;

        int newTestTick = 0;
        int resetGraphCounterR = 0;
        int resetGraphCounterL = 0;
        
        // Initialize test results
        int rightTestResult = 0, leftTestResult = 0;
        int[] testResult = { 0, 0, 0,  0, 0 ,0};         // R[normal operation, stall test, microswitch] + L[normal, stall, microswitch]


        bool rightGraphOn = true;
        bool leftGraphOn = true;

        public testForm()                       // connects to DB and initializes Zed Graphs
        {
            InitializeComponent();
            
            myConnection = new SqlConnection("user id=PRCAdmin;" +
                                               "password=***;" +
                                               "server=***;" +
                                               "Trusted_Connection=false;" +
                                               "database=PRCData; ");
            try
            {
                myConnection.Open();
                connection_lbl.Text = "Connected";
                connection_lbl.ForeColor = Color.Green;
            }
            catch
            {
                connection_lbl.Text = "Connection failed";
                connection_lbl.ForeColor = Color.Red;
            }
             

            // RIGHT, Current
            ampGraphRight.IsShowPointValues = true;
            ampGraphPaneRight = ampGraphRight.GraphPane;                // points to pane
            ampGraphPaneRight.Title.Text = "Amps";
            ampGraphPaneRight.XAxis.Title.Text = "Time (s)";
            ampGraphPaneRight.YAxis.Title.Text = "Amps (mA)";
            ampGraphPaneRight.YAxis.Scale.Min = 0;
            ampGraphPaneRight.YAxis.Scale.Max = 1;
            myCurveOneRight = ampGraphRight.GraphPane.AddCurve(null, listPointsOneRight, Color.Red, SymbolType.None);         // points to curve in graph
            ampGraphRight.IsAutoScrollRange = true;
            ampGraphRight.IsShowHScrollBar = false;
            myCurveOneRight.Line.Width = 4;

            // RIGHT RPM 
            rpmGraphRight.IsShowPointValues = true;
            rpmGraphPaneRight = rpmGraphRight.GraphPane;                // points to pane
            rpmGraphPaneRight.Title.Text = "RPM";
            rpmGraphPaneRight.XAxis.Title.Text = "Time (s)";
            rpmGraphPaneRight.YAxis.Title.Text = "Revolutions";
            rpmGraphPaneRight.YAxis.Scale.Min = 0;
            rpmGraphPaneRight.YAxis.Scale.Max = 100;
            myCurveTwoRight = rpmGraphRight.GraphPane.AddCurve(null, listPointsTwoRight, Color.Blue, SymbolType.None);        // points to curve in graph
            rpmGraphRight.IsAutoScrollRange = true;
            rpmGraphRight.IsShowHScrollBar = false;
            myCurveTwoRight.Line.Width = 4;


            // LEFT, Current
            ampGraphLeft.IsShowPointValues = true;
            ampGraphPaneLeft = ampGraphLeft.GraphPane;                // points to pane
            ampGraphPaneLeft.Title.Text = "Amps";
            ampGraphPaneLeft.XAxis.Title.Text = "Time (s)";
            ampGraphPaneLeft.YAxis.Title.Text = "Amps (mA)";
            ampGraphPaneLeft.YAxis.Scale.Min = 0;
            ampGraphPaneLeft.YAxis.Scale.Max = 1;
            myCurveOneLeft = ampGraphLeft.GraphPane.AddCurve(null, listPointsOneLeft, Color.Red, SymbolType.None);         // points to curve in graph
            ampGraphLeft.IsAutoScrollRange = true;
            ampGraphLeft.IsShowHScrollBar = false;
            myCurveOneLeft.Line.Width = 4;

            // LEFT RPM 
            rpmGraphLeft.IsShowPointValues = true;
            rpmGraphPaneLeft = rpmGraphLeft.GraphPane;                // points to pane
            rpmGraphPaneLeft.Title.Text = "RPM";
            rpmGraphPaneLeft.XAxis.Title.Text = "Time (s)";
            rpmGraphPaneLeft.YAxis.Title.Text = "Revolutions";
            rpmGraphPaneLeft.YAxis.Scale.Min = 0;
            rpmGraphPaneLeft.YAxis.Scale.Max = 100;
            myCurveTwoLeft = rpmGraphLeft.GraphPane.AddCurve(null, listPointsTwoLeft, Color.Blue, SymbolType.None);        // points to curve in graph
            rpmGraphLeft.IsAutoScrollRange = true;
            rpmGraphLeft.IsShowHScrollBar = false;
            myCurveTwoLeft.Line.Width = 4;

        }

        // ON LOAD get available ports and place on combo box
        private void testForm_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            arduinoPort_cmb.DataSource = ports;

        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            resetBothGraphsTimer.Start();

            if (!myPort.IsOpen)     // if port is closed
            {
                myPort = new SerialPort();
                myPort.BaudRate = 9600;
                //myPort.PortName = "COM8";
                myPort.PortName = arduinoPort_cmb.Text;           // pick port in comboBox
                myPort.Parity = Parity.None;
                myPort.DataBits = 8;
                myPort.StopBits = StopBits.One;
                myPort.Encoding = System.Text.Encoding.Default;
                myPort.DataReceived += myPort_DataReceived;     // calls function when data received
                myPort.DtrEnable = true;
                myPort.RtsEnable = false;

                try
                {
                    myPort.Open();
                    txtBox1.Text = "Connection Established ... \n";
                    connect_btn.Text = "Disconnect";
                    connect_btn.BackColor = Color.IndianRed;
                }
                catch { };


            }
            else if (myPort.IsOpen)                      // if port is open and Btn_Click, Disconnect
            {
                try
                {
                    myPort.DiscardInBuffer();
                    myPort.Close();
                    connect_btn.Text = "Connect";
                    connect_btn.BackColor = Color.White;
                }
                catch { };
            }


        } // end of CONNECT_BTN_CLICK

                //======== connect_btn DATA RECEIVED Function ========
                private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
                {               // use TRy to avoid multiTHreading issue with closing port.
                    try
                    {
                        output = myPort.ReadLine();
                        this.Invoke(new EventHandler(displaydata_event));
                    }
                    catch { }
                  
                }   // end of myPort_DataReceived

                // Gets called every time through the loop
                void updatePassLabels()
                {

                    receiveBuffer_lbl.Text = Convert.ToString(myPort.BytesToRead);
                    sendBuffer_lbl.Text = Convert.ToString(myPort.BytesToWrite);
                    bufferSize_lbl.Text = Convert.ToString(myPort.ReadBufferSize);


  // SHARED: UPDATES CHECKBOXES 
                    PictureBox[] testPicArrayRight = { RnormalOperation_pic, RstallTest_pic, RdropTest_lbl, LnormalOperation_pic, LstallTest_pic, LdropTest_lbl };                   
                    for (int i = 0; i < testResult.Length; i++)                                 // update checkbox picture of corresponding test
                    {
                        if (testResult[i] == 1) 
                            testPicArrayRight[i].Image = Properties.Resources.check;
                        else if (testResult[i] == 2)
                            testPicArrayRight[i].Image = Properties.Resources.xMark;
                        else
                            testPicArrayRight[i].Image = null;
                    }

  //=========== WHEEL DROP right and left:  ON or OFF
                    int[] tempArray = {switchStatusR, switchStatusL};
                    string tempString= null;
                    System.Drawing.Color tempColor = Color.White;
                    for (int i = 0; i < tempArray.Length; i++ )
                    {                        
                        switch (tempArray[i])              // value in array, 0 = OFF, 1= ON
                        {
                            case 0:
                                tempString = "OFF";  tempColor = Color.Red;
                                break;
                            case 1:
                                tempString = "ON"; tempColor = Color.Green;
                                break;
                        }

                        if (i == 0)             // we're on RIGHT WHEEL
                        {
                            switchcounterRight++;
                            Rswitch_txtBox.Text = tempString;
                            Rswitch_txtBox.ForeColor = tempColor;
                        }
                        else if (i == 1)        // we're on LEFT WHEEL
                        {
                            switchCounterLeft++;
                            Lswitch_txtBox.Text = tempString;
                            Lswitch_txtBox.ForeColor = tempColor;
                        }                            
                    }
 // ============= END OF WHEEL DROP

 //============== PHYSICAL BUTTON: Restart everything (graphs and test parameters)
                    if (physicalButton == 0)
                        resetGraphAndTests();                  


  // ============= RIGHT test is OVER but WHEEL REMAINS
                    if (sameWheelR == 1 && wheelPresentR == 1 && nextTestReadyR == 1)             // if its the same wheel and wheel is detected and timer has elapsed (nextTestReadyR = 1)
                    {          
                         finalPassR_lbl.Text = "Remove Wheel";
                         finalPassR_lbl.ForeColor = Color.Peru;
                         saveStatusR_lbl.Text = "Waiting";

                         resetGraphR.Start();                   // have same timer to clear graph 
                    }

  // =========== MICROCONTROLLER OUTPUTS STATUS OF TESTS. L & R. Test 1, 2, 3. Pass/Fail. 12 possibilities
                    string[] testString = new string[] { "RT1 CW", "RT1 : 0", "RT1 : 1", "RT2 : 0", "RT2 : 1", "RT3 : 0", "RT3 : 1", "LT1 CW", "LT1 : 0", "LT1 : 1", "LT2 : 0", "LT2 : 1", "LT3 : 0", "LT3 : 1", "UNIT" };
                    foreach (string result in testString)
                    {                                       // checks each element in testString[]
                        bool stringInOutput = output.Contains(result);
                        if (stringInOutput)                 // If STRING is contained in OUTPUT Switch 
                        {                            
                            switch (result)
                            {
                                case "UNIT":
                                    pFrom = output.IndexOf("UNIT ") + "UNIT ".Length;
                                    pTo = output.LastIndexOf(":");
                                    try
                                    {
                                        unitID = output.Substring(pFrom, pTo - pFrom);
                                    }
                                    catch { }

                                    if (output.Substring(0, 1).Contains("L"))
                                        testLcomplete = true;
                                    if (output.Substring(0, 1).Contains("R"))
                                        testRcomplete = true;                                   

                                    break;

                                case "RT1 CW":
                                    pFrom = output.IndexOf("RPM: ") + "RPM: ".Length;
                                    pTo = output.LastIndexOf(";");
                                    normalRPMrightCW = output.Substring(pFrom, pTo - pFrom);
                                    break;

                                case "RT1 : 0":
                                    testResult[0] = 2;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    normalCurrentR = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    normalRPMrightCCW = output.Substring(pFrom, pTo - pFrom);
                                    break;

                                case "RT1 : 1":
                                    testResult[0] = 1;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    normalCurrentR = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    normalRPMrightCCW = output.Substring(pFrom, pTo - pFrom);
                                    break;

                                case "RT2 : 0":
                                    testResult[1] = 2;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    stallCurrentR = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    stallRPMright = output.Substring(pFrom, pTo - pFrom);

                                    rightGraphOn = false;           // pause graph after 2nd test is compelte
                                    break;

                                case "RT2 : 1":
                                    testResult[1] = 1;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    stallCurrentR = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    stallRPMright = output.Substring(pFrom, pTo - pFrom);

                                    rightGraphOn = false;            // pause graph after 2nd test is compelte
                                    break;

                                case "RT3 : 0":
                                    testResult[2] = 2;
                                    switchTestR = "Fail";
                                    break;

                                case "RT3 : 1":
                                    testResult[2] = 1;
                                    switchTestR = "Pass";
                                    break;

                                // LEFT WHEEL 
                                case "LT1 CW":
                                    pFrom = output.IndexOf("RPM: ") + "RPM: ".Length;
                                    pTo = output.LastIndexOf(";");
                                    normalRPMleftCW = output.Substring(pFrom, pTo - pFrom);
                                    break;

                                case "LT1 : 0":
                                    testResult[3] = 2;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    normalCurrentL = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    normalRPMleftCCW = output.Substring(pFrom, pTo - pFrom);
                                    break;

                                case "LT1 : 1":
                                    testResult[3] = 1;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    normalCurrentL = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    normalRPMleftCCW = output.Substring(pFrom, pTo - pFrom);
                                    break;

                                case "LT2 : 0":
                                    testResult[4] = 2;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    stallCurrentL = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    stallRPMleft = output.Substring(pFrom, pTo - pFrom);

                                    leftGraphOn = false;                 // pause graph after 2nd test is compelte
                                    break;

                                case "LT2 : 1":
                                    testResult[4] = 1;
                                    pFrom = output.IndexOf("currentAvg = ") + "currentAvg = ".Length;
                                    pTo = output.LastIndexOf(",");
                                    stallCurrentL = output.Substring(pFrom, pTo - pFrom);

                                    pFrom = output.IndexOf("rpmAvg = ") + "rpmAvg = ".Length;
                                    pTo = output.LastIndexOf(";");
                                    stallRPMleft = output.Substring(pFrom, pTo - pFrom);

                                    leftGraphOn = false;                 // pause graph after 2nd test is compelte
                                    break;

                                case "LT3 : 0":
                                    testResult[5] = 2;
                                    switchTestL = "Fail";
                                    break;

                                case "LT3 : 1":
                                    testResult[5] = 1;
                                    switchTestL = "Pass";
                                    break;
                            }
                        }   // end of IF (stringOUTPUT)

                    }   // end of FOR EACH (string s in testString)

// ========= WRITE to DB: RIGHT and LEFT. Done when tests are complete (pass OR fail), runs ONCE
                    // RIGHT WHEEL
                    if (testResult[0] != 0 && testResult[1] != 0 && testResult[2] != 0 && sameWheelR == 0 && testRcomplete)         
                    {
                       // rightGraphOn = false;
                        wheelSide = "Right";
                        System.Drawing.Color tempForeColor = Color.White;
                        string tempFinalPassLabelText = null;

                        if (testResult[0] == 1 && testResult[1] == 1 && testResult[2] == 1)        // all tests PASS
                         {                             
                             finalTestResultR = "Pass";         // for DB
                             tempForeColor = Color.Green;
                             tempFinalPassLabelText = "ALL PASS!";
                         }
                         else
                         {                                                                          // at least one test FAILED
                             finalTestResultR = "Fail";         // for DB
                             tempForeColor = Color.Red;
                             tempFinalPassLabelText = "Wheel Failed!";
                         }

                        finalPassR_lbl.ForeColor = tempForeColor;
                        finalPassR_lbl.Text = tempFinalPassLabelText;

                        SqlCommand writeCommand = new SqlCommand("INSERT INTO iRobotWheelTest (UnitID, wheelSide, normalCurrent, normalRPMcw, normalRPMccw, stallCurrent, stallRPM, switchTest, testResult, DateTimeCreated)" +
                                                                 "VALUES('" + unitID + "','" + wheelSide + "', '" + normalCurrentR + "', '" + normalRPMrightCW + "', '" + normalRPMrightCCW + "', '" + stallCurrentR + "', '" + stallRPMright + "', '" + switchTestR + "', '" + finalTestResultR + "', GetDate() )", myConnection);
                        int success = writeCommand.ExecuteNonQuery();            // returns # of rows affected
                        if (success >= 1)
                            saveStatusR_lbl.Text = "Saved to Database!";
                        else
                            saveStatusR_lbl.Text = "Error";
                        
                        sameWheelR = 1;
                        testRcomplete = false;
                        newTestTimerR.Start();                 // start newTest timer. 100 ms /tick
                    }   // END of WRITE RIGHT WHEEL to DB

                    // LEFT WHEEL
                    if (testResult[3] != 0 && testResult[4] != 0 && testResult[5] != 0 && sameWheelL == 0 && testLcomplete)
                    {
                        //leftGraphOn = false;
                        wheelSide = "Left";
                        System.Drawing.Color tempForeColor = Color.White;
                        string tempFinalPassLabelText = null;

                        if (testResult[3] == 1 && testResult[4] == 1 && testResult[5] == 1)        // all tests PASS
                        {
                            finalTestResultL = "Pass";         // for DB
                            tempForeColor = Color.Green;
                            tempFinalPassLabelText = "ALL PASS!";
                        }
                        else
                        {                                                                          // at least one test FAILED
                            finalTestResultL = "Fail";         // for DB
                            tempForeColor = Color.Red;
                            tempFinalPassLabelText = "Wheel Failed!";
                        }

                        finalPassL_lbl.ForeColor = tempForeColor;
                        finalPassL_lbl.Text = tempFinalPassLabelText;

                        SqlCommand writeCommand = new SqlCommand("INSERT INTO iRobotWheelTest (UnitID, wheelSide, normalCurrent, normalRPMcw, normalRPMccw, stallCurrent, stallRPM, switchTest, testResult, DateTimeCreated)" +
                                                                     "VALUES('" + unitID + "','" + wheelSide + "', '" + normalCurrentL + "', '" + normalRPMleftCW + "', '" + normalRPMleftCCW + "', '" + stallCurrentL + "', '" + stallRPMleft + "', '" + switchTestL + "', '" + finalTestResultL + "', GetDate() )", myConnection);
                        int success = writeCommand.ExecuteNonQuery();            // returns # of rows affected
                        if (success >= 1)
                            saveStatusL_lbl.Text = "Saved to Database!";
                        else
                            saveStatusL_lbl.Text = "Error";

                        sameWheelL = 1;
                        testLcomplete = false;
                    }
// ======== END of 'WRITE to DB'

// ======== WHEEL PRESENCE: RIGHT AND LEFT
                    // RIGHT WHEEL PRESENCE
                    if (wheelPresentR == 0)               // NO RIGHT WHEEL present
                    {
                        rightWheelNotPresentFunc();       // resets graphs, results, timer, and sameWheel
                        rightGraphOn = true;
                    }
                        

                    else                                        //  (wheelPresentR = 1)
                    {                                           // only change if wheel is present and NOT saved to DB yet
                        if (sameWheelR == 0)                  
                        {    
                            finalPassR_lbl.Text = "Testing Wheel";
                            finalPassR_lbl.ForeColor = Color.Peru;
                            saveStatusR_lbl.Text = "Please Wait";
                            rightWheel_btn.BackColor = Color.YellowGreen;
                        }                      
                    } // end of ELSE (if WHEEL PRESENT)

                    // LEFT WHEEL PRESENCE
                    if (wheelPresentL == 0)               // NO RIGHT WHEEL present
                    {
                        leftWheelNotPresentFunc();       // resets graphs, results, timer, and sameWheel
                        leftGraphOn = true;
                    }
                        

                    else                                        //  (wheelPresentR = 1)
                    {                                           // only change if wheel is present and NOT saved to DB yet
                        if (sameWheelL == 0)
                        {
                            finalPassL_lbl.Text = "Testing Wheel";
                            finalPassL_lbl.ForeColor = Color.Peru;
                            saveStatusL_lbl.Text = "Please Wait";
                            leftWheel_btn.BackColor = Color.YellowGreen;
                        }
                    } // end of ELSE (if WHEEL PRESENT)
// ====== END OF WHEEL PRESENCE


                }   // end of updatePassLabel()

                        
                        // DISPLAY DATA EVENT. gets called every time DATA RECEIVED on myPort. Plots, saves to global variables and graphs
                        void displaydata_event(object s, EventArgs e)
                        {
                            //txtBox1.AppendText(output + "\n");                               // write all 'output' in txtbox1
                           // txtBox1.ScrollToCaret();

                            try
                            {
                                // SEPARATE OUTPUT: 
                                string[] readings = output.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

                                // write each value of array into text box
                                ampsRight_txtBox.Text = readings[0];                     // string for textbox
                                currentRight = Convert.ToDouble(readings[0]);       // convert to double for calculation
                                rightRPM_txtBox.Text = readings[1];
                                rightWheelRPM = Convert.ToDouble(readings[1]);  
                                switchStatusR = Convert.ToInt32(readings[2]);
                                wheelPresentR = Convert.ToInt32(readings[3]);            // active low
                                                                                                                        
                                physicalButton = Convert.ToInt32(readings[4]);

                                ampsLeft_txtBox.Text = readings[5];                    
                                currentLeft = Convert.ToDouble(readings[5]);       
                                leftRPM_txtBox.Text = readings[6];
                                leftWheelRPM = Convert.ToDouble(readings[6]);
                                switchStatusL = Convert.ToInt32(readings[7]);
                                wheelPresentL = Convert.ToInt32(readings[8]);        
                                //---------- OUTPUT (readings) is now saved into each variable. CLEAR 'readings' !!!!
                                readings = new string[] { };

                                
                                // ZedGraph Plotting
                                if (rightGraphOn)
                                {
                                    listPointsOneRight.Add(counterRight, currentRight);
                                   // myCurveOneRight.AddPoint(counterRight, currentRight);
                                    ampGraphPaneRight.XAxis.Scale.Max = counterRight;          //resize axis every time
                                    ampGraphPaneRight.XAxis.Scale.Min = counterRight - 100;
                                    ampGraphRight.AxisChange();                                 // scale axis
                                    ampGraphRight.Invalidate();                                 // redraw axis
                                    ampGraphRight.Refresh();


                                    listPointsTwoRight.Add(counterRight++, rightWheelRPM);
                                    //myCurveTwoRight.AddPoint(counterRight++, rightWheelRPM);
                                    rpmGraphPaneRight.XAxis.Scale.Max = counterRight;          //resize axis every time
                                    rpmGraphPaneRight.XAxis.Scale.Min = counterRight - 100;
                                    rpmGraphRight.AxisChange();
                                    rpmGraphRight.Invalidate();
                                    rpmGraphRight.Refresh();
                                }
                                
                                if (leftGraphOn)
                                {
                                    // LEFT WHEEL  ------------- (change counter)
                                    listPointsOneLeft.Add(counterLeft, currentLeft);
                                    //myCurveOneLeft.AddPoint(counterLeft, currentLeft);
                                    ampGraphPaneLeft.XAxis.Scale.Max = counterLeft;          //resize axis every time
                                    ampGraphPaneLeft.XAxis.Scale.Min = counterLeft - 100;
                                    ampGraphLeft.AxisChange();                                 // scale axis
                                    ampGraphLeft.Invalidate();                                 // redraw axis
                                    ampGraphLeft.Refresh();

                                    listPointsTwoLeft.Add(counterLeft++, leftWheelRPM);
                                   // myCurveTwoLeft.AddPoint(counterLeft++, leftWheelRPM);
                                    rpmGraphPaneLeft.XAxis.Scale.Max = counterLeft;          //resize axis every time
                                    rpmGraphPaneLeft.XAxis.Scale.Min = counterLeft - 100;
                                    rpmGraphLeft.AxisChange();
                                    rpmGraphLeft.Invalidate();
                                    rpmGraphLeft.Refresh();
                                }
                                
                               
                                

                            }
                            catch { }
        
                            updatePassLabels();                            
                            output = "";
                        }   // end of display data event


        private void resetGraphAndTests()
        {
            testResult[0] = 0;
            testResult[1] = 0;
            testResult[2] = 0; switchcounterRight = 0;
            rightTestResult = 0;

            // Clear line Points
            listPointsOneRight.Clear();
            listPointsTwoRight.Clear();


            myCurveTwoRight.Clear();
            myCurveOneRight.Clear();

            // Reset graph X- Axis
            counterRight = 0;

            nextTestReadyR = 0;
            sameWheelR = 0;

            // RESET TIMERS counting down to next test
            newTestTimerR.Stop();
            newTestTick = 0;

            resetGraphCounterR = 0;
            resetGraphR.Stop();

            rightGraphOn = true;


// LEFT WHEEL
            testResult[3] = 0;
            testResult[4] = 0;
            testResult[5] = 0; switchCounterLeft = 0;
            leftTestResult = 0;

            // Clear line Points
            listPointsOneLeft.Clear();
            listPointsTwoLeft.Clear();
            myCurveTwoLeft.Clear();
            myCurveOneLeft.Clear();


            // Reset graph X- Axis
            counterLeft = 0;

            nextTestReadyL = 0;
            sameWheelL = 0;

            leftGraphOn = true;
        }

        private void rightWheelNotPresentFunc()
        {
            rightWheel_btn.BackColor = Color.White;
            finalPassR_lbl.Text = "Place Wheel";
            finalPassR_lbl.ForeColor = Color.Peru;
            saveStatusR_lbl.Text = "Waiting";

            testResult[0] = 0;
            testResult[1] = 0;
            testResult[2] = 0; switchcounterRight = 0;
            rightTestResult = 0;

            // Clear line Points
            listPointsOneRight.Clear();
            listPointsTwoRight.Clear();
            myCurveTwoRight.Clear();
            myCurveOneRight.Clear();

            nextTestReadyR = 0;
            sameWheelR = 0;

            newTestTimerR.Stop();
            newTestTick = 0;

            // if (resetGraphCounterR > 30)

            resetGraphCounterR = 0;


            resetGraphR.Stop();

            resetGraphR.Start();            // TIMER: after 15 seonds restarts graph
        }

        private void leftWheelNotPresentFunc()
        {
            leftWheel_btn.BackColor = Color.White;
            finalPassL_lbl.Text = "Place Wheel";
            finalPassL_lbl.ForeColor = Color.Peru;
            saveStatusL_lbl.Text = "Waiting";

            testResult[3] = 0;
            testResult[4] = 0;
            testResult[5] = 0; switchCounterLeft = 0;
            leftTestResult = 0;

            // Clear line Points
            listPointsOneLeft.Clear();
            listPointsTwoLeft.Clear();
            myCurveTwoLeft.Clear();
            myCurveOneLeft.Clear();

            nextTestReadyL = 0;
            sameWheelL = 0;

            newTestTimerL.Stop();
            newTestTick = 0;                    //?

            resetGraphCounterL = 0;
            resetGraphL.Stop();

            resetGraphL.Start();            // TIMER: after 15 seonds restarts graph
        }


        private void serial_txtBoc_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar == 13)                  // scanner will press 'Enter' at the end
             {
                myPort.Write(serial_txtBox.Text);
              }
        }


        // NEW Test TICK. adds 1 to newTestTick
        private void newTestDelay_Tick(object sender, EventArgs e)
        {
            newTestTick++;
            if (newTestTick > 100)
            {
                // Clear line Points
                listPointsOneRight.Clear();
                listPointsTwoRight.Clear();
                myCurveTwoRight.Clear();
                myCurveOneRight.Clear();

                // Reset graph X- Axis
                counterRight = 0;

                sameWheelR = 1;  
                nextTestReadyR = 1;

                newTestTimerR.Stop();
                newTestTick = 0;

            }
        }

        // when closing form, close port first.
        private void testForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void resetGraphR_Tick(object sender, EventArgs e)
        {                  
            resetGraphCounterR++;
            if (resetGraphCounterR > 30)
            {
                // Clear line Points
                listPointsOneRight.Clear();
                listPointsTwoRight.Clear();
                myCurveTwoRight.Clear();
                myCurveOneRight.Clear();

                // Reset graph X- Axis
                counterRight = 0;


                resetGraphCounterR = 0;
                resetGraphR.Stop();
            }            
                     

        }

        private void resetGraphL_Tick(object sender, EventArgs e)
        {
            resetGraphCounterL++;
            if (resetGraphCounterL > 30)
            {
                // Clear line Points
                listPointsOneLeft.Clear();
                listPointsTwoLeft.Clear();
                myCurveTwoLeft.Clear();
                myCurveOneLeft.Clear();

                // Reset graph X- Axis
                counterLeft = 0;

                resetGraphCounterL = 0;
                resetGraphL.Stop();
            } 
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void resetBothGraphsTimer_Tick(object sender, EventArgs e)
        {
            // Clear line Points
            listPointsOneRight.Clear();
            listPointsTwoRight.Clear();
            myCurveTwoRight.Clear();
            myCurveOneRight.Clear();

            // Reset graph X- Axis
            counterRight = 0;


            // Clear line Points
            listPointsOneLeft.Clear();
            listPointsTwoLeft.Clear();
            myCurveTwoLeft.Clear();
            myCurveOneLeft.Clear();

            // Reset graph X- Axis
            counterLeft = 0;
        }


    }
}// end of namespace arduinoMultimeter
