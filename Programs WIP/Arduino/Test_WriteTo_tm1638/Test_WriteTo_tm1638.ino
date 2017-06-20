#include <TM1638.h>
// define a module on data pin 8, clock pin 9 and strobe pin 10
TM1638 module(8, 9, 10);
unsigned long RpmLeds;
int RpmMax =  7000;
char m_data[15];

String cTimeM1 = "";
String cTimeM2 = "";
String cTimeS1 = "";
String cTimeS2 = "";

String Gear;
String cRound;

int count = 0;

void setup()
{
  Serial.begin(9600);
  //Welcome();
}

void loop()
{
  count++;
  if (Serial.available() > 0)
  {
    Serial.readBytes(m_data, 12);
    if (m_data[0] == 97 && m_data[1] == 98)
    {
      count = 0;
      cTime();
      RPM();
      Gears();
      module.setDisplayToString(Gear + " " + cTimeM1 + cTimeM2 + cTimeS1 + cTimeS2 + "  " + cRound);
    }
  }
  if(count > 10)
  {
    module.setDisplayToString("  idle  ");
  }
}

void Brakes()
{
  if (m_data[2] == 65)
  {
    module.setDisplayToString("breaking");
  }
  else if ( m_data[2] == 66)
  {
    //module.setDisplayToString("        ");
  }
}
void RPM()
{
  int a = (m_data[3] - 48) * 1000;
  int b = (m_data[4] - 48) * 100;
  int c = (m_data[5] - 48) * 10;
  int d = m_data[6] - 48;

  int e = a + b + c + d;

  RpmLeds = map(e, 500, RpmMax, 1 , 8);
  if (RpmLeds == 1) {
    module.setLEDs(0b00000001 | 0b00000000 << 8);
  }
  if (RpmLeds == 2) {
    module.setLEDs(0b00000011 | 0b00000000 << 8);
  }
  if (RpmLeds == 3) {
    module.setLEDs(0b00000111 | 0b00000000 << 8);
  }
  if (RpmLeds == 4) {
    module.setLEDs(0b00001111 | 0b00000000 << 8);
  }
  if (RpmLeds == 5) {
    module.setLEDs(0b00011111 | 0b00000000 << 8);
  }
  if (RpmLeds == 6) {
    module.setLEDs(0b00011111 | 0b00100000 << 8);
  }
  if (RpmLeds == 7) {
    module.setLEDs(0b00011111 | 0b01100000 << 8);
  }
  if (RpmLeds == 8) {
    module.setLEDs(0b00011111 | 0b11100000 << 8);
  }
}
void Gears()
{
  if (m_data[2] == 78)
  {
    Gear = "N";
  }
  else if (m_data[2] == 82)
  {
    Gear = "R";
  }
  else
  {
    Gear = String(m_data[2] - 48);

  }
}
void cTime()
{
  cTimeM1 = char(m_data[7]);
  cTimeM2 = char(m_data[8]);
  cTimeS1 = char(m_data[9]);
  cTimeS2 = char(m_data[10]);
}
void Round()
{
  cRound = String(m_data[11] - 48);
}
void Welcome() {
  int Delay = 300;
  module.setDisplayToString("        W");
  module.setLEDs           (128);
  delay(Delay);
  module.setDisplayToString("       WE");
  module.setLEDs           (64);
  delay(Delay);
  module.setDisplayToString("      WEL");
  module.setLEDs           (160);
  delay(Delay);
  module.setDisplayToString("     WELC");
  module.setLEDs           (80);
  delay(Delay);
  module.setDisplayToString("    WELCO");
  module.setLEDs           (168);
  delay(Delay);
  module.setDisplayToString("   WELCOM");
  module.setLEDs           (84);
  delay(Delay);
  module.setDisplayToString("  WELCOME");
  module.setLEDs           (170);
  delay(Delay);
  module.setDisplayToString(" WELCOME ");
  module.setLEDs           (85);
  delay(Delay);
  module.setDisplayToString("WELCOME  ");
  module.setLEDs           (170);
  delay(Delay);
  module.setDisplayToString("ELCOME T");
  module.setLEDs           (85);
  delay(Delay);
  module.setDisplayToString("LCOME TO");
  module.setLEDs           (170);
  delay(Delay);
  module.setDisplayToString("COME TO ");
  module.setLEDs           (85);
  delay(Delay);
  module.setDisplayToString("OME TO  ");
  module.setLEDs           (170);
  delay(Delay);
  module.setDisplayToString("ME TO  S");
  module.setLEDs           (85);
  delay(Delay);
  module.setDisplayToString("E TO  SI");
  module.setLEDs           (42);
  delay(Delay);
  module.setDisplayToString(" TO  SIM");
  module.setLEDs           (21);
  delay(Delay);
  module.setDisplayToString("TO  SIMH");
  module.setLEDs           (10);
  delay(Delay);
  module.setDisplayToString("O  SIMHU");
  module.setLEDs           (5);
  delay(Delay);
  module.setDisplayToString("  SIMHUB");
  module.setLEDs           (2);
  delay(Delay);
  module.setDisplayToString(" SIMHUB ");
  module.setLEDs           (1);
  delay(Delay);
  module.setDisplayToString("        ");
  module.setLEDs           (0);
  delay(Delay);
  module.setDisplayToString(" SIMHUB ");
  module.setLEDs           (65280);
  delay(Delay);
  module.setDisplayToString("        ");
  module.setLEDs           (255);
  delay(Delay);
  module.setDisplayToString(" SIMHUB ");
  module.setLEDs           (65280);
  delay(Delay);
  module.setDisplayToString("        ");
  module.setLEDs           (255);
  delay(Delay);
  module.setDisplayToString(" SIMHUB ");
  module.setLEDs           (65280);
  delay(Delay);
  module.setDisplayToString("        ");
  module.setLEDs           (255);
  delay(Delay);
  module.setDisplayToString(" SIMHUB ");
  module.setLEDs           (65280);
  delay(Delay);
  delay(Delay);
  delay(Delay);
  delay(Delay);
}
