
#define pinoUltrasonico 12 //pino de sinal

void setup(){//parte do código que rodará uma vez no começo
  Serial.begin(9600);//inicia o console serial do Arduino
}
void loop(){//parte que estará em repetição
  Serial.println(ultra());//print do valor do ultrassom
}
