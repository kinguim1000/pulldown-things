int ultra() {
  
  pinMode(pinoUltrasonico, OUTPUT);
  digitalWrite(pinoUltrasonico, LOW);
  delayMicroseconds(2);
  digitalWrite(pinoUltrasonico, HIGH);
  delayMicroseconds(5);
  digitalWrite(pinoUltrasonico, LOW);
  pinMode(pinoUltrasonico, INPUT);
  
  long duration = pulseIn(pinoUltrasonico, HIGH);

  long cm = microsecondsToCentimeters(duration);
  delay(100);

  return cm;
}


long microsecondsToCentimeters(long microseconds) {
  return microseconds / 29 / 2;
}
