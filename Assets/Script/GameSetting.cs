using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public static class GameSetting
{
    // standard 100 hp
    // standard 10 per hit
    // standard 10m/s
    public static float maxHealth = 150;
    public static float maxDamage = 15;
    public static float maxSpeed = 15;
    public static Dictionary<int, Dictionary<string, float>> plane_info =
        new Dictionary<int, Dictionary<string, float>>() {
            {
                    0,new Dictionary<string, float>
                    {
                        { "Health",100f},
                        { "Damage",10f},
                        { "Speed",10f},

                    }
            },
            {
                    1,new Dictionary<string, float>
                    {
                        { "Health",150f},
                        { "Damage",11f},
                        { "Speed",6f},

                    }
            },
            {
             2, new Dictionary<string, float>
             {
                 { "Health",100f},
                 { "Damage",8f},
                 { "Speed",11f},

             }
        },{
         3, new Dictionary<string, float>
         {
             { "Health",70f},
             { "Damage",15f},
             { "Speed",15f},

         } 
    }
};

    //===========================================================================
    public static int currentplayerplane=0; 
}
