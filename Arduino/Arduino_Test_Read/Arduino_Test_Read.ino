boolean DigitOn = LOW;
boolean DigitOff = HIGH;
boolean SegOn=HIGH;
boolean SegOff=LOW;
int DigitPins[] = {2, 3, 4, 5};
int SegmentPins[] = {6, 7, 8, 9, 10, 11, 12, 13};
 
int BLANK[] = {LOW, LOW, LOW, LOW, LOW, LOW, LOW, LOW};
int N0[]    = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, LOW, LOW};
int N0P[]   = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, LOW, HIGH};
int N1[]    = {LOW, HIGH, HIGH, LOW, LOW, LOW, LOW, LOW};
int N1P[]   = {LOW, HIGH, HIGH, LOW, LOW, LOW, LOW, HIGH};
int N2[]    = {HIGH, HIGH, LOW, HIGH, HIGH, LOW, HIGH, LOW};
int N2P[]   = {HIGH, HIGH, LOW, HIGH, HIGH, LOW, HIGH, HIGH};
int N3[]    = {HIGH, HIGH, HIGH, HIGH, LOW, LOW, HIGH, LOW};
int N3P[]   = {HIGH, HIGH, HIGH, HIGH, LOW, LOW, HIGH, HIGH};
int N4[]    = {LOW, HIGH, HIGH, LOW, LOW, HIGH, HIGH, LOW};
int N4P[]   = {LOW, HIGH, HIGH, LOW, LOW, HIGH, HIGH, HIGH};
int N5[]    = {HIGH, LOW, HIGH, HIGH, LOW, HIGH, HIGH, LOW};
int N5P[]   = {HIGH, LOW, HIGH, HIGH, LOW, HIGH, HIGH, HIGH};
int N6[]    = {HIGH, LOW, HIGH, HIGH, HIGH, HIGH, HIGH, LOW};
int N6P[]   = {HIGH, LOW, HIGH, HIGH, HIGH, HIGH, HIGH, HIGH};
int N7[]    = {HIGH, HIGH, HIGH, LOW, LOW, LOW, LOW, LOW};
int N7P[]   = {HIGH, HIGH, HIGH, LOW, LOW, LOW, LOW, HIGH};
int N8[]    = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, LOW};
int N8P[]   = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, HIGH};
int N9[]    = {HIGH, HIGH, HIGH, HIGH, LOW, HIGH, HIGH, LOW};
int N9P[]   = {HIGH, HIGH, HIGH, HIGH, LOW, HIGH, HIGH, HIGH};
int MIN[]   = {LOW, LOW, LOW, LOW, LOW, LOW, HIGH, LOW};
int A[] = {HIGH, HIGH, HIGH, LOW, HIGH, HIGH, HIGH, LOW};
int B[] = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, LOW};
int C[] = {HIGH, LOW, LOW, HIGH, HIGH, HIGH, LOW, LOW};
int D[] = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, LOW, LOW};
int E[] = {HIGH, LOW, LOW, HIGH, HIGH, HIGH, HIGH, LOW};
int F[] = {HIGH, LOW, LOW, LOW, HIGH, HIGH, HIGH, LOW};
int G[] = {HIGH, LOW, HIGH, HIGH, HIGH, HIGH, HIGH, LOW};
int H[] = {LOW, HIGH, HIGH, LOW, HIGH, HIGH, HIGH, LOW};
int I[] = {LOW, HIGH, HIGH, LOW, LOW, LOW, LOW, LOW};
int J[] = {LOW, HIGH, HIGH, HIGH, HIGH, LOW, LOW, LOW};
int L[] = {LOW, LOW, LOW, HIGH, HIGH, HIGH, LOW, LOW};
int O[] = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, LOW, LOW};
int P[] = {HIGH, HIGH, LOW, LOW, HIGH, HIGH, HIGH, LOW};
int Q[] = {HIGH, HIGH, HIGH, HIGH, HIGH, HIGH, LOW, HIGH};
int R[] = {HIGH, HIGH, HIGH, LOW, HIGH, HIGH, HIGH, LOW};
int S[] = {HIGH, LOW, HIGH, HIGH, LOW, HIGH, HIGH, LOW};
int U[] = {LOW, HIGH, HIGH, HIGH, HIGH, HIGH, LOW, LOW};
int Y[] = {LOW, HIGH, HIGH, HIGH, LOW, HIGH, HIGH, LOW};
 
int* lights[4];
char incoming[9] = {}; 

int brake = A0;

void setup() {
  Serial.begin(9600);

  for (byte digit=0;digit<4;digit++) {
    pinMode(DigitPins[digit], OUTPUT);
  }
  for (byte seg=0;seg<8;seg++) {
    pinMode(SegmentPins[seg], OUTPUT);
  }
  //initialize display with 1.234
  lights[0] = N1;

  pinMode(brake,OUTPUT);
}

void loop(){
  SetOutputPinFromCsharp();
    
  //read the numbers and / or chars from the serial interface
  if (Serial.available() > 0) {
    int i = 0;
    //clear the array of char 
    memset(incoming, 0, sizeof(incoming));
    while (Serial.available() > 0 && i < sizeof(incoming) - 1) {
      incoming[i] = Serial.read();
      i++;
      delay(3);
    }
    Serial.println(incoming); 
     
    //tmp is for the incoming string and counter for the 4 digits
    int counter = -1;
    for (int tmp = 0; tmp < 9; tmp++) {
      counter++;
      switch(incoming[tmp]){
        case '0': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N0P;
            tmp++;
          } else {
            lights[counter] = N0; 
          }
          break;
        case '1': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N1P;
            tmp++;
          } else {
            lights[counter] = N1; 
          }
          break;
        case '2': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N2P;
            tmp++;
          } else {
            lights[counter] = N2; 
          }
          break;
        case '3': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N3P;
            tmp++;
          } else {
            lights[counter] = N3; 
          }
          break;
        case '4': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N4P;
            tmp++;
          } else {
            lights[counter] = N4; 
          }
          break;
        case '5': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N5P;
            tmp++;
          } else {
            lights[counter] = N5; 
          }
          break;
        case '6': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N6P;
            tmp++;
          } else {
            lights[counter] = N6; 
          }
          break;
        case '7': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N7P;
            tmp++;
          } else {
            lights[counter] = N7; 
          }
          break;
        case '8': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N8P;
            tmp++;
          } else {
            lights[counter] = N8; 
          }
          break;
        case '9': 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            lights[counter] = N9P;
            tmp++;
          } else {
            lights[counter] = N9; 
          }
          break;
        case '-':
          lights[counter] = MIN; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        //with letters the decimal point is ignored!
        //if you need it, just write AP, BP etc with HIGH in the last position
        case 'a': //falls through to the next case
        case 'A': 
          lights[counter] = A; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'b': //falls through to the next case
        case 'B': 
          lights[counter] = B; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'c': //falls through to the next case
        case 'C': 
          lights[counter] = C; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'd': //falls through to the next case
        case 'D': 
          lights[counter] = D; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'e': //falls through to the next case
        case 'E': 
          lights[counter] = E; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'f': //falls through to the next case
        case 'F': 
          lights[counter] = F; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'g': //falls through to the next case
        case 'G': 
          lights[counter] = G; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'h': //falls through to the next case
        case 'H': 
          lights[counter] = H; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'i': //falls through to the next case
        case 'I': 
          lights[counter] = I; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'j': //falls through to the next case
        case 'J': 
          lights[counter] = J; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'l': //falls through to the next case
        case 'L': 
          lights[counter] = L; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'o': //falls through to the next case
        case 'O': 
          lights[counter] = O; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'p': //falls through to the next case
        case 'P': 
          lights[counter] = P; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'q': //falls through to the next case
        case 'Q': 
          lights[counter] = Q; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'r': //falls through to the next case
        case 'R': 
          lights[counter] = R; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 's': //falls through to the next case
        case 'S': 
          lights[counter] = S; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'u': //falls through to the next case
        case 'U': 
          lights[counter] = U; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;        
        case 'y': //falls through to the next case
        case 'Y': 
          lights[counter] = Y; 
          if (tmp  < 8 && (incoming[tmp + 1] == '.' || incoming[tmp + 1] == ',')) {
            tmp++;
          } 
          break;       
        case 1 ... 43: counter--; break;//special chars are ignored 
        //44 to 46 are , - .
        case 47: counter--; break;//special chars are ignored 
        //chars between Z and a
        case 91 ... 96: counter--; break;//special chars are ignored 
        case 123 ... 127: counter--; Serial.println("above 122"); break;//special chars are ignored 
         
        default: lights[counter] = BLANK;
      }
    } //end for
     
    //show the input values
    for (int y = 0; y < 4; y++) {
      Serial.print(y);
      Serial.print(": ");
      for (int z = 0; z < 8; z++) {
        Serial.print(lights[y][z]);
      }
      Serial.println("");
    }
  } //end if, i.e. reading from serial interface
 
  //This part of the code is from the library SevSeg by Dean Reading
  for (byte seg=0;seg<8;seg++) {
    //Turn the relevant segment on
    digitalWrite(SegmentPins[seg],SegOn);
 
    //For each digit, turn relevant digits on
    for (byte digit=0;digit<4;digit++){
      if (lights[digit][seg]==1) {
        digitalWrite(DigitPins[digit],DigitOn);
      }
      //delay(200); //Uncomment this to see it in slow motion
    }
    //Turn all digits off
    for (byte digit=0;digit<4;digit++){
      digitalWrite(DigitPins[digit],DigitOff);
    }
 
    //Turn the relevant segment off
    digitalWrite(SegmentPins[seg],SegOff);
  } //end of for
}

void SetOutputPinFromCsharp()
{
  lights[0] = BLANK;
  int m_receivedValue;
  char m_data[4];

  if (Serial.available() > 0)
  {
    Serial.readBytes(m_data, 4);
    if (m_data[0] == 65)
    {
        digitalWrite(brake,HIGH);
    }
    else if(m_data[0] == 66)
    {
        digitalWrite(brake,LOW);
    }
    
    if(m_data[1] == 48)
    {
      lights[1] = N1;
    }else if(m_data[1] == 49)
    {
      lights[1] = N1;    
    }else if(m_data[1] == 50)
    {
      lights[1] = N2;    }
    
    if(m_data[2] == 48)
    {
      lights[2] = N0;
    }else if(m_data[2] == 49)
    {
      lights[2] = N1;
    }else if(m_data[2] == 50)
    {
      lights[2] = N2;
    }else if(m_data[2] == 51)
    {
      lights[2] = N3;
    }else if(m_data[2] == 52)
    {
      lights[2] = N4;
    }else if(m_data[2] == 53)
    {
      lights[2] = N5;
    }else if(m_data[2] == 54)
    {
      lights[2] = N6;
    }else if(m_data[2] == 55)
    {
      lights[2] = N7;
    }else if(m_data[2] == 56)
    {
      lights[2] = N8;
    }else if(m_data[2] == 57)
    {
      lights[2] = N9;
    }

    if(m_data[3] == 48)
    {
      lights[3] = N0;
    }else if(m_data[3] == 49)
    {
      lights[3] = N1;
    }else if(m_data[3] == 50)
    {
      lights[3] = N2;
    }else if(m_data[3] == 51)
    {
      lights[3] = N3;
    }else if(m_data[3] == 52)
    {
      lights[3] = N4;
    }else if(m_data[3] == 53)
    {
      lights[3] = N5;
    }else if(m_data[3] == 54)
    {
      lights[3] = N6;
    }else if(m_data[3] == 55)
    {
      lights[3] = N7;
    }else if(m_data[3] == 56)
    {
      lights[3] = N8;
    }else if(m_data[3] == 57)
    {
      lights[3] = N9;
    }
  }
}


