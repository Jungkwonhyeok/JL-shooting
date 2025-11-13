using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;

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

    public float boomCooldown;
    public float focusCooldown;

    public bool isFocus;
    public float focusOrigin = 1f;
    public float focusTime = 3f;
    public float focusSlow = 0.5f;
    public int BoomCount = 3;
    public int FocusCount = 5;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject bulletObjC;
    public GameObject bulletObjD;
    public GameObject shieldEffect;

    public GameManager gameManager;
    public ObjectManager objectManager;

    float time;
    bool shield;
    bool isFocusing;
    float lastBoomTime;
    float lastFocusTime;

    Animator anim;

    void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        health = maxhealth;
        lastBoomTime = -boomCooldown;
        lastFocusTime = -focusCooldown;
    }


    void Update()
    {
        time += Time.deltaTime;
        gameTime = maxgameTime - time;
        Move();
        Fire();
        Reload();
        if (health >= maxhealth)
        {
            health = maxhealth;
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time - lastBoomTime >= boomCooldown && BoomCount != 0 && gameManager.GamePlay == true)
        {
            BoomCount--;
            Boom();
            lastBoomTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.E) && !isFocusing && Time.time - lastFocusTime >= focusCooldown && FocusCount != 0 && gameManager.GamePlay == true)
        {
            FocusCount--;
            StartCoroutine(FocusMode());
            lastFocusTime = Time.time;
        }

    }
    public float BoomCoolcnt()
    {
        float passedTime = Time.time - lastBoomTime;
        return Mathf.Clamp(boomCooldown - passedTime, 0, boomCooldown);
    }

    public float FocusCoolcnt()
    {
        float passedTime = Time.time - lastFocusTime;
        return Mathf.Clamp(focusCooldown - passedTime, 0, focusCooldown);
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

    void LBulletAudio()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LBullet);
    }
    void SBulletAudio()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.SBullet);
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
                SBulletAudio();
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 2:
                SBulletAudio();
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 3:
                SBulletAudio();
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;
                LBulletAudio();
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;

                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 4:
                SBulletAudio();
                GameObject bulletRRR = objectManager.MakeObj("BulletPlayerC");
                bulletRRR.transform.position = transform.position + Vector3.right * 0.35f;
                LBulletAudio();
                GameObject bulletCCC = objectManager.MakeObj("BulletPlayerD");
                bulletCCC.transform.position = transform.position;
                GameObject bulletLLL = objectManager.MakeObj("BulletPlayerC");
                bulletLLL.transform.position = transform.position + Vector3.left * 0.35f;

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
                    health = 0;
                    break;
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (shield == true)
                return;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
            health -= Time.deltaTime * 10;
            anim.SetTrigger("OnHit");
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            if (shield == true)
                return;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            health -= bullet.dmg;
            anim.SetTrigger("OnHit");
        }

        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Power":
                    if (power == maxPower)
                        score += 500;
                    else
                        power++;
                    break;
                case "HP":
                    if (health == maxhealth)
                        return;
                    else
                        health += 7;
                    break;
                case "Shield":
                    OnShield();
                    break;
                case "Time":
                    maxgameTime += 20;
                    break;
            }
            collision.gameObject.SetActive(false); ;
        }
        if (health <= 0 || gameTime <= 0)
        {
            if  (GameDataManager.instance != null)
            {
                GameDataManager.instance.SaveScore(score);
            }
            gameManager.GameOver();
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

    void Boom()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < enemies.Length; index++)
        {
            Enemy enemyLogic = enemies[index].GetComponent<Enemy>();
            enemyLogic.OnHit(100);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int index = 0; index < bullets.Length; index++)
        {
            bullets[index].SetActive(false);
        }
    }

    IEnumerator FocusMode()
    {
        isFocusing = true;
        isFocus = true;
        focusOrigin = focusSlow;

        // 이미 있는 애들 느리게 (곱하기)
        ApplyFocusToExistingMultiply(focusSlow);

        yield return new WaitForSeconds(focusTime);

        // 복구 (나누기)
        ApplyFocusToExistingDivide(focusSlow);

        isFocus = false;
        focusOrigin = 1f;
        isFocusing = false;
    }

    void ApplyFocusToExistingMultiply(float origin)
    {
        if (origin <= 0f) return;

        foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Rigidbody2D rb = e.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity *= origin;
        }

        foreach (var b in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity *= origin;
        }
    }

    void ApplyFocusToExistingDivide(float origin)
    {
        if (origin <= 0f) return;

        foreach (var e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Rigidbody2D rb = e.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity /= origin;
        }

        foreach (var b in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity /= origin;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.linearVelocity = Vector2.zero;
        rigid.angularVelocity = 0f;
    }
}