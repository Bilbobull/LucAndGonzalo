using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    public BaseAbility[] abilities;

    // Update is called once per frame
    void Update()
    {
        abilities = GetComponentsInChildren<BaseAbility>();
        GameObject target = GetComponent<EnemyBehavior>().ClosestPlayer;

        // HACK
        // if (GetComponent<EnemyBehavior>().movement != EnemyBehavior.MovementType.WalkTowardsPlayer) return;

        if (!target) return;

        foreach (BaseAbility a in abilities)
        {
            if(a.AIShouldUseAbility(target))
            {
                a.StartCoroutine(a.AIAttackRoutine(target));
                break;
            }
        }
    }
}
