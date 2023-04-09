using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ForceShield : EnhancementSkill
{
    public override string Name => "ForceShield";
    public override string Description => "protect";
    public AudioClip clip;
    //AudioSource source;
    public GameObject Sheildprefab;
    public GameObject[] underShield;
    public Material materialShield;
    public float brightnessCollision = 5.0f;
    public float fadingGlow = 1.0f;
    public float armor = 100;
    public float speedOnOff = 10.0f;
    public Vector4 speedOffset = new Vector4(0, 0, 0, 0);
    public bool sphere = false;
    public float sphereScale = 2.0f;
    public Vector3 spherePosition = new Vector3(0, 0, 0);

    [HideInInspector] public float mTime;
    [HideInInspector] public Color shieldColor;
    [HideInInspector] public bool hit = false;
    [HideInInspector] public bool hit2 = false;
    [HideInInspector] public MeshRenderer[] mesh;
    [HideInInspector] public int i = 0;
    private float shieldA = 0.0f;
    private GameObject sp;
    private bool activ = false;
    private Vector4 offset = new Vector4(0, 0, 0, 0);
    GameObject player;
    bool active = false;
    bool cooldown=false;
    //bool startcount = false;
    //float count = 0;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public override void DoAction()
    {
        if (!cooldown)
        {
            StartCoroutine(forcefield());
        }
    }
    IEnumerator forcefield()
    {

        CreateForceField(player.transform);
        active = true;
        cooldown = true;
        yield return new WaitForSeconds(10);
        DestroyForceField();
        active = false;
        yield return new WaitForSeconds(10);
        cooldown = false;
    }
    private void Update()
    {
        if (active)
        {
            if (mesh != null)
            {
                offset.x += Time.deltaTime * speedOffset.x;
                offset.y += Time.deltaTime * speedOffset.y;
                offset.z += Time.deltaTime * speedOffset.z;
                offset.w += Time.deltaTime * speedOffset.w;
                foreach (MeshRenderer tMesh in mesh)
                {
                    tMesh.materials[i].SetTextureOffset("_MainTex", new Vector2(offset.x, offset.y));
                    tMesh.materials[i].SetTextureOffset("_NormalMap", new Vector2(offset.z, offset.w));
                }
            }

            if (hit == true && mTime < 0.9f)
            {
                if (mesh != null) { foreach (MeshRenderer tMesh in mesh) { tMesh.materials[i].SetVector("_Color", shieldColor); } }
                hit = false;

            }

            if (hit2 == true)
            {
                if (mTime < 0.0f)
                {
                    mTime = 0.0f;
                    if (mesh != null) { foreach (MeshRenderer tMesh in mesh) { tMesh.materials[i].SetFloat("_EffectTime", mTime); } }
                    hit2 = false;
                }
                else
                {
                    mTime -= Time.deltaTime * fadingGlow;
                    if (mesh != null) { foreach (MeshRenderer tMesh in mesh) { tMesh.materials[i].SetFloat("_EffectTime", mTime); } }
                }
            }
        }
    }
    public void CreateForceField(Transform parent)
    {
        sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp.name = "ForceField";
        sp.transform.parent = parent;
        sp.transform.localPosition = spherePosition;
        sp.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
        i = 0;
        mesh = new MeshRenderer[1];
        mesh[0] = sp.GetComponent<MeshRenderer>();
        mesh[0].material = materialShield;
        mesh[0].material.SetFloat("_MeshOffset", 0.0f);

    }
    public void DestroyForceField()
    {
        
        Destroy(sp);
        mesh = null;
        

        mTime = 0.0f;
        shieldA = 0.0f;
        shieldColor = new Color(0, 0, 0, 0);
        hit = false;
        hit2 = false;
        activ = false;
    }
}
