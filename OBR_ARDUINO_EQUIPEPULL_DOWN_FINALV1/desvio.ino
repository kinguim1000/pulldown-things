void desvio() {
  cont++;
  if (cont > 50) {
    laser();
    cont=0;
  }
}

int laser() {

  if (laserF.read() < distancia) {
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

  motor (-10 , 10);
  delay(2000);
    do {
      motor (-10 , 10);
      atualizar();

    }
    while (senC < black || senE < black);
  }
}





