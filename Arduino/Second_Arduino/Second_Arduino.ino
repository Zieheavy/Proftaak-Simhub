#include <TM1638.h>
// define a module on data pin 8, clock pin 9 and strobe pin 10
TM1638 module(8, 9, 10);
char m_data[15];



String tTimeM1;
String tTimeM2;
String tTimeS1;
String tTimeS2;


char Speed100;
char Speed10;
char Speed1;

String pos;
int a;

int count = 0;

void setup()
{
  Serial.begin(9600);
  //Welcome();
}

void loop()
{
  count++;
  tTime();
  Speed();
  Breaking();
  if (Serial.available() > 0)
  {
    Serial.readBytes(m_data, 12);
    if (m_data[0] == 97 && m_data[1] == 98)
    {
      count = 0;
      module.setDisplayToString(tTimeM1 + tTimeM2 + tTimeS1 + tTimeS2 + " " + Speed100 + Speed10 + Speed1,64,false);
    }
  }
  if(count > 10)
  {
    module.setDisplayToString("  Idle  ");
  }
  
}

void tTime()
{
  tTimeM1 = char(m_data[2]);
  tTimeM2 = char(m_data[3]);
  tTimeS1 = char(m_data[4]);
  tTimeS2 = char(m_data[5]);
}


void Speed()
{
  Speed100 = char(m_data[6]);
  Speed10 = char(m_data[7]);
  Speed1 = char(m_data[8]); // String(m_data[9] -48);
}

void Breaking()
{
  if(m_data[9] == 65)
  {
    module.setLEDs(65280);
  }
  else if (m_data[9] == 66)
  {
    module.setLEDs(0);    
  }
}


