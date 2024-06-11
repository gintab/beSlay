using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public override void Attack()
    {
        base.Attack();
        Player.Instance.ChangeHP(-currentDamage);
    }
}
