using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{

    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    [SerializeField] float acceleration = 2.0f;
    [SerializeField] float deceleration = 2.0f;
    [SerializeField] float maximumWalkVelocity = 0.5f;
    [SerializeField] float maximumRunVelocity = 2.0f;

    // increase performance
    int VelocityZHash;
    int VelocityXHash;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // increase performance
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

   


    // Update is called once per frame
    void Update()
    {
        // get key input from player
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);




        float currentMaxVelocity  = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        if (forwardPressed && velocityZ < currentMaxVelocity) {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity)
            velocityX -= Time.deltaTime * acceleration;
        {

        if (rightPressed && velocityX < currentMaxVelocity) {
                velocityX += Time.deltaTime * acceleration;    
        }

            if (!forwardPressed && velocityZ > 0.0f) {
                velocityZ = 0.0f;
            }

            if (!leftPressed && velocityX < 0.0f) {
                velocityX += Time.deltaTime * deceleration;
            }

            if (!rightPressed && velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }

            if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX >- 0.05f && velocityX  < 0.05f)) { 
            velocityX = 0.0f;
            }

            if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
            {
                velocityZ = currentMaxVelocity;
            }
            else if (forwardPressed && velocityZ > currentMaxVelocity)
            {
                velocityZ -= Time.deltaTime * deceleration;
                if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f)) {
                    velocityZ = currentMaxVelocity;
                }
            }
            else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f)) {
                velocityZ = currentMaxVelocity;
            }

            // locking right
            if (rightPressed && runPressed && velocityX > currentMaxVelocity)
            {
                velocityX = currentMaxVelocity;
            }
            // decelerate to maximum walk velocity
            else if (rightPressed && velocityX > currentMaxVelocity)
            {
                velocityX -= Time.deltaTime * deceleration;
                // round to the currentMaxVelocity if within offset
                if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
                {
                    velocityX = currentMaxVelocity;
                }
            }
            // round to the currentMaxVelocity if within offset
            else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
            {
                velocityX = currentMaxVelocity;
            }

            // locking left
            if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
            {
                velocityX = -currentMaxVelocity;
            }
            // decelerate to maximum walk velocity
            else if (leftPressed && velocityX < -currentMaxVelocity)
            {
                velocityX += Time.deltaTime * deceleration;
                // round to the currentMaxVelocity if within offset
                if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
                {
                    velocityX = -currentMaxVelocity;
                }
            }
            // round to the currentMaxVelocity if within offset
            else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }

            animator.SetFloat(VelocityZHash, velocityZ);
            animator.SetFloat(VelocityXHash, velocityX);


        }
    }
}
