void verde(){
 if (senEE <= 400 && corVE >= greenE ){
//   if (senDD <= 400 && corVD >= greenD)
//  {&& corVD <= greenD
//    motor (10 , -10);
//    delay(4000);
//  }if (){
  
    
  
  motor(vel, vel);
    delay(200);
    motor(-vel, vel);
    delay(1200);
  }
 
   if (senDD <= 400 && corVD >= greenD){
    

//  if (senEE <= 400 && corVE >= greenE){ && senEE >= white && corVE <= greenD
//    motor (-10 , 10);
//    delay(4000);
//  } if (){
    motor(vel, vel);
    delay(200);
    motor(vel, -vel);
    delay(1200);
  }
   
















  
}

