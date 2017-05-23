#include <Wire.h>

#define ADDRESS 0x60                                           //Defines address of CMPS10
String stringToCheck = "";
int doRxAck = 7;

void setup()
{
  Wire.begin();  

  pinMode(doRxAck, OUTPUT);

  digitalWrite(doRxAck, HIGH);
  delay(500);
  digitalWrite(doRxAck, LOW);

  Serial.begin(9600);
  Serial.println("Start up finished..");
}

void loop()
{
  byte highByte, lowByte, fine;              // highByte and lowByte store high and low bytes of the bearing and fine stores decimal place of bearing
  byte pitch, roll;                          // Stores pitch and roll values of CMPS10, chars are used because they support signed value
  int bearing;                               // Stores full bearing

    int m_receivedValue;
  char m_data[3];

  Serial.println("Start communication with CMPS10..");
  Wire.beginTransmission(ADDRESS);          // starts communication with CMPS10
  Wire.write(2);                            // Sends the register we wish to start reading from
  Wire.endTransmission();

  Serial.println("Get data from CMPS10..");
  Wire.requestFrom(ADDRESS, 4);             // Request 4 bytes from CMPS10
  while(Wire.available() < 4);              // Wait for bytes to become available

  highByte = Wire.read();           
  lowByte = Wire.read();            
  pitch = (byte)Wire.read();              
  roll = (byte)Wire.read();               

  bearing = ((highByte<<8)+lowByte)/10;     // Calculate full bearing
  fine = ((highByte<<8)+lowByte)%10;        // Calculate decimal place of bearing

  if(Serial.available() > 0)
  {
        m_receivedValue = Serial.read();
        
        if(m_receivedValue == 55)
        {
          digitalWrite(doRxAck, HIGH);
          delay(500);      
        }
        digitalWrite(doRxAck, LOW);

    Serial.readBytes(m_data, 3);

    if(m_data[0] == 65 && m_data[1] == 66)
    {
      switch(m_data[2])
      {
      case 67: 
        digitalWrite(doRxAck, HIGH);
        Serial.println("GAY");
        delay(500); 
        break;     
      case 68: 
        digitalWrite(doRxAck, HIGH);
        delay(1000); 
        break;
      default: 
        digitalWrite(doRxAck, HIGH);
        delay(100); 
        break;
      }
    }

    digitalWrite(doRxAck, LOW);
  }

   Serial.println("#received value: " + (String)m_receivedValue + (String)"%");
  Serial.println("#received value: " + (String)m_data[0] + " > "  + (String)m_data[1] + " > " + (String)m_data[2] + " > " + (String)"%");

  Serial.println((String)"#" + "bearing: " + (String)bearing + "\t\t" + "fine: " + (String)fine + "\t\t" + 
    "pitch: " + (String)pitch + "\t\t" + "roll: " + (String)roll +
    (String)"%"); 

  Serial.flush();



  delay(100);
}
