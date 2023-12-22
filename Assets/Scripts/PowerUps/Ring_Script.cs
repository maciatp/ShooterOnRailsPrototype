using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ring_Script : MonoBehaviour
{
    public float rotationSpeed;
    public bool activated;
    [SerializeField] RingTypes ringType;
    public float healthWillRestore = 15;


    enum RingTypes
    {
        Silver,
        ExtraSilver,
        Gold
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

            transform.parent = other.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = other.transform.localRotation;

            Sequence s = DOTween.Sequence();                       
            s.Append(transform.DORotate(new Vector3(0, 0, -900), 3, RotateMode.LocalAxisAdd));
            s.Join(transform.DOScale(0, .5f).SetDelay(1f));
            s.AppendCallback(() => Destroy(gameObject));
            //ARREGLAR QUE SE COLOQUE EN EL CENTRO DE LA NAVE Y LA SIGA MIENTRAS GIRA Y DESPARECE


            PlayerHealth_Script playerHealth_Script = other.GetComponent<PlayerHealth_Script>();
            playerHealth_Script.IncreaseHealth(healthWillRestore);
            if(ringType == RingTypes.Gold)
            {
                playerHealth_Script.AddGoldRing();
            }
        }
    }
}
