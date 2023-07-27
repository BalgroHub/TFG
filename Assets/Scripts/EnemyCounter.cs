using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private TextMesh numberBig;
    private TextMesh numberFast;

    private void Start()
    {
        numberBig = transform.Find("BigECount").transform.GetChild(0).GetComponent<TextMesh>();
        numberFast = transform.Find("FastECount").transform.GetChild(0).GetComponent<TextMesh>();

        SetNumbers(SpawnerManager.Instance.CountEnemiesType());
    }


    /// <summary>
    /// We set the numbers of the incoming enemies
    /// </summary>
    /// <param name="vec">First value is fast enemies, then big</param>
    public void SetNumbers(Vector2 vec)
    {
        numberFast.text = "" + vec.x;
        numberBig.text = "" + vec.y;
    }
}
