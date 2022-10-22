using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dist : MonoBehaviour
{
    public float maxdis = 5;
    public float mindis = 1;

    public float currdis;
    public Transform playerTr;

    public Image block;

    private void Update()
    {
        currdis = Vector3.Distance(this.transform.position, playerTr.position);
        float a = 1 - ((maxdis - (maxdis - currdis)) / (maxdis - mindis));
        block.color = new Color(a, a, a, 1);
    }
}
