int ultra() {
  
  pinMode(pinoUltrasonico, OUTPUT);//torna o pino como saida
  digitalWrite(pinoUltrasonico, LOW);
  delayMicroseconds(2);
  digitalWrite(pinoUltrasonico, HIGH);
  delayMicroseconds(5);
  digitalWrite(pinoUltrasonico, LOW);
  pinMode(pinoUltrasonico, INPUT);//torna o pino como entrada
  
  long duration = pulseIn(pinoUltrasonico, HIGH);

  long cm = microsecondsToCentimeters(duration);
  delay(100);

  return cm; //retorna a distância
}


long microsecondsToCentimeters(long microseconds) {
  return microseconds / 29 / 2;
}
