using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb_PowerUp_Script : MonoBehaviour
{   
    bool isPickedUp = false;

    [SerializeField] float rotationSpeed = 50;

    [SerializeField] float verticalOffsetWhenPicked = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, rotationSpeed, 0) * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(((other.gameObject.tag == "Player")) && (isPickedUp == false))
        {            
            PlayerShooting_Script playerShooting_ = other.GetComponent<PlayerShooting_Script>();

            isPickedUp = true;

            playerShooting_.AddOneBomb();
            gameObject.GetComponent<AudioSource>().Play();
            

            transform.position = other.transform.position + new Vector3(0,verticalOffsetWhenPicked,0);
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
