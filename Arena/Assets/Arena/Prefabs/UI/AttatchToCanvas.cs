using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttatchToCanvas : MonoBehaviour {

    void Awake()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
