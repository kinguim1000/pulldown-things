void garra(int a,int b) {


  servoE.attach(8);
  servoD.attach(9);
  servoE.write(a);
  servoD.write(b);
  
}
