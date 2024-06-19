using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleArrow : AttackSpell
{
    [SerializeField]
    float delay = 1f;
    public DoubleArrow(int cost, int rarity, int damage, string cardName) : base(cost, rarity, damage, cardName)
    {

    }

    public override void Use(List<Enemy> enemy)
    {
        base.Use(enemy);
        //2âÒçUåÇ
        StartCoroutine(base.AttackMultiTime(delay, 2, enemy[0]));
    }
    
}
