void Main () {
    /*
    +=============================================================
    | D E C L A R A C A O   D E  V A R I A V E I S   
    +=============================================================
    */
    /// Hello Guys!
    float s0 = bc.lightness (0);
    float s1 = bc.lightness (1);
    float s2 = bc.lightness (2);
    float s3 = bc.lightness (3);
    float s4 = bc.lightness (4);
    string corEsq = bc.returnColor (3);
    string corDir = bc.returnColor (1);
    string corCentro = bc.returnColor (2);
    string corEsq1 = bc.returnColor (4);
    string corDir1 = bc.returnColor (0);
    float smax = 0;
    float smin = 0;
    float direcao = 0;
    float dirmax = 0;
    int vel = 160;
    float inclpadrao = 0;
    float inclatual = 0;
    float ultraFS = bc.distance (0);
    float ultraL = bc.distance (1);
    float ultraFI = bc.distance (2);
    string dirtxt = "X";
    string dirtxtant = "X";
    int limitegap = Convert.ToInt32 (180000 / vel);
    int curvaanterior = 0;
    int limitecurva = 0;
    int contencruz = 0;
    int tempoencruz = 400000;
    int encruzant = 0;
    int curva = 20;
    int angulogap = 45;
    int confirmaencruz = 0;
    bool sair = true;
    int tempo1 = 0;
    int tempo2 = 0;
    float tempoobst = 0;
    float tempoobst1 = 0;
    float tempogap = bc.timer ();
    float tempogap1 = bc.timer ();
    float temporampa = bc.timer ();
    int posarea = 0;
    int qtdevit = 0;
    int flag = 0;
    float[, ] localvit = new float[10, 4]; //angulo, dist baixo, dist cima, cor(depois).
    float[, ] localvitcls = new float[10, 4];
    float limitearenamin = 0;
    float limitearenamax = 0;
    string funcatual = "INICIO";
    string subfunc = "INICIO";
    float printdelay = 300;
    int motorA = 0;
    int motorB = 0;
    float abresg = 0;
    float abultra = 0;
    float posbolaant = bc.distance (0);
    float temporedutor = 0;
    float tempoarea = bc.timer ();
    bool endtarget = false;
    int locentr = 0;
    int locsaida = 0;

    /*
    +=============================================================
    | F U N C O E S   A U X I L I A R E S
    +=============================================================
    */

    //---Motor---
    Action<int, int> motor = (left, right) => {
        bc.onTF (Convert.ToInt32 (left), Convert.ToInt32 (right));
        motorA = left;
        motorB = right;
    };
    Action re = () => {
        bc.onTF (-150, -150);
        bc.wait (100);
    };
    Action<int> parar = (time) => {
        bc.onTF (0, 0);
        bc.wait (time);
    };

    //---Print---
    Action print = () => {
        if (bc.timer () > printdelay) {
            printdelay = bc.timer () + 50;
            //-----------------------------------------------------------------------------------------------------------1---2---3---4---5---6---7---8---9
            int t = bc.timer ();
            int aB = bc.angleBucket ();
            int aA = bc.angleActuator ();

            bc.printLCD (1, s4.ToString () + " : " + corEsq1 + " || " + s3.ToString () + " : " + corEsq + " || " + s2.ToString () + " : " + corCentro + " || " + s1.ToString () + " : " + corDir + " || " + s0.ToString () + " : " + corDir1);
            bc.printLCD (2, "FS: " + ultraFS.ToString () + ", FI: " + ultraFI.ToString () + ", L: " + ultraL.ToString () + " || Bus: " + direcao.ToString () + ", Inc: " + inclatual.ToString () + " || Timer: " + t.ToString () + " || Balde: " + aB.ToString () + ", Atuador: " + aA.ToString ());
        }
        bc.printLCD (3, "Funcao: " + funcatual + ", Subfuncao: " + subfunc + " || Motor Esquerdo: " + motorA.ToString () + ", Motor Direito: " + motorB.ToString ());
    };

    //---Map---
    Func<float, float, float, float, float, float> map = (n, in_min, in_max, out_min, out_max) => {
        return (n - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    };

    //---Inclinacao---
    Func<float> inclinacao = () => {
        return bc.inclination ();
    };

    //---Bussola---
    Func<float> bussola = () => {
        return bc.compass ();
    };

    //---Rotacionar---
    Action<int, int> rotacionar = (a, b) => bc.onTFRot (a, b);

    //---Definir sMin---
    Action<int> definirs = (lixo) => {
        s0 = bc.lightness (0);
        s1 = bc.lightness (1);
        s3 = bc.lightness (3);
        s4 = bc.lightness (4);
        if (s3 < smin) smin = s3;
        if (s4 < smin) smin = s4;
        if (s0 > s1 && s0 > s3 && s0 > s4) smax = s0;
        if (s1 > s0 && s1 > s3 && s1 > s4) smax = s1;
        if (s3 > s0 && s3 > s1 && s3 > s4) smax = s3;
        if (s4 > s0 && s4 > s1 && s4 > s3) smax = s4;
        if (smax > 100) smax = 100;
    };

    //Ultrassonic
    Func<int, float> distance = (sensor) => {
        return bc.distance (sensor);
    };

    //---Ler Cores---
    Action lercores = () => {
        corEsq = bc.returnColor (3);
        corDir = bc.returnColor (1);
        corCentro = bc.returnColor (2);
        corEsq1 = bc.returnColor (4);
        corDir1 = bc.returnColor (0);
        s0 = bc.lightness (0);
        s1 = bc.lightness (1);
        s2 = bc.lightness (2);
        s3 = bc.lightness (3);
        s4 = bc.lightness (4);
        ultraFS = distance (0);
        ultraL = distance (1);
        ultraFI = distance (2);
        inclatual = inclinacao ();
        direcao = bc.compass ();
        if (direcao > 80 && direcao < 100) dirtxt = "L";
        if (direcao > 170 && direcao < 190) dirtxt = "S";
        if (direcao > 260 && direcao < 280) dirtxt = "O";
        if (direcao > 350 || direcao < 10) dirtxt = "N";
        if ((inclatual > 358 || inclatual < 2) || ultraL > 50) temporampa = bc.timer ();
    };

    //---Delay---
    Action<int> delay = (tempo) => bc.wait (tempo);

    Action<int> gapparam = (param) => {
        switch (param) {
            case 1:
                definirs (1);
                break;
            case 2:
                lercores ();
                if (corCentro == "PRETO") {
                    sair = false;
                    tempogap = bc.timer ();
                    tempogap1 = bc.timer ();
                }
                break;
            case 3:
                lercores ();
                if (ultraFS - ultraFI > 10) sair = false;
                break;
        }
    };
    //---Gap Esquerda---
    Action<int, int> gapesq = (ang, param) => {
        direcao = bussola ();
        dirmax = bussola ();
        sair = true;
        //Verifica se vai passar pelo angulo 0
        if (ang > direcao) { // situacao que passa no angulo 0
            while (direcao < 358 && sair) {
                rotacionar (1000, -1);
                direcao = bussola ();
                gapparam (param);
            }
            dirmax += 360 - ang;
        } else dirmax -= ang; // situacao que nao passa no angulo 0
        //ajuste para a margem de seguranca(cagaco)
        if (dirmax >= 0 && dirmax <= 2) dirmax = 2;
        if (dirmax >= 358 && dirmax <= 360) dirmax = 358;
        //rotaciona ate o angulo definido
        while (direcao > dirmax && sair) {
            rotacionar (1000, -1);
            direcao = bussola ();
            gapparam (param); //teste de parametros: 0-fazer nada, 1-calibrar, 2-procurar preto, 3-encontrar area de resgate
        }
        lercores ();
    };

    //---Gap Direita---
    Action<int, int> gapdir = (ang, param) => {
        dirmax = bussola ();
        direcao = bussola ();
        sair = true;
        //Verifica se vai passar pelo angulo 0
        if (ang >= (360 - direcao)) { // situacao que passa no angulo 0
            while (direcao > 2 && sair) {
                rotacionar (1000, 1);
                direcao = bussola ();
                gapparam (param);
            }
            dirmax += ang - 360;
        } else dirmax += ang; // situacao que nao passa no angulo 0
        //ajuste para a margem de seguranca(cagaco)
        if (dirmax > 0 && dirmax <= 2) dirmax = 2;
        if (dirmax >= 358 && dirmax < 360) dirmax = 358;
        //rotaciona ate o angulo definido
        while (direcao < dirmax && sair) {
            rotacionar (1000, 1);
            direcao = bussola ();
            gapparam (param); //teste de parametros: 0-fazer nada, 1-calibrar, 2-procurar preto, 3-encontrar area de resgate
        }
        lercores ();
    };

    //---Acerta a Garra---
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
            ////print (3, "a1");
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

    Action<bool> bugresg = (dir) => {
        int mult = 1;
        if (dir) mult = -1; //false vai pra tras
        //motor (-vel * mult, -vel * mult);
        //delay (1500);
        //motor (vel * mult, vel * mult);
        //delay (1500);
    };

    /*
    +=============================================================
    | F U N C O E S   P R I N C I P A I S
    +=============================================================
    */

    //---Calibracao---
    Action calibrar = () => {
        funcatual = "CALIBRAR";
        subfunc = "";
        print ();
        gapdir (25, 1);
        gapesq (25, 1);
        //Definir limite
        inclpadrao = inclinacao ();
        if (direcao > 80 && direcao < 100) dirtxtant = "L";
        if (direcao > 170 && direcao < 190) dirtxtant = "S";
        if (direcao > 260 && direcao < 280) dirtxtant = "O";
        if (direcao > 350 || direcao < 10) dirtxtant = "N";
        acertagarraobst1 ();
    };

    //---Seguir Linha---
    Action seguirlinha = () => {
        if (corEsq == "PRETO" || corEsq1 == "PRETO" || corDir == "PRETO" || corDir1 == "PRETO") {
            funcatual = "SEGUIR LINHA";
            subfunc = "";
            print ();
            tempogap = bc.timer ();
            tempogap1 = bc.timer ();
            if (corEsq != "VERDE" || corEsq1 != "VERDE" || corDir != "VERDE" || corDir1 != "VERDE" || corCentro != "VERDE") {
                if (corEsq1 == "PRETO" && corDir1 != "PRETO") {
                    subfunc = "Gap 1 a";
                    print ();
                    contencruz = 0;
                    encruzant = 0;
                    bc.turnLedOn (255, 0, 255);
                    motor (60, 60);
                    delay (50);
                    lercores ();
                    if (corDir1 != "PRETO") {
                        motor (150, 150);
                        delay (250);
                        gapdir (5, 0);
                        lercores ();
                        gapesq (115, 2);
                        print ();
                        if (corCentro != "PRETO") {
                            subfunc = "Gap 1";
                            print ();
                            gapdir (40, 2);
                            motor (150, 150);
                            delay (1100);
                            lercores ();
                            if (corCentro != "PRETO") gapdir (40, 2);
                            if (corCentro != "PRETO") gapesq (40, 2);
                            if (corCentro != "PRETO") gapesq (40, 2);
                            if (corCentro != "PRETO") gapdir (40, 2);
                        }
                    }
                    re ();
                } else if (corDir1 == "PRETO" && corEsq1 != "PRETO") {
                    bc.turnLedOn (255, 0, 255);
                    subfunc = "Gap 2 a";
                    print ();
                    contencruz = 0;
                    encruzant = 0;
                    motor (60, 60);
                    delay (50);
                    lercores ();
                    if (corEsq1 != "PRETO") {
                        motor (150, 150);
                        delay (250);
                        gapesq (5, 0);
                        lercores ();
                        gapdir (115, 2);
                        if (corCentro != "PRETO") {
                            subfunc = "Gap 2---";
                            print ();
                            gapesq (40, 2);
                            motor (150, 150);
                            delay (1100);
                            lercores ();
                            if (corCentro != "PRETO") gapesq (40, 2);
                            if (corCentro != "PRETO") gapdir (40, 2);
                            if (corCentro != "PRETO") gapdir (40, 2);
                            if (corCentro != "PRETO") gapesq (40, 2);
                        }
                    }
                    re ();
                }
            }
        }
    };

    //--- Calcular o ajuste dos motores e acionar para frente
    Action ajustemotor = () => {
        funcatual = "AJUSTE MOTOR";
        subfunc = "";
        print ();
        int velredutor = 0;
        if (bc.timer () < temporedutor + 2000) velredutor = 50;

        //if (corEsq == corDir && corEsq != "VERDE" && corCentro != "VERDE" && s3 > 15 && s1 > 15) {
        if (corEsq != "PRETO" && corEsq != "VERDE" && corDir != "PRETO" && corDir != "VERDE" && corCentro != "VERDE" && s3 > 20 && s1 > 20) {
            print ();
            bc.turnLedOn (0, 0, 0);
            if (inclatual > 10 && inclatual < 50) {
                motor (vel - 50, vel - 50);
            } else {
                motor (vel - velredutor, vel - velredutor);
            }
        } else if (corEsq == "PRETO" || s3 <= 15) {
            print ();
            bc.turnLedOn (255, 255, 0);
            tempogap = bc.timer ();
            tempogap1 = bc.timer ();
            lercores ();
            if (encruzant != 1) {
                if (curvaanterior == 1) {
                    motor (150, 150);
                    delay (10);
                    while (corCentro != "PRETO") {
                        motor (vel * curva, -vel * curva);
                        lercores ();
                    }
                } else motor (vel * curva, -vel * curva);
                curvaanterior = 0;
            }
        } else if (corDir == "PRETO" || s1 <= 15) {
            print ();
            bc.turnLedOn (0, 255, 255);
            tempogap = bc.timer ();
            tempogap1 = bc.timer ();
            lercores ();
            if (encruzant != 2) {
                if (curvaanterior == 0) {
                    motor (150, 150);
                    delay (10);
                    while (corCentro != "PRETO") {
                        motor (-vel * curva, vel * curva);
                        lercores ();
                    }
                } else motor (-vel * curva, vel * curva);
                curvaanterior = 1;
            }
        }
    };

    //---SuperGap---
    Action Supergap = () => {
        if (corCentro == "PRETO" || corEsq == "PRETO" || corEsq1 == "PRETO" || corDir == "PRETO" || corDir1 == "PRETO") {
            tempogap = bc.timer ();
            tempogap1 = bc.timer ();
        } else {
            funcatual = "SUPERGAP";
            subfunc = "";
            print ();
            tempogap1 = bc.timer ();
            if ((tempogap1 - tempogap) >= limitegap && curvaanterior == 0) {
                bc.turnLedOn (255, 0, 0);
                subfunc = "SuperGap Esq ";
                print ();
                lercores ();
                while ((tempogap1 - tempogap) > 0) {
                    if (corCentro != "PRETO") gapesq (40, 2);
                    if (corCentro != "PRETO") gapdir (40, 2);
                    if (corCentro != "PRETO") gapdir (40, 2);
                    if (corCentro != "PRETO") gapesq (40, 2);
                    if ((tempogap1 - tempogap) > 0) {
                        motor (-150, -150);
                        delay (1500);
                    }
                }
            }
            if ((tempogap1 - tempogap) >= limitegap && curvaanterior == 1) {
                bc.turnLedOn (255, 0, 0);
                subfunc = "SuperGap Dir ";
                print ();
                lercores ();
                while ((tempogap1 - tempogap) > 0) {
                    if (corCentro != "PRETO") gapdir (40, 2);
                    if (corCentro != "PRETO") gapesq (40, 2);
                    if (corCentro != "PRETO") gapesq (40, 2);
                    if (corCentro != "PRETO") gapdir (40, 2);
                    if ((tempogap1 - tempogap) > 0) {
                        float tempogapre = bc.timer ();
                        lercores ();
                        while (corEsq1 != "PRETO" && corEsq != "PRETO" && corCentro != "PRETO" && corDir != "PRETO" && corDir1 != "PRETO" && (bc.timer () - tempogapre < 1000)) {
                            motor (-150, -150);
                            lercores ();
                        }
                    }
                }
            }
            lercores ();
        }
    };

    //---Encruzilhada---
    Action encruzilhada = () => {
        //---Funcoes pra encruzilhada---   
        if (corEsq1 == "VERDE" || corEsq == "VERDE" || corCentro == "VERDE" || corDir == "VERDE" || corDir1 == "VERDE") {
            funcatual = "ENCRUZILHADA";
            subfunc = "";
            print ();
            //ajuste de posicao
            if (corCentro == "VERDE") {
                subfunc = "Caso 1";
                lercores ();
                motor (80, 80);
                delay (30);
                lercores ();
                while (corCentro == "VERDE") {
                    motor (120 * curva, -120 * curva);
                    lercores ();
                    subfunc = "Lado 1";
                }
                limitecurva = 0;
                if (corCentro == "BRANCO") {
                    while (corCentro != "PRETO" && limitecurva < 70) {
                        motor (-120 * curva, 120 * curva);
                        lercores ();
                        subfunc = "Lado 2";
                        delay (1);
                        limitecurva++;
                    }
                    limitecurva = 0;
                    lercores ();
                    while (corCentro != "PRETO") {
                        motor (120 * curva, -120 * curva);
                        lercores ();
                        subfunc = "Lado 3";
                    }
                }
            }
            if (corEsq1 == "VERDE") {
                while (corCentro != "PRETO") {
                    motor (120 * curva, -120 * curva);
                    lercores ();
                }
            }
            if (corDir1 == "VERDE") {
                while (corCentro != "PRETO") {
                    motor (-120 * curva, 120 * curva);
                    lercores ();
                }
            }
            lercores ();
            if (corEsq == "VERDE" && corEsq1 == "PRETO") {
                subfunc = "Caso 2";
                while (corCentro != "PRETO") {
                    motor (120 * curva, -120 * curva);
                    lercores ();
                }
            }
            lercores ();
            if (corDir == "VERDE" && corDir1 == "PRETO") {
                subfunc = "Caso 3";
                lercores ();
                while (corCentro != "PRETO") {
                    motor (-120 * curva, 120 * curva);
                    lercores ();
                }
            }
            lercores ();

            //curva 180 graus
            if (corDir == "VERDE" || corEsq == "VERDE") {
                motor (150, 150);
                delay (30);
                lercores ();
                if (corDir == "VERDE" && corEsq == "VERDE") {
                    while (corEsq == "VERDE") {
                        motor (80, 80);
                        lercores ();
                    }
                    if (corEsq == "PRETO") {
                        motor (-100 * curva, 100 * curva);
                        delay (1000);
                        lercores ();
                        while (corCentro != "PRETO") {
                            motor (-100 * curva, 100 * curva);
                            lercores ();
                        }
                    }
                }
            }
            lercores ();

            if (corDir == "VERDE") {
                lercores ();
                bc.turnLedOn (0, 255, 0);
                subfunc = "Verde Direita";
                while (corDir == "VERDE") {
                    motor (80, 80);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                }
                limitecurva = 0;
                while (corDir != "PRETO" && limitecurva < 30) {
                    motor (80, 80);
                    delay (1);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                    limitecurva++;
                }
                lercores ();
                limitecurva = 0;
                if (confirmaencruz == 0) {
                    limitecurva = 0;
                    while (corDir1 != "PRETO" && limitecurva < 100) {
                        motor (120, 120);
                        lercores ();
                        subfunc = "Confirmando encruzilhada...";
                        delay (1);
                        if (corDir1 == "PRETO") confirmaencruz = 1;
                        limitecurva++;
                    }
                    if (confirmaencruz == 1) {
                        motor (150, 150);
                        delay (300);
                        motor (-100 * curva, 100 * curva);
                        delay (800);
                        lercores ();
                        while (corCentro != "PRETO") {
                            motor (-120 * curva, 120 * curva);
                            lercores ();
                        }
                        confirmaencruz = 0;
                        contencruz = 1;
                        encruzant = 1;
                    }
                    limitecurva = 0;
                }
                lercores ();
                if (corDir == "PRETO" && confirmaencruz == 1) {
                    motor (150, 150);
                    delay (300);
                    motor (-100 * curva, 100 * curva);
                    delay (800);
                    lercores ();
                    while (corCentro != "PRETO") {
                        motor (-120 * curva, 120 * curva);
                        lercores ();
                    }
                    contencruz = 1;
                    encruzant = 1;
                }
            } else if (corEsq == "VERDE") {
                lercores ();
                bc.turnLedOn (0, 255, 0);
                subfunc = "Verde Esquerda";
                while (corEsq == "VERDE") {
                    motor (80, 80);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                }
                limitecurva = 0;
                while (corEsq != "PRETO" && limitecurva < 30) {
                    motor (80, 80);
                    delay (1);
                    lercores ();
                    if (corEsq1 == "PRETO" || corDir1 == "PRETO") confirmaencruz = 1;
                    limitecurva++;
                }
                if (confirmaencruz == 0) {
                    limitecurva = 0;
                    while (corEsq1 != "PRETO" && limitecurva < 100) {
                        motor (120, 120);
                        lercores ();
                        subfunc = "Confirmando encruzilhada...";
                        delay (1);
                        if (corEsq1 == "PRETO") confirmaencruz = 1;
                        limitecurva++;
                    }
                    if (confirmaencruz == 1) {
                        motor (150, 150);
                        delay (300);
                        motor (100 * curva, -100 * curva);
                        delay (800);
                        lercores ();
                        while (corCentro != "PRETO") {
                            motor (120 * curva, -120 * curva);
                            lercores ();
                        }
                        contencruz = 1;
                        confirmaencruz = 0;
                        encruzant = 2;
                    }
                    limitecurva = 0;
                }
                limitecurva = 0;
                lercores ();
                if (corEsq == "PRETO" && confirmaencruz == 1) {
                    motor (150, 150);
                    delay (300);
                    motor (100 * curva, -100 * curva);
                    delay (800);
                    lercores ();
                    while (corCentro != "PRETO") {
                        motor (120 * curva, -120 * curva);
                        lercores ();
                    }
                    contencruz = 1;
                    encruzant = 2;
                }
            }
            lercores ();
            temporedutor = bc.timer ();
        }
    };

    //Desviar de obstaculo
    Action obstaculo = () => {
        if (inclatual <= 10 || inclatual > 357) {
            tempoobst1 = bc.timer ();
            if (ultraFI <= 10) {
                funcatual = "OBSTACULO";
                subfunc = "";
                print ();
                if ((tempoobst1 - tempoobst) > 1000) {
                    while (ultraFI <= 12) {
                        motor (-150, -150);
                        lercores ();
                    }
                    bc.turnLedOn (0, 0, 255);
                    float ultraLado = distance (1);
                    while (ultraFI < 100) {
                        motor (100 * curva, -100 * curva);
                        ultraFI = distance (2);
                    }
                    delay (1000);
                    ultraLado = distance (1);
                    while (ultraLado > 25) {
                        motor (150, 150);
                        ultraLado = distance (1);
                        lercores ();
                    }
                    delay (600);
                    lercores ();
                    int target = 0;
                    if (bc.compass () <= 360 && bc.compass () > 270) target = 358;
                    if (bc.compass () <= 90 && bc.compass () > 0) target = 90;
                    if (bc.compass () <= 180 && bc.compass () > 90) target = 180;
                    if (bc.compass () <= 270 && bc.compass () > 180) target = 270;

                    if (target > bc.compass ()) gapdir (Convert.ToInt32 (target - bc.compass ()), 0);
                    else gapdir (Convert.ToInt32 (target + 360 - bc.compass ()), 0);

                    motor (-200, -200);
                    delay (300);

                    int count = 1150;
                    while (corCentro != "PRETO") {

                        ultraLado = distance (1);
                        lercores ();
                        while (ultraLado > 15 && corCentro != "PRETO") {
                            motor (150, 150);
                            lercores ();
                            ultraLado = distance (1);
                        }
                        if (corCentro != "PRETO") {
                            delay (count);
                            count += 100;
                            target = 0;
                            if (bc.compass () <= 45 || bc.compass () > 315) target = 90;
                            if (bc.compass () <= 135 && bc.compass () > 45) target = 180;
                            if (bc.compass () <= 225 && bc.compass () > 135) target = 270;
                            if (bc.compass () <= 315 && bc.compass () > 225) target = 358;

                            if (target > bc.compass ()) gapdir (Convert.ToInt32 (target - bc.compass ()), 0);
                            else gapdir (Convert.ToInt32 (target + 360 - bc.compass ()), 0);

                            motor (-200, -200);
                            delay (200);
                        }
                    }

                    motor (vel, vel);
                    delay (500);
                    lercores ();
                    while (corCentro != "PRETO") {
                        motor (1000 * curva, -1000 * curva);
                        lercores ();
                        if (corDir == "PRETO") corCentro = "PRETO";
                    }
                    lercores ();
                }
            }
        } else {
            tempoobst = bc.timer ();
            tempoobst1 = bc.timer ();
        }
    };

    /*
    +=============================================================
    | A R E A   D E   R E S G A T E
    +=============================================================
    */
    Action areaderesgate = () => {
        bc.printLCD (2, "Area de Resgate");
        while (bc.angleBucket () < 50) bc.turnActuatorDown (10);
        while (bc.angleBucket () >= 320) bc.turnActuatorDown (10);
        lercores ();
        bc.printLCD (3, ultraFS.ToString ());
        if (ultraFS >= 295 && ultraFS <= 315) {
            motor (150, 150);
            delay (3000);
            ultraL = bc.distance (1);
            if (ultraL > 300) {
                motor (-150, -150);
                delay (3200);
                lercores ();
            } else {
                flag = 1;
            }
        }
    };

    Action dir90 = () => {
        int target = 0;
        if (bc.compass () < 20 || bc.compass () > 340) {
            while (bc.compass () > 5) motor (-1000, 1000);
            target = 90;
        }
        if (bc.compass () < 110 && bc.compass () > 70) target = 180;
        if (bc.compass () < 200 && bc.compass () > 160) target = 270;
        if (bc.compass () < 290 && bc.compass () > 250) target = 358;

        while (bc.compass () < target) motor (-1000, 1000);
    };

    Action posinibusca = () => {
        while ((bc.distance (0) - bc.distance (2)) >= 6.55 || (bc.distance (0) - bc.distance (2)) <= 6.4) motor (-1000, 1000);
        posbolaant = bc.compass (); // zera posicao de ajuste de resgate de bola
    };

    Func<bool> abandonaresgate = () => {
        if (bc.distance (0) != abultra) {
            abultra = bc.distance (0);
            abresg = bc.timer ();
            return true;
        } else if (bc.timer () > abresg + 50 && bc.distance (0) < 20) {
            return false;
        }
        return true;
    };

    Action<bool> contvit = (sb) => {
        float target1 = 0;
        float posbuss = 0;
        float dist = 0;
        float dist1 = 0;
        float distmax = 260;
        float dist2 = 0;
        float temposaida = 0;
        float tempotrajeto = 0;
        float tempochegada = 0;
        float tempoatual = 0;
        string corbola = "DEFINIR";
        qtdevit = 0;

        if (posarea == 1) target1 = 225; //estah em 45 graus e vai ate 225
        if (posarea == 2) target1 = 315; //estah em 135 graus e vai ate 315
        if (posarea == 3) target1 = 45; //estah em 225 graus e vai ate 45
        if (posarea == 4) target1 = 135; //estah em 315 graus e vai ate 135
        bc.printLCD (2, " contvit");

        while (posbuss <= target1) {
            posbuss = bc.compass ();
            motor (-1000, 1000);
            dist1 = bc.distance (0);
            dist2 = bc.distance (2);
            bc.printLCD (1, posarea.ToString ());

            if (posarea == 1) {
                if (bc.compass () > 0 && bc.compass () <= 95) distmax = 245;
                if (bc.compass () > 95 && bc.compass () <= 135) distmax = 260;
                if (bc.compass () > 135 && bc.compass () <= 225) distmax = 166;
            }
            if (posarea == 2) {
                if (bc.compass () > 90 && bc.compass () <= 185) distmax = 245;
                if (bc.compass () > 185 && bc.compass () <= 225) distmax = 260;
                if (bc.compass () > 225 && bc.compass () <= 315) distmax = 166;
            }
            if (posarea == 3) {
                if (bc.compass () > 180 && bc.compass () <= 275) distmax = 245;
                if (bc.compass () > 275 && bc.compass () <= 315) distmax = 260;
                if (bc.compass () > 315 && bc.compass () <= 360 || bc.compass () > 0 && bc.compass () <= 45) distmax = 166;
            }
            if (posarea == 4) {
                if (bc.compass () > 270 && bc.compass () <= 360 || bc.compass () > 0 && bc.compass () <= 5) distmax = 245;
                if (bc.compass () > 5 && bc.compass () <= 45) distmax = 260;
                if (bc.compass () > 45 && bc.compass () <= 135) distmax = 166;
            }

            if (dist1 > distmax) dist1 = distmax;

            if ((dist1 - dist2) > 15) {
                motor (300, 300); //toque pra frente pra confirmar leitura
                delay (30);
                dist1 = bc.distance (0);
                dist2 = bc.distance (2);
                motor (-300, -300); // toque pra tras pra voltar posicao
                delay (30);

                if (dist1 > 300) dist1 = 300;

                if ((dist1 - dist2) > 15) {
                    //ajuste para ver cor da bola
                    motor (-1000, 1000);
                    if (dist2 >= 150) delay (Convert.ToInt32 (10000 / dist2));
                    if (dist2 < 150) delay (50 + Convert.ToInt32 (10000 / dist2));
                    temposaida = bc.timer ();

                    //ataque a bola
                    dist = bc.distance (2);
                    while (dist > 5) {
                        motor (300, 300);
                        dist = bc.distance (2);
                        while (dist > 5 && dist < 20) {
                            motor (100, 100);
                            dist = bc.distance (2);
                        }
                        bc.printLCD (2, " Caso 1" + dist.ToString () + " || " + dist1.ToString () + " || " + dist2.ToString ());
                    }
                    tempotrajeto = bc.timer () - temposaida;

                    //verificar se vitima viva ou morta
                    corbola = bc.returnColor (5);

                    if (corbola == "PRETO" || corbola == "BRANCO") {
                        posbolaant = bc.compass ();
                        if (corbola == "BRANCO" || !sb) { //resgatar a vitima

                            //reh
                            motor (-200, -200);
                            delay (800);
                            motor (0, 0);

                            //abaixar escavadeira
                            bc.actuatorSpeed (60);
                            while (bc.angleBucket () <= 358) bc.turnActuatorUp (20);
                            while (bc.angleBucket () > 350) bc.turnActuatorUp (20);
                            while (bc.angleBucket () < 8) bc.turnActuatorUp (20);
                            while (bc.angleActuator () <= 360 && bc.angleActuator () > 12) bc.ChangeAngleActuatorDown ();
                            while (bc.angleActuator () < 10) bc.ChangeAngleActuatorDown ();
                            bc.actuatorSpeed (0);
                            delay (200);
                            bc.printLCD (2, corbola);
                            motor (300, 300);
                            delay (300);
                            while (!bc.hasVictims () && abandonaresgate ()) {
                                motor (300, 300);
                                abandonaresgate ();
                            }
                            if (bc.hasVictims ()) {
                                delay (400);
                                motor (150, 150);
                                delay (300);
                                motor (0, 0);
                            }

                            //levantar escavadeira
                            bc.actuatorSpeed (60);
                            bc.turnActuatorDown (100);
                            while (bc.angleActuator () < 358) bc.ChangeAngleActuatorUp ();
                            while (bc.angleActuator () > 318) bc.ChangeAngleActuatorUp ();
                            while (bc.angleBucket () < 358) bc.turnActuatorDown (20);
                            while (bc.angleBucket () > 318) bc.turnActuatorDown (20);
                            bc.actuatorSpeed (0);
                        }
                        bc.printLCD (2, corbola);
                        qtdevit++;
                        localvit[qtdevit, 0] = posbuss;
                        localvit[qtdevit, 1] = dist2;
                        localvit[qtdevit, 2] = dist1;
                    }

                    //retorna posicao inicial

                    //tempochegada = bc.timer ();
                    //tempoatual = bc.timer ();
                    gapdir (175, 0);
                    gapdir (35, 3);
                    lercores ();
                    //dist1 = bc.distance (0);
                    //dist2 = bc.distance (2);
                    while (ultraFS > 50 && ultraFI > 20) {
                        motor (300, 300);
                        lercores ();
                        while ((ultraFS > 40 && ultraFS < 60) && (ultraFI > 15 && ultraFI < 35)) {
                            motor (200, 200);
                            lercores ();
                        }
                    }
                    if (bc.hasVictims ()) {
                        float bussola1 = 0;
                        bussola1 = bc.compass ();
                        gapesq (20, 0);
                        if (posarea == 1) target1 = 315;
                        if (posarea == 2) target1 = 45;
                        if (posarea == 3) target1 = 135;
                        if (posarea == 4) target1 = 225;

                        if (bc.compass () >= target1) gapdir (Convert.ToInt32 ((target1 + 360) - bc.compass ()), 0);
                        else gapdir (Convert.ToInt32 (target1 - bc.compass ()), 0);

                        //ajuste do angulo para salvamento

                        dist1 = bc.distance (0);
                        if (dist1 < 40) {
                            while (dist1 < 40) {
                                dist1 = bc.distance (0);
                                motor (-1000, 1000);
                            }
                        }

                        //descarregar vitima

                        bugresg (false); //ir para tras e frente erro sistema

                        bc.actuatorSpeed (60);
                        while (bc.angleActuator () < 330) bc.ChangeAngleActuatorDown ();
                        bc.actuatorSpeed (0);
                        motor (0, 0);
                        delay (1000);
                        acertagarraobst1 ();
                    }

                    //voltar posicao inicial
                    if (bc.compass () >= posbolaant) gapdir (Convert.ToInt32 ((posbolaant + 365) - bc.compass ()), 0);
                    else gapdir (Convert.ToInt32 ((posbolaant + 5) - bc.compass ()), 0);
                    posbolaant = bc.compass ();
                    bugresg (true);
                }

                bugresg (false); //ir para tras e frente erro sistema

                lercores ();
                bc.printLCD (3, dist1.ToString () + " || " + dist2.ToString () + " || " + qtdevit.ToString ());
                while ((bc.distance (0) - bc.distance (2)) > 10) motor (-1000, 1000);
                delay (100);
                lercores ();
                bc.printLCD (3, dist1.ToString () + " || " + dist2.ToString () + " || " + qtdevit.ToString ());
            }
            if (posarea == 3 && posbuss > 180) posbuss = 1;
            if (posarea == 4 && posbuss > 180) posbuss = 1;
        }
        if (posbuss >= target1) endtarget = true;
    };

    // Procurar area de resgate
    Func<int> procarea = () => {
        int locarea = 0;
        int grau = 0;
        //ajusta angulo inicial
        if (bc.compass () < 20) grau = 0;
        if (bc.compass () < 110 && bc.compass () > 70) grau = 90;
        if (bc.compass () < 200 && bc.compass () > 160) grau = 180;
        if (bc.compass () < 290 && bc.compass () > 250) grau = 270;
        if (bc.compass () > 340) grau = 360;
        if (bc.compass () >= grau) gapesq (Convert.ToInt32 (bc.compass () - grau), 0);
        else gapdir (Convert.ToInt32 (grau - bc.compass ()), 0);
        gapdir (35, 0);
        if (bc.distance (0) > 400) {
            if (bc.compass () < 325 || bc.compass () > 295) locsaida = 1;
            if (bc.compass () < 75 && bc.compass () > 35) locsaida = 2;
            if (bc.compass () < 145 && bc.compass () > 105) locsaida = 3;
            if (bc.compass () < 235 && bc.compass () > 195) locsaida = 4;
            gapesq (35, 0);
        } else if (locsaida == 0) {
            gapdir (55, 0);
            if (bc.distance (0) > 400) {
                if (bc.compass () < 20 || bc.compass () > 340) locsaida = 2;
                if (bc.compass () < 110 && bc.compass () > 70) locsaida = 3;
                if (bc.compass () < 200 && bc.compass () > 160) locsaida = 4;
                if (bc.compass () < 290 && bc.compass () > 250) locsaida = 1;
            }
            gapesq (90, 0);
        }
        if (locsaida == 0) {
            if (bc.compass () < 20 || bc.compass () > 340) locsaida = 1;
            if (bc.compass () < 110 && bc.compass () > 70) locsaida = 2;
            if (bc.compass () < 200 && bc.compass () > 160) locsaida = 3;
            if (bc.compass () < 290 && bc.compass () > 250) locsaida = 4;
        }

        if (bc.compass () < 20 || bc.compass () > 340) locentr = 4;
        if (bc.compass () < 110 && bc.compass () > 70) locentr = 1;
        if (bc.compass () < 200 && bc.compass () > 160) locentr = 2;
        if (bc.compass () < 290 && bc.compass () > 250) locentr = 3;

        while (locarea == 0) {
            while (bc.distance (0) >= 90) motor (vel, vel);
            if (bc.distance (2) <= 35) {
                motor (1000, -1000);
                delay (500);
                float dist = bc.distance (2);
                bc.printLCD (1, dist.ToString ());
                if (bc.distance (2) < 35) {
                    motor (-1000, 1000);
                    delay (500);
                    if (bc.compass () < 20 || bc.compass () > 340) locarea = 1;
                    if (bc.compass () < 110 && bc.compass () > 70) locarea = 2;
                    if (bc.compass () < 200 && bc.compass () > 160) locarea = 3;
                    if (bc.compass () < 290 && bc.compass () > 250) locarea = 4;
                } else {
                    motor (-1000, 1000);
                    delay (500);
                }
                motor (0, 0);
                bc.printLCD (2, "achei");
            } else {
                while (bc.distance (0) > 15) motor (300, 300);
                gapdir (90, 0);
                grau = 0;
                //ajusta angulo 
                if (bc.compass () < 20) grau = 0;
                if (bc.compass () < 110 && bc.compass () > 70) grau = 90;
                if (bc.compass () < 200 && bc.compass () > 160) grau = 180;
                if (bc.compass () < 290 && bc.compass () > 250) grau = 270;
                if (bc.compass () > 340) grau = 360;
                if (bc.compass () >= grau) gapesq (Convert.ToInt32 (bc.compass () - grau), 0);
                else gapdir (Convert.ToInt32 (grau - bc.compass ()), 0);
            }
        }
        return locarea;
    };

    /*
    +=============================================================
    | L O O P
    +=============================================================
    */

    calibrar ();

    while (true) {
        if (flag == 0) {
            while (flag == 0) {
                lercores ();
                print ();
                obstaculo ();
                print ();
                Supergap ();
                print ();
                encruzilhada ();
                print ();
                seguirlinha ();
                print ();
                ajustemotor ();
                print ();
                if (bc.timer () > (temporampa + 6000)) flag = 1;
                if (ultraFS > 274 && ultraFS < 278 && (bc.timer () - tempoarea > 100)) {
                    gapdir (20, 0);
                    lercores ();
                    if (ultraFS > 285 && ultraFS < 295) flag = 1;
                    else tempoarea = bc.timer ();
                    gapesq (20, 0);

                }
            }
        } else if (flag == 1) { //area de resgate
            while (inclatual < 358 && inclatual > 2) {
                seguirlinha ();
                ajustemotor ();
                lercores ();
            }
            motor (vel, vel);
            delay (500);

            if (posarea == 0) posarea = procarea ();
            posinibusca ();
            bc.printLCD (2, posarea.ToString () + " || " + locentr.ToString () + " || " + locsaida.ToString ());

            while (!endtarget) contvit (true);
            bc.turnLedOn (255, 255, 0);
            endtarget = false;
            bugresg (false);
            gapdir (90, 0);
            posinibusca ();
            bugresg (true);
            while (!endtarget) contvit (false);

            motor (0, 0);
            delay (10000);

        }
    }
}

// codigo top do flavin do pineu