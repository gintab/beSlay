using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : AttackSpell
{
    public Whip(int cost, int rarity, int damage, string cardName) : base(cost, rarity, damage, cardName)
    {

    }
    public override void Select()
    {
        base.Select();
        //実行するか確認処理
        AttackAll(); //全体攻撃
    }
    protected override void AttackAll()
    {
        base.AttackAll();
    }
}

