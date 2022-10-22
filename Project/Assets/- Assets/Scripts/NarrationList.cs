using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NarrationPlayer;

public class NarrationList : MonoBehaviour
{
    [SerializeField] Narration[] _data;

    public Narration[] Data { get { return _data; } }
}
