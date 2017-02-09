using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseAbility : MonoBehaviour
{
    // Each player can have only one of each ability type
    public enum AbilityType
    {
        Ranged, Melee, Movement
    };

    public AbilityType type;

    public string abilityName;
    protected PlayerController GetPlayer()
    {
        return GetComponentInParent<PlayerController>() as PlayerController;
    }

    [System.Serializable()]
    public struct AISettings
    {
        public float minAttackRadius;
        public float maxAttackRadius;
    };
    public AISettings ai;

    // Defaults to a radius min/max
    public virtual bool ShouldUseAbility(GameObject currentTarget)
    {
        float dist = (transform.position - currentTarget.transform.position).sqrMagnitude;

        if (dist < ai.minAttackRadius * ai.minAttackRadius)
            return false;
        if (dist > ai.maxAttackRadius * ai.maxAttackRadius)
            return false;
        return true;
    }

    public virtual IEnumerator AIAttackRoutine(GameObject target)
    {
        // Does nothing if not implemented
        yield break;
    }
}
