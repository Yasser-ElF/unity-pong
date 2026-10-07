using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OscProcess : MonoBehaviour
{
    public extOSC.OSCReceiver oscReceiver;
    public GameManager gameManager;
    public Ball ball;

    public PlayerPaddle playerPaddle;

    public int potInMin = 0;
    public int potInMax = 1023;
    public float potOutMin = 0.0f;
    public float potOutMax = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/but0", TraiterMessageBut0);
        oscReceiver.Bind("/pot", TraiterMessagePot0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TraiterMessageBut0(OSCMessage message)
    {
        // Validez qu’il y a bien le nombre attendu d’arguments (1 dans l’exemple) :
        if (message.Values.Count != 1)
        {
            Debug.Log("Le message " + message.Address + " na pas le bon nombre d’arguments");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Vérifiez que l’argument est du type attendu (`int` dans l’exemple) :
        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Le premier argument du message " + message.Address + "nest pas un entier");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Récupérer la valeur de l’argument :
        int valeur = message.Values[0].IntValue;

        // Deboguer
        // Debug.Log("Reçu : " + message.Address + " " + valeur);

        // FAIRE DE QUOI AVEC LA VARIABLE VALEUR ICI !
        if (valeur == 1)
        {
            gameManager.ThrowBall();
        }
        else
        {

        }


    }

    void TraiterMessagePot0(OSCMessage message)
    {
        if (message.Values.Count != 1)
        {
            Debug.Log("Le message " + message.Address + " na pas le bon nombre d’arguments");
            return;
        }

        if (message.Values[0].Type != OSCValueType.Float && message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Le premier argument du message " + message.Address + " nest pas un nombre");
            return;
        }

        float valeur = message.Values[0].Type == OSCValueType.Float
            ? message.Values[0].FloatValue
            : message.Values[0].IntValue;

        // Utiliser la valeur du potentiomètre ici.
        Debug.Log("Pot0: " + valeur);

        // Ajuster la position de la raquette du joueur en fonction de la valeur du potentiomètre
        float ajuste = (((float)valeur - potInMin) / (potInMax - potInMin) * (potOutMax - potOutMin) + potOutMin);
        playerPaddle.SetPosition(ajuste);
    }

}