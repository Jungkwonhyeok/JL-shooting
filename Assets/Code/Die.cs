using UnityEngine;

public class Die : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Invoke("Disable", 0.5f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
    public void StartDie(string target)
    {
        anim.SetTrigger("Die");

        switch (target)
        {
            case "S":
                transform.localScale = Vector3.one * 0.6f;
                break;
            case "M":
            case "P":
                transform.localScale = Vector3.one * 0.9f;
                break;
            case "L":
                transform.localScale = Vector3.one * 1.2f;
                break;
        }
    }
}