using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserPowerUp_Script : MonoBehaviour
{
    public bool isPickedUp = false;

    public GameObject playerInScene;
    public PlayerShooting_Script playerShooting_Script;
    public Rigidbody rb_LaserUpgrade;

    public AudioSource laserUpgradeAudioSource;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb_LaserUpgrade = this.GetComponent<Rigidbody>();

        playerInScene = GameObject.FindGameObjectWithTag("Player");

        playerShooting_Script = playerInScene.gameObject.GetComponent<PlayerShooting_Script>();

        laserUpgradeAudioSource = this.GetComponent<AudioSource>();
     

    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb_LaserUpgrade.transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(( other.gameObject.tag == "Player") && (isPickedUp == false))
        {
            isPickedUp = true;

            playerShooting_Script.AddOneLaserPowerUp();

            laserUpgradeAudioSource.Play();

            transform.position = other.transform.position;
            transform.parent = other.transform;
            transform.localRotation = other.transform.localRotation;

            Sequence s = DOTween.Sequence();

            //s.Append(transform.DORotate(Vector3.zero, .2f));
            s.Append(transform.DORotate(new Vector3(0, 720, 0), 2, RotateMode.LocalAxisAdd));
            s.Join(transform.DOScale(0, .5f).SetDelay(1f));
            s.AppendCallback(() => Destroy(gameObject));
        }
        
    }
}
