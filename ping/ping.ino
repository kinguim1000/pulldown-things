
#define ultrasonico 12

void setup(){
  Serial.begin(9600);
}
void loop(){
  Serial.println(ultra());
}
