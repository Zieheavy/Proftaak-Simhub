

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
    Serial.readBytes(m_data, 3);
    if (m_data[0] == 65)
    {
        Serial.println("YOU DID IT");
    }
  }
}


