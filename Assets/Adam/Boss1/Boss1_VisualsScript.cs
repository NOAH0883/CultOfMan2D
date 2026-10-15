using UnityEngine;

public class Boss1_VisualsScript : MonoBehaviour
{


    private Animator anim;
    public bool moving;
    public bool charging;
    public bool swiping;
    public bool slamming;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("moving", moving);
        anim.SetBool("charging", charging);
        anim.SetBool("swiping", swiping);
        anim.SetBool("slamming", slamming);
    }
}
