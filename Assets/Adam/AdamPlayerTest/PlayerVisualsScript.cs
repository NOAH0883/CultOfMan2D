using UnityEngine;

public class PlayerVisualsScript : MonoBehaviour
{
    public bool lookingUp;
    public bool moving;
    public bool rolling;
    public float lookValue;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetFloat("lookValue", (lookValue + 180) / 360);
        anim.SetBool("lookingUp", lookingUp);
        anim.SetBool("moving", moving);
        anim.SetBool("rolling", rolling);
    }
}
