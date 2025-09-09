using NUnit.Framework;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoint;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    void Awake()
    {
        spawnList = new List<Spawn>();

        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB" };

        ReadSpawnFile();
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

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
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

            case "B":
                enemyIndex = 3;
                break;

        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoint[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;
        float speed = enemyLogic.speed * Player.focusOrigin;

        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.linearVelocity = new Vector2(-speed, -1 * Player.focusOrigin);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.linearVelocity = new Vector2(speed, -1 * Player.focusOrigin);
        }
        else
        {
            rigid.linearVelocity = new Vector2(0, -speed);
        }

        //����Ʈ �ε��� ����
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        //���� ������ ������ ����
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    public void CallDie(Vector3 pos, string type)
    {
        GameObject die = objectManager.MakeObj("Die");
        Die dieLogic = die.GetComponent<Die>();

        die.transform.position = pos;
        dieLogic.StartDie(type);
    }
}