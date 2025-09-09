using UnityEngine;

public class Die : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StartDie(string target)
    {
        anim.SetTrigger("Die");

        switch (target)
        {
            case "S":
                transform.localScale = Vector3.one * 0.7f;
                break;
            case "M":
            case "P":
                transform.localScale = Vector3.one * 1f;
                break;
            case "L":
                transform.localScale = Vector3.one * 2f;
                break;
            case "B" +
            "":
                transform.localScale = Vector3.one * 3f;
                break;
        }
    }
}
