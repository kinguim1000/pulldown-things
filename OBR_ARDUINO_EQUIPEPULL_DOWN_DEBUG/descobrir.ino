void descobrir(){
motor(0,0);
 delay(2000);
 
  numerodl = laserE.read();
  motor(15, 15);
  delay(2000);
  motor(0,0);
  delay(2000);
  motor(-15,-15);
  delay(2000);
  motor(0,0);
  delay(2000);
  
  numerodl=numerodl + laserE.read();
  
  numerodl = numerodl/2;
  numerodl = numerodl*0.8;
  
  flag = 1;
  lcd.setCursor(110, 24);
  lcd.print(numerodl);
  lcd.setCursor(80, 24);
  lcd.print(numerodl*0.8);

  
}
