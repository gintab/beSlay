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
        //���s���邩�m�F����
        AttackAll(); //�S�̍U��
    }
    protected override void AttackAll()
    {
        base.AttackAll();
    }
}

