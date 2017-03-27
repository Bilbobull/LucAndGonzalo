using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnDestroy : MonoBehaviour
{
    public GameObject prefab;
    public bool createAtLocation = true;

    // We don't want to do this if we're quitting the application
    private bool isQuitting = false;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        if (!isQuitting)
        {
            GameObject instance;
            if (createAtLocation)
                instance = Instantiate(prefab, transform.position, transform.rotation);
            else
                instance = Instantiate(prefab);
        }

    }
}
