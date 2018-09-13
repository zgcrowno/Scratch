using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Entity {

    public static readonly int Thug = 0;
    public static readonly int Scrapper = 1;
    public static readonly int Downer = 2;
    public static readonly int Audiophile = 3;
    public static readonly int Hermit = 4;
    public static readonly int Wizard = 5;
    public static readonly int Bear = 6;
    public static readonly int Sage = 7;
    public static readonly int Illusionist = 8;
    public static readonly int Berserker = 9;

    public float jumpHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Activate the character's primary ability
    public abstract void Primary();

    //Activate the character's secondary ability
    public abstract void Secondary();

    //Activate the character's tertiary ability
    public abstract void Tertiary();

    //Activate the character's melee ability
    public abstract void Melee();
}
