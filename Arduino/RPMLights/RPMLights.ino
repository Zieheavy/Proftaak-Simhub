#include <TM1638.h>
// define a module on data pin 8, clock pin 9 and strobe pin 10
TM1638 module(8, 9, 10);
unsigned long RpmLeds=0;
int a = 0;
int RpmMax = 1000;
 
void setup()
{
module.setDisplayToString("SIMHUB");
   delay(10000);
}
 
void loop()
{
for (a=1; a<=RpmMax; a++)
{
RpmLeds = map(a, 500, RpmMax,1 ,8);
         if (RpmLeds == 1) { module.setLEDs(0b00000001 | 0b00000000<< 8);} 
         if (RpmLeds == 2) { module.setLEDs(0b00000011 | 0b00000000<< 8);}
         if (RpmLeds == 3) { module.setLEDs(0b00000111 | 0b00000000<< 8);}  
         if (RpmLeds == 4) { module.setLEDs(0b00001111 | 0b00000000<< 8);}
         if (RpmLeds == 5){ module.setLEDs(0b00011111 | 0b00000000<< 8);}
         if (RpmLeds == 6){ module.setLEDs(0b00011111 | 0b00100000<< 8);}
         if (RpmLeds == 7){ module.setLEDs(0b00011111 | 0b01100000<< 8);}
         if (RpmLeds == 8){ module.setLEDs(0b00011111 | 0b11100000<< 8);}
       
         //module.setDisplayToDecNumber(Speed, 0, false);  //displays numerical the Speed 
         module.setDisplayToDecNumber(a, 0, false);  //displays numerical the rpm
}
}
