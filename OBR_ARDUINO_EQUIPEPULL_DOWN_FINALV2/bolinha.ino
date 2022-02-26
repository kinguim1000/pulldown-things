void bolinha () {
  do {
    motor(10 , 10);
  } while (laserF.read() > 50 && laserE.read() > numerodl);
  if (laserF.read() < 50) {
    motor(-15 , 15);
    delay(1900);
  }
  if (laserE.read() < numerodl) {

    movimentos();
    flag = 2;

  }



}

void movimentos() {
  motor(15, 16);
  delay(300);
  motor(-15, 15);
  delay (1900);
  motor(-15,-15);
  delay(1000);
  
  do {
    motor(6, 7);
    //atualizar();
  } while (ultra() > 8);


  
  motor(8,9);
  delay(600);
  motor(0, 0);
  delay(2000);
//laserF.read() < 50 &&
  garra(70, 80);
}

