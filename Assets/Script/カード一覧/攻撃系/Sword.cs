using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AttackSpell
{
    public Sword(int cost, int rarity, int damage,string cardName) : base(cost, rarity, damage,cardName)
    {

    }

    public override void Use(Enemy enemy)
    {
        base.Use(enemy);
        //使用時効果
        enemy.TakeDamage(Damage);
    }
}
