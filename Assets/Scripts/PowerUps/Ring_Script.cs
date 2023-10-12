using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ring_Script : MonoBehaviour
{
    public float rotationSpeed;
    public bool activated;
    public bool isGoldRing = false;
    public float healthWillRestore = 15;
    public PlayerHealth_Script playerHealth_script_;
    public GameObject playerInScene;
    public GameObject gameplayPlane;

    private void Awake()
    {
        playerInScene = GameObject.FindGameObjectWithTag("Player");
        playerHealth_script_ = playerInScene.GetComponent<PlayerHealth_Script>();
        gameplayPlane = GameObject.Find("GameplayPlane");
    }
    // Update is called once per frame
    void Update()
    {
        if(!activated)
            transform.eulerAngles += new Vector3(0, rotationSpeed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activated = true;


            transform.position = other.transform.position;
            transform.parent = other.transform;
            transform.localRotation = other.transform.localRotation;

            Sequence s = DOTween.Sequence();

            //s.Append(transform.DORotate(Vector3.zero, .2f));
            s.Append(transform.DORotate(new Vector3(0, 0, -900), 3, RotateMode.LocalAxisAdd));
            s.Join(transform.DOScale(0, .5f).SetDelay(1f));
            s.AppendCallback(() => Destroy(gameObject));


           


            //transform.localPosition = transform.localPosition - new Vector3(0, 0, 1.3f);
            //ARREGLAR QUE SE COLOQUE EN EL CENTRO DE LA NAVE Y LA SIGA MIENTRAS GIRA Y DESPARECE

            playerHealth_script_.IncreaseHealth(healthWillRestore);
            if(isGoldRing == true)
            {
                playerHealth_script_.AddGoldRing();
            }
        }
    }
}
