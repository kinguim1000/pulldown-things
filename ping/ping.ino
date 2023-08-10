
#define pinoUltrasonico 12 //pino de sinal

void setup(){
  Serial.begin(9600);
}
void loop(){
  Serial.println(ultra());
}
