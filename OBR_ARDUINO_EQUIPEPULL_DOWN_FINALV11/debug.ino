void debug() {

  atualizar();

  Serial.print(corVE);
  Serial.print(" | ");
  Serial.print(senEE);
  Serial.print(" | ");
  Serial.print(senE);
  Serial.print(" | ");
  Serial.print(senC);
  Serial.print(" | ");
  Serial.print(senD);
  Serial.print(" | ");
  Serial.print(senDD);
  Serial.print(" | ");
  Serial.println(corVD);

}
void debuglcd () {
  motor(0, 0);
  atualizar();

  lcd.setCursor(0, 10);
  lcd.print(analogRead(0));

  lcd.setCursor(30, 10);
  lcd.print(analogRead(1));

  lcd.setCursor(57, 10);
  lcd.print(analogRead(2));

  lcd.setCursor(85, 10);
  lcd.print(analogRead(3));


  lcd.setCursor(110, 10);
  lcd.print(analogRead(6));

  lcd.println(" \n ");

  lcd.setCursor(0, 24);
  lcd.print(corVE);

  lcd.setCursor(110, 24);
  lcd.print(corVD);

  lcd.println("\n");
  lcd.setCursor(40, 24);
  lcd.print(laserE.read());
  lcd.setCursor(80, 24);
  lcd.print(laserF.read());

  lcd.println("\n");
  lcd.setCursor(60, 40);
  lcd.print(ultra());
  lcd.clear();

}
