void desvio() {
  cont++;
  if (cont > 50) {
    laserp();
    cont=0;
  }
}

int laserg() {

  if (laserF.read() < distancia) {
    motor(0,0);
    delay(500);
    
    motor (-15 , -15);
    delay(500);

    motor (-15 , 15);
    delay(1900);

    motor(15, 15);
    delay(1500);

    motor(15, -15);
    delay(1750);

    motor(15, 15);
    delay(3700);

    motor(15, -15);
    delay(1750);

    do {

      motor(10, 10);
      atualizar();

    } while (senC > white || senE > white);
    motor (10 , 10);
    delay(500);

  motor (-13 , 10);
  delay(2000);
    do {
      motor (-13 , 10);
      atualizar();

    }
    while (senC < black || senE < black);
  }
}

int laserp() {

  if (laserF.read() < distancia) {
    motor(0,0);
    delay(500);
    
    motor (-15 , -15);
    delay(500);

    motor (-15 , 15);
    delay(1900);

    motor(15, 15);
    delay(1500);

    motor(15, -15);
    delay(1750);

    motor(15, 15);
    delay(2700);

    motor(15, -15);
    delay(1750);

    do {

      motor(10, 10);
      atualizar();

    } while (senC > white || senE > white);
    motor (10 , 10);
    delay(500);

  motor (-13 , 10);
  delay(2000);
    do {
      motor (-13 , 10);
      atualizar();

    }
    while (senC < black || senE < black);
  }
}
