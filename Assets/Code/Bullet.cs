using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public bool isRotate;

    void Update()
    {
        if (isRotate)
            transform.Rotate(Vector3.forward * 10);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "EnemyBullet")
            if (collision.gameObject.tag == "BorderBullet" || collision.gameObject.tag == "Player")
            {
                gameObject.SetActive(false);
            }
        else if (gameObject.tag == "PlayerBullet")
            if (collision.gameObject.tag == "BorderBullet")
            {
                gameObject.SetActive(false);
            }
    }
}