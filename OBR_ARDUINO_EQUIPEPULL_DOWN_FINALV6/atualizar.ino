void atualizar() {

  senDD = analogRead (A6);
  senD = analogRead (A3);
  senC = analogRead (A2);
  senE = analogRead (A1);
  senEE = analogRead(A0);

  corVE = pulseIn (corE, LOW);
  corVD = pulseIn (corD, LOW);

}
