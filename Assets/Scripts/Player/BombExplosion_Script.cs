using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion_Script : MonoBehaviour
{

    private BoxCollider collider_BombExplosion;
    private ParticleSystem bombExplosionParticles;
    public float maxSize = 3;

    public float colliderIncreasingRate = 1.08f;

    private void Awake()
    {
        collider_BombExplosion = this.gameObject.GetComponent<BoxCollider>();
        bombExplosionParticles = this.gameObject.GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bombExplosionParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(collider_BombExplosion.size.x <= maxSize )
        {
            collider_BombExplosion.size = new Vector3((collider_BombExplosion.size.x) * colliderIncreasingRate, (collider_BombExplosion.size.y) * colliderIncreasingRate, (collider_BombExplosion.size.z) * colliderIncreasingRate);
        }
        
    }
}
