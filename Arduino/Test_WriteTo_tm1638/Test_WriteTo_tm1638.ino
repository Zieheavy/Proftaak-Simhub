#include <TM1638.h>
// define a module on data pin 8, clock pin 9 and strobe pin 10
TM1638 module(8, 9, 10);
int a = 5;
char incoming[9] = {};
void setup()
{
  Serial.begin(9600);
}

void loop()
{
  SetOutputPinFromCsharp();
  delay(10);
}

void SetOutputPinFromCsharp()
{
  int m_receivedValue;
  char m_data[19];

  if (Serial.available() > 0)
  {
    Serial.readBytes(m_data, 19);
    
//    if (m_data[0] == 65)
//    {
//      module.setDisplayToString("breaking");
//    } 
//    else if( m_data[0] == 66)
//    {
//      module.setDisplayToString("     Not");
//    }
    if (m_data[8] == 49)
    {
      module.setDisplayToString("       1");
    }
    else if (m_data[8] == 50)
    {
      module.setDisplayToString("       2");
    }
    else if (m_data[8] == 51)
    {
      module.setDisplayToString("       3");
    }
    else if (m_data[8] == 52)
    {
      module.setDisplayToString("       4");
    }
    else if (m_data[8] == 53)
    {
      module.setDisplayToString("       5");
    }
    else if (m_data[8] == 54)
    {
      module.setDisplayToString("       6");
    }
    else if (m_data[8] == 78)
    {
      module.setDisplayToString("       N");
    }
    else if (m_data[8] == 82)
    {
      module.setDisplayToString("       R");
    }
  }
}
