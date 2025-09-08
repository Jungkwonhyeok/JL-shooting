using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemHPPrefab;
    public GameObject itemTimePrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemShieldPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletPlayerCPrefab;
    public GameObject bulletPlayerDPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;
    public GameObject diePrefab;

    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemHP;
    GameObject[] itemTime;
    GameObject[] itemPower;
    GameObject[] itemShield;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletPlayerC;
    GameObject[] bulletPlayerD;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;
    GameObject[] die;

    GameObject[] targetPool;

    void Awake()
    {
        enemyB = new GameObject[10];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemHP = new GameObject[20];
        itemTime = new GameObject[20];
        itemPower = new GameObject[10];
        itemShield = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletPlayerC = new GameObject[100];
        bulletPlayerD = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[500];
        bulletBossA = new GameObject[50];
        bulletBossB = new GameObject[1000];
        die = new GameObject[30];

        Generate();
    }

    void Generate()
    {
        //Enemy
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }

        for (int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }


        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);

        }


        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }

        //Item
        for (int index = 0; index < itemHP.Length; index++)
        {
            itemHP[index] = Instantiate(itemHPPrefab);
            itemHP[index].SetActive(false);
        }

        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }

        for (int index = 0; index < itemShield.Length; index++)
        {
            itemShield[index] = Instantiate(itemShieldPrefab);
            itemShield[index].SetActive(false);
        }

        for (int index = 0; index < itemTime.Length; index++)
        {
            itemTime[index] = Instantiate(itemTimePrefab);
            itemTime[index].SetActive(false);
        }



        //Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerC.Length; index++)
        {
            bulletPlayerC[index] = Instantiate(bulletPlayerCPrefab);
            bulletPlayerC[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerD.Length; index++)
        {
            bulletPlayerD[index] = Instantiate(bulletPlayerDPrefab);
            bulletPlayerD[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);

        }

        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);
        }


        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }


        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);
        }

        for (int index = 0; index < die.Length; index++)
        {
            die[index] = Instantiate(diePrefab);
            die[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {

        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "ItemHP":
                targetPool = itemHP;
                break;
            case "ItemTime":
                targetPool = itemTime;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemShield":
                targetPool = itemShield;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletPlayerC":
                targetPool = bulletPlayerC;
                break;
            case "BulletPlayerD":
                targetPool = bulletPlayerD;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "Die":
                targetPool = die;
                break;

        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}