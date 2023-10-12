using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb_PowerUp_Script : MonoBehaviour
{
    public GameObject playerInScene;
    public Rigidbody rb_Bomb_PowerUp;
    public PlayerShooting_Script playerShooting_;

    public AudioSource bombPickUpAudioSource;

    public bool isPickedUp = false;

    public float rotationSpeed = 50;

    private void Awake()
    {
        rb_Bomb_PowerUp = this.gameObject.GetComponent<Rigidbody>();
        playerInScene = GameObject.FindGameObjectWithTag("Player");
        playerShooting_ = playerInScene.GetComponent<PlayerShooting_Script>();
        bombPickUpAudioSource = this.GetComponent<AudioSource>();
       

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, rotationSpeed, 0) * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(((other.gameObject.name == "ArwingHD") && (other.gameObject.tag == "Player")) && (isPickedUp == false))
        {
            isPickedUp = true;

            playerShooting_.AddOneBomb();
            bombPickUpAudioSource.Play();


            transform.position = other.transform.position + new Vector3(0,0.5f,0);
            transform.parent = other.transform;
            transform.localRotation = other.transform.localRotation;

            Sequence s = DOTween.Sequence();

            //s.Append(transform.DORotate(Vector3.zero, .2f));
            s.Append(transform.DORotate(new Vector3(0, 720, 0), 1, RotateMode.LocalAxisAdd));
            s.Join(transform.DOScale(0, .5f).SetDelay(0.75f));
            s.AppendCallback(() => Destroy(gameObject));


            
;        }
    }
}
