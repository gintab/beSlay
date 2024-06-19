using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    [SerializeField]
    List<Enemy> stageEnemies;
    private List<Enemy> currentEnemies;

    private void Start()
    {
        currentEnemies = stageEnemies;
    }
    public void AttackEnemies()
    {
        foreach (var enemy in currentEnemies)
        {
            enemy.Attack();
        }
    }
    public List<Enemy> GetAllEnemies()
    {
        return currentEnemies;
    }
    public Enemy GetRandomEnemy()
    {
        return AllManager.GetRandom(currentEnemies);
    }
}
