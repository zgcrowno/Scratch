using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController charCont;
    private Character currentCharacter;

    public GameObject[] characterArray;

    // Use this for initialization
    void Start () {
        charCont = GetComponent<CharacterController>();
        currentCharacter = characterArray[Character.Thug].GetComponent<Thug>();
        currentCharacter.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Primary();
        }
        Move();
	}

    void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * currentCharacter.speed;
        float deltaZ = Input.GetAxis("Vertical") * currentCharacter.speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, currentCharacter.speed); //Limits the max speed of the player

        movement.y = currentCharacter.gravity;

        movement *= Time.deltaTime; //Ensures the speed at which the player moved does not change based on frame rate
        movement = transform.TransformDirection(movement);
        charCont.Move(movement);
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
}
