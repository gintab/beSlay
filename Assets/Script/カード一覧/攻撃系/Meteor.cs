using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : AttackSpell
{
    public Meteor(int cost, int rarity, int damage,string cardName) : base(cost, rarity, damage,cardName)
    {

    }

    public override void Use(Enemy enemy)
    {
        base.Use(enemy);
        //Žg—pŽžŒø‰Ê
        enemy.TakeDamage(Damage);
    }
}
