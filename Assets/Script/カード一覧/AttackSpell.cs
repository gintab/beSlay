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
    protected virtual void Attack(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(Damage);
        }
    }
    /// <summary>
    /// �S�̍U��
    /// </summary>
    protected virtual void AttackAll()
    {
        List<Enemy> Enemies = EnemyManager.Instance.GetAllEnemies(); //�S�̍U��
        foreach (var enemy in Enemies)
        {
            enemy.TakeDamage(Damage);
        }
    }
    /// <summary>
    /// �����U��
    /// </summary>
    /// <param name="time">�U����</param>
    /// <param name="enemy">�U���Ώ�</param>
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
