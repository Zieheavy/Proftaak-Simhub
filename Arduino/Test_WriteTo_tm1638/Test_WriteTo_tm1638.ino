#include <TM1638.h>
// define a module on data pin 8, clock pin 9 and strobe pin 10
TM1638 module(8, 9, 10);
 
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
  char m_data[21];

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
  }
}

