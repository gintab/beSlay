using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpell : Card
{
    [SerializeField]
    public int Damage; // 攻撃力
    public AttackSpell(int cost, int rarity, int damage, string cardName) : base(cost, CardType.Attack, rarity, cardName)
    {
        Damage = damage;
    }
    // カードの情報を表示するメソッド（オーバーライド）
    public new void ShowInfo()
    {
        Debug.Log($"攻撃カード情報 - タイプ: {Type}, コスト: {Cost}, レア度: {Rarity}, 攻撃力: {Damage}");
    }
}
