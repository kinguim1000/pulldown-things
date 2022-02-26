void motor(int a , int b) {

  motorA.attach(10);
  motorB.attach(11);


  byte x = map(a, -100, 100 , 0, 180 );
  motorA.write(x);
  byte y = map(b, -100,100 , 180, 0 );
  motorB.write(y);  

}
