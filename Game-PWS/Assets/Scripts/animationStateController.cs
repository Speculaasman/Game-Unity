using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float deceleration = 0.5f;
    int VelocityHash;
 

    // Start is called before the first frame update
    void Start()
    {
        //set refernce for animator
        animator = GetComponent<Animator>();

        // increases performance
        VelocityHash = Animator.StringToHash("Velocity");
    
    }

    // Update is called once per frame
    void Update()
    {
        // get key input
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed && velocity < 1.0f) { 
            velocity += Time.deltaTime * acceleration;
            }

        animator.SetFloat(VelocityHash, velocity);

        if (!forwardPressed && velocity > 0.0f) 
        {
            velocity -= Time.deltaTime * deceleration;
        }

        if (!forwardPressed && velocity < 0.0f) {
            velocity = 0.0f;
        }

    }
}
