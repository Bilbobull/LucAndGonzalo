using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityMeter))]
public class PlayerRangedAbility : BaseAbility
{
    public GameObject RangedAttackPrefab;
    public PlayerController player;

    public GameObject AimIndicatorPrefab;
    private GameObject AimIndicatorInstance;

    private AbilityMeter meter;

    float SpeedMult = 0.1f;

    // Use this for initialization
    void Start ()
    {
        player = GetPlayer();
        meter = GetComponent<AbilityMeter>();
        if(player)
            InputEvents.RangedAttack.Subscribe(OnRangedAttack, player.PlayerNum);
    }

    void OnRangedAttack(InputEventInfo info)
    {
        switch (info.inputState)
        {
            case InputState.Triggered:
                StartCharging();
                break;

            case InputState.Released:
                EndCharging(null);
                break;
        }
    }

    void StartCharging()
    {
        // Turn off regular movement here
        if (player)
        {
            InputEvents.Movement.Subscribe(OnChargeMovement, player.PlayerNum);
            // Apply our speed reduction
            player.Speed *= SpeedMult;
        }
        meter.StartCharging();
        // Create our aiming instance if we have one
        if(AimIndicatorPrefab)
        {
            AimIndicatorInstance = Instantiate(AimIndicatorPrefab, transform);
            AimIndicatorInstance.SendMessage("SetAttacker", gameObject);
        }
    }

    void Update()
    {
        if(meter.IsCharging && AimIndicatorInstance)
        {
            // Draw our aiming thingy
            Vector3 dir = transform.forward;
            // TODO maybe wrap this logic in a component 
            //      so we can just pass a direction/amount & do fancy stuff somewhere else
            const float scale = 5.0f;
            Vector3 s = transform.position + dir * meter.Amount * scale;
            Vector3 e = s + dir * meter.Amount * scale;
            LineRenderer line = AimIndicatorInstance.GetComponent<LineRenderer>();

            line.positionCount = 2;

            line.SetPosition(0, s);
            line.SetPosition(1, e);

            Debug.DrawLine(s, e, Color.red, 0.0f, false);
        }
    }

    void OnChargeMovement(InputEventInfo info)
    {
        // NOTE maybe just set speed to 0 while we aim?
        // Face movement direction
        Vector3 lookDir = new Vector3(info.dualAxisValue.x, 0, info.dualAxisValue.y);
        // Apply the inverse of the camera rotation and normalize so that it's screen relative
        lookDir = Camera.main.transform.TransformDirection(lookDir);
        // Project onto the world plane (y = 0)
        lookDir.y = 0.0f;
        lookDir.Normalize();
        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    void EndCharging(GameObject target)
    {
        if(player)
        {
            InputEvents.Movement.Unsubscribe(OnChargeMovement, player.PlayerNum);
            player.Speed /= SpeedMult;
        }
        float charge = meter.EndCharging();
        if (charge > 0.0f)
            DoAttack(charge, target);
        // Clean up our aim indicator if we need to
        if (AimIndicatorInstance)
            Destroy(AimIndicatorInstance);
    }

    void DoAttack(float charge, GameObject target)
    {
        // Create our attack projectile
        GameObject proj = Instantiate(RangedAttackPrefab, transform.position, transform.rotation);

        // Create the attack as a child of the player in local space
        if (player)
        {
            proj.SendMessage("ChargeValue", charge, SendMessageOptions.DontRequireReceiver);
            proj.SendMessage("SetAttacker", gameObject);
        }
        else if (target)
        {
            proj.GetComponentInChildren<DamageOnCollision>().DamagingUnitTag = target.tag;
            proj.SendMessage("SetAttacker", gameObject);
        }
    }

    void OnRemoveAbility()
    {
        if(player)
        {
            InputEvents.Movement.Unsubscribe(OnChargeMovement, player.PlayerNum);
            InputEvents.RangedAttack.Unsubscribe(OnRangedAttack, player.PlayerNum);
        }
    }

    public override bool AIShouldUseAbility(GameObject currentTarget)
    {
        if (!meter.IsEmpty || meter.IsCharging) return false;
        return base.AIShouldUseAbility(currentTarget);
    }

    public override IEnumerator AIAttackRoutine(GameObject target)
    {
        StartCharging();
        float chargeAmount = UnityEngine.Random.Range(meter.MinCharge, 1.0f);
        yield return new WaitUntil(() => {
            return (meter.Amount >= chargeAmount) ||
                (target == null);
        });
        EndCharging(target);
        yield break;
    }
}
