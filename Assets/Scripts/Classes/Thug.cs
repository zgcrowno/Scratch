using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thug : Character {

    public static int NumShotgunBullets = 6;
    public static int NumPistolBullets = 1;
    
    private float shotgunFireRate = 1f;
    private float pistolFireRate = 0.25f;
    private float shotgunRange = 20f;
    private float pistolRange = 30f;

    private bool primaryEquipped; //Used to switch between primary and secondary weapons
    private float nextFire; //The time at which the player will be able to fire again after firing
    private Camera weaponCam;
    private GameObject shotgun;
    private GameObject pistol;
    private GameObject crosshair;

    public Sprite shotgunCrosshair;
    public Sprite pistolCrosshair;
    public GameObject bulletSpawnShotgun;
    public GameObject bulletSpawnPistol;
    public GameObject[] bulletPrefabArray;
    public ParticleSystem muzzleFlashShotgun;
    public ParticleSystem muzzleFlashPistol;

    // Use this for initialization
    void Start () {
        hp = 200f;
        speed = 15.0f;
        gravity = -9.8f;
        jumpStrength = 20f;
        jumpGravity = 25f;
        primaryEquipped = true;
        shotgun = transform.Find("Shotgun").gameObject;
        pistol = transform.Find("Pistol").gameObject;
        crosshair = transform.parent.parent.Find("Canvas").Find("Crosshair").gameObject;
        weaponCam = transform.parent.GetComponentInParent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Activate the character's primary ability
    public override void Primary()
    {
        if(primaryEquipped)
        {
            FireGun(shotgunFireRate, shotgunRange, muzzleFlashShotgun, NumShotgunBullets, bulletSpawnShotgun);
        }
        else
        {
            FireGun(pistolFireRate, pistolRange, muzzleFlashPistol, NumPistolBullets, bulletSpawnPistol);
        }
    }

    //Activate the character's secondary ability
    public override void Secondary()
    {
        primaryEquipped = !primaryEquipped;
        
        if(primaryEquipped)
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);

            crosshair.GetComponent<Image>().sprite = shotgunCrosshair;
        }
        else
        {
            shotgun.SetActive(false);
            pistol.SetActive(true);

            crosshair.GetComponent<Image>().sprite = pistolCrosshair;
        }
    }

    //Activate the character's tertiary ability
    public override void Tertiary()
    {

    }

    //Activate the character's melee ability
    public override void Melee()
    {

    }

    public void FireGun(float fireRate, float fireRange, ParticleSystem muzzleFlash, int numBullets, GameObject bulletSpawn)
    {
        if (Time.time > nextFire) //Ensure enough time has passed to allow for further firing
        {
            nextFire = Time.time + fireRate;

            muzzleFlash.Play();

            for (int i = 0; i < numBullets; i++)
            {
                Vector3 direction = weaponCam.transform.forward;
                if (primaryEquipped)
                {
                    //Modify these values for bullet spread
                    direction.x += Random.Range(-.04f, .04f);
                    direction.y += Random.Range(-.04f, .04f);
                    direction.z += Random.Range(-.04f, .04f);
                }

                Vector3 rayOrigin = weaponCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //The center of the camera
                RaycastHit hit; //Will hold hit data from the raycast

                bulletPrefabArray[i] = Instantiate(bulletPrefabArray[i], bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bulletPrefabArray[i].SetActive(true);

                if (Physics.Raycast(rayOrigin, direction, out hit, fireRange)) //Ray has hit an object in its range
                {
                    bulletPrefabArray[i].GetComponent<Transform>().LookAt(hit.point);

                    //Uncomment if I want to add force to shot
                    //if(hit.rigidbody != null)
                    //{
                    //    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    //}
                }
                else //Ray hasn't hit an object
                {
                    bulletPrefabArray[i].GetComponent<Transform>().LookAt(rayOrigin + (direction * fireRange));
                }
            }
        } 
    }
}
