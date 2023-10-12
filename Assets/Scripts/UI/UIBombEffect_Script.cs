using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBombEffect_Script : MonoBehaviour
{
    [Header("Public References")]

    public Animator UIBombEffectAnimator;

    private void Awake()
    {
        UIBombEffectAnimator = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayUIBombAnimation()
    {
        UIBombEffectAnimator.Play("UIBombEffect_Anim");
    }

}
