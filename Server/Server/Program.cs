using System;
using System.Threading;
using System.Collections.Generic;
using Server;

class ServerProgram {
    public static void Main()
    {
        BackBone mainBackbone = new BackBone();
        mainBackbone.NumberOfClientsYouNeedToConnect = 2;
        mainBackbone.PlayGame();
    }

}