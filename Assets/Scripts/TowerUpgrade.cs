using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade
{
    public int Price { get; private set; }

    public int Damage { get; private set; }

    public float DebuffDuration { get; private set; }
    public float ProcChance { get; private set; }

    public float SlowingFactor { get; private set; }

    public int SpecialDamage { get; private set; }

    public TowerUpgrade(int price, int damage, float debuffduration, float procChance, float slowingFactor, int specialDamage)
    {
        this.Price = price;
        this.Damage = damage;
        this.DebuffDuration = debuffduration;
        this.ProcChance = procChance;
        this.SlowingFactor = slowingFactor;
        this.SpecialDamage = specialDamage;
    }
}