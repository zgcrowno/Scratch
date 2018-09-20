using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static float RegularHeight = 4f;
    public static float CrouchHeight = 1f;
    public static float WalkingSpeed = 5f;
    public static float CrouchingSpeed = 8f;

    private CharacterController charCont;
    private Character currentCharacter;
    private float verticalVelocity;

    public GameObject[] characterArray;

    // Use this for initialization
    void Start () {
        charCont = GetComponent<CharacterController>();
        currentCharacter = characterArray[Character.Thug].GetComponent<Thug>();
        currentCharacter.gameObject.SetActive(true);
        verticalVelocity = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        //Detect primary ability status
        if (Input.GetButtonDown("Fire1"))
        {
            Primary();
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            Secondary();
        }
        else if(Input.GetButtonDown("Melee"))
        {
            Melee();
        }
    
        Move();
        Jump();
        Crouch();
        Walk();
    }

    void Move()
    {
        float movementSpeed = GetMovementSpeed();

        float deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, movementSpeed); //Limits the max speed of the player

        movement.y = currentCharacter.gravity;

        movement *= Time.deltaTime; //Ensures the speed at which the player moved does not change based on frame rate
        movement = transform.TransformDirection(movement);
        charCont.Move(movement);
    }

    void Jump()
    {
        if (charCont.isGrounded)
        {
            verticalVelocity = -currentCharacter.jumpGravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = currentCharacter.jumpStrength;
            }
        }
        else
        {
            verticalVelocity -= currentCharacter.jumpGravity * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        charCont.Move(jumpVector * Time.deltaTime);
    }

    void Crouch()
    {
        float deltaTimeMultiplier = 20f;

        //User linear interpolation here to smooth out crouching animation
        if (Input.GetButton("Crouch"))
        {
            charCont.height = Mathf.Lerp(charCont.height, CrouchHeight, deltaTimeMultiplier * Time.deltaTime);
        }
        else
        {
            charCont.height = Mathf.Lerp(charCont.height, RegularHeight, deltaTimeMultiplier * Time.deltaTime);
        }
    }

    void Walk()
    {

    }

    //Activate the currentCharacter's primary ability
    void Primary()
    {
        currentCharacter.Primary();
    }

    //Activate the currentCharacter's secondary ability
    void Secondary()
    {
        currentCharacter.Secondary();
    }

    //Activate the currentCharacter's tertiary ability
    void Tertiary()
    {
        currentCharacter.Tertiary();
    }

    //Activate the currentCharacter's melee ability
    void Melee()
    {
        currentCharacter.Melee();
    }

    float GetMovementSpeed()
    {
        float movementSpeed = 0;
        if (Input.GetButton("Crouch"))
        {
            movementSpeed = CrouchingSpeed;
        }
        else if (Input.GetButton("Walk"))
        {
            movementSpeed = WalkingSpeed;
        }
        else
        {
            movementSpeed = currentCharacter.speed;
        }
        return movementSpeed;
    }
}
