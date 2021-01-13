using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public bool run;
    public bool crouch;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        run = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(crouch)
            run = false;
        
        anim.SetBool("run", run);
        anim.SetBool("crouch", crouch);
    }
}
