using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifeTime;
    public float upDistance;
    public float inclinationAngle;
    public float lowSizeUmbral;
    public float sizeInterval;
    public string text;


    //Testing variables
    public float divisor;

    private TextMesh textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Start()
    {
        StartCoroutine(Dissapear());
    }


    public void setText(string tx)
    {
        text = tx;
        textMesh.text = text;
    }

    /// <summary>
    /// The number goes up, tilted on a random left or right inclination, and starts getting smaller
    /// When it reaches the low size umbral, it destroys itself
    /// Also it goes back in z axis
    /// Has the damage number
    /// </summary>
    /// <returns></returns>
    IEnumerator Dissapear()
    {
        float randomInclination = Random.Range((inclinationAngle * -1), inclinationAngle);

        for (float size = 0.1f; size >= lowSizeUmbral; size -= sizeInterval)
        {
            transform.localScale = new Vector3(size, size, size);
            transform.Translate(randomInclination, upDistance, 0);
            textMesh.color = new Color(textMesh.color.r * divisor, textMesh.color.g * divisor, textMesh.color.b * divisor);

            yield return new WaitForEndOfFrame();
            //yield return new WaitForSeconds(.012f);
        }

        Destroy(gameObject);
    }


}
