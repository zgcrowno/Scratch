using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public int primaryHash = Animator.StringToHash("Primary");
    public int secondaryHash = Animator.StringToHash("Secondary");
    public int tertiaryHash = Animator.StringToHash("Tertiary");
    public int meleeHash = Animator.StringToHash("Melee");
    public int primaryStateHash = Animator.StringToHash("Base Layer.Fire");
    public int meleeStateHash = Animator.StringToHash("Base Layer.Melee");
    public int idleStateHash = Animator.StringToHash("Base Layer.Idle");

    public Animator animator;
    public Camera weaponCam;
    public Sprite crosshair;

	// Use this for initialization
	protected void Start () {
        animator = GetComponent<Animator>();
        weaponCam = transform.parent.parent.GetComponentInParent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Activate the weapon's primary ability
    public abstract void Primary(ObjectPooler pooler);

    //Activate the weapon's secondary ability
    public abstract void Secondary();

    //Activate the weapon's tertiary ability
    public abstract void Tertiary();

    //Activate the weapon's melee ability
    public abstract void Melee(ObjectPooler pooler);
}
