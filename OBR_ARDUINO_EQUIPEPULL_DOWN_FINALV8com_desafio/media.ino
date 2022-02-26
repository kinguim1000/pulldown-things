void media () {
  m2 = 0;
  m = 0;

  for (int i = 0; i < 20; i++) {
    m += laserE.read();
  }
  m2 = m / 20;
  m = 0;

}
