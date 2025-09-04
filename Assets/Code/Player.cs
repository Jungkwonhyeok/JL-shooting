using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{

    public bool isTouchTop;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isTouchBottom;

    public int score;
    public float speed;

    public int power;
    public int maxPower;

    public float maxhealth;
    public float health;

    public float gameTime;
    public float maxgameTime;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject bulletObjC;
    public GameObject bulletObjD;
    public GameObject shieldEffect;
    public GameManager manager;

    bool shield;

    void Start()
    {
        health = maxhealth;
    }
    void Update()
    {
        gameTime += Time.deltaTime;
        Move();
        Fire();
        Reload();
        if (health >= maxhealth)
        {
            health = maxhealth;
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0).normalized * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;
        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjA, transform.position + Vector3.left * 0.35f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 4:
                GameObject bulletRRR = Instantiate(bulletObjC, transform.position + Vector3.right * 0.35f, transform.rotation);
                GameObject bulletLLL = Instantiate(bulletObjD, transform.position, transform.rotation);
                GameObject bulletCCC = Instantiate(bulletObjC, transform.position + Vector3.left * 0.35f, transform.rotation);
                Rigidbody2D rigidRRR = bulletRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCC = bulletCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLL = bulletLLL.GetComponent<Rigidbody2D>();
                rigidRRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }



        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (shield == true)
                return;
            health -= Time.deltaTime * 10;
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            if (shield == true)
                return;
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            health -= bullet.dmg;
        }

        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Power":
                    if (power == maxPower)
                        score += 1;
                    else
                        power++;
                    break;
                case "HP":
                    if (health == maxhealth)
                        return;
                    else
                        health += 5;
                    break;
                case "Shield":
                    OnShield();
                    break;
                case "Time":
                    maxgameTime += 20;
                    break;
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
            }
        }
    }

    void OnShield()
    {
        shield = true;
        shieldEffect.SetActive(true);
        Invoke("OffshieldEffect", 5f);
    }

    void OffshieldEffect()
    {
        shieldEffect.SetActive(false);
        shield = false;
    }
}