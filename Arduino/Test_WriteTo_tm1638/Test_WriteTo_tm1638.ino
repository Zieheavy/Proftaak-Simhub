#include <TM1638.h>
// define a module on data pin 8, clock pin 9 and strobe pin 10
TM1638 module(8, 9, 10);
 unsigned long RpmLeds=0;
 int RpmMax = 8000;
 char m_data[21];
void setup()
{
  Serial.begin(9600);
module.setDisplayToString("SIMHUB");
   delay(10000);
module.setDisplayToString("        ");
}
 
void loop()
{
  SetOutputPinFromCsharp();
  delay(10);
}

void SetOutputPinFromCsharp()
{
  int m_receivedValue;
  

  if (Serial.available() > 0)
  {
    Serial.readBytes(m_data, 21);
    if (m_data[0] == 97 && m_data[1] == 98)
    {
//
//          if (m_data[2] == 65)
//          {
//            module.setDisplayToString("breaking");
//          }
//          else if( m_data[2] == 66)
//          {
//            module.setDisplayToString("        ");
//          }
      Gear();
      RPM();

    }
  }
}
void RPM()
{
  
  RpmLeds = map(0, 500, RpmMax,1 ,8);
         if (RpmLeds == 1) { module.setLEDs(0b00000001 | 0b00000000<< 8);} 
         if (RpmLeds == 2) { module.setLEDs(0b00000011 | 0b00000000<< 8);}
         if (RpmLeds == 3) { module.setLEDs(0b00000111 | 0b00000000<< 8);}  
         if (RpmLeds == 4) { module.setLEDs(0b00001111 | 0b00000000<< 8);}
         if (RpmLeds == 5){ module.setLEDs(0b00011111 | 0b00000000<< 8);}
         if (RpmLeds == 6){ module.setLEDs(0b00011111 | 0b00100000<< 8);}
         if (RpmLeds == 7){ module.setLEDs(0b00011111 | 0b01100000<< 8);}
         if (RpmLeds == 8){ module.setLEDs(0b00011111 | 0b11100000<< 8);}
       
         //module.setDisplayToDecNumber(Speed, 0, false);  //displays numerical the Speed 
        //module.setDisplayToDecNumber(a, 0, false);  //displays numerical the rpm
}
void Gear ()
{
   if (m_data[2] == 78)
      {
        module.setDisplayToString("       N");
      }
      else if (m_data[2] == 82)
      {
        module.setDisplayToString("       R");
      }
      else if (m_data[2] == 49)
      {
        module.setDisplayToString("       1");
      }
      else if (m_data[2] == 50)
      {
        module.setDisplayToString("       2");
      }
      else if (m_data[2] == 51)
      {
        module.setDisplayToString("       3");
      }
      else if (m_data[2] == 52)
      {
        module.setDisplayToString("       4");
      }
      else if (m_data[2] == 53)
      {
        module.setDisplayToString("       5");
      }
      else if (m_data[2] == 54)
      {
        module.setDisplayToString("       6");
      }
}

