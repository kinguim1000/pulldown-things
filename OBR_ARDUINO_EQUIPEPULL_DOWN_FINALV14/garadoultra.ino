void garradoultra(){
  if(ultra() < 10 && laserF.read() > 29 ){
     motor(15,15);
     delay(700);  
    garra(70, 80);
    flag = 2;
  }












  
}
