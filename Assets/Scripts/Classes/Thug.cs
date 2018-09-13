using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug : Character {

    public GameObject bulletSpawn;
    public GameObject[] bulletPrefabArray;
    public ParticleSystem muzzleFlash;

    // Use this for initialization
    void Start () {
        hp = 200f;
        speed = 15.0f;
        gravity = -9.8f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Activate the character's primary ability
    public override void Primary()
    {
        muzzleFlash.Play();
        for (int i = 0; i < bulletPrefabArray.Length; i++)
        {
            bulletPrefabArray[i] = Instantiate(bulletPrefabArray[i], bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bulletPrefabArray[i].SetActive(true);
        }
    }

    //Activate the character's secondary ability
    public override void Secondary()
    {

    }

    //Activate the character's tertiary ability
    public override void Tertiary()
    {

    }

    //Activate the character's melee ability
    public override void Melee()
    {

    }
}
