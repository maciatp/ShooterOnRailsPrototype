using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlowMoBar_Script : MonoBehaviour
{

    [Header("Bools")]
    public bool isBarExtraSize = false;
    [Space]
    [Header("Public References")]
    public TimeManager_Script timeManager_Script_;
    public Image slowMoImage;
    public Animator uISlowMo_Animator;

    [Space]

    [Header("Parameters")]
    public Vector3 barNormalSize = new Vector3(100, 1);
    public Vector3 barExtraSize = new Vector3(150, 1);

    private void Awake()
    {
        //timeManager_Script_ = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager_Script>();
        timeManager_Script_ = GameObject.Find("Player").gameObject.GetComponent<TimeManager_Script>();
        slowMoImage = this.GetComponent<Image>();
        uISlowMo_Animator = this.GetComponent<Animator>();
        //slowMoImage.transform.localScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        slowMoImage.transform.localScale = new Vector2(timeManager_Script_.actualSlowMoPoints, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (slowMoImage.transform.localScale.x >= barNormalSize.x) 
        {
            uISlowMo_Animator.Play("SlowMoBar_Full_Anim");

            uISlowMo_Animator.SetBool("isFull", true);
            /*
            if(slowMoImage.color == Color.blue)
            {
                slowMoImage.color = Color.white;
            }
            else
            {
                slowMoImage.color = Color.blue;
            }
            */
            
        }
        else
        {
            uISlowMo_Animator.SetBool("isFull", false);
            if(slowMoImage.color != Color.white)
            {
                slowMoImage.color = Color.white;
            }
        }
        
       
       

    }


    public void IncreaseBarSize(float slowMoPointsAdded)
    {
        if (slowMoImage.transform.localScale.x < barNormalSize.x)
        {
            slowMoImage.transform.localScale += new Vector3(slowMoPointsAdded, 0);
        }
        if ((slowMoImage.transform.localScale.x > barNormalSize.x) && (isBarExtraSize == false))
        {
            slowMoImage.transform.localScale = barNormalSize;
            
        }

    }

    public void DecreaseBarSize(float slowMoPointsDepleted)
    {
        if (slowMoImage.transform.localScale.x > 0)
        {
            slowMoImage.transform.localScale -= new Vector3(slowMoPointsDepleted, 0f);
        }
        if (slowMoImage.transform.localScale.x < 0)
        {
            slowMoImage.transform.localScale = new Vector3(0, 1);
        }


    }


}
