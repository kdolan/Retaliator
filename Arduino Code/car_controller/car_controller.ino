/*
Adafruit Arduino - Lesson 15. Bi-directional Motor
*/
 

int RF_enbl = 8;
int RF_in1 = 30;
int RF_in2 = 31;

int LF_enbl = 2;
int LF_in1 = 32;
int LF_in2 = 33;

int LR_enbl = 13;
int LR_in1 = 40;
int LR_in2 = 41;

int RR_enbl = 3;
int RR_in1 = 42;
int RR_in2 = 43;

void setup()
{
  Serial.begin(9600);
  
  
  pinMode(LF_enbl, OUTPUT);
  pinMode(LF_in1, OUTPUT);
  pinMode(LF_in2, OUTPUT);
  
  pinMode(RF_enbl, OUTPUT);
  pinMode(RF_in1, OUTPUT);
  pinMode(RF_in2, OUTPUT);
  
  pinMode(LR_enbl, OUTPUT);
  pinMode(LR_in1, OUTPUT);
  pinMode(LR_in2, OUTPUT);
  
  pinMode(RR_enbl, OUTPUT);
  pinMode(RR_in1, OUTPUT);
  pinMode(RR_in2, OUTPUT);

  //pinMode(switchPin, INPUT_PULLUP);
}
 
void loop()
{
    Serial.println(y);
    //Wait for pi
    boolean piFound = false;
    while(!piFound)
    {
        Serial.write("CAR\r\n");
        if (Serial.available() > 0) 
            //Device has been picked up by parent
            piFound = true;
    }
    Serial.flush();

    //Wait for commands
    while(true)
    {
      char cmdChar;
      char inData[24]; // Allocate some space for the string
      byte index = 0; // Index into array; where to store the character
      //Serial.print(Serial.available());
      //int SerAval=Serial.available();
      if (Serial.available() > 0) 
      {
        Serial.readBytesUntil('#',inData,24);
        //cmdChar = Serial.read(); // Read a character
        cmdChar = inData[0];
        Serial.print(cmdChar);
        index = 0;
        if(cmdChar=='D')
        {          
           String str_inData = String(inData);
           String str_LF = str_inData.substring(1,4);
           String str_RF = str_inData.substring(4,7);
           String str_LR = str_inData.substring(7,10);
           String str_RR = str_inData.substring(10,13);
           Serial.println();
           //Serial.println(str_RR);
           Serial.println();
           //Serial.println(str_LF);Serial.println(str_RF);Serial.println(str_LR);Serial.println(str_RR);
  
           int LF = str_LF.toInt();
           int RF = str_RF.toInt();
           int LR = str_LR.toInt();
           int RR = str_RR.toInt();
           //Serial.println(LF);Serial.println(RF);Serial.println(LR);Serial.println(RR);
           //Serial.print("-->");
           //Serial.print(str_inData);
           //Serial.print("<--");
           //int LF = int(
           
           boolean rev_LF=false;
           boolean rev_RF=false;
           boolean rev_LR=false;
           boolean rev_RR=false;
           
           if(LF<100)
             rev_LF=true;
           else
             LF=LF-100;

           if(RF<100)
             rev_RF=true;
           else
             RF=RF-100;
             
           if(LR<100)
             rev_LR=true;
           else
             LR=LR-100;
             
            if(RR<100)
             rev_RR=true;
           else
             RR=RR-100;
           
           LF = ((float)LF/100.0)*255;
           RF = ((float)RF/100.0)*255;
           LR = ((float)LR/100.0)*255;
           RR = ((float)RR/100.0)*255;
           Serial.println(LF);Serial.println(RF);Serial.println(LR);Serial.println(RR);
           
        }
      }
    }
      
  
    //int speed = analogRead(potPin) / 4;
    int speed1 = 255;
    //boolean reverse = digitalRead(switchPin);
    //setMotor(speed1, true);
}
 
void setMotor(int enablePin, int in1Pin, int in2Pin, int speed1, boolean reverse)
{
  analogWrite(enablePin, speed1);
  digitalWrite(in1Pin, ! reverse);
  digitalWrite(in2Pin, reverse);
}

