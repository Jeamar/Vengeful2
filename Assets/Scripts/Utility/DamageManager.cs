using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private UnitMain unit;

    void Start()
    {
        unit = GetComponent<UnitMain>();
    }

    public void takeDmg(float dmg)
    {
        unit.currentHP -= dmg;
    }
}
