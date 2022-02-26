void seguir() {

  atualizar();

  //-- Frente

  if (senE >= white && senC <= black && senD >= white)
  {
    motor(vel, vel);
  }


  //-- Esquerda

  if (senE <= black && senC <= black && senD >= white)
  {
    motor(-vel, vel);
  }

  if (senE <= black && senC >= white && senD >= white)
  {
    motor(-vel, vel);
  }

  //-- Direita

  if (senE >= white && senC <= black && senD <= black)
  {
    motor(vel, -vel);
  }

  if (senE >= white && senC >= white && senD <= black)
  {
    motor(vel, -vel);
  }

  //-- Auxiliares

  if (senE <= black && senC <= black && senD <= black)
  {
    motor(vel, vel);
  }

  if (senE <= black && senC >= white && senD <= black)
  {
    motor(vel, vel);
  }

  //-- Verdes

/*
  if (senEE <= 400 && corVE >= greenE)
  {
    motor(vel, vel);
    delay(200);
    motor(-vel, vel);
    delay(1200);
  }
  if (senDD <= 400 && corVD >= greenD)
  {
    motor(vel, vel);
    delay(200);
    motor(vel, -vel);
    delay(1200);
  }
  if (senEE <= 700 && corVE >= greenE && senDD <= 700 && corVD >= greenD){
    motor(0 , 0);
    delay(2000);
    do {
      
      delay(000);
      atualizar();
    }while(senE >= white && senC <= black && senD >= white);
  */
  }



  
