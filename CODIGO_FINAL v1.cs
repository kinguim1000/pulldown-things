//---Variaveis---
int contr = 0;
int contr1 = 0;
int contr2 = 0;
int contr3 = 0;
int contr4 = 0;
int contr5 = 0;
int contr6 = 0;
int flag = 0;
int flagr1 = 0;
int flag2 = 0;
int limitecurva = 0;
int supergap = 0;
int vel = 200;
int limitegap = 380000;
int tempo1 = 0;
int confirmaencruz = 0;
int curvaanterior = 0;

float inclpadrao = 0;
float inclatual = 0;
float ultraFS = bc.distance (0);
float ultraL = bc.distance (1);
float ultraFI = bc.distance (2);
float s0 = bc.lightness (0);
float s1 = bc.lightness (1);
float s2 = bc.lightness (2);
float s3 = bc.lightness (3);
float s4 = bc.lightness (4);
float smax = 0;
float smin = 0;
float direcao = 0;
float dirmax = 0;
float limitearenamin = 0;
float limitearenamax = 0;

string dirtxt = "X";
string dirtxtant = "X";
string corEsq = bc.returnColor (3);
string corDir = bc.returnColor (1);
string corCentro = bc.returnColor (2);
string corEsq1 = bc.returnColor (4);
string corDir1 = bc.returnColor (0);

//---Funcoes---

Action<int, int> motorr = (esq, dir) => bc.onTF (esq, dir);

Action<int> delayr = (tempo) => bc.wait (tempo);

Action<int, string> printr = (a, b) => bc.printLCD (a, b);

Action<int, int> rotacionarr = (a, b) => bc.onTFRot (a, b);

Action<int> levantarr = (n) => bc.actuatorUp (n);

Action<int> abaixarr = (n) => bc.actuatorDown (n);

Action<int> girarcimar = (n) => bc.turnActuatorUp (n);

Action<int> girarbaixor = (n) => bc.turnActuatorDown (n);

Action sober1 = () => {
    bc.actuatorSpeed (150);
    bc.turnLedOn (0, 0, 255);
    if (bc.angleActuator () < 60) {

        //bc.openActuator ();
        levantarr (20);
        while (bc.angleActuator () > 3) {
            levantarr (1);
            //levantarr (100);

            printr (1, bc.angleActuator ().ToString ());
        }

        //bc.closeActuator ();
        levantarr (100);
    }
    if (bc.angleBucket () < 60) {
        girarbaixor (20);
        while (bc.angleBucket () > 5) {
            girarbaixor (1);
            printr (2, bc.angleBucket ().ToString ());
        }

        girarcimar (100);

    }

    levantarr (100);
    girarcimar (100);
    //bc.openActuator ();
    while (bc.angleActuator () > 310) {
        levantarr (2);
        //levantarr (100);
        printr (1, bc.angleActuator ().ToString ());
    }
    //bc.closeActuator ();
    bc.turnLedOn (255, 0, 0);
    while (bc.angleBucket () > 319) {
        girarcimar (2);
        printr (2, bc.angleBucket ().ToString ());
    }
};

Action sober = () => {
    bc.actuatorSpeed (125);
    /*
        while ((bc.angleActuator () >= 310 || bc.angleActuator () <= 305) || (bc.angleBucket () >= 320 || bc.angleBucket () <= 310)) {
            if (bc.angleBucket () >= 320 || bc.angleBucket () <= 310) {
                bc.turnActuatorDown (1);

            }
            if (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
                bc.actuatorUp (1);
            }

        }
        */

    while (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
        bc.actuatorUp (1);
    }

    while (bc.angleBucket () >= 320 || bc.angleBucket () <= 310) {
        bc.turnActuatorDown (1);
    }

    bc.actuatorSpeed (150);
};
Action descer1 = () => {
    bc.actuatorSpeed (150);
    if (bc.angleBucket () == 0) {
        girarcimar (50);
        printr (3, "to subindo");
    }

    bc.turnLedOn (0, 0, 255);

    while (bc.angleActuator () > 30) {
        abaixarr (1);
        printr (1, bc.angleActuator ().ToString ());
    }
    while (bc.angleBucket () > 30) {
        girarbaixor (1);
        printr (2, bc.angleBucket ().ToString ());
    }

    bc.openActuator ();
    bc.actuatorSpeed (80);
    while (bc.angleActuator () < 9) {
        bc.turnLedOn (0, 255, 0);
        abaixarr (1);
        printr (1, bc.angleActuator ().ToString ());
    }
    bc.closeActuator ();

    while (bc.angleBucket () < 3) {
        bc.turnLedOn (255, 0, 0);
        girarbaixor (1);
        printr (2, bc.angleBucket ().ToString ());
    }
    //     

};

Action descer = () => {
    bc.actuatorSpeed (150);
    /*
    while (bc.angleActuator () > 30 || bc.angleActuator () < 10) {
        bc.actuatorDown (1);
    }

    while (bc.angleBucket () > 30 || bc.angleBucket () < 10) {
        bc.turnActuatorUp (1);
    }
*/
    while ((bc.angleActuator () > 30 || bc.angleActuator () < 10) || (bc.angleBucket () > 30 || bc.angleBucket () < 10)) {
        if (bc.angleBucket () > 30 || bc.angleBucket () < 10) {
            bc.turnActuatorUp (1);

        }
        if (bc.angleActuator () > 30 || bc.angleActuator () < 10) {
            bc.actuatorDown (1);
        }

    }
    bc.actuatorSpeed (150);
};

Func<int, string> corr = (n) => bc.returnColor (n);

Func<int, float> luzr = (n) => bc.lightness (n);

Func<int, float> ultrar = (n) => bc.distance (n);

Func<int, int, int, bool> disr = (m, min, max) => bc.detectDistance (m, min, max);

Func<float, int, int, int, int, float> mapr = (n, in_min, in_max, out_min, out_max) => {
    return (n - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
};

Func<float> inclinacaor = () => {
    return bc.inclination ();
};

Func<float> bussolar = () => {
    return bc.compass ();
};

Func<float, float, float, float, float, float> map = (n, in_min, in_max, out_min, out_max) => {
    return (n - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
};

//Motor
Action<int, int> motorc = (left, right) => bc.onTF (Convert.ToInt32 (left * 20), Convert.ToInt32 (right * 20));
Action<int, int> motor = (left, right) => bc.onTF (Convert.ToInt32 (left * 0.8F), Convert.ToInt32 (right * 0.8F));
Action re = () => {
    bc.onTF (-150, -150);
    bc.wait (100);
};

//Lightness
Func<int, float> lightness = (sensor) => {
    return bc.lightness (sensor);
};

//Ultrassonic
Func<int, float> distance = (sensor) => {
    return bc.distance (sensor);
};

//Ler Cores
Action lercores = () => {
    corEsq = bc.returnColor (3);
    corDir = bc.returnColor (1);
    corCentro = bc.returnColor (2);
    corEsq1 = bc.returnColor (4);
    corDir1 = bc.returnColor (0);
};

//Ler Sensores
Action lersensores = () => {
    ultraFS = distance (0);
    ultraL = distance (1);
    ultraFI = distance (2);
    inclatual = inclinacaor ();
    limitecurva = 0;
    direcao = bc.compass ();
    s0 = bc.lightness (0);
    s1 = bc.lightness (1);
    s2 = bc.lightness (2);
    s3 = bc.lightness (3);
    s4 = bc.lightness (4);
    if (s0 > 60) s0 = 60;
    if (s1 > 60) s1 = 60;
    if (s2 > 60) s2 = 60;
    if (s3 > 60) s3 = 60;
    if (s4 > 60) s4 = 60;
    if (direcao > 80 && direcao < 100) dirtxt = "L";
    if (direcao > 170 && direcao < 190) dirtxt = "S";
    if (direcao > 260 && direcao < 280) dirtxt = "O";
    if (direcao > 350 || direcao < 10) dirtxt = "N";
    s0 = map (s0, smin, smax, 1, 46);
    s1 = map (s1, smin, smax, 1, 46);
    s2 = map (s2, smin, smax, 1, 46);
    s3 = map (s3, smin, smax, 1, 46);
    s4 = map (s4, smin, smax, 1, 46);
    lercores ();
    //Verrificar Sombra
    if ((s0 - smin) < 10) {
        s3 = smax;
        s4 = smax;
    }
    if ((s4 - smin) < 10) {
        s0 = smax;
        s1 = smax;
    }
};

//Delay
Action<int> delay = (tempo) => bc.wait (tempo);

//Print
Action<int, string> print = (linha, texto) => bc.printLCD (linha, texto);

//Garra Obstaculo
Action acertagarraobst = () => {
    bc.actuatorSpeed (60);
    //bc.ActivateAngleActuator ()
    if (bc.angleActuator () < 340 && bc.angleActuator () >= 50) {
        if (bc.angleActuator () <= 340)
            while (bc.angleActuator () <= 340) bc.ChangeAngleActuatorDown ();
        if (bc.angleActuator () > 340) {
            while (bc.angleActuator () >= 340) bc.ChangeAngleActuatorUp ();
        }
    } else {
        while (bc.angleActuator () < 10) bc.ChangeAngleActuatorUp ();
        while (bc.angleActuator () >= 340) bc.ChangeAngleActuatorUp ();
    }
    //bc.DesactivateAngleActuator ()
    bc.actuatorSpeed (0);

    if (bc.angleBucket () >= 0 && bc.angleBucket () < 325) {
        while (bc.angleBucket () >= 0 && bc.angleBucket () < 325) bc.turnActuatorDown (10);
        print (3, "a1");
        while (bc.angleBucket () > 325) bc.turnActuatorDown (10);
    } else {
        while (bc.angleBucket () > 325) bc.turnActuatorUp (10);
    }
    bc.actuatorSpeed (0);
};

Action acertagarraobst1 = () => {
    bc.actuatorSpeed (60);
    //bc.ActivateAngleActuator ()
    if (bc.angleActuator () < 300 && bc.angleActuator () >= 50) {
        if (bc.angleActuator () <= 300)
            while (bc.angleActuator () <= 300) bc.ChangeAngleActuatorDown ();
        if (bc.angleActuator () > 300) {
            while (bc.angleActuator () >= 300) bc.ChangeAngleActuatorUp ();
        }
    } else {
        while (bc.angleActuator () < 15) bc.ChangeAngleActuatorUp ();
        while (bc.angleActuator () >= 300) bc.ChangeAngleActuatorUp ();
    }
    //bc.DesactivateAngleActuator ()
    while (bc.angleBucket () < 50) bc.turnActuatorDown (10);
    while (bc.angleBucket () >= 320) bc.turnActuatorDown (10);
    bc.actuatorSpeed (0);

};

//Calibracao
Action<int> calibrar = (lixo1) => {

    // Definir sMax
    if (s0 > s1 && s0 > s3 && s0 > s4) smax = s0;
    if (s1 > s0 && s1 > s3 && s1 > s4) smax = s1;
    if (s3 > s0 && s3 > s1 && s3 > s4) smax = s3;
    if (s4 > s0 && s4 > s1 && s4 > s3) smax = s4;
    if (smax > 60) smax = 60;

    // Definir sMin
    string s4cor = bc.returnColor (4);
    while (s4cor == "BRANCO") {
        motorc (-50, 50);
        s4cor = bc.returnColor (4);
    }
    string s2cor = bc.returnColor (2);
    while (s2cor == "BRANCO") {
        motorc (50, -50);
        s2cor = bc.returnColor (2);
        s3 = bc.lightness (3);
        s4 = bc.lightness (4);
        if (s3 < smin) smin = s3;
        if (s4 < smin) smin = s4;
    }

    //Definir limite
    s0 = bc.lightness (0);
    s1 = bc.lightness (1);
    s2 = bc.lightness (2);
    s3 = bc.lightness (3);
    s4 = bc.lightness (4);
    dirmax = s1;
    if (s2 > s1 && s2 > s3) dirmax = s2;
    if (s3 > s2 && s3 > s1) dirmax = s3;
    if (s0 > 60) s0 = 60;
    if (s1 > 60) s1 = 60;
    if (s3 > 60) s3 = 60;
    if (s4 > 60) s4 = 60;
    s0 = map (s0, smin, smax, 1, 46);
    s1 = map (s1, smin, smax, 1, 46);
    s3 = map (s3, smin, smax, 1, 46);
    s4 = map (s4, smin, smax, 1, 46);
    inclpadrao = inclinacaor ();
    if (direcao > 80 && direcao < 100) dirtxtant = "L";
    if (direcao > 170 && direcao < 190) dirtxtant = "S";
    if (direcao > 260 && direcao < 280) dirtxtant = "O";
    if (direcao > 350 || direcao < 10) dirtxtant = "N";

};

//Desviar de obstaculo
Action<int> obstaculo = (inutil) => {
    bc.turnLedOn (0, 0, 255);
    float ultraLado = distance (1);
    while (ultraFI < 100) {
        motorc (100, -100);
        ultraFI = distance (2);
    }
    delay (1000);
    ultraLado = distance (1);

    while (ultraLado > 25) {
        motor (150, 150);
        ultraLado = distance (1);
        lercores ();
        print (2, ultraLado.ToString ());
    }
    while (ultraLado > 20 && ultraLado < 50) {
        motorc (-100, 100);
        ultraLado = distance (1);
        lercores ();
        print (2, ultraLado.ToString ());
    }
    lercores ();
    while (corCentro != "PRETO") {

        ultraLado = distance (1);
        while (ultraLado < 15 && corCentro != "PRETO") {
            motor (150, 150);
            lercores ();
            ultraLado = distance (1);
        }
        while (ultraLado > 10 && ultraLado < 40 && corCentro != "PRETO") {
            motorc (-100, 100);
            lercores ();
            ultraLado = distance (1);
        }

        bc.resetTimer ();
        tempo1 = bc.timer ();
        while (ultraLado >= 30 && tempo1 < 50 && corCentro != "PRETO") {
            tempo1 = bc.timer ();
            motor (150, 150);
            ultraLado = distance (1);
            lercores ();
        }
        bc.resetTimer ();
        if (ultraLado >= 30 && corCentro != "PRETO") {
            tempo1 = bc.timer ();
            while (ultraLado >= 30 && tempo1 < 120 && corCentro != "PRETO") {
                tempo1 = bc.timer ();
                motorc (-100, 100);
                lercores ();
                ultraLado = distance (1);
            }
        }
    }
    motor (vel, vel);
    delay (300);
    lercores ();
    while (corCentro != "PRETO") {
        motorc (1000, -1000);
        lercores ();
        if (corDir == "PRETO") corCentro = "PRETO";
    }

};
Action areaderesgate = () => {
    print (2, "Area de Resgate");
    motor (0, 0);
    while (bc.angleBucket () < 50) bc.turnActuatorDown (10);
    while (bc.angleBucket () >= 320) bc.turnActuatorDown (10);
    lersensores ();
    print (3, ultraFS.ToString ());
    //delay (2000);
    if (ultraFS >= 295 && ultraFS <= 315) {
        motor (150, 150);
        delay (3000);
        ultraL = bc.distance (1);
        if (ultraL > 300) {
            motor (-150, -150);
            delay (3200);
            lersensores ();
        } else {
            flag = 2;
        }
    }
};
//---Auxiliar---
print (2, "1");
motor (-150, -150);
delay (200);
print (2, "2");
motor (0, 0);
if (bc.returnRed (1) >= 55 && bc.returnGreen (1) >= 54 && bc.returnBlue (1) >= 54 && bc.returnRed (1) <= 57 && bc.returnGreen (1) <= 57 && bc.returnBlue (1) <= 57) {
    print (2, "if 1");
    if (bc.returnRed (2) >= 55 && bc.returnGreen (2) >= 54 && bc.returnBlue (2) >= 54 && bc.returnRed (2) <= 57 && bc.returnGreen (2) <= 57 && bc.returnBlue (2) <= 57) {
        print (2, "if 2");
        if (bc.returnRed (3) >= 55 && bc.returnGreen (3) >= 54 && bc.returnBlue (3) >= 54 && bc.returnRed (3) <= 57 && bc.returnGreen (3) <= 57 && bc.returnBlue (3) <= 57) {
            print (2, "if 3");
            acertagarraobst1 ();
            motor (0, 0);
            delay (2000);
            motor (150, 150);
            delay (3000);
            flag = 2;
        } else {
            acertagarraobst1 ();
            calibrar (1);
            flag = 0;
        }
    } else {
        acertagarraobst1 ();
        calibrar (1);
        flag = 0;
    }
} else {
    acertagarraobst1 ();
    calibrar (1);
    flag = 0;
    print (2, "3");
}

///---Loop---
while (true) {
    //---entrada da rampa---
    print (2, "4");

    if (flag == 0) {
        //ler sensores
        ultraFS = distance (0);
        ultraL = distance (1);
        ultraFI = distance (2);
        inclatual = inclinacaor ();
        limitecurva = 0;
        s0 = bc.lightness (0);
        s1 = bc.lightness (1);
        s2 = bc.lightness (2);
        s3 = bc.lightness (3);
        s4 = bc.lightness (4);
        if (s0 > 60) s0 = 60;
        if (s1 > 60) s1 = 60;
        if (s2 > 60) s2 = 60;
        if (s3 > 60) s3 = 60;
        if (s4 > 60) s4 = 60;
        s0 = map (s0, smin, smax, 1, 46);
        s1 = map (s1, smin, smax, 1, 46);
        s2 = map (s2, smin, smax, 1, 46);
        s3 = map (s3, smin, smax, 1, 46);
        s4 = map (s4, smin, smax, 1, 46);
        corEsq = bc.returnColor (3);
        corDir = bc.returnColor (1);
        corCentro = bc.returnColor (2);
        corEsq1 = bc.returnColor (4);
        corDir1 = bc.returnColor (0);
        confirmaencruz = 0;
        //Verificar Sombra
        if ((s0 - smin) < 10) {
            s3 = smax;
            s4 = smax;
        }
        if ((s4 - smin) < 10) {
            s0 = smax;
            s1 = smax;
        }

        //supergap
        if (corCentro == "PRETO" || corEsq == "PRETO" || corEsq1 == "PRETO" || corDir == "PRETO" || corDir1 == "PRETO") {
            supergap = 0;
        } else {
            supergap++;
            if (supergap >= limitegap && curvaanterior == 0) {
                bc.turnLedOn (255, 0, 0);
                print (1, "SuperGap Esq ");
                corCentro = bc.returnColor (2);

                while (corCentro != "PRETO" && limitecurva <= 130) {
                    motorc (1000, -1000);
                    lercores ();
                    limitecurva++;
                    delay (1);
                    if (corCentro == "PRETO") supergap = 0;
                    if (corEsq1 == "PRETO") {
                        motor (150, 150);
                        delay (50);
                        while (corCentro != "PRETO") {
                            supergap = 0;
                            corCentro = bc.returnColor (2);
                            motorc (1000, -1000);
                            limitecurva = 0;
                        }
                    }
                    if (corDir1 == "PRETO") {
                        motor (150, 150);
                        delay (50);
                        while (corCentro != "PRETO") {
                            supergap = 0;
                            corCentro = bc.returnColor (2);
                            motorc (-1000, 1000);
                            limitecurva = 0;
                        }
                    }
                }

                while (supergap > 0) {
                    motor (-1000, 1000);
                    delay (limitecurva * 20);
                    limitecurva = 0;
                    corCentro = bc.returnColor (2);
                    if (corCentro == "PRETO") supergap = 0;
                    while (corCentro != "PRETO" && limitecurva <= 130) {
                        motorc (-1000, 1000);
                        lercores ();
                        limitecurva++;
                        delay (1);
                        if (corCentro == "PRETO") supergap = 0;
                        if (corDir1 == "PRETO") {
                            while (corCentro != "PRETO") {
                                supergap = 0;
                                corCentro = bc.returnColor (2);
                                motorc (-1000, 1000);
                                limitecurva = 0;
                            }
                        }
                        if (corEsq1 == "PRETO") {
                            while (corCentro != "PRETO") {
                                supergap = 0;
                                corCentro = bc.returnColor (2);
                                motorc (1000, -1000);
                                limitecurva = 0;
                            }
                        }
                    }
                    if (supergap != 0 && corCentro != "PRETO") {
                        motor (1000, -1000);
                        delay (limitecurva * 20);
                        motor (-150, -150);
                        delay (1500);
                        lercores ();
                        if (corCentro == "PRETO") supergap = 0;
                        limitecurva = 0;
                        while (corCentro != "PRETO" && limitecurva <= 130) {
                            motorc (1000, -1000);
                            print (2, "Supergap Esq fase 2");
                            lercores ();
                            limitecurva++;
                            delay (1);
                            if (corCentro == "PRETO") supergap = 0;
                            if (corEsq1 == "PRETO") {
                                while (corCentro != "PRETO") {
                                    supergap = 0;
                                    print (2, "Supergap Esq fase 2A");
                                    corCentro = bc.returnColor (2);
                                    motorc (1000, -1000);
                                    limitecurva = 0;
                                }
                            }
                            if (corDir1 == "PRETO") {
                                while (corCentro != "PRETO") {
                                    print (2, "Supergap Esq fase 2B");
                                    supergap = 0;
                                    corCentro = bc.returnColor (2);
                                    motorc (-1000, 1000);
                                    limitecurva = 0;
                                }
                            }
                        }
                    }
                }
                limitecurva = 0;
            }
            if (supergap >= limitegap && curvaanterior == 1) {
                bc.turnLedOn (255, 0, 0);
                print (1, "SuperGap Dir ");
                corCentro = bc.returnColor (2);
                while (corCentro != "PRETO" && limitecurva <= 130) {
                    motorc (-1000, 1000);
                    lercores ();
                    limitecurva++;
                    delay (1);
                    if (corCentro == "PRETO") supergap = 0;
                    if (corDir1 == "PRETO") {
                        motor (150, 150);
                        delay (50);
                        while (corCentro != "PRETO") {
                            supergap = 0;
                            corCentro = bc.returnColor (2);
                            motorc (-1000, 1000);
                            limitecurva = 0;
                        }
                    }
                    if (corEsq1 == "PRETO") {
                        motor (150, 150);
                        delay (50);
                        while (corCentro != "PRETO") {
                            supergap = 0;
                            corCentro = bc.returnColor (2);
                            motorc (1000, -1000);
                            limitecurva = 0;
                        }
                    }
                }
                while (supergap > 0) {
                    motor (1000, -1000);
                    delay (limitecurva * 20);
                    limitecurva = 0;
                    lercores ();
                    if (corCentro == "PRETO") supergap = 0;
                    while (corCentro != "PRETO" && limitecurva <= 130) {
                        motorc (1000, -1000);
                        lercores ();
                        limitecurva++;
                        delay (1);
                        if (corCentro == "PRETO") supergap = 0;
                        if (corEsq1 == "PRETO") {
                            while (corCentro != "PRETO") {
                                supergap = 0;
                                corCentro = bc.returnColor (2);
                                motorc (1000, -1000);
                                limitecurva = 0;
                            }
                        }
                        if (corDir1 == "PRETO") {
                            while (corCentro != "PRETO") {
                                supergap = 0;
                                corCentro = bc.returnColor (2);
                                motorc (-1000, 1000);
                                limitecurva = 0;
                            }
                        }
                    }
                    if (supergap != 0 && corCentro != "PRETO") {
                        motor (-1000, 1000);
                        delay (limitecurva * 20);
                        motor (-150, -150);
                        delay (1500);
                        lercores ();
                        limitecurva = 0;
                        while (corCentro != "PRETO" && limitecurva <= 130) {
                            motorc (-1000, 1000);
                            print (2, "Supergap Dir fase 2");
                            lercores ();
                            limitecurva++;
                            delay (1);
                            if (corCentro == "PRETO") supergap = 0;
                            if (corEsq1 == "PRETO") {
                                while (corCentro != "PRETO") {
                                    supergap = 0;
                                    print (2, "Supergap Dir fase 2A");
                                    corCentro = bc.returnColor (2);
                                    motorc (1000, -1000);
                                    limitecurva = 0;
                                }
                            }
                            if (corDir1 == "PRETO") {
                                while (corCentro != "PRETO") {
                                    supergap = 0;
                                    print (2, "Supergap Dir fase 2B");
                                    corCentro = bc.returnColor (2);
                                    motorc (-1000, 1000);
                                    limitecurva = 0;
                                }
                            }
                        }
                    }
                }
                limitecurva = 0;
            }
        }

        //ler sensores
        lersensores ();

        // Execucao do desviar de obstaculo
        if (ultraFI <= 10 && inclatual <= 15) {
            while (ultraFI <= 12) {
                motor (-150, -150);
                lersensores ();
            }
            obstaculo (1);
            motor (0, 0);
        }

        //Casos de 2 ou 1 sensor no preto
        if (corEsq == "PRETO" || corEsq1 == "PRETO" || corDir == "PRETO" || corDir1 == "PRETO") {
            supergap = 0;
            if (corEsq != "VERDE" || corEsq1 != "VERDE" || corDir != "VERDE" || corDir1 != "VERDE" || corCentro != "VERDE") {
                if (corEsq1 == "PRETO" && corDir1 != "PRETO") {
                    bc.turnLedOn (255, 0, 255);
                    motor (60, 60);
                    delay (50);
                    corDir1 = bc.returnColor (0);
                    if (corDir1 != "PRETO") {
                        motor (150, 150);
                        delay (500);
                        corDir = bc.returnColor (1);
                        corCentro = bc.returnColor (2);
                        while (corCentro != "PRETO" && limitecurva <= 235) {
                            motorc (1000, -1000);
                            corDir = bc.returnColor (1);
                            corCentro = bc.returnColor (2);
                            if (corDir == "PRETO") corCentro = "PRETO";
                            limitecurva++;
                            delay (1);
                        }
                        if (limitecurva >= 235) {
                            print (2, "Gap 1");
                            corEsq1 = bc.returnColor (4);
                            while (corEsq1 != "PRETO") {
                                motorc (1000, -1000);
                                corEsq1 = bc.returnColor (4);
                            }
                            motorc (-1000, 1000);
                            delay (3000);
                            motor (150, 150);
                            delay (1300);
                            lercores ();
                            limitecurva = 0;
                            while (corCentro != "PRETO" && corDir != "PRETO" && corDir1 != "PRETO" && limitecurva < 120) {
                                motorc (100, -100);
                                lercores ();
                                limitecurva++;
                                delay (1);
                            }
                            if (limitecurva >= 120) {
                                lercores ();
                                while (corCentro != "PRETO" && corEsq != "PRETO" && corEsq1 != "PRETO") {
                                    motorc (-100, 100);
                                    lercores ();
                                    delay (1);
                                }
                            }
                            limitecurva = 0;
                        }
                    }
                    re ();
                } else if (corDir1 == "PRETO" && corEsq1 != "PRETO") {
                    bc.turnLedOn (255, 0, 255);
                    motor (60, 60);
                    delay (50);
                    corEsq1 = bc.returnColor (4);
                    if (corEsq1 != "PRETO") {
                        motor (150, 150);
                        delay (500);
                        corEsq = bc.returnColor (3);
                        corCentro = bc.returnColor (2);
                        while (corCentro != "PRETO" && limitecurva <= 235) {
                            motorc (-1000, 1000);
                            corCentro = bc.returnColor (2);
                            corEsq = bc.returnColor (3);
                            if (corEsq == "PRETO") corCentro = "PRETO";
                            limitecurva++;
                            delay (1);
                        }
                        if (limitecurva >= 235) {
                            print (2, "Gap 2");
                            corDir1 = bc.returnColor (0);
                            while (corDir1 != "PRETO") {
                                motorc (-1000, 1000);
                                corDir1 = bc.returnColor (0);
                            }
                            motorc (1000, -1000);
                            delay (3000);
                            motor (150, 150);
                            delay (1300);
                            lercores ();
                            limitecurva = 0;
                            while (corCentro != "PRETO" && corEsq != "PRETO" && corEsq1 != "PRETO" && limitecurva < 120) {
                                motorc (-100, 100);
                                lercores ();
                                limitecurva++;
                                delay (1);
                            }
                            if (limitecurva >= 120) {
                                lercores ();
                                while (corCentro != "PRETO" && corDir != "PRETO" && corDir1 != "PRETO") {
                                    motorc (100, -100);
                                    lercores ();
                                    delay (1);
                                }
                            }
                            limitecurva = 0;
                        }
                    }
                    re ();
                }
            }
        }

        //ler sensores
        lersensores ();

        //---Funcoes pra encruzilhada---   
        if (corEsq1 == "VERDE" || corEsq == "VERDE" || corCentro == "VERDE" || corDir == "VERDE" || corDir1 == "VERDE") {

            //ajuste de posicao
            if (corCentro == "VERDE") {
                print (2, "Caso 1");
                lercores ();
                print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                motor (80, 80);
                delay (30);
                lercores ();
                print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                while (corCentro == "VERDE") {
                    motorc (120, -120);
                    lercores ();
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    print (2, "Lado 1");
                }
                limitecurva = 0;
                if (corCentro == "BRANCO") {
                    while (corCentro != "PRETO" && limitecurva < 70) {
                        motorc (-120, 120);
                        lercores ();
                        print (2, "Lado 2");
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        delay (1);
                        limitecurva++;
                    }
                    limitecurva = 0;
                    lercores ();
                    while (corCentro != "PRETO") {
                        motorc (120, -120);
                        lercores ();
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        print (2, "Lado 3");
                    }
                }
            }
            if (corEsq1 == "VERDE") {
                while (corCentro != "PRETO") {
                    motorc (120, -120);
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    lercores ();
                }
            }
            if (corDir1 == "VERDE") {
                while (corCentro != "PRETO") {
                    motorc (-120, 120);
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    lercores ();
                }
            }
            lercores ();
            if (corEsq == "VERDE" && corEsq1 == "PRETO") {
                print (2, "Caso 2");
                while (corCentro != "PRETO") {
                    motorc (120, -120);
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    lercores ();
                }
            }
            lercores ();
            if (corDir == "VERDE" && corDir1 == "PRETO") {
                print (2, "Caso 3");
                lercores ();
                while (corCentro != "PRETO") {
                    motorc (-120, 120);
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    lercores ();
                }
            }
            lercores ();

            //curva 180 graus
            if (corDir == "VERDE" || corEsq == "VERDE") {
                motor (150, 150);
                delay (50);
                lercores ();
                print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                if (corDir == "VERDE" && corEsq == "VERDE") {
                    while (corEsq == "VERDE") {
                        motor (80, 80);
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        lercores ();
                    }
                    if (corEsq == "PRETO") {
                        motorc (-100, 100);
                        delay (1000);
                        lercores ();
                        while (corCentro != "PRETO") {
                            print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                            motorc (-100, 100);
                            lercores ();
                        }
                    }
                }
            }
            lercores ();

            if (corDir == "VERDE") {
                lercores ();
                bc.turnLedOn (0, 255, 0);
                print (2, "Verde Direita");
                print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                while (corDir == "VERDE") {
                    motor (80, 80);
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                }
                limitecurva = 0;
                while (corDir != "PRETO" && limitecurva < 30) {
                    motor (80, 80);
                    delay (1);
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                    limitecurva++;
                }
                lercores ();
                limitecurva = 0;
                print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                if (confirmaencruz == 0) {
                    limitecurva = 0;
                    while (corDir1 != "PRETO" && limitecurva < 100) {
                        motor (120, 120);
                        lercores ();
                        print (2, "Confirmando encruzilhada...");
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        delay (1);
                        if (corDir1 == "PRETO") confirmaencruz = 1;
                        limitecurva++;
                    }
                    if (confirmaencruz == 1) {
                        motor (120, 120);
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        delay (700);
                        motorc (-100, 100);
                        delay (700);
                        lercores ();
                        while (corCentro != "PRETO") {
                            motorc (-120, 120);
                            lercores ();
                        }
                        confirmaencruz = 0;
                    }
                    limitecurva = 0;
                }
                lercores ();
                if (corDir == "PRETO" && confirmaencruz == 1) {
                    motor (120, 120);
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    delay (700);
                    motorc (-100, 100);
                    delay (700);
                    lercores ();
                    while (corCentro != "PRETO") {
                        motorc (-120, 120);
                        lercores ();
                    }
                }
            } else if (corEsq == "VERDE") {
                lercores ();
                bc.turnLedOn (0, 255, 0);
                print (2, "Verde Esquerda");
                print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                while (corEsq == "VERDE") {
                    motor (80, 80);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                }
                limitecurva = 0;
                while (corEsq != "PRETO" && limitecurva < 30) {
                    motor (80, 80);
                    delay (1);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    limitecurva++;
                }
                if (confirmaencruz == 0) {
                    limitecurva = 0;
                    while (corEsq1 != "PRETO" && limitecurva < 100) {
                        motor (120, 120);
                        lercores ();
                        print (2, "Confirmando encruzilhada...");
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        delay (1);
                        if (corEsq1 == "PRETO") confirmaencruz = 1;
                        limitecurva++;
                    }
                    if (confirmaencruz == 1) {
                        motor (120, 120);
                        delay (700);
                        motorc (100, -100);
                        delay (700);
                        lercores ();
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        while (corCentro != "PRETO") {
                            motorc (120, -120);
                            lercores ();
                            print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                        }
                        confirmaencruz = 0;
                    }
                    limitecurva = 0;
                }
                limitecurva = 0;
                lercores ();
                if (corEsq == "PRETO" && confirmaencruz == 1) {
                    motor (120, 120);
                    delay (700);
                    motorc (100, -100);
                    delay (700);
                    lercores ();
                    print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    while (corCentro != "PRETO") {
                        motorc (120, -120);
                        lercores ();
                        print (3, corEsq1 + " || " + corEsq + " || " + corCentro + " || " + corDir + " || " + corDir1);
                    }
                }
            }
        }

        //ler sensores
        lersensores ();

        //verificar mudanca de direcao
        if (dirtxtant != dirtxt) {
            dirtxtant = dirtxt;
            s0 = bc.lightness (0);
            s1 = bc.lightness (1);
            s2 = bc.lightness (2);
            s3 = bc.lightness (3);
            s4 = bc.lightness (4);
            dirmax = s1;
            if (s2 > s1 && s2 > s3) dirmax = s2;
            if (s3 > s2 && s3 > s1) dirmax = s3;
            if (dirmax > 95) {
                limitearenamin = 75;
                limitearenamax = 82;

            } else if (dirmax > 90 && dirmax <= 95) {
                limitearenamin = 63;
                limitearenamax = 70;

            } else {
                limitearenamin = 54;
                limitearenamax = 60;

            }
        }

        //verificar entrada na area de resgate
        s0 = bc.lightness (0);
        s1 = bc.lightness (1);
        s2 = bc.lightness (2);
        s3 = bc.lightness (3);
        s4 = bc.lightness (4);

        if (bc.returnRed (1) >= limitearenamin && bc.returnGreen (1) >= limitearenamin && bc.returnBlue (1) >= limitearenamin && bc.returnRed (1) <= limitearenamax && bc.returnGreen (1) <= limitearenamax && bc.returnBlue (1) <= limitearenamax) {
            if (bc.returnRed (2) >= limitearenamin && bc.returnGreen (2) >= limitearenamin && bc.returnBlue (2) >= limitearenamin && bc.returnRed (2) <= limitearenamax && bc.returnGreen (2) <= limitearenamax && bc.returnBlue (2) <= limitearenamax) {
                if (bc.returnRed (3) >= limitearenamin && bc.returnGreen (3) >= limitearenamin && bc.returnBlue (3) >= limitearenamin && bc.returnRed (3) <= limitearenamax && bc.returnGreen (3) <= limitearenamax && bc.returnBlue (3) <= limitearenamax) {
                    areaderesgate ();
                }
            }
        } else if (s0 > 55 && s0 < 57 && s1 > 55 && s1 < 57 && s2 > 55 && s2 < 57) {
            areaderesgate ();
        } else if (s2 > 55 && s2 < 57 && s3 > 55 && s3 < 57 && s4 > 55 && s4 < 57) {
            areaderesgate ();
        } else if (s0 == s1 && s1 == s2 && ultraFS >= 305 && ultraFS <= 315 && s2 < 100) {
            areaderesgate ();
        } else if (s2 == s3 && s3 == s4 && ultraFS >= 305 && ultraFS <= 315 && s2 < 100) {
            areaderesgate ();
        } else if (s1 == s2 && s2 == s3 && ultraFS >= 305 && ultraFS <= 315 && s2 < 100) {
            areaderesgate ();
        }

        lersensores ();
        //Calcular ajuste dos motores

        if (corEsq == corDir && corEsq != "VERDE") {
            bc.turnLedOn (0, 255, 255);
            if (inclpadrao - inclatual > 10) motor (vel - 30, vel - 30);
            if (inclpadrao - inclatual <= 10) motor (vel - 30, vel - 30);

            if (s2 < 30) supergap = 0;
            if (s1 < 30) supergap = 0;
            if (s3 < 30) supergap = 0;
        } else if (corEsq == "PRETO" || s3 < 20) {
            bc.turnLedOn (0, 255, 255);
            supergap = 0;
            corCentro = bc.returnColor (2);
            if (curvaanterior == 1) {
                motor (150, 150);
                delay (10);
                while (corCentro != "PRETO") {
                    motorc (vel, -vel);
                    corCentro = bc.returnColor (2);
                }
            } else motorc (vel, -vel);
            curvaanterior = 0;
        } else if (corDir == "PRETO" || s1 < 20) {
            bc.turnLedOn (0, 255, 255);
            supergap = 0;
            corCentro = bc.returnColor (2);
            if (curvaanterior == 0) {
                motor (150, 150);
                delay (10);
                while (corCentro != "PRETO") {
                    motorc (-vel, vel);
                    corCentro = bc.returnColor (2);
                }
            } else motorc (-vel, vel);
            curvaanterior = 1;
        }

        //atualizar informacoes no console
        print (1, curvaanterior.ToString () + " || " + supergap.ToString () + " || " + dirtxt + " || " + dirmax.ToString ());
        print (2, vel.ToString () + " || " + inclpadrao.ToString () + " || " + inclatual.ToString ());
        print (3, s0.ToString () + " || " + s1.ToString () + " || " + s2.ToString () + " || " + s3.ToString () + " || " + s4.ToString ());

    }

    //---Saida da Rampa---

    if (flag == 2) { ///////////////////comeco da procura
        print (2, "Flag2");
        motor (3000, 3000);

        if (ultrar (2) < 10 && ultrar (0) > 20) {
            print (2, "frente");
            while (ultrar (2) < 30) {
                motor (-200, -200);

            }
            motor (0, 0);
            descer ();
            while (!bc.hasVictims () && ultrar (0) < 40) {
                motor (200, 200);
            }
            motorr (700, 700);
            delayr (700);
            while ((bc.angleActuator () >= 310 || bc.angleActuator () <= 305) || (bc.angleBucket () >= 320 || bc.angleBucket () <= 310)) {
                if (bc.angleBucket () >= 320 || bc.angleBucket () <= 310) {
                    bc.turnActuatorDown (1);

                }
                if (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
                    bc.actuatorUp (1);
                }

            }

        }

        if (bc.detectDistance (1, 20, 190)) {
            print (2, "lado");
            contr3 = 0;
            while (bc.detectDistance (1, 25, 200) && contr3 < 5000000) {
                motor (-100, -100);
                contr3 = contr3 + 1;
                print (3, contr3.ToString ());
            }
            if (contr3 < 4999999) {
                contr3 = 0;
                while (!bc.detectDistance (1, 25, 200) && contr3 < 5000000) {
                    motor (100, 100);
                    contr3 = contr3 + 1;
                    print (3, contr3.ToString ());
                }
                if (contr3 < 4999999) {
                    motorr (100, 100);
                    delayr (300);
                    motorr (0, 0);
                    rotacionarr (1000, 85);
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, -1);
                    }
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, -1);
                    }
                    if (bc.detectDistance (2, 10, 100)) {
                        rotacionarr (1000, -2);
                    }
                    if (bc.detectDistance (2, 100, 170)) {
                        rotacionarr (1000, -1);
                    }

                    motorr (100, 100);
                    delayr (1300);
                    motorr (-100, -100);
                    delayr (1300);
                    motorr (0, 0);
                    descer ();
                    contr2 = 0;
                    while (ultrar (0) > 60 && !bc.hasVictims ()) {
                        motorr (1000, 1000);

                    }
                    contr2 = 0;

                    motorr (700, 700);
                    delayr (700);

                    while (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
                        bc.actuatorUp (1);

                    }

                    contr6 = 0;
                    while (!bc.touch (0) && contr6 < 40000000) {
                        motorr (-200, -200);
                        contr6++;
                        print (3, contr6.ToString ());
                    }
                    while (bc.angleBucket () >= 320 || bc.angleBucket () <= 310) {
                        bc.turnActuatorDown (1);
                    }
                    motorr (200, 200);
                    delayr (200);
                    rotacionarr (1000, -90);

                }

            }

        }
        if (bc.hasVictims ()) {
            flag = 3;
        }

        if (ultrar (0) < 25) {
            rotacionarr (1000, 30);

        }
        if (corr (0) == "PRETO" || corr (1) == "PRETO" || corr (2) == "PRETO" || corr (3) == "PRETO" || corr (4) == "PRETO") {
            flag = 4;
        }

    }

    if (flag == 3) {
        print (2, "Flag 3");

        printr (3, bc.hasVictims ().ToString ());

        if (ultrar (0) < 70) {
            motorr (120, 130);
        } else {
            motorr (190, 200);
        }

        if (ultrar (0) < 35) {

            rotacionarr (1000, 30);
        }
        if (bc.returnRed (1) >= 55 && bc.returnGreen (1) >= 54 && bc.returnBlue (1) >= 54 && bc.returnRed (1) <= 57 && bc.returnGreen (1) <= 57 && bc.returnBlue (1) <= 57) {
            if (bc.returnRed (2) >= 55 && bc.returnGreen (2) >= 54 && bc.returnBlue (2) >= 54 && bc.returnRed (2) <= 57 && bc.returnGreen (2) <= 57 && bc.returnBlue (2) <= 57) {
                if (bc.returnRed (3) >= 55 && bc.returnGreen (3) >= 54 && bc.returnBlue (3) >= 54 && bc.returnRed (3) <= 57 && bc.returnGreen (3) <= 57 && bc.returnBlue (3) <= 57) {
                    motorr (-100, -100);
                    delay (400);
                    rotacionarr (1000, 30);
                }
            }
        }
        if (!bc.hasVictims ()) {
            flag = 2;
        }
        if (corr (0) == "PRETO" || corr (1) == "PRETO" || corr (2) == "PRETO" || corr (3) == "PRETO" || corr (4) == "PRETO") {

            while (corr (0) == "PRETO" || corr (1) == "PRETO" || corr (2) == "PRETO" || corr (3) == "PRETO" || corr (4) == "PRETO") {
                motorr (-200, -200);

            }

            motorr (200, 200);
            delayr (200);
            motorr (0, 0);
            while (bc.angleActuator () > 30 || bc.angleActuator () < 10) {
                bc.actuatorDown (1);
            }
            while (bc.hasVictims ()) {
                motor (-100, -100);
            }
            motor (-100, -100);
            delayr (500);
            while (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
                bc.actuatorUp (1);
            }

            /*
            descer ();
            motorr (-1000, -1000);
            delayr (700);
            motorr (0, 0);
            sober ();
            */
            flag = 4;

        }
    }
    if (flag == 4) {
        print (2, "Flag4");
        motor (-1000, -1000);
        contr4 = contr4 + 1;
        printr (3, contr4.ToString ());
        if (contr4 > 4000000) {
            flag = 5;
            contr4 = 0;
        }
        if (bc.touch (0)) {
            flag = 5;
        }

        if (bc.detectDistance (1, 20, 190)) {
            print (2, "lado");
            contr3 = 0;
            while (bc.detectDistance (1, 25, 200) && contr3 < 5000000) {
                motor (-100, -100);
                contr3 = contr3 + 1;
                print (3, contr3.ToString ());
            }
            if (contr3 < 4999999) {
                contr3 = 0;
                while (!bc.detectDistance (1, 25, 200) && contr3 < 5000000) {
                    motor (100, 100);
                    contr3 = contr3 + 1;
                    print (3, contr3.ToString ());
                }
                if (contr3 < 4999999) {
                    flag2 = 1;
                    motorr (-200, -200);
                    delayr (200);
                    motorr (0, 0);
                    rotacionarr (1000, 85);
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, -1);
                    }
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, -1);
                    }
                    if (bc.detectDistance (2, 10, 100)) {
                        rotacionarr (1000, -2);
                    }
                    if (bc.detectDistance (2, 100, 170)) {
                        rotacionarr (1000, -1);
                    }
                    motorr (100, 100);
                    delayr (1300);
                    motorr (-100, -100);
                    delayr (1300);
                    motorr (0, 0);
                    descer ();
                    contr2 = 0;
                    while (ultrar (0) > 60 && !bc.hasVictims ()) {
                        motorr (1000, 1000);

                    }
                    contr2 = 0;

                    motorr (700, 700);
                    delayr (700);

                    while (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
                        bc.actuatorUp (1);

                    }

                    contr6 = 0;
                    while (!bc.touch (0) && contr6 < 40000000) {
                        motorr (-200, -200);
                        contr6++;
                        print (3, contr6.ToString ());
                    }
                    while (bc.angleBucket () >= 320 || bc.angleBucket () <= 310) {
                        bc.turnActuatorDown (1);
                    }
                    motorr (200, 200);
                    delayr (200);
                    rotacionarr (1000, -90);

                }

            }

        }
        if (bc.hasVictims ()) {
            flag = 3;
        }

    }



    if(flag == 5){

        print (2, "Flag2");
        motor (3000, 3000);

        if (ultrar (2) < 10 && ultrar (0) > 20) {
            print (2, "frente");
            while (ultrar (2) < 30) {
                motor (-200, -200);

            }
            motor (0, 0);
            descer ();
            while (!bc.hasVictims () && ultrar (0) < 40) {
                motor (200, 200);
            }
            motorr (700, 700);
            delayr (700);
            while ((bc.angleActuator () >= 310 || bc.angleActuator () <= 305) || (bc.angleBucket () >= 320 || bc.angleBucket () <= 310)) {
                if (bc.angleBucket () >= 320 || bc.angleBucket () <= 310) {
                    bc.turnActuatorDown (1);

                }
                if (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
                    bc.actuatorUp (1);
                }

            }

        }

        if (bc.detectDistance (1, 20, 190)) {
            print (2, "lado");
            contr3 = 0;
            while (bc.detectDistance (1, 25, 200) && contr3 < 5000000) {
                motor (-100, -100);
                contr3 = contr3 + 1;
                print (3, contr3.ToString ());
            }
            if (contr3 < 4999999) {
                contr3 = 0;
                while (!bc.detectDistance (1, 25, 200) && contr3 < 5000000) {
                    motor (100, 100);
                    contr3 = contr3 + 1;
                    print (3, contr3.ToString ());
                }
                if (contr3 < 4999999) {
                    motorr (100, 100);
                    delayr (300);
                    motorr (0, 0);
                    rotacionarr (1000, 85);
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, -1);
                    }
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, 1);
                    }
                    while (!bc.detectDistance (2, 10, 170)) {
                        rotacionarr (1000, -1);
                    }
                    if (bc.detectDistance (2, 10, 100)) {
                        rotacionarr (1000, -2);
                    }
                    if (bc.detectDistance (2, 100, 170)) {
                        rotacionarr (1000, -1);
                    }

                    motorr (100, 100);
                    delayr (1300);
                    motorr (-100, -100);
                    delayr (1300);
                    motorr (0, 0);
                    descer ();
                    contr2 = 0;
                    while (ultrar (0) > 60 && !bc.hasVictims ()) {
                        motorr (1000, 1000);

                    }
                    contr2 = 0;

                    motorr (700, 700);
                    delayr (700);

                    while (bc.angleActuator () >= 310 || bc.angleActuator () <= 305) {
                        bc.actuatorUp (1);

                    }

                    contr6 = 0;
                    while (!bc.touch (0) && contr6 < 40000000) {
                        motorr (-200, -200);
                        contr6++;
                        print (3, contr6.ToString ());
                    }
                    while (bc.angleBucket () >= 320 || bc.angleBucket () <= 310) {
                        bc.turnActuatorDown (1);
                    }
                    motorr (200, 200);
                    delayr (200);
                    rotacionarr (1000, -90);

                }

            }

        }
        if (bc.hasVictims ()) {
            flag = 3;
        }

        if (ultrar (0) < 25) {
            rotacionarr (1000, 30);

        }
        if (corr (0) == "PRETO" || corr (1) == "PRETO" || corr (2) == "PRETO" || corr (3) == "PRETO" || corr (4) == "PRETO") {
            rotacionarr (1000, 30);
        }

    }

    if (flag == 9) {

        descer ();
        sober ();
    }
}