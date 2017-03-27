using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOnCollision : MonoBehaviour {

    public LayerMask layers;
    public string[] tags;

    public float Spring;
    public float Damper;
    public bool OnlyOnce = true;
    public void OnTriggerEnter(Collider other)
    {
        if (IsSticktarget(other.gameObject))
        {
            SpringJoint joint = gameObject.AddComponent<SpringJoint>();
            joint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
            joint.anchor = other.ClosestPointOnBounds(transform.position);
            joint.connectedAnchor = GetComponent<Collider>().ClosestPointOnBounds(other.transform.position);
            joint.spring = Spring;
            joint.damper = Damper;
            joint.enableCollision = false;

            if(GetComponent<ConstantForce>())
            {
                Destroy(GetComponent<ConstantForce>());
            }
            // Remove ourselves if we only want to stick once
            if (OnlyOnce) Destroy(this);
        }
    }

    private static bool InMask(LayerMask mask, int layer)
    {
        return mask == (mask | 1 << layer);
    }

    // Returns true if colliding/triggering other object should destroy this one
    bool IsSticktarget(GameObject other)
    {
        // Stick if in layer
        if (InMask(layers, other.layer))
            return true;
        // Stick if matches tag
        foreach (string t in tags)
            if (other.CompareTag(t))
                return true;
        // Otherwise don't destroy
        return false;
    }
}
