using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeter : MonoBehaviour
{
    // How much of our ability we actually have
    [Range(0, 1)]
    public float Amount;

    [Tooltip("The minimum ammount we have to charge for a successfull attack")]
    [Range(0, 1)]
    public float MinCharge;

    [Tooltip("How much the meter charges when not activated")]
    public float RechargeRate;

    [Tooltip("How much the meter charges when activated")]
    public float ChargeRate;

    [Tooltip("UI Element Prefab")]
    public GameObject ui;

    // Are we charging right now?
    public bool IsCharging
    { get; private set; }

    public bool IsFull
    {
        get
        {
            return Amount >= 1.0f;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return Amount <= 0.0f;
        }
    }

    private void Awake()
    {
        IsCharging = false;
        if(ui)
        {
            ui = Instantiate(ui);
            ui.GetComponent<AbilityMeterUI>().meter = this;
        }
    }

    public void StartCharging()
    {
        Debug.Assert(IsCharging == false);
        IsCharging = true;
    }

    void Update ()
    {
	    if(IsCharging)
        {
            Amount += ChargeRate * Time.deltaTime;
        }
        else
        {
            Amount += RechargeRate * Time.deltaTime;
        }
        Amount = Mathf.Clamp01(Amount);
    }

    // Returns how much the meter has charged [0..1] if it is at least MinCharge
    public float EndCharging()
    {
        Debug.Assert(IsCharging == true);
        IsCharging = false;
        if (Amount < MinCharge)
            return 0.0f;
        else
            return Amount;
    }

    private void OnDestroy()
    {
        Destroy(ui);
    }
}
