#include <TM1638.h>
// define a module on data pin 8, clock pin 9 and strobe pin 10
TM1638 module(8, 9, 10);
 unsigned long RpmLeds;
 int RpmMax = 8000;
 char m_data[21];
void setup()
{
  Serial.begin(9600);
module.setDisplayToString("S       ");
   delay(500);
   module.setDisplayToString("SI      ");
   delay(500);
   module.setDisplayToString("SIM     ");
   delay(500);
   module.setDisplayToString("SIMH   ");
   delay(500);
   module.setDisplayToString("SIMHU  ");
   delay(500);
   module.setDisplayToString("SIMHUB  ");
   delay(1000);
   module.setDisplayToString("       ");
   delay(500);
   module.setDisplayToString("SIMHUB  ");
   delay(3000);
module.setDisplayToString("        ");
}
 
void loop()
{
  SetOutputPinFromCsharp();
  delay(1);
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
      //Gear();
      RPM();
      Speed();

    }
  }
}
void RPM()
{
  int a = 0;
      if (m_data[3] == 49)
      {
        a= a+1000;
      }
      else if (m_data[3] == 50)
      {
        a= a+2000;
      }
      else if (m_data[3] == 51)
      {
         a= a+3000;
      }
      else if (m_data[3] == 52)
      {
         a= a+4000;
      }
      else if (m_data[3] == 53)
      {
         a= a+5000;
      }
      else if (m_data[3] == 54)
      {
        a= a+6000;
      }
      else if (m_data[3] == 55)
      {
         a= a+7000;
      }
      else if (m_data[3] == 56)
      {
         a= a+8000;
      }
      else if (m_data[3] == 57)
      {
         a= a+9000;
      }



      if (m_data[4] == 49)
      {
        a= a +100;
      }
      else if (m_data[4] == 50)
      {
        a= a +200;
      }
      else if (m_data[4] == 51)
      {
         a= a+300;
      }
      else if (m_data[4] == 52)
      {
         a= a+400;
      }
      else if (m_data[4] == 53)
      {
         a= a+500;
      }
      else if (m_data[4] == 54)
      {
        a= a+600;
      }
      else if (m_data[4] == 55)
      {
         a= a+700;
      }
      else if (m_data[4] == 56)
      {
         a= a+800;
      }
      else if (m_data[4] == 57)
      {
         a= a+900;
      }


if (m_data[5] == 49)
      {
        a= a +10;
      }
      else if (m_data[5] == 50)
      {
        a= a +20;
      }
      else if (m_data[5] == 51)
      {
         a= a+30;
      }
      else if (m_data[5] == 52)
      {
         a= a+40;
      }
      else if (m_data[5] == 53)
      {
         a= a+50;
      }
      else if (m_data[5] == 54)
      {
        a= a+60;
      }
      else if (m_data[5] == 55)
      {
         a= a+70;
      }
      else if (m_data[5] == 56)
      {
         a= a+80;
      }
      else if (m_data[5] == 57)
      {
         a= a+90;
      }


      if (m_data[6] == 49)
      {
        a= a +1;
      }
      else if (m_data[6] == 50)
      {
        a= a +2;
      }
      else if (m_data[6] == 51)
      {
         a= a+3;
      }
      else if (m_data[6] == 52)
      {
         a= a+4;
      }
      else if (m_data[6] == 53)
      {
         a= a+5;
      }
      else if (m_data[6] == 54)
      {
        a= a+6;
      }
      else if (m_data[6] == 55)
      {
         a= a+7;
      }
      else if (m_data[6] == 56)
      {
         a= a+8;
      }
      else if (m_data[6] == 57)
      {
         a= a+9;
      }
RpmLeds = map(a, 1, RpmMax,1 ,8);
         if (RpmLeds == 1) { module.setLEDs(0b00000001 | 0b00000000<< 8);} 
         if (RpmLeds == 2) { module.setLEDs(0b00000011 | 0b00000000<< 8);}
         if (RpmLeds == 3) { module.setLEDs(0b00000111 | 0b00000000<< 8);}  
         if (RpmLeds == 4) { module.setLEDs(0b00001111 | 0b00000000<< 8);}
         if (RpmLeds == 5){ module.setLEDs(0b00011111 | 0b00000000<< 8);}
         if (RpmLeds == 6){ module.setLEDs(0b00011111 | 0b00100000<< 8);}
         if (RpmLeds == 7){ module.setLEDs(0b00011111 | 0b01100000<< 8);}
         if (RpmLeds == 8){ module.setLEDs(0b00011111 | 0b11100000<< 8);}
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
void Speed()
{
  int a = 0;
      if (m_data[7] == 49)
      {
        a= a+100;
      }
      else if (m_data[7] == 50)
      {
        a= a+200;
      }



      if (m_data[8] == 49)
      {
        a= a +10;
      }
      else if (m_data[8] == 50)
      {
        a= a +20;
      }
      else if (m_data[8] == 51)
      {
         a= a+30;
      }
      else if (m_data[8] == 52)
      {
         a= a+40;
      }
      else if (m_data[8] == 53)
      {
         a= a+50;
      }
      else if (m_data[8] == 54)
      {
        a= a+60;
      }
      else if (m_data[8] == 55)
      {
         a= a+70;
      }
      else if (m_data[8] == 56)
      {
         a= a+80;
      }
      else if (m_data[8] == 57)
      {
         a= a+90;
      }


if (m_data[9] == 49)
      {
        a= a +1;
      }
      else if (m_data[9] == 50)
      {
        a= a +2;
      }
      else if (m_data[9] == 51)
      {
         a= a+3;
      }
      else if (m_data[9] == 52)
      {
         a= a+4;
      }
      else if (m_data[9] == 53)
      {
         a= a+5;
      }
      else if (m_data[9] == 54)
      {
        a= a+6;
      }
      else if (m_data[9] == 55)
      {
         a= a+7;
      }
      else if (m_data[9] == 56)
      {
         a= a+8;
      }
      else if (m_data[9] == 57)
      {
         a= a+9;
      }

         module.setDisplayToDecNumber(a, 0, false);  //displays numerical the Speed 
}
