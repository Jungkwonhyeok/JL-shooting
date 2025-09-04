using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoint;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public ObjectManager objectManager;

    void Awake()
    {
        enemyObjs = new string[] {"EnemyS", "EnemyM" , "EnemyL" };
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoint[ranPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        if (ranPoint == 5 || ranPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.linearVelocity = new Vector2(enemyLogic.speed*(-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.linearVelocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.linearVelocity =new Vector2(0, enemyLogic.speed*(-1));
        }
    }
}