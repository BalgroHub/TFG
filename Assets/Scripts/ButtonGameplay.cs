using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGameplay : MonoBehaviour
{
    public static ButtonGameplay Instance { get; private set; }

    public Color baseColor;
    public Color deactivatedColor;
    public SpriteRenderer sprite;
    public TextMeshProUGUI text;

    public bool canPress;

    private void Start()
    {
        Instance = this;
        text = GameObject.Find("TextNoWeapon").GetComponent<TextMeshProUGUI>();
        sprite = GetComponent<SpriteRenderer>();

        canPress = false;
    }

    public void SetReady(bool bo)
    {
        if(bo)
        {
            //Debug.Log("Preparado");
            sprite.color = baseColor;
            text.alpha = 0;
            canPress = true;

        }
        else
        {
            //Debug.Log("Falta arma");
            sprite.color = deactivatedColor;
            text.alpha = 255;
            canPress = false;
        }
    }
}
