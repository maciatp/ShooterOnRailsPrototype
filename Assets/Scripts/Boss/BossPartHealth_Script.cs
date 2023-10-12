using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartHealth_Script : MonoBehaviour
{
    public bool isPartDestroyed = false;
    public float partHealth = 30;
    public int hitsWillAdd = 0;

    public BossHealth_Script bossHealth_Script_;


    public GameObject explosionFX;
    public GameObject waterExplosion;

    

    //public Animator right_Damage_Animator;

    public List<Animator> animators = new List<Animator>();

    private void Awake()
    {

        foreach (Animator animator_GO in animators)
        {
            //right_Damage_Animator = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
           Animator _animator = animator_GO.gameObject.GetComponent<Animator>();
        }
        //bossHealth_Script_ = this.transform.parent.GetComponent<BossHealth_Script>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Water") && (isPartDestroyed == true))
        {
            Instantiate(waterExplosion, this.transform.position, new Quaternion(0, 0, 0, 0), null);
            GameObject bigExplosion_ = Instantiate(explosionFX, this.transform.position, new Quaternion(0, 0, 0, 0), null) as GameObject;
            bigExplosion_.transform.localScale *= 5;
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LaserBeam")
        {
            DepletePartHealth(other.gameObject.GetComponent<Beam_Script>().damagePoints);
            //bossHealth_Script_.DepleteBossHealth(other.gameObject.GetComponent<Beam_Script>().damagePoints);
        }

        //FALTA AÑADIR TODO EL ARMAMENTO PARA DAÑO
    }

    void DepletePartHealth(float damage)
    {
        if((partHealth > 0) && (isPartDestroyed == false))
        {
            partHealth -= damage;
            int i = 0;
            foreach(Animator animator_GO in animators)
            {
                //int i = 0;
                animators[i].Play("Boss_Damage_Right_anim");
                i++;
            }

            if(partHealth <= 0)
            {
                DestroyPart();
            }
            //AHORA FALTA HACER LO MISMO PARA EL CUERPO PRINCIPAL (LISTA CON TODOS LOS ANIMATORS Y REPRODUCIR CUANDO DAÑO)
            
        }
        //else if((partHealth <= 0) && (isPartDestroyed == false))
        //{
        //    DestroyPart();
        //}
    }

    private void DestroyPart()
    {
        partHealth = 0;
        isPartDestroyed = true;
        //collider.enabled = true;
        //this.gameObject.SetActive(false);
        GameObject.Find("ScoreManager").GetComponent<ScoreManager_Script>().AddHits(hitsWillAdd);
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        this.gameObject.transform.SetParent(null);
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        bossHealth_Script_.partsDestroyed += 1;
        
        if(this.gameObject.name == "BOSSPART_RIGHT")
        {
            bossHealth_Script_.isRightPartDestroyed = true;
        }
        else if( this.gameObject.name == "BOSSPART_UP")
        {
            bossHealth_Script_.isLeftUpPartDestroyed = true;
        }
        else if( this.gameObject.name == "BOSSPART_DOWN")
        {
            bossHealth_Script_.isLeftDownPartDestroyed = true;
        }

        if(bossHealth_Script_.partsDestroyed == 3)
        {
            bossHealth_Script_.BossFinalForm();
        }
    }

    
}
