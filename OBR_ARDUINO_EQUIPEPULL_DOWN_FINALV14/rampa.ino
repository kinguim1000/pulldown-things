void rampa() {
  cont1++;
  if (cont1 > 17000) {
    if (laserE.read() < 100) {
      rampinha();
      
      
    }
    cont1 = 0;
  }
}




void rampinha () {
 do{
  motor(31, 35);
 }while(laserE.read() < 200);
 motor(15,15);
 delay(1000);
 do{
 motor(15,15);
 }while(laserE.read() < 200);
motor(0,0);
 delay(2000);
 
  numerodl = laserE.read();
  motor(15, 15);
  delay(2000);
  motor(0,0);
  motor(-15,-15);
  delay(2000);
  motor(0,0);
  delay(2000);
  
  numerodl=numerodl + laserE.read();
  
  numerodl = numerodl/2;
  numerodl = numerodl*0.5;
  
  flag = 1;
  lcd.setCursor(110, 24);
  lcd.print(numerodl);
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  /*media();
  atualizar();
  //
  if (m2 > 115) {
    do {
      motor(15 , 15);
      atualizar();
    } while (laserF.read() > 80);


    if (m2 < 115 && laserF.read() < 80) {
      motor (0, 0);
      delay(2000);
    }
    //if (m2 < 50){
    //  do{
    //
    //  motor(0 , 0);
    //  atualizar();
    //
    //
    //
    //  }while( laserF.read() < 29);
    //}


  }








  do {
    motor (15, 16);

  } while (laserE.read() > 100);
  motor(0, 0);
  delay(2000);
  int tamanho = laserE.read();

*/
}
