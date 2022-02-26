void procurando(){
  do{
  motor(18, 15);
  atualizar();
  }while(laserF.read() > 100 && senEE >= white && senE >= white && senC >= white && senD >= white && senDD >= white);


  do {
    motor(-20 , 15);
  } while (laserF.read() < 190);
  
if ((senEE <= black && senE <= black && senC <= black) ||( senD <= black && senDD <= black)){
  motor(5, 5);
  delay(500);
  motor(0,0);
  garra(10, 150);
  delay(2000);
  motor(-15, -15);
  delay(5000);
  motor(0,0);
  delay(2000);
  flag = 1;
}
}
