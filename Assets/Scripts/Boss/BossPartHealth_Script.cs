using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartHealth_Script : MonoBehaviour
{
    bool isPartDestroyed = false;
    BossHealth_Script bossHealth_Script_;
    
    [Space]
    [Header("Boss Part Parameters")]
    [SerializeField] float partHealth = 30;
    [SerializeField] int hitsWillAdd = 0;

    [Space]
    [Header("Prefab References")]
    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject waterExplosion;



    //PARA QUE SE VUELVA ROJO AL DAÑO
    [Tooltip("PARA QUE SE VUELVA ROJO AL DAÑO")]
    [SerializeField] List<Animator> animators = new List<Animator>();

    private void Awake()
    {

        foreach (Animator animatorComponent in animators)
        {
            
           Animator _animator = animatorComponent.gameObject.GetComponent<Animator>();
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = false;
        bossHealth_Script_ = GetComponentInParent<BossHealth_Script>();
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
        }

        //TODO: FALTA AÑADIR TODO EL ARMAMENTO PARA DAÑO (Bombas, charged sphere, explosions...)
    }

    void DepletePartHealth(float damage)
    {
        if((partHealth > 0) && (isPartDestroyed == false))
        {
            partHealth -= damage;
            int i = 0;
            foreach(Animator animator_GO in animators)
            {                
                animators[i].Play("Boss_Damage_Right_anim");
                i++;
            }

            if(partHealth <= 0)
            {
                DestroyPart();
            }
            
        }
        
    }

    private void DestroyPart()
    {
        partHealth = 0;
        isPartDestroyed = true;
        
        GameObject.Find("ScoreManager").GetComponent<ScoreManager_Script>().AddHits(hitsWillAdd);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.SetParent(null);
        bossHealth_Script_.PartsDestroyed += 1;
        
        if(this.gameObject.name == "BOSSPART_RIGHT")
        {
            bossHealth_Script_.RightPartDestroyed = true;
        }
        else if( this.gameObject.name == "BOSSPART_DOWN")
        {
            bossHealth_Script_.LeftDownPartDestroyed = true;
        }
        else if( this.gameObject.name == "BOSSPART_UP")
        {
            bossHealth_Script_.LeftUpPartDestroyed = true;
        }

        if(bossHealth_Script_.PartsDestroyed == 3)
        {
            bossHealth_Script_.BossFinalForm();
        }
    }

    
}
