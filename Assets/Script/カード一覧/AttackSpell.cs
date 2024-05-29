using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpell : Card
{
    [SerializeField]
    public int Damage; // �U����
    public AttackSpell(int cost, int rarity, int damage, string cardName) : base(cost, CardType.Attack, rarity, cardName)
    {
        Damage = damage;
    }
    // �J�[�h�̏���\�����郁�\�b�h�i�I�[�o�[���C�h�j
    public new void ShowInfo()
    {
        Debug.Log($"�U���J�[�h��� - �^�C�v: {Type}, �R�X�g: {Cost}, ���A�x: {Rarity}, �U����: {Damage}");
    }
}
