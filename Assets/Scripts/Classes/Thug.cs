using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thug : Character {

    private float tertiaryForce = 20f;

    private ObjectPooler pooler;
    private GameObject shotgun;
    private GameObject pistol;
    private GameObject crosshair;
    private Weapon shotgunWeaponScript;
    private Weapon pistolWeaponScript;
    private Weapon currentWeapon;

    // Use this for initialization
    void Start () {
        //Entity data
        hp = 200f;
        speed = 15.0f;
        gravity = -9.8f;

        //Character data
        jumpStrength = 20f;
        jumpGravity = 25f;

        //Thug data
        pooler = GameObject.Find("/World/Object Pooler").GetComponent<ObjectPooler>();
        shotgun = transform.Find("Shotgun").gameObject;
        shotgunWeaponScript = shotgun.GetComponent<Weapon>();
        pistol = transform.Find("Pistol").gameObject;
        pistolWeaponScript = pistol.GetComponent<Weapon>();
        crosshair = transform.parent.parent.Find("Canvas").Find("Crosshair").gameObject;
        currentWeapon = shotgunWeaponScript;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Activate the character's primary ability
    public override void Primary()
    {
        currentWeapon.Primary();
    }

    //Activate the character's secondary ability
    public override void Secondary()
    {
        AnimatorStateInfo stateInfo = currentWeapon.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.fullPathHash == currentWeapon.idleStateHash)
        {
            if(currentWeapon == shotgunWeaponScript)
            {
                currentWeapon = pistolWeaponScript;
                shotgun.SetActive(false);
                pistol.SetActive(true);
            }
            else
            {
                currentWeapon = shotgunWeaponScript;
                pistol.SetActive(false);
                shotgun.SetActive(true);
            }
        }

        crosshair.GetComponent<Image>().sprite = currentWeapon.crosshair;
    }

    //Activate the character's tertiary ability
    public override void Tertiary()
    {
        GameObject pipeBomb = pooler.SpawnFromPool("Pipe Bomb", transform.position, transform.rotation);
        Rigidbody rb = pipeBomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * tertiaryForce, ForceMode.VelocityChange); //Using transform.right because thug is incorrectly rotated by default; using ForceMode.VelocityChange so velocity is not constant
    }

    //Activate the character's melee ability
    public override void Melee()
    {
        currentWeapon.Melee();
    }
}
