void bolinhav1() {
 
  do {
    motor(10 , 12);
    lcd.clear();
  lcd.println("\n");
  lcd.setCursor(40, 24);
  lcd.print(laserE.read());
  lcd.setCursor(80, 24);
  lcd.print(laserF.read());
  lcd.println("\n");
  lcd.setCursor(60, 40);
  lcd.print(ultra());
  } while (laserF.read() > 60 && laserE.read() > 170 && ultra() > 10);
  if (senDD < black && senD < black && senC < black && senE < black && senEE < black ){
    
      motor(-15 ,15);
    
    delay(2000);
    
    do{
    motor(13 , 13);
    
    }while (senDD < black || senD < black || senC < black || senE < black || senEE < black );
  }
  
  do {
    motor(-20 , 15);
  } while (laserF.read() < 190);
  
  if (laserE.read() < 200) {
  movimentos();
  }
  

  garradoultra();
}
