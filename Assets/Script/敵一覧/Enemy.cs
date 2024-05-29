using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public int maxHP = 100; // 最大HP
    private int currentHP; // 現在のHP

    public Text hpText; // HPを表示するテキスト

    public int maxAttackCount = 1;
    private int attackCount;

    void Start()
    {
        attackCount = maxAttackCount;
        currentHP = maxHP; // 最大HPで初期化
        UpdateHPText(); // HPテキストを更新
    }

    // HPを減らすメソッド
    public void TakeDamage(int damage)
    {
        currentHP -= damage; // ダメージを適用
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HPが0未満にならないようにする
        UpdateHPText(); // HPテキストを更新

        // HPが0になったら敵を破壊するなどの処理を追加する
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    // HPテキストを更新するメソッド
    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP.ToString(); // HPテキストを更新
        }
    }
    public virtual void Attack()
    {

    }
    public virtual void StepCount()
    {
        attackCount--;
        if (attackCount <= 0)
        {
            Attack();
            attackCount = maxAttackCount;
        }
    }
}
