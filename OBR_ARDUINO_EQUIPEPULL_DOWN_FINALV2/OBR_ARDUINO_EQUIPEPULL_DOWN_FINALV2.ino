#include <Wire.h>
#include "SSD1306AsciiWire.h"
//#include <Adafruit_NeoPixel.h>
#include <VL53L1X.h>

#include <Servo.h>

#define NUMPIXELS 2
#define PIN 2
#define I2C_ADDRESS 0x3C
#define RST_PIN -1

#define ultrasonico 5
Servo servoE;
Servo servoD;
SSD1306AsciiWire lcd;

Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);
VL53L1X laserF;
VL53L1X laserE;

int senDD = 0;
int senD = 0;
int senC = 0;
int senE = 0;
int senEE = 0;

int corVE = 0;
int corVD = 0;
int cont = 0;
int cont1 = 0;
int numerodl = 0;
int vel = 14;
int test = 1;
int flag = 0;
Servo motorA;
Servo motorB;
int black = 700;
int white = 800;
int greenD = 300 ;
int notgreenD = 400;
int greenE = 200 ;
//int notgreenE = 40;
int distancia = 50;
#define corE 6
#define corD 7
int m;
int m2;
void atualizar();
void laserconfig();
long duration, inches, cm;
void setup() {

  pixels.begin();
  Wire.begin();
  Wire.setClock(400000L);
  Serial.begin(9600);
  pinMode(corE, INPUT);
  pinMode(corD, INPUT);

  Wire.begin();
  Wire.setClock(400000L);
  laserconfig();
  garra(10, 150);



  /*  sensor.init();
    sensor.setDistanceMode(VL53L1X::Long);
    sensor.setMeasurementTimingBudget(20000);
    sensor.startContinuous(50);
  */
  lcd.begin(&Adafruit128x64,  0x3C);

  lcd.setFont(System5x7);
  lcd.print("     PULL DOWN");
  delay(4000);

  lcd.clear();

  motor(vel - 5 , vel - 5);

}

void loop() {
  //debuglcd();
  //delay(1000);
  //teste()
  //debuglcd();

  // desvio();

    if (flag == 0) {
      seguir();
      desvio();
  
  
      verde();
      rampa();
    }
    if (flag == 1) {
      bolinha();
  
    }
    if (flag == 2) {
  
      procurando();
    }
  



}


