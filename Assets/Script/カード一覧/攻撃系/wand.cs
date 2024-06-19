using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : AttackSpell
{
    public Wand(int cost, int rarity, int damage,string cardName) : base(cost, rarity, damage,cardName)
    {

    }

    public override void Use(List<Enemy> enemy)
    {
        base.Use(enemy);
        //使用時効果
        base.Attack(enemy);
    }
}
