using NUnit.Framework;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoint;

    public float maxSpawnDelay;
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    void Awake()
    {
<<<<<<< Updated upstream
        enemyObjs = new string[] {"EnemyS", "EnemyM" , "EnemyL", "EnemyB" };
=======
        spawnList = new List<Spawn>();
        enemyObjs = new string[] {"EnemyS", "EnemyM" , "EnemyL" };
        ReadSpawnFile();
>>>>>>> Stashed changes
    }

    void ReadSpawnFile()
    {
        //���� �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //���� ���� �б�
        TextAsset textFile = Resources.Load("Stage 1") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        //�� �پ� ������ ����
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            

            if (line == null)
                break;

            //������ ������ ����
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        //�ؽ�Ʈ ���� �ݱ�
        stringReader.Close();

        //ó����° ���� ������ ����
        nextSpawnDelay = spawnList[0].delay;

    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
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
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoint[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (ranPoint == 5 || ranPoint == 6)
        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.linearVelocity = new Vector2(enemyLogic.speed*(-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.linearVelocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.linearVelocity =new Vector2(0, enemyLogic.speed*(-1));
        }

        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
}