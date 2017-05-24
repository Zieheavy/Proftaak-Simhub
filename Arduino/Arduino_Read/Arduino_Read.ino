void setup() {
  Serial.begin(9600);


}

void loop(){
  SetOutputPinFromCsharp();
}

void SetOutputPinFromCsharp()
{
  int m_receivedValue;
  char m_data[1];

  if (Serial.available() > 0)
  {
    Serial.readBytes(m_data, 4);
    if (m_data[0] == 65)
    {
        Serial.println("YOU DID IT");
    }
    if(m_data[1] != "" && m_data[2] != "" && m_data[3] != "")
    {
//      int m_speed1 = ((m_data[1]-48)*-1);
//      int m_speed2 = ((m_data[2]-48)*-1);
//      int m_speed3 = ((m_data[3]-48)*-1);
    }
  }
}


