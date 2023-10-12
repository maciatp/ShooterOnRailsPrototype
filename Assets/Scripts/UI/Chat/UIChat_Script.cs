using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//extra para que no dé fallos al importar de mix and jam, borrar si acaso luego
using DG.Tweening;
using Cinemachine;

public class UIChat_Script : MonoBehaviour
{
    public bool inDialogue;
    float characterSpawnEvery = 0.2f;
    int visibleCharacters = 0;
    int totalCharacters = 0;
    [SerializeField] float timeToDisableChat = 1;

    public Animator chatImageAnimator;
    public TMP_Animated animatedChatText;
    public AudioSource audioSource;
    private void Awake()
    {

        chatImageAnimator = this.transform.GetChild(1).gameObject.GetComponent<Animator>();
        animatedChatText = this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TMP_Animated>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        totalCharacters = animatedChatText.text.Length;

        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

        animatedChatText.onEmotionChange.AddListener((newEmotion) => EmotionChanger(newEmotion));
        animatedChatText.onAction.AddListener((action) => SetAction(action));
        animatedChatText.onDialogueFinish.AddListener(() => FinishDialogue());
    }

    // Update is called once per frame
    void Update()
    {


    }




    //EMOTION CHANGER PARA CONTROL DE ICONOS chatImageAnimator
    //AL AÑADIR ESTADOS O EMOCIONES TAMBIÉN HAY QUE AÑADIRLOS EN TMP_Animated.cs, arriba.
    public void EmotionChanger(Emotion e)
    {
        //if (this != InterfaceManager.instance.currentVillager)
        //    return;

        //si los triggers del animator se llaman igual que los estados, se llaman desde aquí directamente, así SE REPRODUCE ESE ESTADO DESDE EL INICIO SI SE PONE DELANTE DEL TODO DEL TEXTO
        chatImageAnimator.SetTrigger(e.ToString());
        
        //si <emotion= xx> se pone delante, se inicia así. Se se pone por detrás, puede saltar al estado.
        
        //PUEDO usar estos estados para diferentes animaciones

        if (e == Emotion.suprised)
        { } //eyesRenderer.material.SetTextureOffset("_BaseMap", new Vector2(.33f, 0));

        if (e == Emotion.angry)
        {
           // Debug.Log("Fox ANGRY");
            chatImageAnimator.SetTrigger("angry");
            //chatImageAnimator.Play("Fox_Angry_Anim");

            //chatanimator.settrigger("Angry") //y así pasándole quién habla y cómo ya diferencio entre caras enfadadas,etc.
        } //eyesRenderer.material.SetTextureOffset("_BaseMap", new Vector2(.66f, 0));
        if(e == Emotion.lookaround)
        {
            //Debug.Log("Fox LOOKAROUND");
            chatImageAnimator.SetTrigger("lookaround");
        }
        if (e == Emotion.lookLeft)
        {
           // Debug.Log("Fox LookLeft");
            chatImageAnimator.SetTrigger("lookLeft");
        }
        if (e == Emotion.lookRight)
        {
            //Debug.Log("LookRight");
            chatImageAnimator.SetTrigger("lookRight");
        }
        if (e == Emotion.shouting)
        {
            //Debug.Log("Shouting");
            chatImageAnimator.SetTrigger("shouting");
        }

        if (e == Emotion.sad)
        { } //eyesRenderer.material.SetTextureOffset("_BaseMap", new Vector2(.33f, -.33f));
    }

    public void SetAction(string action)
    {
        //if (this != InterfaceManager.instance.currentVillager)
        //    return;

        if (action == "shake")
        {
            Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
        else
        {
            //PlayParticle(action);

            //if (action == "sparkle")
            //{
            //    dialogueAudio.effectSource.clip = dialogueAudio.sparkleClip;
            //    dialogueAudio.effectSource.Play();
            //}
            //else if (action == "rain")
            //{
            //    dialogueAudio.effectSource.clip = dialogueAudio.rainClip;
            //    dialogueAudio.effectSource.Play();
            //}
        }
    }

    public void ReadDialogue(string dialoguestring)
    {
        animatedChatText.ReadText(dialoguestring);
    }

    public void FinishDialogue()
    {
        //Debug.Log("He terminado diálogo");
       //SI HAY QUE METER ALGO AL MÉTODO, QUE SEA AL ENUMERATOR
        StartCoroutine("DeactivateChat");
    }


    public void EnableChat()
    {
        this.gameObject.SetActive(true);

        //chatImageAnimator.Play("UIChatBox_Opening"); no cambia nada
    }

    public void DisableChat()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator DeactivateChat()
    {
        chatImageAnimator.SetTrigger("finished");
//        Debug.Log("Empieza corutina");
        yield return new WaitForSecondsRealtime(timeToDisableChat);

        chatImageAnimator.Play("UIChatImage_Closing_Anim");
        this.gameObject.GetComponent<Animator>().SetTrigger("CloseChatBox");

        audioSource.Stop();
        ClearText();
        // DisableChat();
    }

   


    public void ClearText()
    {
        animatedChatText.text = string.Empty;
    }



}
