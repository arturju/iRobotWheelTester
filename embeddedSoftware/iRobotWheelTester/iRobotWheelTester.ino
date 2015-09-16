#include <Wire.h>					// for i2c communication		http://www.nxp.com/documents/data_sheet/PCF8574_PCF8574A.pdf pg 5
#include "TimerOne.h"				// for timer interrupt

//============= FOR RIGHT WHEEL: A0, A1, A4, A5. 

int unitID = 0;


	// PCF8574AT addresses:			
	byte rightChipAddress = B00100000;	// 32 DEC
	//byte rightChipAddress = B01000000;		// 0x40
	byte i2cOut= B11111111;			// value to be sent to chip. will save values of previous command. Active LOW

	byte motor1F = B11111110;		// forward
	byte motor1R = B11111101;		// reverse
	byte arm1F = B11111011;		
	byte arm1R = B11110111;	

	byte rightWheelDrop = B10111111;
	int rightWheelDropValue= 0;
	byte rightWheelPresent = B01111111;
	int rightWheelPresentValue = 0;	

	// RGB LED
	byte redOn = B11101111;
	byte greenOn=B11011111;
	byte allLightsOff =B11111111;
	int rightRedLED = 3;
	int rightGreenLED= 5;
	int rightBlueLED = 6;	

	// INPUTS (not from i2c)
	int rightCurrentPin = A1;    
	int rightEncoderPin = A0;
	// A4 and A5 for SDA and SCL. needed for I2C 

	// global variables for readings (current, RPM) and Timer
	int rightAnalogAmps= 0;
		float rightRealAmps = 0.0;  
	unsigned long timeBetweenPulseR;
	float rightWheelRotation;
	float rWheelRotationArray[] = {0.0, 0.0, 0.0, 0.0};
		int rWheelIndex = 0;
		float rWheelTotal = 0;
		float rWheelAverage= 0;
		
	long rightStartTime= 0;
	float rightElapsedTime= 0;

	// SUMMARY: variables to store test results[4]= [normalCurrent, normalRPM, stallCurrent, stallRPM, dropSwitch]		
	int rightTestComplete= 0;
	int rightTestInProgress= 0;
	int rightAutoTest = 0;
	
	float rT1sumCurrent=0;	int rT1count =0;
	float rT1sumRPM=0;		int rT2count=0;
	float rT2sumCurrent=0;
	float rT2sumRPM=0;
	
	
//========= FOR LEFT WHEEL: A2, A3, A4, A5

	//PCF8574AT addresses:
	byte leftChipAddress = B10100001;	// 0x42 (?)
	//byte i2cOut= B11111111;			// value to be sent to chip. will save values of previous command. Active LOW

	byte motor2F = B11111110;		// forward
	byte motor2R = B11111101;		// reverse
	byte arm2F = B11110111;
	byte arm2R = B11111011; 

	byte leftWheelDrop = B10111111;
	int leftWheelDropValue= 0;
	byte leftWheelPresent = B01111111;
	int leftWheelPresentValue = 0;

	// RGB LED
//	byte redOn = B11111111;
	int leftRedLED = 9;
	int leftGreenLED= 10;
	int leftBlueLED = 11;

	// INPUTS (not from i2c)
	int leftCurrentPin = A3;
	int leftEncoderPin = A2;
	// A4 and A5 for SDA and SCL. needed for I2C
	

	// global variables for readings (current, RPM) and Timer
	int leftAnalogAmps= 0;
		float leftRealAmps = 0.0;
	unsigned long timeBetweenPulseL;
	float leftWheelRotation;
	float lWheelRotationArray[]= {0.0, 0.0, 0.0, 0.0};
		int lWheelIndex= 0;
		float lWheelTotal = 0;
		float lWheelAverage= 0;	

	long leftStartTime= 0;
	float leftElapsedTime= 0;


	int leftTestComplete= 0;
	int leftTestInProgress= 0;
	int leftAutoTest = 0;
	
		float lT1sumCurrent=0;	int lT1count =0;
		float lT1sumRPM=0;		int lT2count=0;
		float lT2sumCurrent=0;
		float lT2sumRPM=0;
	
	int tempFailR= 0;		// for normal Operation 2.
	int tempFailL =0;

// ===== SHARED : D4, AREF
	int physicalButton = 4;
	int physicalButtonValue = 0;
	char inData;								// from Serial
	boolean chipInterrupted = false;			// both chips will be connected to INT0, arduino Pin2


void setup() {
	analogReference(EXTERNAL);					// set to 1 Volt	
	pinMode(rightCurrentPin, INPUT);	pinMode(leftCurrentPin, INPUT);
	pinMode(rightEncoderPin, INPUT);	pinMode(leftEncoderPin, INPUT);
	pinMode(physicalButton,INPUT);
	
	pinMode(rightRedLED, OUTPUT); 	pinMode(leftRedLED, OUTPUT);
	pinMode(rightGreenLED, OUTPUT); pinMode(leftGreenLED, OUTPUT);
	pinMode(rightBlueLED, OUTPUT);  pinMode(leftBlueLED, OUTPUT);

	Wire.begin();										// A5: SCL and A4: SDA
	attachInterrupt(0, hardwareISR, CHANGE);			// on Arduino UNO = Digital pin2.
	
	//	Timer1 BREAKS analogWrite() for digital pin 9 &10
	//Timer1.initialize(900000);							// Timer ticks every 500k MicroS; 0.50 secs
	//Timer1.attachInterrupt(timerTickFunction, 900000);	// wheel is ~70RPM = 1.2 RPS. 8 H->L per revolution
	
	Serial.begin(9600);	
}

float mapfloat(float x, float in_min, float in_max, float out_min, float out_max)
{
	return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}

void hardwareISR()		// state on the i2C changed. Read
{
	detachInterrupt(0);
	//Serial.println("Interrupted");				// Serial.print IS an interrupt; Freezes				
	chipInterrupted = true;
	attachInterrupt(0, hardwareISR, CHANGE);
	
}

// Read values from Arduino Pins-> Right: A0, A1, [D4,D5] . Left: A2, A3, [D4, D5].  Shared: A2 (button), [D4, D5, D2(INT0) ]
void getAndProcessAllValues(){
		
// =======RIGHT WHEEL: Current, RPM, [ dropSwitch, WheelPresence ] <- from i2C chip. SHARED
	rightAnalogAmps = analogRead(rightCurrentPin);
	rightRealAmps = mapfloat(rightAnalogAmps, 0, 1023, 0.0, 1.0);	
	
	unsigned long timeoutR= 20000;						// 20k microSecs
	timeBetweenPulseR = pulseIn(rightEncoderPin, HIGH,timeoutR);					// in microseconds	
	int HertzR = (1000000/timeBetweenPulseR)/2;										// 1/s
	int motorRotationR = HertzR/4;													// in rps
	//rightWheelRotation = motorRotationR * (float)63.0/60.0;						// in rpm: 63 revolutions per rotation
	
		rWheelTotal = rWheelTotal - rWheelRotationArray[rWheelIndex];				// Average 4 last WHEEL ROTATION readings 
		rWheelRotationArray[rWheelIndex] = motorRotationR * (float)63.0/60.0;
		rWheelTotal = rWheelTotal + rWheelRotationArray[rWheelIndex];				
		rWheelIndex++;	                  
		if (rWheelIndex >= 4)	rWheelIndex= 0;	
		rightWheelRotation = abs(rWheelTotal/4.0);			// sometimes '-0.0' 		
			
// =======LEFT WHEEL: Current, RPM, [ dropSwitch, WheelPresence ] <- from i2C chip. SHARED
	leftAnalogAmps = analogRead(leftCurrentPin);
	leftRealAmps = mapfloat(leftAnalogAmps, 0, 1023, 0.0, 1.0);

	unsigned long timeoutL= 20000;
	timeBetweenPulseL = pulseIn(leftEncoderPin, HIGH,timeoutL);		// in microseconds
	int HertzL = (1000000/timeBetweenPulseL)/2;						// 1/s
	int motorRotationL = HertzL/4;									// in rps
	//leftWheelRotation = motorRotationL * (float)63.0/60.0;			// in rpm: 63 revolutions per rotation

		lWheelTotal = lWheelTotal - lWheelRotationArray[lWheelIndex];				// Average 4 last WHEEL ROTATION readings
		lWheelRotationArray[lWheelIndex] = motorRotationL * (float)63.0/60.0;
		lWheelTotal = lWheelTotal + lWheelRotationArray[lWheelIndex];
		lWheelIndex++;
		if (lWheelIndex >= 4)	lWheelIndex= 0;
		leftWheelRotation = abs(lWheelTotal/4.0);			// sometimes '-0.0'

// ==== SHARED			
	// BOTH i2c PCF8574AT chips when INTERRUPTED
	if (true)								// (dropSWITCH or wheelPresence) R || L Changed.		CHANGE TO IF (chipInterrupted)
	{	
		//Serial.println("INTERRUPTED");
		// RIGHT CHIP
		Wire.requestFrom(B00100000,1);							//1 byte
		while (Wire.available()){								// read rightWheelDrop and rightWheelPresent value from i2c chip
			byte readByte = Wire.read();
			rightWheelDrop = readByte | B10111111;				// only concerned with WD
			rightWheelPresent = readByte | B01111111;			// only concerned with WP
					
			// wheel DROP
			rightWheelDropValue = (rightWheelDrop==191) ? 1:0;					// if active, wheelDropValue = 1, else wheelDropValue = 0
					
			// wheel PRESENCE
			if (rightWheelPresent == 255)						// IF rightWHEEL IS NOT PRESENT:
			{
				rightWheelPresentValue = 0;
				rightTestComplete = 0;							// restart rightTestComplete status
				setColor('R', 255,255,255);						// orange/yellow/white?
				sendToChip(rightChipAddress,B11111111);			// everything off
						
				rightAutoTest = 1;								// if no wheel set rightAutoTest to yes
			}
			else
			rightWheelPresentValue = 1;							// will do stuff on main loop
		}
			
		// LEFT CHIP
		Wire.requestFrom(B10100001,1);					//1 byte from LEFT chip
		while (Wire.available()){						// read rightWheelDrop and rightWheelPresent value from i2c chip
			byte readByte = Wire.read();
			leftWheelDrop = readByte | B10111111;		// only concerned with WD
			leftWheelPresent = readByte | B01111111;	// only concerned with WP
				
			// wheel DROP
			leftWheelDropValue = (leftWheelDrop==191) ? 1:0;					// if active, wheelDropValue = 1, else wheelDropValue = 0
				
			// wheel PRESENCE
			if (leftWheelPresent == 255)				// IF rightWHEEL IS NOT PRESENT:
			{
				leftWheelPresentValue = 0;
				leftTestComplete = 0;							// restart rightTestComplete status
				setColor('L', 255,255,255);						// orange/yellow/white?
				sendToChip(leftChipAddress,B11111111);			// everything off
					
				leftAutoTest = 1;								// if no wheel set rightAutoTest to yes
			}
			else
			leftWheelPresentValue = 1;						// will do stuff on main loop			
		}
	} // end of chipInterrupted
		
		
	// PHYSICAL BUTTON
	physicalButtonValue = digitalRead(physicalButton);				// active LOW. Choose which to run based on presence
				
	if (physicalButtonValue == 0 && leftWheelPresentValue ==1 && rightWheelPresentValue ==1)	// BOTH  found
	{
		Serial.println("\nBoth Wheel Found!");
			
		leftStartTime = millis();			// start timer
		leftTestComplete = 0;				// restart rightTestComplete status
		leftTestInProgress =1;				// start test
			
		rightStartTime = millis();			// start timer
		rightTestComplete = 0;				// restart rightTestComplete status
		rightTestInProgress =1;				// start test
	}
				
	else if (physicalButtonValue == 0 && rightWheelPresentValue ==1)		// RIGHT is found
	{
		Serial.println("\nRight Wheel Found!");
		rightStartTime = millis();			// set startTime to timeNow
		rightTestComplete = 0;				// restart rightTestComplete status
		rightTestInProgress =1;				// start test
	}
		
	else if (physicalButtonValue == 0 && leftWheelPresentValue ==1)		// LEFT is found
	{
		Serial.println("\nLeft Wheel Found!");
		leftStartTime = millis() + 1;			// start timer
		leftTestComplete = 0;				// restart rightTestComplete status
		leftTestInProgress =1;				// start test
	}
	
}

void sendToChip(byte address, byte data)
{
	Wire.beginTransmission(address);
	Wire.write(data);
	Wire.endTransmission();
}

void timerTick()
{	
	rightElapsedTime = millis()- rightStartTime;	
	rightElapsedTime = rightElapsedTime/1000.0;
	
	leftElapsedTime = millis()- leftStartTime;
	leftElapsedTime = leftElapsedTime/1000.0;
}

void setColor(char LEDSide, int red, int green, int blue)
{
	switch (LEDSide){
		case 'R':
			analogWrite(rightRedLED, red);
			analogWrite(rightGreenLED, green);
			analogWrite(rightBlueLED, blue);
			break;
		
		case 'L':
			analogWrite(leftRedLED, red);
			analogWrite(leftGreenLED, green);
			analogWrite(leftBlueLED, blue);
			break;
	}
}



void testingProcedure(char leftOrRightWheel)	
{
	// set following variables depending on 'L' or 'R'
	float xElapsedTime;
	byte xChipAddress;	
	char wheelSide;
	
	float *xRealAmpsPointer; float *xWheelRotationPointer;	
	int *xTestPassPointer;	 int *xWheelDropValuePointer; int *xRunOncePointer;
	int *xRedLedPointer; int *xGreenLedPointer; int *xBlueLedPointer;
	int *tempFailX;
	
	static int rightRunOnceCounter[] = {0, 0 ,0 , 0 , 0};	
	static int leftRunOnceCounter[] = {0, 0 , 0, 0 , 0};
		
	static int rightTestPass[]= {0,0,0};
	static int leftTestPass[] = {0,0,0};
		
	byte operationOne = B11111111;
	byte operationTwo = B11111111;
	byte operationThree = B11111111;
	byte operationFour = B11111111;
	byte operationFive = B11111111;
	
	int *xT1CountPointer, *xT2CountPointer;
	int *xTestComplete, *xTestInProgress;
	float *xT1CurrentSumPointer, *xT1RPMSumPointer;
	float *xT2CurrentSumPointer, *xT2RPMSumPointer;
		
	switch (leftOrRightWheel)
	{
		case 'L':
			xElapsedTime = leftElapsedTime;
			xChipAddress = leftChipAddress;
			wheelSide = 'L';
			
			xRunOncePointer = &leftRunOnceCounter[0];			//set to memory address of 0th element. Ex: 200. (*(RunOncePointer+1)) == rightRunOnceCounter[1]
			xTestPassPointer = &leftTestPass[0];
			xRealAmpsPointer = &leftRealAmps;
			xWheelRotationPointer = &leftWheelRotation;
			xWheelDropValuePointer = &leftWheelDropValue;
			xRedLedPointer = &leftRedLED;
			xGreenLedPointer = &leftGreenLED;
			xBlueLedPointer = &leftBlueLED;
			
			xT1CountPointer = &lT1count;
			xT2CountPointer = &lT2count;
			xTestComplete = &leftTestComplete;
			xTestInProgress = &leftTestInProgress;
			
			xT1CurrentSumPointer = &lT1sumCurrent;
			xT2CurrentSumPointer = &lT2sumCurrent;
			xT1RPMSumPointer = &lT1sumRPM;
			xT2RPMSumPointer = &lT2sumRPM;
			
			tempFailX = &tempFailL;
			
			operationOne = B11110110;				// wheel(B11111110) and arm F (B11110111)
			operationTwo = motor2R;					// B11111101
			operationThree = B11111001;				// arm2R (B11111011 PRESSES ON WHEEL) and Motor1F (B11111110)
			operationFour = B11111010;				// Arm is currently pressing on the wheel. turn wheel : B11111101 while arm pressing B11111011
			operationFive = arm2F;					// move arm away
			break;
			
		case 'R':
			xElapsedTime = rightElapsedTime;
			xChipAddress = rightChipAddress;
			wheelSide = 'R';
			
			xRunOncePointer = &rightRunOnceCounter[0];			//set to memory address of 0th element. Ex: 200. (*(RunOncePointer+1)) == rightRunOnceCounter[1]
			xTestPassPointer = &rightTestPass[0];
			xRealAmpsPointer = &rightRealAmps;
			xWheelRotationPointer = &rightWheelRotation;
			xWheelDropValuePointer = &rightWheelDropValue;
			xRedLedPointer = &rightRedLED;
			xGreenLedPointer = &rightGreenLED;
			xBlueLedPointer = &rightBlueLED;
			
			xT1CountPointer = &rT1count;
			xT2CountPointer = &rT2count; 
			xTestComplete = &rightTestComplete;
			xTestInProgress = &rightTestInProgress;
						
			xT1CurrentSumPointer = &rT1sumCurrent;
			xT2CurrentSumPointer = &rT2sumCurrent;					
			xT1RPMSumPointer = &rT1sumRPM;				
			xT2RPMSumPointer = &rT2sumRPM;			
			
			tempFailX = &tempFailR;
			
			operationOne = motor1R;				// wheel(B11111110) and arm F (B11111011)		B11111010
			operationTwo = B11111010;					// B11111101
			operationThree = B11110110;				// arm1R (B11110111 PRESSES ON WHEEL) and Motor1F (B11111110)
			operationFour = B11110101;				// Arm is currently pressing on the wheel. turn wheel R: B11111101 while arm pressing B11110111
			operationFive = arm1F;					// move arm away
			break;
	}
	
	
// --- START OF NORMAL OPERATION TEST
	
	// TURN WHEEL CW until t= 2.0
	if (xElapsedTime <= 2.0)													// will do this (once) until second #2
	{
		for ( ; *xRunOncePointer < 1 ; (*xRunOncePointer)++){
			sendToChip(xChipAddress, operationOne);						
			Serial.print("\nNORMAL OPERATION 1"); Serial.println(wheelSide); 			
			setColor(wheelSide, 255,65,0);										// turn LED orange
		}
		
		(*xT1CountPointer)++;									
		if ( (*xT1CountPointer) > 5)							// wait for STABILITY, then sum to calculate average
		{
			(*xT1RPMSumPointer) += (*xWheelRotationPointer);
			/*
			if (*xRealAmpsPointer > .06 && *xRealAmpsPointer < .15 && *xWheelRotationPointer < 64.0)			// FAIL criteria:
			{
				(*xTestPassPointer) = 0;
				(*tempFailX) = 1;
			}
			*/
		}		
	}
	
	// TURN WHEEL CCW until t = 4.0. Print CW average RPM
	else if (xElapsedTime <= 4.0)	
	{
		for ( ; *(xRunOncePointer+1) < 1 ; (*(xRunOncePointer+1))++){
			// Print average CW RPM
			float T1rpmAvgCW = (*xT1RPMSumPointer)/((*xT1CountPointer)-5);
			if (T1rpmAvgCW > 66.0 && T1rpmAvgCW <80.0)
				(*xTestPassPointer)= 1;
			else
				(*tempFailX) = 1;
			Serial.print(wheelSide); Serial.print("T1 CW RPM: "); Serial.print(T1rpmAvgCW);	Serial.println("; ");
			(*xT1CountPointer) = 0;											// reset variables after printing
			(*xT1RPMSumPointer) = 0;
			
			
			// Starts normal Operation 2 CCW
			sendToChip(xChipAddress, operationTwo);
			Serial.print("\nNORMAL OPERATION 2"); Serial.println(wheelSide);
		}
		
		(*xT1CountPointer)++;									// R or L T1counter
		if ( (*xT1CountPointer) > 12)							// wait for STABILITY: start only after the 12th reading. 
		{
			(*xT1CurrentSumPointer) += (*xRealAmpsPointer);			// add realAmps to currentSUM
			(*xT1RPMSumPointer) += (*xWheelRotationPointer);
		}					
			
		// check NORMAL OPERATION PARAMETERS for Failure: Current and RPM		
		if ( (*xRealAmpsPointer > 0.6 && *xRealAmpsPointer < .15) && (*xWheelRotationPointer < 64.0) )			// FAIL criteria
		{
			(*xTestPassPointer) = 0;
			(*tempFailX) = 1;
		}		

/*
		// if test hasn't failed (tempFailR) check parameters. tempFailR resets at end of test
		if (*xRealAmpsPointer > .17 && *xRealAmpsPointer < .26 && *xWheelRotationPointer > 69.0 && *xWheelRotationPointer < 80.0 && (*tempFailX) == 0)
			(*xTestPassPointer) = 1;
*/

	}
// ----- end of NORMAL OPERATION TEST
		
		
// ------- STALL TEST
	else if (xElapsedTime <= 6.0)		// from 4.0 to 6.0s
	{
		for ( ; *(xRunOncePointer+2) < 1 ; (*(xRunOncePointer+2))++){												// RUNS ONCE. PRINTS NORMAL TEST RESULTS
			float T1currentAvg = (*xT1CurrentSumPointer)/((*xT1CountPointer)-12);
			float T1rpmAvg = (*xT1RPMSumPointer)/ ( (*xT1CountPointer)-12);
			// T1 CRITERIA: CURRENT 0.06- 0.15 Amps    | RPM 66 - 80 
			if (  (T1currentAvg > 0.06 && T1currentAvg < 0.15) && (T1rpmAvg > 66.0 && T1rpmAvg < 80.0) && (*tempFailX) == 0)
				(*xTestPassPointer) = 1;
			else 
				(*xTestPassPointer) = 0;
				
			
			Serial.print(wheelSide);	Serial.print("T1 : ");	Serial.print(*xTestPassPointer);					// print NORMAL test result. RT1 CCW: 1
			Serial.print(" | currentAvg = "); Serial.print( T1currentAvg );
			Serial.print(", rpmAvg = ");	  Serial.print( T1rpmAvg );	Serial.println(";");
			(*xT1CountPointer) = 0;
			(*xT1CurrentSumPointer) = 0;																// reset variables
			(*xT1RPMSumPointer) = 0;
			
			sendToChip(xChipAddress, operationThree);			// arm1R (B11110111 PRESSES ON WHEEL) and Motor1F (B11111110)
			Serial.println("\nSTALL TEST");
		}
		
		(*xT2CountPointer)++;									// R or L T1counter
		if ( (*xT2CountPointer) > 9)							// Stabilizes after ~7 readings
		{
			(*xT2CurrentSumPointer) += (*xRealAmpsPointer);			// add realAmps to currentSUM
			(*xT2RPMSumPointer) += (*xWheelRotationPointer);
		}				
		
		/*
		// check STALL OPERATION PARAMETERS: Current and RPM
		if (*xRealAmpsPointer > .75 && *xRealAmpsPointer < .95 && *xWheelRotationPointer >= 0.0 && *xWheelRotationPointer < 0.5)
			(*(xTestPassPointer+1)) = 1;
		*/
	}
		
// ---- MICROSWITCH TEST
	else if (xElapsedTime <= 6.30)						// from 6.0 to 6.28 seconds
	{
		for ( ; *(xRunOncePointer+3) < 1 ; (*(xRunOncePointer+3))++){								// runs ONCE
			float T2currentAvg = (*xT2CurrentSumPointer)/((*xT2CountPointer)-9);
			float T2rpmAvg = (*xT2RPMSumPointer)/( (*xT2CountPointer)-9) ;			
			
			// check STALL OPERATION : Current .75 - .98  | RPM 0.0 - 4.0
			if ( (T2currentAvg > .75 && T2currentAvg < .98 ) && (T2rpmAvg >= 0.0 && T2rpmAvg < 4.0))
				(*(xTestPassPointer+1)) = 1;
			
			Serial.print(wheelSide);	Serial.print("T2 : ");	Serial.print(*(xTestPassPointer+1));				// print STALL test result
			Serial.print(" | currentAvg = "); Serial.print( T2currentAvg );
			Serial.print(", rpmAvg = ");	  Serial.print(T2rpmAvg ); Serial.println(";");
			(*xT2CountPointer) = 0;
			(*xT2CurrentSumPointer) = 0;																// reset variables
			(*xT2RPMSumPointer) = 0;

			sendToChip(xChipAddress, operationFour);		// Arm is currently stalling wheel. turn wheel R: B11111101 while arm pressing B11110111
			Serial.println(" \nSWITCH TEST");
		}
			
	}
	else if (xElapsedTime <= 6.5)						// from 6.28 to 6.5s
	{
		for ( ; *(xRunOncePointer+4) < 1 ; (*(xRunOncePointer+4))++){
			sendToChip(xChipAddress, B11111111);			// Motors off
			Serial.print(wheelSide); Serial.println(" TEST COMPLETE \n");
		}
		// check MICROSWITCH PARAMETERS
		if ((*xWheelDropValuePointer) == 1)
			(*(xTestPassPointer+2)) = 1;
	}
	else if (xElapsedTime <= 6.7)	// move arm away: operationFive.
	{
		sendToChip(xChipAddress, operationFive);
	}
	
// ----- TEST IS COMPLETE	
	else if (xElapsedTime <= 6.9)
	{
		Serial.print(wheelSide);	Serial.print("T3 : ");	Serial.println(*(xTestPassPointer+2));		// print SWITCH test result
		
		Serial.println();
		Serial.print(wheelSide);
		Serial.print(" PASS/FAIL UNIT "); Serial.print(unitID); Serial.print(": ");
		for (int i= 0; i < 3; i++)
		{
			Serial.print(*(xTestPassPointer+i));
			Serial.print(" , ");
		}
		Serial.println("\n");
				
		// CHECK IF ALL TESTS PASS
		 if ((*(xTestPassPointer+0)) == 1 && (*(xTestPassPointer+1)) == 1 && (*(xTestPassPointer+2)) == 1)
		 {
			 sendToChip(xChipAddress, greenOn);			 
			 setColor(wheelSide, 0,250,0);
		 }
		 else														// one or more tests fail
		 {
			 sendToChip(xChipAddress, redOn);	
			 setColor(wheelSide, 250,0,0);
		 }	
		
		// change variables to escape out of testing procedure. RESET STATICs
		(*xTestComplete) = 1;
		(*xTestInProgress) = 0;

		for (int i = 0; i < 5; i++)
			(*(xRunOncePointer+i)) = 0;

		for (int i = 0; i < 3; i++)
			(*(xTestPassPointer+i)) = 0;
		
		(*tempFailX) = 0;
	}		
}

void serialDataResponse()
{
	inData = Serial.read();
	switch (inData){
			
		// =====>> MOTOR
		case '1':
		Serial.println("motor 1. Dir1");
		i2cOut |= B00000011;				// reset last 2 bit every time
		i2cOut &= motor1F;					// B11111110;
		sendToChip(rightChipAddress, i2cOut);
		break;
			
		case '2':
		Serial.println("motor 1. Dir2");
		i2cOut |= B00000011;		//reset last 2 bit
		i2cOut &= motor1R;			//B11111101;
		sendToChip(rightChipAddress, i2cOut);
		break;
		// === ARM
		case '3':
		Serial.println("motor 2. Dir1");
		i2cOut |= B00001100;			// rest arm every time
		i2cOut &= arm1F;
		sendToChip(rightChipAddress, i2cOut);
		break;
			
		case '4':
		Serial.println("motor 2. Dir2");
		i2cOut |= B00001100;			// reset arm every time
		i2cOut &= arm1R;
		sendToChip(rightChipAddress, i2cOut);
		break;
			
		//== RED LIGHT
		case 'r':
		Serial.println("Red On");
		i2cOut |= B00110000;			// reset lights every time
		i2cOut &= redOn;
		sendToChip(rightChipAddress, i2cOut);
		break;
			
		//== Green LIGHT
		case 'g':
		Serial.println("Green On");
		i2cOut |= B00110000;			// reset lights every time
		i2cOut &= greenOn;
		sendToChip(rightChipAddress, i2cOut);
		break;
			
		case 'o':
		Serial.println("Lights Off");
		i2cOut |= B00110000;			// reset lights every time
		sendToChip(rightChipAddress, i2cOut);
		break;
			
		case 'e':
		Serial.println("Motors Off");
		i2cOut |= B00001111;			//reset both motors
		sendToChip(rightChipAddress, i2cOut);
		break;
			
		case 't':
		Serial.println("Testing begins");
		rightStartTime = millis();			// start timer
		rightTestComplete = 0;				// restart rightTestComplete status
		rightTestInProgress =1;				// start test
		break;

	}	// end of switch	
}


void loop() {		
	timerTick();							// starts counting L and R. 'rightStarTime' and 'leftStartTime' control test procedure.
	getAndProcessAllValues();				// gets readings from i2c chip and Arduino and saves to global variables
	
	// RIGHT WHEEL VALUES                                                                                                                                                                                                                   
		Serial.print(rightRealAmps);			Serial.print(" , ");
		Serial.print(rightWheelRotation);	Serial.print(" , ");	
		Serial.print(rightWheelDropValue);	Serial.print(" , ");
		Serial.print(rightWheelPresentValue);Serial.print(" ,   ");
		Serial.print(physicalButtonValue); Serial.print("   , "); //Serial.print(" | "); Serial.print(rightElapsedTime); Serial.println("s");
	
	 //LEFT WHEEL VALUES 
		Serial.print(leftRealAmps);			Serial.print(" , ");
		Serial.print(leftWheelRotation);	Serial.print(" , ");
		Serial.print(leftWheelDropValue);	Serial.print(" , ");
		Serial.println(leftWheelPresentValue);//Serial.print(" ,  ");
	
	
	// if test is activated (BUTTON PRESS or serial command t), start test procedure.
	if (rightTestComplete == 0 && rightTestInProgress ==1)
		testingProcedure('R');

	if (leftTestComplete == 0 && leftTestInProgress ==1)
		testingProcedure('L');


	// if RIGHT wheel is present and test is not complete Start Test
	if (rightWheelPresentValue == 1 && rightAutoTest == 1)
	{
		rightStartTime = millis();			// start timer
		rightTestComplete = 0;				// restart rightTestComplete status
		rightTestInProgress =1;				// start test	
		rightAutoTest = 0;				
		testingProcedure('R');
	}
	
	// if LEFT Wheel is present and test is not complete; start test
	if (leftWheelPresentValue == 1 && leftAutoTest == 1)
	{
		leftStartTime = millis();			// start timer
		leftTestComplete = 0;				// restart rightTestComplete status
		leftTestInProgress =1;				// start test
		leftAutoTest = 0;
		testingProcedure('L');
	}
	
	// if data is sent on serial monitor, respond
	if (Serial.available() >0)
		serialDataResponse();
		
	//delay(1);
		                   
}
