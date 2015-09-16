namespace arduinoMultimeter
{
    partial class testForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(testForm));
            this.myPort = new System.IO.Ports.SerialPort(this.components);
            this.connection_lbl = new System.Windows.Forms.TextBox();
            this.arduinoPort_cmb = new System.Windows.Forms.ComboBox();
            this.connect_btn = new System.Windows.Forms.Button();
            this.txtBox1 = new System.Windows.Forms.TextBox();
            this.ampsRight_txtBox = new System.Windows.Forms.TextBox();
            this.Lswitch_txtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rightRPM_txtBox = new System.Windows.Forms.TextBox();
            this.rpmGraphRight = new ZedGraph.ZedGraphControl();
            this.ampGraphRight = new ZedGraph.ZedGraphControl();
            this.normalOperation_lbl = new System.Windows.Forms.Label();
            this.stallTest_lbl = new System.Windows.Forms.Label();
            this.finalPassR_lbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.saveStatusR_lbl = new System.Windows.Forms.Label();
            this.leftWheel_btn = new System.Windows.Forms.Button();
            this.rightWheel_btn = new System.Windows.Forms.Button();
            this.serial_txtBox = new System.Windows.Forms.TextBox();
            this.LnormalOperation_pic = new System.Windows.Forms.PictureBox();
            this.normal_gif = new System.Windows.Forms.PictureBox();
            this.LstallTest_pic = new System.Windows.Forms.PictureBox();
            this.LdropTest_lbl = new System.Windows.Forms.PictureBox();
            this.stallTest_gif = new System.Windows.Forms.PictureBox();
            this.switchTest_gif = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.newTestTimerR = new System.Windows.Forms.Timer(this.components);
            this.ampGraphLeft = new ZedGraph.ZedGraphControl();
            this.rpmGraphLeft = new ZedGraph.ZedGraphControl();
            this.Rswitch_txtBox = new System.Windows.Forms.TextBox();
            this.RnormalOperation_pic = new System.Windows.Forms.PictureBox();
            this.RstallTest_pic = new System.Windows.Forms.PictureBox();
            this.RdropTest_lbl = new System.Windows.Forms.PictureBox();
            this.leftRPM_txtBox = new System.Windows.Forms.TextBox();
            this.ampsLeft_txtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.resetGraphR = new System.Windows.Forms.Timer(this.components);
            this.saveStatusL_lbl = new System.Windows.Forms.Label();
            this.finalPassL_lbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.newTestTimerL = new System.Windows.Forms.Timer(this.components);
            this.resetGraphL = new System.Windows.Forms.Timer(this.components);
            this.resetBothGraphsTimer = new System.Windows.Forms.Timer(this.components);
            this.version_lvl = new System.Windows.Forms.Label();
            this.receiveBuffer = new System.Windows.Forms.Label();
            this.sendBuffer = new System.Windows.Forms.Label();
            this.receiveBuffer_lbl = new System.Windows.Forms.Label();
            this.sendBuffer_lbl = new System.Windows.Forms.Label();
            this.bufferSize = new System.Windows.Forms.Label();
            this.bufferSize_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LnormalOperation_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normal_gif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LstallTest_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LdropTest_lbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stallTest_gif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchTest_gif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RnormalOperation_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RstallTest_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RdropTest_lbl)).BeginInit();
            this.SuspendLayout();
            // 
            // connection_lbl
            // 
            this.connection_lbl.BackColor = System.Drawing.SystemColors.Desktop;
            this.connection_lbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.connection_lbl.ForeColor = System.Drawing.Color.Red;
            this.connection_lbl.Location = new System.Drawing.Point(1334, 10);
            this.connection_lbl.Multiline = true;
            this.connection_lbl.Name = "connection_lbl";
            this.connection_lbl.Size = new System.Drawing.Size(75, 20);
            this.connection_lbl.TabIndex = 0;
            this.connection_lbl.TabStop = false;
            this.connection_lbl.Text = "Disconnected";
            // 
            // arduinoPort_cmb
            // 
            this.arduinoPort_cmb.FormattingEnabled = true;
            this.arduinoPort_cmb.Location = new System.Drawing.Point(1250, 66);
            this.arduinoPort_cmb.Name = "arduinoPort_cmb";
            this.arduinoPort_cmb.Size = new System.Drawing.Size(62, 21);
            this.arduinoPort_cmb.TabIndex = 1;
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(1318, 66);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(91, 23);
            this.connect_btn.TabIndex = 0;
            this.connect_btn.Text = "Connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // txtBox1
            // 
            this.txtBox1.Font = new System.Drawing.Font("Complex_IV25", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox1.Location = new System.Drawing.Point(539, 704);
            this.txtBox1.Multiline = true;
            this.txtBox1.Name = "txtBox1";
            this.txtBox1.Size = new System.Drawing.Size(360, 90);
            this.txtBox1.TabIndex = 3;
            this.txtBox1.Visible = false;
            // 
            // ampsRight_txtBox
            // 
            this.ampsRight_txtBox.BackColor = System.Drawing.Color.White;
            this.ampsRight_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ampsRight_txtBox.Font = new System.Drawing.Font("TechnicLite", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ampsRight_txtBox.Location = new System.Drawing.Point(417, 209);
            this.ampsRight_txtBox.Name = "ampsRight_txtBox";
            this.ampsRight_txtBox.Size = new System.Drawing.Size(72, 33);
            this.ampsRight_txtBox.TabIndex = 5;
            this.ampsRight_txtBox.Text = "0.00";
            // 
            // Lswitch_txtBox
            // 
            this.Lswitch_txtBox.BackColor = System.Drawing.Color.White;
            this.Lswitch_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Lswitch_txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lswitch_txtBox.Location = new System.Drawing.Point(806, 560);
            this.Lswitch_txtBox.Multiline = true;
            this.Lswitch_txtBox.Name = "Lswitch_txtBox";
            this.Lswitch_txtBox.Size = new System.Drawing.Size(42, 32);
            this.Lswitch_txtBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(550, 704);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Raw Data";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(485, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Amps";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(665, 691);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Microswitch";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(485, 478);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = " RPM";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // rightRPM_txtBox
            // 
            this.rightRPM_txtBox.BackColor = System.Drawing.Color.White;
            this.rightRPM_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rightRPM_txtBox.Font = new System.Drawing.Font("TechnicLite", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightRPM_txtBox.Location = new System.Drawing.Point(404, 468);
            this.rightRPM_txtBox.Name = "rightRPM_txtBox";
            this.rightRPM_txtBox.Size = new System.Drawing.Size(85, 33);
            this.rightRPM_txtBox.TabIndex = 12;
            this.rightRPM_txtBox.Text = "00.00";
            // 
            // rpmGraphRight
            // 
            this.rpmGraphRight.Location = new System.Drawing.Point(12, 461);
            this.rpmGraphRight.Name = "rpmGraphRight";
            this.rpmGraphRight.ScrollGrace = 0D;
            this.rpmGraphRight.ScrollMaxX = 0D;
            this.rpmGraphRight.ScrollMaxY = 0D;
            this.rpmGraphRight.ScrollMaxY2 = 0D;
            this.rpmGraphRight.ScrollMinX = 0D;
            this.rpmGraphRight.ScrollMinY = 0D;
            this.rpmGraphRight.ScrollMinY2 = 0D;
            this.rpmGraphRight.Size = new System.Drawing.Size(532, 240);
            this.rpmGraphRight.TabIndex = 15;
            // 
            // ampGraphRight
            // 
            this.ampGraphRight.Location = new System.Drawing.Point(12, 204);
            this.ampGraphRight.Name = "ampGraphRight";
            this.ampGraphRight.ScrollGrace = 0D;
            this.ampGraphRight.ScrollMaxX = 0D;
            this.ampGraphRight.ScrollMaxY = 0D;
            this.ampGraphRight.ScrollMaxY2 = 0D;
            this.ampGraphRight.ScrollMinX = 0D;
            this.ampGraphRight.ScrollMinY = 0D;
            this.ampGraphRight.ScrollMinY2 = 0D;
            this.ampGraphRight.Size = new System.Drawing.Size(532, 240);
            this.ampGraphRight.TabIndex = 14;
            // 
            // normalOperation_lbl
            // 
            this.normalOperation_lbl.AutoSize = true;
            this.normalOperation_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.normalOperation_lbl.Location = new System.Drawing.Point(635, 306);
            this.normalOperation_lbl.Name = "normalOperation_lbl";
            this.normalOperation_lbl.Size = new System.Drawing.Size(149, 20);
            this.normalOperation_lbl.TabIndex = 17;
            this.normalOperation_lbl.Text = "Normal Operation";
            // 
            // stallTest_lbl
            // 
            this.stallTest_lbl.AutoSize = true;
            this.stallTest_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stallTest_lbl.Location = new System.Drawing.Point(686, 494);
            this.stallTest_lbl.Name = "stallTest_lbl";
            this.stallTest_lbl.Size = new System.Drawing.Size(85, 20);
            this.stallTest_lbl.TabIndex = 18;
            this.stallTest_lbl.Text = "Stall Test";
            // 
            // finalPassR_lbl
            // 
            this.finalPassR_lbl.AutoSize = true;
            this.finalPassR_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finalPassR_lbl.ForeColor = System.Drawing.Color.Peru;
            this.finalPassR_lbl.Location = new System.Drawing.Point(117, 716);
            this.finalPassR_lbl.Name = "finalPassR_lbl";
            this.finalPassR_lbl.Size = new System.Drawing.Size(317, 46);
            this.finalPassR_lbl.TabIndex = 22;
            this.finalPassR_lbl.Text = "Please Connect";
            this.finalPassR_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(119, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(272, 31);
            this.label6.TabIndex = 23;
            this.label6.Text = "Current Consumption";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(173, 452);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 31);
            this.label7.TabIndex = 24;
            this.label7.Text = "Wheel RPM";
            // 
            // saveStatusR_lbl
            // 
            this.saveStatusR_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveStatusR_lbl.Location = new System.Drawing.Point(203, 761);
            this.saveStatusR_lbl.Name = "saveStatusR_lbl";
            this.saveStatusR_lbl.Size = new System.Drawing.Size(127, 23);
            this.saveStatusR_lbl.TabIndex = 28;
            this.saveStatusR_lbl.Text = ".";
            this.saveStatusR_lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // leftWheel_btn
            // 
            this.leftWheel_btn.Font = new System.Drawing.Font("Segoe Print", 24.75F, System.Drawing.FontStyle.Bold);
            this.leftWheel_btn.Location = new System.Drawing.Point(1020, 106);
            this.leftWheel_btn.Name = "leftWheel_btn";
            this.leftWheel_btn.Size = new System.Drawing.Size(289, 70);
            this.leftWheel_btn.TabIndex = 29;
            this.leftWheel_btn.Text = "Left Wheel";
            this.leftWheel_btn.UseVisualStyleBackColor = true;
            // 
            // rightWheel_btn
            // 
            this.rightWheel_btn.Font = new System.Drawing.Font("Segoe Print", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightWheel_btn.Location = new System.Drawing.Point(144, 106);
            this.rightWheel_btn.Name = "rightWheel_btn";
            this.rightWheel_btn.Size = new System.Drawing.Size(286, 71);
            this.rightWheel_btn.TabIndex = 30;
            this.rightWheel_btn.Text = "Right Wheel";
            this.rightWheel_btn.UseVisualStyleBackColor = true;
            // 
            // serial_txtBox
            // 
            this.serial_txtBox.Location = new System.Drawing.Point(864, 774);
            this.serial_txtBox.Name = "serial_txtBox";
            this.serial_txtBox.Size = new System.Drawing.Size(100, 20);
            this.serial_txtBox.TabIndex = 31;
            this.serial_txtBox.Visible = false;
            this.serial_txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.serial_txtBoc_KeyPress);
            // 
            // LnormalOperation_pic
            // 
            this.LnormalOperation_pic.Location = new System.Drawing.Point(806, 239);
            this.LnormalOperation_pic.Name = "LnormalOperation_pic";
            this.LnormalOperation_pic.Size = new System.Drawing.Size(67, 64);
            this.LnormalOperation_pic.TabIndex = 20;
            this.LnormalOperation_pic.TabStop = false;
            // 
            // normal_gif
            // 
            this.normal_gif.Image = ((System.Drawing.Image)(resources.GetObject("normal_gif.Image")));
            this.normal_gif.Location = new System.Drawing.Point(622, 175);
            this.normal_gif.Name = "normal_gif";
            this.normal_gif.Size = new System.Drawing.Size(184, 128);
            this.normal_gif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.normal_gif.TabIndex = 25;
            this.normal_gif.TabStop = false;
            // 
            // LstallTest_pic
            // 
            this.LstallTest_pic.Location = new System.Drawing.Point(806, 427);
            this.LstallTest_pic.Name = "LstallTest_pic";
            this.LstallTest_pic.Size = new System.Drawing.Size(67, 64);
            this.LstallTest_pic.TabIndex = 21;
            this.LstallTest_pic.TabStop = false;
            // 
            // LdropTest_lbl
            // 
            this.LdropTest_lbl.Location = new System.Drawing.Point(806, 624);
            this.LdropTest_lbl.Name = "LdropTest_lbl";
            this.LdropTest_lbl.Size = new System.Drawing.Size(67, 64);
            this.LdropTest_lbl.TabIndex = 16;
            this.LdropTest_lbl.TabStop = false;
            // 
            // stallTest_gif
            // 
            this.stallTest_gif.Image = global::arduinoMultimeter.Properties.Resources.stallTest;
            this.stallTest_gif.Location = new System.Drawing.Point(622, 363);
            this.stallTest_gif.Name = "stallTest_gif";
            this.stallTest_gif.Size = new System.Drawing.Size(184, 128);
            this.stallTest_gif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stallTest_gif.TabIndex = 26;
            this.stallTest_gif.TabStop = false;
            // 
            // switchTest_gif
            // 
            this.switchTest_gif.Image = global::arduinoMultimeter.Properties.Resources.dropTest1;
            this.switchTest_gif.Location = new System.Drawing.Point(622, 560);
            this.switchTest_gif.Name = "switchTest_gif";
            this.switchTest_gif.Size = new System.Drawing.Size(184, 128);
            this.switchTest_gif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.switchTest_gif.TabIndex = 27;
            this.switchTest_gif.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::arduinoMultimeter.Properties.Resources.iRobotWheelTesterHeader;
            this.pictureBox1.Location = new System.Drawing.Point(-4, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1432, 103);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // newTestTimerR
            // 
            this.newTestTimerR.Tick += new System.EventHandler(this.newTestDelay_Tick);
            // 
            // ampGraphLeft
            // 
            this.ampGraphLeft.Location = new System.Drawing.Point(879, 193);
            this.ampGraphLeft.Name = "ampGraphLeft";
            this.ampGraphLeft.ScrollGrace = 0D;
            this.ampGraphLeft.ScrollMaxX = 0D;
            this.ampGraphLeft.ScrollMaxY = 0D;
            this.ampGraphLeft.ScrollMaxY2 = 0D;
            this.ampGraphLeft.ScrollMinX = 0D;
            this.ampGraphLeft.ScrollMinY = 0D;
            this.ampGraphLeft.ScrollMinY2 = 0D;
            this.ampGraphLeft.Size = new System.Drawing.Size(532, 240);
            this.ampGraphLeft.TabIndex = 32;
            // 
            // rpmGraphLeft
            // 
            this.rpmGraphLeft.Location = new System.Drawing.Point(879, 461);
            this.rpmGraphLeft.Name = "rpmGraphLeft";
            this.rpmGraphLeft.ScrollGrace = 0D;
            this.rpmGraphLeft.ScrollMaxX = 0D;
            this.rpmGraphLeft.ScrollMaxY = 0D;
            this.rpmGraphLeft.ScrollMaxY2 = 0D;
            this.rpmGraphLeft.ScrollMinX = 0D;
            this.rpmGraphLeft.ScrollMinY = 0D;
            this.rpmGraphLeft.ScrollMinY2 = 0D;
            this.rpmGraphLeft.Size = new System.Drawing.Size(532, 240);
            this.rpmGraphLeft.TabIndex = 33;
            // 
            // Rswitch_txtBox
            // 
            this.Rswitch_txtBox.BackColor = System.Drawing.Color.White;
            this.Rswitch_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Rswitch_txtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rswitch_txtBox.Location = new System.Drawing.Point(582, 560);
            this.Rswitch_txtBox.Multiline = true;
            this.Rswitch_txtBox.Name = "Rswitch_txtBox";
            this.Rswitch_txtBox.Size = new System.Drawing.Size(42, 32);
            this.Rswitch_txtBox.TabIndex = 34;
            // 
            // RnormalOperation_pic
            // 
            this.RnormalOperation_pic.Location = new System.Drawing.Point(557, 239);
            this.RnormalOperation_pic.Name = "RnormalOperation_pic";
            this.RnormalOperation_pic.Size = new System.Drawing.Size(67, 64);
            this.RnormalOperation_pic.TabIndex = 36;
            this.RnormalOperation_pic.TabStop = false;
            // 
            // RstallTest_pic
            // 
            this.RstallTest_pic.Location = new System.Drawing.Point(557, 427);
            this.RstallTest_pic.Name = "RstallTest_pic";
            this.RstallTest_pic.Size = new System.Drawing.Size(67, 64);
            this.RstallTest_pic.TabIndex = 37;
            this.RstallTest_pic.TabStop = false;
            // 
            // RdropTest_lbl
            // 
            this.RdropTest_lbl.Location = new System.Drawing.Point(557, 624);
            this.RdropTest_lbl.Name = "RdropTest_lbl";
            this.RdropTest_lbl.Size = new System.Drawing.Size(67, 64);
            this.RdropTest_lbl.TabIndex = 35;
            this.RdropTest_lbl.TabStop = false;
            // 
            // leftRPM_txtBox
            // 
            this.leftRPM_txtBox.BackColor = System.Drawing.Color.White;
            this.leftRPM_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.leftRPM_txtBox.Font = new System.Drawing.Font("TechnicLite", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftRPM_txtBox.Location = new System.Drawing.Point(1267, 468);
            this.leftRPM_txtBox.Name = "leftRPM_txtBox";
            this.leftRPM_txtBox.Size = new System.Drawing.Size(90, 33);
            this.leftRPM_txtBox.TabIndex = 41;
            this.leftRPM_txtBox.Text = "00.00";
            // 
            // ampsLeft_txtBox
            // 
            this.ampsLeft_txtBox.BackColor = System.Drawing.Color.White;
            this.ampsLeft_txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ampsLeft_txtBox.Font = new System.Drawing.Font("TechnicLite", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ampsLeft_txtBox.Location = new System.Drawing.Point(1278, 199);
            this.ampsLeft_txtBox.Name = "ampsLeft_txtBox";
            this.ampsLeft_txtBox.Size = new System.Drawing.Size(79, 33);
            this.ampsLeft_txtBox.TabIndex = 38;
            this.ampsLeft_txtBox.Text = "0.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1351, 478);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 40;
            this.label2.Text = " RPM";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1355, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 20);
            this.label8.TabIndex = 39;
            this.label8.Text = "Amps";
            // 
            // resetGraphR
            // 
            this.resetGraphR.Interval = 1000;
            this.resetGraphR.Tick += new System.EventHandler(this.resetGraphR_Tick);
            // 
            // saveStatusL_lbl
            // 
            this.saveStatusL_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveStatusL_lbl.Location = new System.Drawing.Point(1153, 761);
            this.saveStatusL_lbl.Name = "saveStatusL_lbl";
            this.saveStatusL_lbl.Size = new System.Drawing.Size(127, 23);
            this.saveStatusL_lbl.TabIndex = 43;
            this.saveStatusL_lbl.Text = "Waiting";
            this.saveStatusL_lbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // finalPassL_lbl
            // 
            this.finalPassL_lbl.AutoSize = true;
            this.finalPassL_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finalPassL_lbl.ForeColor = System.Drawing.Color.Peru;
            this.finalPassL_lbl.Location = new System.Drawing.Point(1043, 715);
            this.finalPassL_lbl.Name = "finalPassL_lbl";
            this.finalPassL_lbl.Size = new System.Drawing.Size(256, 46);
            this.finalPassL_lbl.TabIndex = 42;
            this.finalPassL_lbl.Text = "Place Wheel";
            this.finalPassL_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(1278, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Network:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1092, 452);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(157, 31);
            this.label10.TabIndex = 46;
            this.label10.Text = "Wheel RPM";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(994, 179);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(272, 31);
            this.label11.TabIndex = 45;
            this.label11.Text = "Current Consumption";
            // 
            // resetGraphL
            // 
            this.resetGraphL.Interval = 1000;
            this.resetGraphL.Tick += new System.EventHandler(this.resetGraphL_Tick);
            // 
            // resetBothGraphsTimer
            // 
            this.resetBothGraphsTimer.Interval = 60000;
            this.resetBothGraphsTimer.Tick += new System.EventHandler(this.resetBothGraphsTimer_Tick);
            // 
            // version_lvl
            // 
            this.version_lvl.AutoSize = true;
            this.version_lvl.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.version_lvl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.version_lvl.Location = new System.Drawing.Point(495, 66);
            this.version_lvl.Name = "version_lvl";
            this.version_lvl.Size = new System.Drawing.Size(49, 13);
            this.version_lvl.TabIndex = 47;
            this.version_lvl.Text = "v6.26.15";
            // 
            // receiveBuffer
            // 
            this.receiveBuffer.AutoSize = true;
            this.receiveBuffer.Location = new System.Drawing.Point(1081, 49);
            this.receiveBuffer.Name = "receiveBuffer";
            this.receiveBuffer.Size = new System.Drawing.Size(78, 13);
            this.receiveBuffer.TabIndex = 48;
            this.receiveBuffer.Text = "Receive Buffer";
            this.receiveBuffer.Visible = false;
            // 
            // sendBuffer
            // 
            this.sendBuffer.AutoSize = true;
            this.sendBuffer.Location = new System.Drawing.Point(1175, 49);
            this.sendBuffer.Name = "sendBuffer";
            this.sendBuffer.Size = new System.Drawing.Size(63, 13);
            this.sendBuffer.TabIndex = 49;
            this.sendBuffer.Text = "Send Buffer";
            this.sendBuffer.Visible = false;
            // 
            // receiveBuffer_lbl
            // 
            this.receiveBuffer_lbl.AutoSize = true;
            this.receiveBuffer_lbl.Location = new System.Drawing.Point(1112, 66);
            this.receiveBuffer_lbl.Name = "receiveBuffer_lbl";
            this.receiveBuffer_lbl.Size = new System.Drawing.Size(13, 13);
            this.receiveBuffer_lbl.TabIndex = 50;
            this.receiveBuffer_lbl.Text = "0";
            this.receiveBuffer_lbl.Visible = false;
            // 
            // sendBuffer_lbl
            // 
            this.sendBuffer_lbl.AutoSize = true;
            this.sendBuffer_lbl.Location = new System.Drawing.Point(1191, 66);
            this.sendBuffer_lbl.Name = "sendBuffer_lbl";
            this.sendBuffer_lbl.Size = new System.Drawing.Size(13, 13);
            this.sendBuffer_lbl.TabIndex = 51;
            this.sendBuffer_lbl.Text = "0";
            this.sendBuffer_lbl.Visible = false;
            // 
            // bufferSize
            // 
            this.bufferSize.AutoSize = true;
            this.bufferSize.Location = new System.Drawing.Point(986, 49);
            this.bufferSize.Name = "bufferSize";
            this.bufferSize.Size = new System.Drawing.Size(58, 13);
            this.bufferSize.TabIndex = 52;
            this.bufferSize.Text = "Buffer Size";
            this.bufferSize.Visible = false;
            // 
            // bufferSize_lbl
            // 
            this.bufferSize_lbl.AutoSize = true;
            this.bufferSize_lbl.Location = new System.Drawing.Point(1017, 69);
            this.bufferSize_lbl.Name = "bufferSize_lbl";
            this.bufferSize_lbl.Size = new System.Drawing.Size(13, 13);
            this.bufferSize_lbl.TabIndex = 53;
            this.bufferSize_lbl.Text = "0";
            this.bufferSize_lbl.Visible = false;
            // 
            // testForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1421, 804);
            this.Controls.Add(this.bufferSize_lbl);
            this.Controls.Add(this.bufferSize);
            this.Controls.Add(this.sendBuffer_lbl);
            this.Controls.Add(this.receiveBuffer_lbl);
            this.Controls.Add(this.sendBuffer);
            this.Controls.Add(this.receiveBuffer);
            this.Controls.Add(this.version_lvl);
            this.Controls.Add(this.ampsLeft_txtBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.saveStatusL_lbl);
            this.Controls.Add(this.finalPassL_lbl);
            this.Controls.Add(this.leftRPM_txtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Rswitch_txtBox);
            this.Controls.Add(this.RnormalOperation_pic);
            this.Controls.Add(this.RstallTest_pic);
            this.Controls.Add(this.RdropTest_lbl);
            this.Controls.Add(this.rpmGraphLeft);
            this.Controls.Add(this.ampGraphLeft);
            this.Controls.Add(this.serial_txtBox);
            this.Controls.Add(this.arduinoPort_cmb);
            this.Controls.Add(this.rightWheel_btn);
            this.Controls.Add(this.leftWheel_btn);
            this.Controls.Add(this.saveStatusR_lbl);
            this.Controls.Add(this.Lswitch_txtBox);
            this.Controls.Add(this.rightRPM_txtBox);
            this.Controls.Add(this.ampsRight_txtBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LnormalOperation_pic);
            this.Controls.Add(this.normal_gif);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.finalPassR_lbl);
            this.Controls.Add(this.LstallTest_pic);
            this.Controls.Add(this.stallTest_lbl);
            this.Controls.Add(this.normalOperation_lbl);
            this.Controls.Add(this.LdropTest_lbl);
            this.Controls.Add(this.rpmGraphRight);
            this.Controls.Add(this.ampGraphRight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBox1);
            this.Controls.Add(this.connect_btn);
            this.Controls.Add(this.connection_lbl);
            this.Controls.Add(this.stallTest_gif);
            this.Controls.Add(this.switchTest_gif);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "testForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iRobot Wheel Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.testForm_FormClosing);
            this.Load += new System.EventHandler(this.testForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LnormalOperation_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normal_gif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LstallTest_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LdropTest_lbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stallTest_gif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchTest_gif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RnormalOperation_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RstallTest_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RdropTest_lbl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort myPort;
        private System.Windows.Forms.TextBox connection_lbl;
        private System.Windows.Forms.ComboBox arduinoPort_cmb;
        private System.Windows.Forms.Button connect_btn;
        private System.Windows.Forms.TextBox txtBox1;
        private System.Windows.Forms.TextBox ampsRight_txtBox;
        private System.Windows.Forms.TextBox Lswitch_txtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rightRPM_txtBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ZedGraph.ZedGraphControl ampGraphRight;
        private ZedGraph.ZedGraphControl rpmGraphRight;
        private System.Windows.Forms.PictureBox LdropTest_lbl;
        private System.Windows.Forms.Label normalOperation_lbl;
        private System.Windows.Forms.Label stallTest_lbl;
        private System.Windows.Forms.PictureBox LnormalOperation_pic;
        private System.Windows.Forms.PictureBox LstallTest_pic;
        private System.Windows.Forms.Label finalPassR_lbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox normal_gif;
        private System.Windows.Forms.PictureBox stallTest_gif;
        private System.Windows.Forms.PictureBox switchTest_gif;
        private System.Windows.Forms.Label saveStatusR_lbl;
        private System.Windows.Forms.Button leftWheel_btn;
        private System.Windows.Forms.Button rightWheel_btn;
        private System.Windows.Forms.TextBox serial_txtBox;
        private System.Windows.Forms.Timer newTestTimerR;
        private ZedGraph.ZedGraphControl ampGraphLeft;
        private ZedGraph.ZedGraphControl rpmGraphLeft;
        private System.Windows.Forms.TextBox Rswitch_txtBox;
        private System.Windows.Forms.PictureBox RnormalOperation_pic;
        private System.Windows.Forms.PictureBox RstallTest_pic;
        private System.Windows.Forms.PictureBox RdropTest_lbl;
        private System.Windows.Forms.TextBox leftRPM_txtBox;
        private System.Windows.Forms.TextBox ampsLeft_txtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer resetGraphR;
        private System.Windows.Forms.Label saveStatusL_lbl;
        private System.Windows.Forms.Label finalPassL_lbl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer newTestTimerL;
        private System.Windows.Forms.Timer resetGraphL;
        private System.Windows.Forms.Timer resetBothGraphsTimer;
        private System.Windows.Forms.Label version_lvl;
        private System.Windows.Forms.Label receiveBuffer;
        private System.Windows.Forms.Label sendBuffer;
        private System.Windows.Forms.Label receiveBuffer_lbl;
        private System.Windows.Forms.Label sendBuffer_lbl;
        private System.Windows.Forms.Label bufferSize;
        private System.Windows.Forms.Label bufferSize_lbl;
    }
}

