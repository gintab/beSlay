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
    protected virtual void Attack(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(Damage);
        }
    }
    /// <summary>
    /// 全体攻撃
    /// </summary>
    protected virtual void AttackAll()
    {
        List<Enemy> Enemies = EnemyManager.Instance.GetAllEnemies(); //全体攻撃
        foreach (var enemy in Enemies)
        {
            enemy.TakeDamage(Damage);
        }
    }
    /// <summary>
    /// 複数攻撃
    /// </summary>
    /// <param name="time">攻撃回数</param>
    /// <param name="enemy">攻撃対象</param>
    protected virtual IEnumerator AttackMultiTime(float delay,int time,Enemy enemy)
    {
        for (int i = 0; i < time; i++)
        {
            yield return new WaitForSeconds(delay);
            enemy.TakeDamage(Damage);
        }
    }
    protected virtual void AttackRandom()
    {
        EnemyManager.Instance.GetRandomEnemy().TakeDamage(Damage);
    }
}
