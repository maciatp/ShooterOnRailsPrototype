using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrows_Script : MonoBehaviour
{
    public GameObject arrow_Right;
    public GameObject arrow_Left;
    public GameObject arrow_Up;
    public GameObject arrow_Down;

    [SerializeField]
    private bool isArrowRightEnabled = false;
    [SerializeField]
    private bool isArrowLeftEnabled = false;
    [SerializeField]
    private bool isArrowUpEnabled = false;
    [SerializeField]
    private bool isArrowDownEnabled = false;

    public PlayerMovement_Script playerMovement_Script_;

    public GameObject UIChooseWayArrows;

    //public Animator arrowsAnimator;

    [SerializeField]
    private Vector2 arrowThreshold = new Vector2(25,12.2f);

    private void Awake()
    {

        playerMovement_Script_ = GameObject.Find("Player").gameObject.GetComponent<PlayerMovement_Script>();
       

        arrow_Right = this.gameObject.transform.GetChild(0).gameObject;
        arrow_Left = this.gameObject.transform.GetChild(1).gameObject;
        arrow_Up = this.gameObject.transform.GetChild(2).gameObject;
        arrow_Down = this.gameObject.transform.GetChild(3).gameObject;

        //arrowsAnimator = arrow_Right.gameObject.GetComponent<Animator>();

        UIChooseWayArrows = this.transform.parent.Find("UIChooseWayArrows").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        //arrowsAnimator.StopPlayback();
        arrow_Right.SetActive(false);
        arrow_Left.SetActive(false);
        arrow_Up.SetActive (false);
        arrow_Down.SetActive(false);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(UIChooseWayArrows.gameObject.activeSelf == false)
        {
            //RIGHT ARROW

            if ((playerMovement_Script_.transform.localPosition.x >= arrowThreshold.x) && isArrowRightEnabled == false)
            {
                EnableRightArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.x < arrowThreshold.x) && isArrowRightEnabled == true)
            {
                DisableRightArrow();
            }

            //LEFT ARROW
            if ((playerMovement_Script_.transform.localPosition.x <= -arrowThreshold.x) && isArrowLeftEnabled == false)
            {
                EnableLeftArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.x > -arrowThreshold.x) && isArrowLeftEnabled == true)
            {
                DisableLeftArrow();
            }


            //UP ARROW
            if ((playerMovement_Script_.transform.localPosition.y >= arrowThreshold.y) && isArrowUpEnabled == false)
            {
                EnableUpArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.y < arrowThreshold.y) && isArrowUpEnabled == true)
            {
                DisableUpArrow();
            }

            //DOWN ARROW
            if ((playerMovement_Script_.transform.localPosition.y <= -arrowThreshold.y) && isArrowDownEnabled == false)
            {
                EnableDownArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.y > -arrowThreshold.y) && isArrowDownEnabled == true)
            {
                DisableDownArrow();
            }
        }
        




    }


    //RIGHT ARROW
    private void EnableRightArrow()
    {
        isArrowRightEnabled = true;
        arrow_Right.SetActive(true);
        //arrowsAnimator.Play("Arrow_Idle_anim");
        //Debug.Log("He activado la flecha derecha");
    }

    public void DisableRightArrow()
    {
        isArrowRightEnabled = false;
        //arrowsAnimator.StopPlayback();
        arrow_Right.SetActive(false);
    }
    //LEFT ARROW
    private void EnableLeftArrow()
    {
        isArrowLeftEnabled = true;
        arrow_Left.SetActive(true);
       
    }
    public void DisableLeftArrow()
    {
        isArrowLeftEnabled = false;
        arrow_Left.SetActive(false);
       
    }

    //UP ARROW
    private void EnableUpArrow()
    {
        isArrowUpEnabled = true;
        arrow_Up.SetActive(true);

    }
    private void DisableUpArrow()
    {
        isArrowUpEnabled = false;
        arrow_Up.SetActive(false);

    }
    //DOWN ARROW
    private void EnableDownArrow()
    {
        isArrowDownEnabled = true;
        arrow_Down.SetActive(true);

    }

    private void DisableDownArrow()
    {
        isArrowDownEnabled = false;
        arrow_Down.SetActive(false);
        
    }
}
