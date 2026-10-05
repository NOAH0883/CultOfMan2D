using UnityEngine;

public class PlayerVisualsScript : MonoBehaviour
{
    public bool lookingUp;
    public bool moving;
    public bool rolling;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("lookingUp", lookingUp);
        anim.SetBool("moving", moving);
        anim.SetBool("rolling", rolling);
    }
}
