using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Transform[] Positions;

    Coroutine c;

    public void Next(int index)
    {
        if(c != null)
        {
            StopCoroutine(c);
            c = null;
        }

        a(this.transform.position, Positions[index].position);
    }

    private void a(Vector3 startposition, Vector3 endPosition)
    {
        c = StartCoroutine(b());

        IEnumerator b()
        {
            for (float t = 0; t < 2f; t += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(startposition, endPosition, t / 2);
                yield return null;
            }
        }
    }
}
