using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageData
{
    public float amount;
    public float knockbackAmount;
    public GameObject damagingObject;
    public Vector3 hitPoint;

    public DamageData(float amount, float knockbackAmount, GameObject damagingObject, Vector3 hitPoint)
    {
        this.amount = amount;
        this.knockbackAmount = knockbackAmount;
        this.damagingObject = damagingObject;
        this.hitPoint = hitPoint;
    }
}
