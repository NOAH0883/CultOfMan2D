using UnityEngine;

public class PlayerVisualsScript : MonoBehaviour
{
    public bool lookingUp;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("lookingUp", lookingUp);
    }
}
