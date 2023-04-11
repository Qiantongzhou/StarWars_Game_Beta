using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class player : MonoBehaviour
{
    public Attributes attr;
    public GameObject playerdieexplosionprefab;
    [HideInInspector]
    public Attributes equipAttr;
    [HideInInspector]
    public Attributes skillAttr;
    [HideInInspector]
    public Attributes ResultAttr;
    private float lastCollisionTime;
    private float collisionCooldown = 2f;


    private int currenthealth;

    private int currentarmor;


    public int damageblock { get; set; }

    public int attackdamage { get; set; }
    public int attackdamagebonus { get; set; }

    public int critdamage { get; set; }
    public int critchance { get; set; }

    public int magicdamage { get; set; }


    public int attackrange { get; set; }
    public int attackspeed { get; set; }

    public int movespeed { set; get; }
    public int turnrate;

    int gems;
    float gold;

    gamesaving gamesaving;
    Canvas canvas;
    float timecaculate;
    void Awake()
    {
        attr.movespeed =Mathf.FloorToInt(GameSetting.plane_info[GameSetting.currentplayerplane]["Speed"]);
        attr.attackdamage= Mathf.FloorToInt(GameSetting.plane_info[GameSetting.currentplayerplane]["Damage"]);
        attr.healthpoint = Mathf.FloorToInt(GameSetting.plane_info[GameSetting.currentplayerplane]["Health"]);
        attr.armor = Mathf.FloorToInt(GameSetting.plane_info[GameSetting.currentplayerplane]["armor"]);
        gamesaving = GameObject.Find("gamesaving").GetComponent<gamesaving>();
        canvas = GameObject.FindGameObjectWithTag("screencanvas").GetComponent<Canvas>();
        //attr// = GetComponent<Attributes>();
        equipAttr = gameObject.AddComponent<Attributes>();
        skillAttr = gameObject.AddComponent<Attributes>();
        ResultAttr = gameObject.AddComponent<Attributes>();

        ResultAttr += attr;
        ResultAttr += equipAttr;
        ResultAttr += skillAttr;


        currenthealth = ResultAttr.healthpoint;
        currentarmor = ResultAttr.armor;
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
       

        TMP_Text[] j = GameObject.FindGameObjectWithTag("screencanvas").GetComponent<Canvas>().GetComponentsInChildren<TMP_Text>();
        //j[3].text = "Gem:"+gems.ToString();
        //j[4].text = "Gold:"+Mathf.FloorToInt(gold).ToString();


        float value = (float)getcurrenthealth() / ResultAttr.healthpoint;
        float magic = (float)getcurrentmagic() / ResultAttr.armor;

        Slider[] y = canvas.GetComponentsInChildren<Slider>();
        y[0].value = value;
        y[1].value = magic;
        TMP_Text[] x = canvas.GetComponentsInChildren<TMP_Text>();
        x[4].text ="Health:"+getcurrenthealth() + "/" + ResultAttr.healthpoint;
        x[6].text = "sheild:"+getcurrentmagic() + "/" + ResultAttr.armor;
        if (ResultAttr.attackdamagebonus > 0)
        {
            x[5].text ="Damage:"+ ResultAttr.attackdamage.ToString() + "<color=green>+" + ResultAttr.attackdamagebonus.ToString() + "</color>";
        }
        if (ResultAttr.attackdamagebonus < 0)
        {
            x[5].text = "Damage:" + ResultAttr.attackdamage.ToString() + "<color=red>+" + ResultAttr.attackdamagebonus.ToString() + "</color>";
        }
        if (ResultAttr.attackdamagebonus == 0)
        {
            x[5].text = "Damage:" + ResultAttr.attackdamage.ToString();
        }
        x[4].text = "MaxSpeed:"+ResultAttr.movespeed.ToString();
        //x[6].text = ResultAttr.critdamage.ToString();
        //x[7].text = ResultAttr.damageblock.ToString();
    }
    private void FixedUpdate()
    {
        ResultAttr.SetZero();
        ResultAttr += attr;
        ResultAttr += equipAttr;
        ResultAttr += skillAttr;
        gems = int.Parse(gamesaving.getGameSavingGems());
        healthregenpersec();
        setplayercontroller();
    }
    private void healthregenpersec()
    {
        if (currenthealth < ResultAttr.healthpoint)
        {
            timecaculate += Time.deltaTime;
            if (timecaculate > 1.0f)
            {
                currenthealth = currenthealth + ResultAttr.healthregen;
                timecaculate = 0.0f;
            }
        }
    }
    //costom
    public void setplayercontroller()
    {
        if(transform.GetComponent<SpaceshipController>() != null)
        {
            SpaceshipController controller = transform.GetComponent<SpaceshipController>();
            controller.m_spaceship.SpeedRange = new Vector2(30, ResultAttr.movespeed);
            controller.m_shooting.bulletSettings.BulletSpeed = GameSetting.plane_info[GameSetting.currentplayerplane]["bulletspeed"];
            controller.m_shooting.bulletSettings.BulletFireDelay = GameSetting.plane_info[GameSetting.currentplayerplane]["firedelay"];
        }
    }
    public void takedamage(int dam)
    {
        print("playertakedamage: " + dam);
        if (currentarmor > 1)
        {
            currentarmor -= dam;
        }
        else
        {
            currenthealth -= dam;
        }
        if(currenthealth < 0)
        {
            currenthealth = 0;
            onplayerdie();
        }
    }
    public void onplayerdie()
    {
        Instantiate(playerdieexplosionprefab, transform.position, Quaternion.identity);
        transform.gameObject.SetActive(false);
    }
    public int getcurrenthealth()
    {
        return currenthealth;
    }
    public int getcurrentmagic()
    {
        return currentarmor;
    }
    public void setcurrentmagic(int armor)
    {
        currentarmor=armor;
    }
    public Attributes GetBaseAttributes()
    {
        return attr;
    }

    public Attributes GetEquipAttributes()
    {
        return equipAttr;
    }

    public Attributes GetSkillAttributes()
    {
        return skillAttr;
    }

    public void SetEquipAttributes(Attributes newAttr)
    {
        equipAttr = newAttr;
    }

    public void SetSkillAttributes(Attributes newAttr)
    {
        equipAttr = newAttr;
    }


    public void GiveMoney(float amount)
    {
        gold += amount;
    }


    /*public void IncreaseAttributes(Attributes newAttr)
    {
        attr += newAttr;
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        // to avoid continus collision
        if (Time.time > lastCollisionTime + collisionCooldown) {
            var collisionLayer = collision.collider.gameObject.layer;
            // if collision is not from missle
            if (collisionLayer != LayerMask.NameToLayer("Missle")) {
                takedamage(10);
            }
            lastCollisionTime = Time.time;
        }
    }
}
