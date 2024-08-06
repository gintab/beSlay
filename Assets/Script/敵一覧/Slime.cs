using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    protected override void Start()
    {
        base.Start();
    }
    public override void Attack()
    {
        base.Attack();
        Player.Instance.ChangeHP(-CurrentDamage);
    }
}
