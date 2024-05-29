using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField]
    int attackDamage = 3;
    public override void Attack()
    {
        base.Attack();
        Player.Instance.ChangeHP(-attackDamage);
    }
}
