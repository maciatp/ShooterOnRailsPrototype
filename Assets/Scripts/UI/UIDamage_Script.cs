using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamage_Script : MonoBehaviour
{
    public Animator damageUIAnimator;

    private void Awake()
    {
        damageUIAnimator = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamageUI()
    {
        damageUIAnimator.Play("UIDamage_Anim");
    }
}
