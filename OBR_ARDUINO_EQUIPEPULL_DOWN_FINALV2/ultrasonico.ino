int ultra() {
  
  pinMode(ultrasonico, OUTPUT);
  digitalWrite(ultrasonico, LOW);
  delayMicroseconds(2);
  digitalWrite(ultrasonico, HIGH);
  delayMicroseconds(5);
  digitalWrite(ultrasonico, LOW);
  pinMode(ultrasonico, INPUT);
  
  duration = pulseIn(ultrasonico, HIGH);

  cm = microsecondsToCentimeters(duration);
  delay(100);

  return cm;
}


long microsecondsToCentimeters(long microseconds) {
  return microseconds / 29 / 2;
}
