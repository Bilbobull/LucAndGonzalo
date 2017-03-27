using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    public string[] tags;
    public LayerMask layers;


    public void OnCollisionEnter(Collision c)
    {
        if (IsDestroytarget(c.gameObject))
            Destroy(gameObject);          
    }

    public void OnTriggerEnter(Collider other)
    {
        if (IsDestroytarget(other.gameObject))
            Destroy(gameObject);
    }

    private static bool InMask(LayerMask mask, int layer)
    {
        return mask == (mask | 1 << layer);
    }

    // Returns true if colliding/triggering other object should destroy this one
    bool IsDestroytarget(GameObject other)
    {
        // Destroy if in layer
        if (InMask(layers, other.layer))
        {
            return true;
        }
        // Destroy if matches tag
        foreach (string t in tags)
            if (other.CompareTag(t))
                return true;
        // Otherwise don't destroy
        return false;
    }
}
