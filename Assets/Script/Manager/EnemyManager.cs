using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    [SerializeField]
    public List<Enemy> stageEnemies;
    private List<Enemy> currentEnemies;
    public GameObject enemyPool;

    private void Start()
    {
        currentEnemies = stageEnemies;
        foreach (Enemy enem in currentEnemies)
        {
            enem.InitStatus();
            GameObject instance = Instantiate(enem.gameObject);
            instance.gameObject.transform.parent = enemyPool.transform;
        }
    }
    public void AttackEnemies()
    {
        foreach (Enemy enemy in currentEnemies)
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
