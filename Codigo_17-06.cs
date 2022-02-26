/*
+=============================================================
|Código completo para a modalidade prática da OBR 2021
|Equipe Pull_Down
|Professor Igor Araújo
|Desenvolvedores: Kauã, Lucca, Murilo e Pedro
|Última modificação no dia 17/06/2021   
+=============================================================
*/

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
    int limitegap = Convert.ToInt32 (180000 / vel);
    int curvaanterior = 0;
    int limitecurva = 0;
    int encruzant = 0;
    int curva = 20;
    int confirmaencruz = 0;
    bool sair = true;
    float tempoobst = 0;
    float tempoobst1 = 0;
    float tempogap = bc.timer ();
    float tempogap1 = bc.timer ();
    float temporampa = bc.timer ();
    int posarea = 0;
    int flag = 0;
    bool flagverm = false;
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
    float target1 = 0;
    float posbuss = 0;
    float dist = 0;
    float dist1 = 0;
    float distmax = 0;
    float dist2 = 0;
    string corbola = "DEFINIR";
    int ladocos = 0;
    int locarea = 0;
    int grau = 0;
    bool paramgap = false;

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
            printdelay = bc.timer () + 20;
            int t = bc.timer ();
            int aB = bc.angleBucket ();
            int aA = bc.angleActuator ();
            bc.printLCD (0, s4.ToString () + ":" + corEsq1 + "||" + s3.ToString () + ":" + corEsq + "||" + s2.ToString () + ":" + corCentro + "||" + s1.ToString () + ":" + corDir + "||" + s0.ToString () + ":" + corDir1);
            bc.printLCD (1, "FS:" + ultraFS.ToString () + ",FI:" + ultraFI.ToString () + ",L:" + ultraL.ToString () + "||B:" + direcao.ToString () + ",I:" + inclatual.ToString () + "||T:" + t.ToString () + "||Bal:" + aB.ToString () + ",At:" + aA.ToString ());
        }
        bc.printLCD (2, "Funcao: " + funcatual + ", Subfuncao: " + subfunc + " || M.E.: " + motorA.ToString () + ", M.D.: " + motorB.ToString ());
    };
    Action printarea = () => {
        if (bc.timer () > printdelay) {
            printdelay = bc.timer () + 20;
            int t = bc.timer ();
            int aB = bc.angleBucket ();
            int aA = bc.angleActuator ();
            string corFrente = bc.returnColor (5);
            float s5 = bc.lightness (5);
            bool vitima = bc.hasVictims ();
            bc.printLCD (0, s5.ToString () + ":" + corFrente + "||PA:" + posarea.ToString () + ", LS:" + locsaida.ToString () + ",LE:" + locentr.ToString () + "||HV:" + vitima + "||Dist Max:" + distmax.ToString () + "||Dist:" + dist2.ToString ());
            bc.printLCD (1, "FS:" + ultraFS.ToString () + ",FI:" + ultraFI.ToString () + ",L:" + ultraL.ToString () + "||B:" + direcao.ToString () + ",I:" + inclatual.ToString () + "||T:" + t.ToString () + "||Bal:" + aB.ToString () + ",At:" + aA.ToString ());
        }
        bc.printLCD (2, "Funcao: " + funcatual + ", Subfuncao: " + subfunc + " || M.E.: " + motorA.ToString () + ", M.D.: " + motorB.ToString ());
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
        if ((inclatual > 358 || inclatual < 2) || ultraL > 50) temporampa = bc.timer ();
        if (flag == 0) print ();
        else printarea ();
    };

    //---Delay---
    Action<int> delay = (tempo) => bc.wait (tempo);

    Action<int, bool> gapparam = (param, dirpar) => {
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
                } else if (corDir1 == "PRETO" && dirpar) {
                    motor (vel, vel);
                    delay (50);
                    while (corCentro != "PRETO") {
                        rotacionar (1000, 1);
                        lercores ();
                    }
                    sair = false;
                    tempogap = bc.timer ();
                    tempogap1 = bc.timer ();
                } else if (corEsq1 == "PRETO" && dirpar) {
                    motor (vel, vel);
                    delay (50);
                    while (corCentro != "PRETO") {
                        rotacionar (1000, -1);
                        lercores ();
                    }
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
                gapparam (param, paramgap);
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
            gapparam (param, paramgap); //teste de parametros: 0-fazer nada, 1-calibrar, 2-procurar preto, 3-encontrar area de resgate
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
                gapparam (param, paramgap);
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
            gapparam (param, paramgap); //teste de parametros: 0-fazer nada, 1-calibrar, 2-procurar preto, 3-encontrar area de resgate
        }
        lercores ();
    };

    //---Acerta a Garra---
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

    //---Vira 90 graus para a direita---
    Action dir90 = () => {
        subfunc = "dir90";
        printarea ();
        int target3 = 0;
        if (bc.compass () < 20 || bc.compass () > 340) {
            while (bc.compass () > 5) motor (-1000, 1000);
            target3 = 90;
        }
        if (bc.compass () < 110 && bc.compass () > 70) target3 = 180;
        if (bc.compass () < 200 && bc.compass () > 160) target3 = 270;
        if (bc.compass () < 290 && bc.compass () > 250) target3 = 358;

        while (bc.compass () < target3) motor (-1000, 1000);
    };

    //---Vira 90 graus para a esquerda---
    Action proxesq90 = () => {
        int target3 = 0;
        if (bc.compass () > 0 && bc.compass () <= 90) target3 = 2;
        if (bc.compass () > 90 && bc.compass () <= 180) target3 = 90;
        if (bc.compass () > 180 && bc.compass () <= 270) target3 = 180;
        if (bc.compass () > 270 && bc.compass () <= 360) target3 = 270;

        while (bc.compass () < target3) motor (1000, -1000);
    };

    //---Vira 90 graus para a direita---
    Action proxdir90 = () => {
        int target3 = 0;
        if (bc.compass () >= 0 && bc.compass () < 90) target3 = 90;
        if (bc.compass () >= 90 && bc.compass () < 180) target3 = 180;
        if (bc.compass () >= 180 && bc.compass () < 270) target3 = 270;
        if (bc.compass () >= 270 && bc.compass () < 360) target3 = 358;

        while (bc.compass () < target3) motor (-1000, 1000);
    };

    //---Ajusta o angulo para comecar o resgate---
    Action posinibusca = () => {
        subfunc = "Posinibusca";
        printarea ();
        while ((bc.distance (0) - bc.distance (2)) >= 6.55 || (bc.distance (0) - bc.distance (2)) <= 6.4) motor (-1000, 1000);
        posbolaant = bc.compass (); // zera posicao de ajuste de resgate de bola
    };

    //---Move a vitima para a esquerda---
    Action deslocvit = () => {
        subfunc = "Deslocar vitima";
        printarea ();
        motor (-200, -200);
        delay (400);
        motor (0, 0);
        gapdir (20, 0);
        subfunc = "Abaixando Garra";
        printarea ();
        bc.actuatorSpeed (60);
        while (bc.angleBucket () <= 358) bc.turnActuatorUp (20);
        while (bc.angleBucket () > 350) bc.turnActuatorUp (20);
        while (bc.angleBucket () < 8) bc.turnActuatorUp (20);
        while (bc.angleActuator () <= 360 && bc.angleActuator () > 12) bc.ChangeAngleActuatorDown ();
        while (bc.angleActuator () < 10) bc.ChangeAngleActuatorDown ();
        bc.actuatorSpeed (0);
        delay (200);
        gapesq (30, 0);
        gapdir (10, 0);
        subfunc = "Levantando Garra";
        printarea ();
        bc.actuatorSpeed (60);
        bc.turnActuatorDown (100);
        while (bc.angleActuator () < 358) bc.ChangeAngleActuatorUp ();
        while (bc.angleActuator () > 318) bc.ChangeAngleActuatorUp ();
        while (bc.angleBucket () < 358) bc.turnActuatorDown (20);
        while (bc.angleBucket () > 318) bc.turnActuatorDown (20);
        bc.actuatorSpeed (0);
    };

    //---Move a vitima para a direita---
    Action deslocvitdir = () => {
        subfunc = "Deslocar vitima Direita";
        printarea ();
        motor (-200, -200);
        delay (400);
        motor (0, 0);
        gapesq (20, 0);
        subfunc = "Abaixando Garra";
        printarea ();
        bc.actuatorSpeed (60);
        while (bc.angleBucket () <= 358) bc.turnActuatorUp (20);
        while (bc.angleBucket () > 350) bc.turnActuatorUp (20);
        while (bc.angleBucket () < 8) bc.turnActuatorUp (20);
        while (bc.angleActuator () <= 360 && bc.angleActuator () > 12) bc.ChangeAngleActuatorDown ();
        while (bc.angleActuator () < 10) bc.ChangeAngleActuatorDown ();
        bc.actuatorSpeed (0);
        delay (200);
        gapdir (30, 0);
        gapesq (10, 0);
        subfunc = "Levantando Garra";
        printarea ();
        bc.actuatorSpeed (60);
        bc.turnActuatorDown (100);
        while (bc.angleActuator () < 358) bc.ChangeAngleActuatorUp ();
        while (bc.angleActuator () > 318) bc.ChangeAngleActuatorUp ();
        while (bc.angleBucket () < 358) bc.turnActuatorDown (20);
        while (bc.angleBucket () > 318) bc.turnActuatorDown (20);
        bc.actuatorSpeed (0);
    };

    //---Funcao para ajustar angulo de aproximacao---
    Action ajustevit = () => {
        subfunc = "Ajuste Angulo";
        printarea ();
        motor (-1000, 1000);
        if (dist2 >= 150) delay (Convert.ToInt32 (10000 / dist2));
        if (dist2 < 150) delay (50 + Convert.ToInt32 (10000 / dist2));
    };

    //---Funcao para aproximar ate a vitima---
    Func<bool> aproximavit = () => {
        subfunc = "Aproximar Vitima";
        printarea ();
        dist = bc.distance (2);
        if (dist < 25) { // empurrar a bola pra frente - totozinho
            while (dist > 5 && dist < 25) { //voltar de 250 para 25
                motor (120, 120);
                dist = bc.distance (2);
            }
            delay (200);
            return (false);
        } else {
            float tempoaprox = 0;
            tempoaprox = 2000 + dist * 22;
            abresg = bc.timer ();
            while (dist > 5 && bc.timer () - abresg < tempoaprox) {
                motor (200, 200);
                dist = bc.distance (2);
                while (dist > 5 && dist < 20 && bc.timer () - abresg < tempoaprox) {
                    motor (100, 100);
                    dist = bc.distance (2);
                }
            }
            if (bc.timer () - abresg > tempoaprox) return (false);
            return (true);
        }
    };

    //---Funcao para avancar e resgatar a vitima---
    Action resgatvit = () => {
        subfunc = "Resgatar Vitima";
        printarea ();
        motor (-200, -200);
        delay (800);
        motor (0, 0);
        subfunc = "Abaixando Garra";
        printarea ();
        bc.actuatorSpeed (60);
        while (bc.angleBucket () <= 358) bc.turnActuatorUp (20);
        while (bc.angleBucket () > 350) bc.turnActuatorUp (20);
        while (bc.angleBucket () < 8) bc.turnActuatorUp (20);
        while (bc.angleActuator () <= 360 && bc.angleActuator () > 12) bc.ChangeAngleActuatorDown ();
        while (bc.angleActuator () < 10) bc.ChangeAngleActuatorDown ();
        bc.actuatorSpeed (0);
        delay (200);
        motor (300, 300);
        delay (300);
        abultra = bc.distance (0);
        abresg = bc.timer ();
        while (!bc.hasVictims () && bc.timer () - abresg < 300) {
            motor (300, 300);
        }
        if (bc.hasVictims ()) {
            delay (400);
            motor (150, 150);
            delay (300);
            motor (0, 0);
        }
        subfunc = "Levantando Garra";
        printarea ();
        bc.actuatorSpeed (60);
        bc.turnActuatorDown (100);
        while (bc.angleActuator () < 358) bc.ChangeAngleActuatorUp ();
        while (bc.angleActuator () > 318) bc.ChangeAngleActuatorUp ();
        while (bc.angleBucket () < 358) bc.turnActuatorDown (20);
        while (bc.angleBucket () > 318) bc.turnActuatorDown (20);
        bc.actuatorSpeed (0);
    };

    //---Funcao para o retornar a posicao inicial---
    Action retornapi = () => {
        subfunc = "Retornar a posicao inicial";
        printarea ();
        gapdir (175, 0);
        gapdir (35, 3);
        lercores ();
        abresg = bc.timer ();
        float temporetorno = 0;
        temporetorno = 2000 + ultraFS * 21;
        while ((ultraFS > 30 && ultraFI > 5) && bc.timer () - abresg < temporetorno) {
            motor (200, 200);
            lercores ();
            while (((ultraFS > 30 && ultraFS < 50) || (ultraFI > 5 && ultraFI < 15)) && (bc.timer () - abresg) < temporetorno) {
                motor (vel, vel);
                lercores ();
            }
        }
    };

    //---Funcao para depositar a vitima na area de resgate---
    Action depositvit = () => {
        if (bc.hasVictims ()) {
            subfunc = "Salvar Vitima";
            printarea ();
            float bussola1 = 0;
            int target2 = 0;
            bussola1 = bc.compass ();
            gapesq (20, 0);
            if (posarea == 1) target2 = 315;
            if (posarea == 2) target2 = 45;
            if (posarea == 3) target2 = 135;
            if (posarea == 4) target2 = 225;
            if (bc.compass () >= target2) gapdir (Convert.ToInt32 ((target2 + 360) - bc.compass ()), 0);
            else gapdir (Convert.ToInt32 (target2 - bc.compass ()), 0);
            //ajuste do angulo para salvamento
            dist1 = bc.distance (0);
            if (dist1 < 40) {
                while (dist1 < 40) {
                    dist1 = bc.distance (0);
                    motor (-1000, 1000);
                }
            }
            //descarregar vitima
            bc.actuatorSpeed (60);
            while (bc.angleActuator () < 330) bc.ChangeAngleActuatorDown ();
            bc.actuatorSpeed (0);
            motor (0, 0);
            delay (1000);
            acertagarraobst1 ();
        }
    };

    //---Funcao para o retornar ao angulo inicial---
    Action retornaai = () => {
        subfunc = "Voltar Angulo Inicial";
        printarea ();
        if (!endtarget) {
            if (bc.compass () >= posbolaant) gapdir (Convert.ToInt32 ((posbolaant + 356) - bc.compass ()), 0);
            else gapdir (Convert.ToInt32 ((posbolaant - 4) - bc.compass ()), 0);
        } else posinibusca ();
        posbolaant = bc.compass ();
        printarea ();
    };

    //---Funcao para o ajuste do angulo inicial---
    Action ajusteangin = () => {
        if (bc.compass () < 20) grau = 0;
        if (bc.compass () < 110 && bc.compass () > 70) grau = 90;
        if (bc.compass () < 200 && bc.compass () > 160) grau = 180;
        if (bc.compass () < 290 && bc.compass () > 250) grau = 270;
        if (bc.compass () > 340) grau = 360;
        if (bc.compass () >= grau) gapesq (Convert.ToInt32 (bc.compass () - grau), 0);
        else gapdir (Convert.ToInt32 (grau - bc.compass ()), 0);
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
        /*
        gapdir (25, 1);
        gapesq (25, 1);
        */
        //Definir limite
        inclpadrao = inclinacao ();
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
                    encruzant = 0;
                    bc.turnLedOn (255, 0, 255);
                    motor (60, 60);
                    delay (50);
                    lercores ();
                    if (corDir1 != "PRETO") {
                        motor (150, 150);
                        delay (300);
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
                            paramgap = true;
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
                    encruzant = 0;
                    motor (60, 60);
                    delay (50);
                    lercores ();
                    if (corEsq1 != "PRETO") {
                        motor (150, 150);
                        delay (300);
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
                            paramgap = true;
                            if (corCentro != "PRETO") gapesq (40, 2);
                            if (corCentro != "PRETO") gapdir (40, 2);
                            if (corCentro != "PRETO") gapdir (40, 2);
                            if (corCentro != "PRETO") gapesq (40, 2);
                        }
                    }
                    re ();
                }
                paramgap = false;
            }
        }
    };

    //--- Calcular o ajuste dos motores e acionar para frente
    Action ajustemotor = () => {
        lercores ();
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
        } else if (corEsq == "PRETO" || (s3 <= 35 && corEsq != "VERDE")) {
            subfunc = "Esquerda";
            print ();
            bc.turnLedOn (255, 255, 0);
            tempogap = bc.timer ();
            tempogap1 = bc.timer ();
            lercores ();
            if (encruzant != 1) {
                if (curvaanterior == 1) {
                    motor (vel, vel);
                    delay (15);
                    while (corCentro != "PRETO") {
                        motor (vel * curva, -vel * curva);
                        lercores ();
                    }
                } else motor (vel * curva, -vel * curva);
                curvaanterior = 0;
            }
        } else if (corDir == "PRETO" || (s1 <= 35 && corDir != "VERDE")) {
            subfunc = "Direita";
            print ();
            bc.turnLedOn (0, 255, 255);
            tempogap = bc.timer ();
            tempogap1 = bc.timer ();
            lercores ();
            if (encruzant != 2) {
                if (curvaanterior == 0) {
                    motor (vel, vel);
                    delay (10);
                    while (corCentro != "PRETO") {
                        motor (-vel * curva, vel * curva);
                        lercores ();
                    }
                } else motor (-vel * curva, vel * curva);
                curvaanterior = 1;
            }
        }
        lercores ();
    };

    //---SuperGap---
    Action Supergap = () => {
        //if (corCentro == "PRETO" || corEsq == "PRETO" || corEsq1 == "PRETO" || corDir == "PRETO" || corDir1 == "PRETO") {
        if (s2 < 38 || s1 < 38 || s0 < 38 || s3 < 38 || s4 < 38) {
            tempogap = bc.timer ();
            tempogap1 = bc.timer ();
        } else {
            funcatual = "SUPERGAP";
            subfunc = "";
            print ();
            float tempogapre = bc.timer ();
            paramgap = true; //ativa o modo do gapparam para virar ateh encontrar preto no sensor central se detectar preto no sensor lateral
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
                        //motor (-150, -150);
                        //delay (1500);
                        //}
                        tempogapre = bc.timer ();
                        lercores ();
                        while (corEsq1 != "PRETO" && corEsq != "PRETO" && corCentro != "PRETO" && corDir != "PRETO" && corDir1 != "PRETO" && (bc.timer () - tempogapre < 1000)) {
                            motor (-150, -150);
                            lercores ();
                        }
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
                        tempogapre = bc.timer ();
                        lercores ();
                        while (corEsq1 != "PRETO" && corEsq != "PRETO" && corCentro != "PRETO" && corDir != "PRETO" && corDir1 != "PRETO" && (bc.timer () - tempogapre < 1000)) {
                            motor (-150, -150);
                            lercores ();
                        }
                    }
                }
            }
            lercores ();
            paramgap = false; //desativa o modo do gapparam para virar ateh encontrar preto no sensor central se detectar preto no sensor lateral
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
                        delay (600);
                        lercores ();
                        while (corCentro != "PRETO") {
                            motor (-120 * curva, 120 * curva);
                            lercores ();
                        }
                        confirmaencruz = 0;
                        encruzant = 1;
                    }
                    limitecurva = 0;
                }
                lercores ();
                if (corDir == "PRETO" && confirmaencruz == 1) {
                    motor (150, 150);
                    delay (300);
                    motor (-100 * curva, 100 * curva);
                    delay (600);
                    lercores ();
                    while (corCentro != "PRETO") {
                        motor (-120 * curva, 120 * curva);
                        lercores ();
                    }
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
                        delay (600);
                        lercores ();
                        while (corCentro != "PRETO") {
                            motor (120 * curva, -120 * curva);
                            lercores ();
                        }
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
                    delay (600);
                    lercores ();
                    while (corCentro != "PRETO") {
                        motor (120 * curva, -120 * curva);
                        lercores ();
                    }
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
                        motor (vel, vel);
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

                    int count = 1400;
                    while (corCentro != "PRETO") {

                        ultraLado = distance (1);
                        lercores ();
                        while (ultraLado > 15 && corCentro != "PRETO") {
                            motor (vel - 20, vel - 20);
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

    //---Detecta quando o robo entra na area de resgate---
    Action areaderesgate = () => {
        funcatual = "AREA DE RESGATE";
        subfunc = "Localizar";
        if (bc.timer () > (temporampa + 6000)) flag = 1;
        if (ultraFS > 274 && ultraFS < 278 && (bc.timer () - tempoarea > 2000)) {
            funcatual = "CHECAR AREA RESG";
            subfunc = "";
            printarea ();
            gapdir (20, 0);
            lercores ();
            if (ultraFS > 285 && ultraFS < 295) flag = 1;
            else tempoarea = bc.timer ();
            gapesq (20, 0);

        }
    };

    //---Funcao para o resgate de vitimas---
    Action<bool> contvit = (sb) => {
        if (posarea == 1) target1 = 225; //estah em 45 graus e vai ate 225
        if (posarea == 2) target1 = 315; //estah em 135 graus e vai ate 315
        if (posarea == 3) target1 = 45; //estah em 225 graus e vai ate 45
        if (posarea == 4) target1 = 135; //estah em 315 graus e vai ate 135
        if (posarea == 1) ladocos = 135;
        if (posarea == 2) ladocos = 225;
        if (posarea == 3) ladocos = 315;
        if (posarea == 4) ladocos = 45;
        funcatual = "CONTVIT";
        subfunc = "";
        printarea ();
        distmax = bc.distance (0);
        posbuss = bc.compass ();
        if (posarea >= 3 && posbuss > 180) posbuss = 1;
        while (posbuss <= target1) {
            subfunc = "Inicio";
            printarea ();
            posbuss = bc.compass ();
            if (posarea >= 3 && posbuss > 180) posbuss = 1;
            motor (-1000, 1000);
            dist1 = bc.distance (0);
            dist2 = bc.distance (2);
            //Define a distancia maximo de busca para ignorar as saidas
            if (dist1 > 300) dist1 = distmax;
            else distmax = bc.distance (0);
            if ((dist1 - dist2) > 15) { //Verifica a diferenca entre os sensores frontais
                subfunc = "Confirmar Vitima";
                printarea ();
                motor (vel, vel); //toque pra frente pra confirmar leitura
                delay (30);
                dist1 = bc.distance (0);
                dist2 = bc.distance (2);
                if (dist1 > 300) dist1 = distmax;
                else distmax = bc.distance (0);
                motor (-vel, -vel); // toque pra tras pra voltar posicao
                delay (30);
                if ((dist1 - dist2) > 15) {
                    if ((dist1 - dist2) > 15) { //a distancia precisa ser calibrada para definir bola na parede
                        ajustevit (); //ajuste para ver cor da bola
                    } else {
                        /*
                        subfunc = "Ajuste Vitima";
                        printarea ();
                        motor (-1000, 1000);
                        if (dist2 >= 150) delay (Convert.ToInt32 (10000 / dist2));
                        if (dist2 < 150) delay (50 + Convert.ToInt32 (10000 / dist2));
                        //ataque a bola
                        dist = bc.distance (2);
                        subfunc = "Ataque Vitima";
                        printarea ();
                        while (dist > 10) {
                            motor (200, 200);
                            dist = bc.distance (2);
                            while (dist > 10 && dist < 15) {
                                motor (100, 100);
                                dist = bc.distance (2);
                            }
                        }
                        if (bc.compass () < ladocos || (posarea == 4 && (bc.compass () > 315 || bc.compass () < 45))) gapesq(45,0); //Ou proxesq90
                        else gapdir (45,0);
                        if (bc.compass () < ladocos || (posarea == 4 && (bc.compass () > 315 || bc.compass () < 45))) proxesq90 (); //Ou proxesq90
                        else proxdir90 ();
                        float distbola = bc.distance (1);
                        while (distbola > 20) {
                            motor (vel, vel);
                            distbola = bc.distance (1);
                            while (distbola > 20 && dist < 22) {
                                motor (100, 100);
                                distbola = bc.distance (1);
                            }
                        }
                        if (bc.compass () < ladocos || (posarea == 4 && (bc.compass () > 315 || bc.compass () < 45))) proxdir90 (); //Ou proxesq90
                        else proxesq90 ();
                        dist = bc.distance (2);
                        while (dist > 5) {
                            motor (300, 300);
                            dist = bc.distance (2);
                            while (dist > 5 && dist < 20) {
                                motor (100, 100);
                                dist = bc.distance (2);
                            }
                        }

                        parar(5000);
                        */
                        /*
                        float angant = bc.compass ();
                        float distbola = bc.distance (2);
                        parar (1000);
                        if (bc.compass () < ladocos || (posarea == 4 && (bc.compass () > 315 || bc.compass () < 45))) proxdir90 (); //Ou proxesq90
                        else proxesq90 ();

                        if (bc.compass () < ladocos || (posarea == 4 && (bc.compass () > 315 || bc.compass () < 45))) bc.printLCD (2, "Proxdir");
                        else bc.printLCD (2, "Proxesq");
                        parar (1000);

                        double alpha = angant - bc.compass ();
                        if (alpha < 0) alpha *= -1;
                        double cosalpha = 0;
                        cosalpha = Math.Cos (alpha);
                        double tamcat = cosalpha * distbola * -1;
                        distbola = bc.distance (2);
                        lercores ();
                        bc.printLCD (1, cosalpha.ToString () + "; " + alpha.ToString () + "; " + tamcat.ToString () + "; " + distbola.ToString ());
                        parar (5000);

                        while (ultraFI > distbola - tamcat) {
                            lercores ();
                            motor (100, 100);
                        }
                        if (bc.compass () > ladocos || (posarea == 4 && (bc.compass () > 315 || bc.compass () < 45))) gapesq (90, 0); //Ou proxesq90
                        else gapdir (90, 0);
                        while (dist > 5) {
                            motor (300, 300);
                            dist = bc.distance (2);
                            while (dist > 5 && dist < 20) {
                                motor (100, 100);
                                dist = bc.distance (2);
                            }
                        }
                        */
                    }
                    //verificar se vitima viva ou morta
                    if (aproximavit ()) {
                        corbola = bc.returnColor (5);
                        if (corbola == "PRETO" || corbola == "BRANCO") {
                            posbolaant = bc.compass ();
                            if (corbola == "BRANCO" || !sb) { //resgatar a vitima
                                resgatvit (); //resgatar a vitima
                            } else {
                                deslocvit (); //deslocar a vitima
                            }
                        }
                    } else {
                        posbolaant = bc.compass () - 2;
                    }
                    retornapi (); //retorna posicao inicial
                    depositvit (); //descarrega a vitima na area de resgate
                    if (sb) retornaai (); //voltar posicao inicial
                }
                lercores ();
                while ((bc.distance (0) - bc.distance (2)) > 10) motor (-1000, 1000);
                delay (50);
                lercores ();
            }
            posbuss = bc.compass ();
            if (posarea >= 3 && posbuss > 180) posbuss = 1;
            if (posbuss >= target1) endtarget = true;
        }
    };

    // Procurar area de resgate
    Func<int> procarea = () => {
        funcatual = "PROCAREA";
        subfunc = "";
        printarea ();
        //localiza area de saida
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
        ajusteangin (); //ajusta angulo inicial
        while (locarea == 0) {
            while (bc.distance (0) >= 86) {
                motor (vel + 20, vel + 20);
                if (bc.distance (2) < 6 && bc.distance (0) > 92) { //deslocar vitima para o centro
                    motor (0, 0);
                    deslocvitdir ();
                    ajusteangin ();
                }
            }
            if (bc.distance (2) <= 35) {
                motor (1000, -1000);
                delay (500);
                if (bc.distance (2) < 25) {
                    motor (-1000, 1000);
                    delay (500);
                    if (bc.compass () < 20 || bc.compass () > 340) locarea = 1;
                    if (bc.compass () < 110 && bc.compass () > 70) locarea = 2;
                    if (bc.compass () < 200 && bc.compass () > 160) locarea = 3;
                    if (bc.compass () < 290 && bc.compass () > 250) locarea = 4;
                } else {
                    motor (-1000, 1000);
                    delay (500);
                    motor(vel, vel);
                    delay(200);
                }
                motor (0, 0);
            } else {
                while (bc.distance (0) > 15) motor (vel, vel);
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

    //---Sair da area de resgate
    Action sairarea = () => {
        funcatual = "SAIRAREA";
        subfunc = "";
        printarea ();
        //posarea e locsaida
        if ((posarea - locsaida) == -1 || (posarea - locsaida) == 3) {
            gapesq (135, 0);
            lercores ();
            while (ultraFS > 25) {
                motor (vel, vel);
                lercores ();
            }
            gapesq (90, 0);
            while (corCentro != "VERDE" && corDir != "VERDE" && corEsq != "VERDE" && corCentro != "CIANO" && corDir != "CIANO" && corEsq != "CIANO") {
                motor (vel - 20, vel - 20);
                lercores ();
            }
        }

        if ((posarea - locsaida) == 2 || (posarea - locsaida) == -2) {
            gapesq (90, 0);
            lercores ();
            while (ultraFS > 40) {
                motor (vel, vel);
                lercores ();
            }
            gapesq (45, 0);
            while (corCentro != "VERDE" && corDir != "VERDE" && corEsq != "VERDE" && corCentro != "CIANO" && corDir != "CIANO" && corEsq != "CIANO") {
                motor (vel - 20, vel - 20);
                lercores ();
            }
        }

        if ((posarea - locsaida) == 1 || (posarea - locsaida) == -3) {
            gapesq (45, 0);
            lercores ();
            while (corCentro != "VERDE" && corDir != "VERDE" && corEsq != "VERDE" && corCentro != "CIANO" && corDir != "CIANO" && corEsq != "CIANO") {
                motor (vel - 20, vel - 20);
                lercores ();
            }
        }
        delay (600);
        flag = 0;
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
                obstaculo ();
                Supergap ();
                encruzilhada ();
                seguirlinha ();
                areaderesgate ();
                ajustemotor ();
                lercores ();
                //Detecta a parada final
                if (corCentro == "VERMELHO" && flagverm) {
                    parar (1000);
                    if (corEsq1 == "VERMELHO" && corEsq == "VERMELHO") parar (10000);
                    if (corEsq == "VERMELHO" && corDir == "VERMELHO") parar (10000);
                    if (corDir == "VERMELHO" && corDir1 == "VERMELHO") parar (10000);
                }
            }
        } else if (flag == 1) { //area de resgate
            flagverm = true;
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
            while (!endtarget) contvit (true); //true = apenas vitimas vivas
            bc.turnLedOn (255, 255, 0);
            endtarget = false;
            gapdir (90, 0);
            posinibusca ();
            while (!endtarget) contvit (false);
            sairarea ();
        }
    }
}