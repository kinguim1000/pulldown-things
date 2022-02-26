void laserconfig() {

  pinMode(2, OUTPUT);
  pinMode(3, OUTPUT);
  digitalWrite(2, LOW);
  digitalWrite(3, LOW);

  delay(500);

  pinMode(3, INPUT);
  delay(150);
  laserF.init(true);
  delay(100);
  laserF.setAddress((uint8_t)22);


  pinMode(2, INPUT);
  delay(150);
  laserE.init(true);
  delay(100);
  laserE.setAddress((uint8_t)43);


  laserF.setDistanceMode(VL53L1X::Long);
  laserF.setMeasurementTimingBudget(20000);

  laserE.setDistanceMode(VL53L1X::Long);
  laserE.setMeasurementTimingBudget(20000);

  // Start continuous readings at a rate of one measurement every 50 ms (the
  // inter-measurement period). This period should be at least as long as the
  // timing budget.
  laserF.startContinuous(50);
  laserE.startContinuous(50);

}
