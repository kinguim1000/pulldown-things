void procurando(){
  do{
  motor(18, 15);
  atualizar();
  }while(laserF.read() > 100 && senEE >= white && senE >= white && senC >= white && senD >= white && senDD >= white);


  if (laserF.read() < 100){
    motor(-15 , 15);
    delay(1900);
  }
if ((senEE <= black && senE <= black && senC <= black) ||( senD <= black && senDD <= black)){
  motor(5, 5);
  delay(500);
  motor(0,0);
  garra(10, 150);
  delay(2000);
  motor(-15, -15);
  delay(10000);
  motor(0,0);
  delay(2000);
  
}
}
