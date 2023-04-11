using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSetting
{
    // standard 100 hp
    // standard 10 per hit
    // standard 10m/s
    public static float maxHealth = 150;
    public static float maxDamage = 2;
    public static float maxSpeed = 350;
    public static Dictionary<int, Dictionary<string, float>> plane_info =
        new Dictionary<int, Dictionary<string, float>>() {
            {
                    0,new Dictionary<string, float>
                    {
                        { "Health",100f},
                        { "Damage",1f},
                        { "Speed",200f},
                        {"armor",100f },
                        { "bulletspeed",900f},
                        { "firedelay",0.08f }

                    }
            },
            {
                    1,new Dictionary<string, float>
                    {
                        { "Health",150f},
                        { "Damage",2f},
                        { "Speed",150f},
                        {"armor",100f },
                        { "bulletspeed",1400f},
                        { "firedelay",0.12f }
                    }
            },
            {
             2, new Dictionary<string, float>
             {
                 { "Health",120f},
                 { "Damage",1f},
                 { "Speed",250f},
                 {"armor",100f },
                 { "bulletspeed",1100f},
                 { "firedelay",0.12f }
             }
        },{
         3, new Dictionary<string, float>
         {
             { "Health",70f},
             { "Damage",2f},
             { "Speed",350f},
             {"armor",100f },
             { "bulletspeed",1700f},
             { "firedelay",0.2f }
         } 
    }
};
    public static Dictionary<int, Dictionary<string, float>> agent_info =
        new Dictionary<int, Dictionary<string, float>>() {
            {
                    0,new Dictionary<string, float>
                    {
                        { "Health",100f},
                        { "Damage",10f},
                        { "Speed",200f},

                    }
            },{
                    1,new Dictionary<string, float>
                    {
                        { "Health",5000f},
                        { "Damage",10f},
                        { "Speed",200f},

                    }
            },


};
    //===========================================================================
    public static int currentplayerplane=0; 
}
