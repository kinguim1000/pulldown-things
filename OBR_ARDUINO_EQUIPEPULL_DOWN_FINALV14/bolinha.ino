void bolinha () {
  do {
    motor(10 , 10);
    if (laserF.read() < 50) {
      motor(0, 0);
      delay(2000);
    motor(-15 , 15);
    delay(1900);
    motor (0 , 0);
    delay(2000);
    motor(5 , 6);
    delay(1800);
    }
  } while ( laserE.read() > numerodl);


  if (laserE.read() < numerodl) {

    movimentos();

  }


//  else {
    
//  }



}

void movimentos() {
  motor(15, 16);
  delay(400);
  motor(-15, 15);
  delay (1900);
  motor(-15, -15);
  delay(1500);

  do {
    motor(6, 7);
    //atualizar();
  } while (ultra() > 8);

  

    motor(8, 9);
    delay(900);
    motor(0, 0);
    delay(2000);
    garra(70, 80);
    flag = 2;

  }
  
