using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

    public static readonly int NumBullets = 6;

    public float primaryDamage = 10f;
    public float primaryRange = 20f;
    public float meleeDamage = 25f;
    public float meleeRange = 2.5f;

    public GameObject bulletSpawn;
    public GameObject bulletTrail;
    public ParticleSystem muzzleFlash;

    // Use this for initialization
    new void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Primary(ObjectPooler pooler)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.fullPathHash == idleStateHash)
        {
            animator.SetTrigger(primaryHash); //Play animation for firing shotgun

            muzzleFlash.Play();

            for (int i = 0; i < NumBullets; i++)
            {
                Vector3 direction = weaponCam.transform.forward;
       
                //Modify these values for bullet spread
                direction.x += Random.Range(-.08f, .08f);
                direction.y += Random.Range(-.08f, .08f);
                direction.z += Random.Range(-.08f, .08f);
                //End bullet spread modification

                Vector3 rayOrigin = weaponCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //The center of the camera
                RaycastHit hit; //Will hold hit data from the raycast

                bulletTrail = pooler.SpawnFromPool("Bullet Trail", bulletSpawn.transform.position, bulletSpawn.transform.rotation);

                if (Physics.Raycast(rayOrigin, direction, out hit)) //Ray has hit an object
                {
                    bulletTrail.transform.LookAt(hit.point); //Orient trail so it has correct trajectory

                    //Apply damage and particle effects here to achieve greater accuracy than using collisions in BulletBehavior
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(primaryDamage);

                        pooler.SpawnFromPool("Blood Spatter", hit.point, Quaternion.LookRotation(hit.normal));
                    }
                    else
                    {
                        pooler.SpawnFromPool("Impact Effect", hit.point, Quaternion.LookRotation(hit.normal));
                    }

                    //Uncomment if I want to add force to shot
                    //if(hit.rigidbody != null)
                    //{
                    //    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    //}
                }
                else //Ray hasn't hit an object
                {
                    bulletTrail.transform.LookAt(rayOrigin + (direction * primaryRange)); //Orient trail so it has correct trajectory
                }
            }
        }
    }

    public override void Secondary()
    {
        
    }

    public override void Tertiary()
    {
        
    }

    public override void Melee(ObjectPooler pooler)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.fullPathHash == idleStateHash)
        {
            animator.SetTrigger(meleeHash); //Play animation for firing shotgun

            Vector3 rayOrigin = weaponCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //The center of the camera
            RaycastHit hit; //Will hold hit data from the raycast

            if (Physics.Raycast(rayOrigin, weaponCam.transform.forward, out hit, meleeRange)) //Ray has hit an object within range
            {
                //Apply damage and particle effects here to achieve greater accuracy than using collisions in BulletBehavior
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(meleeDamage);

                    pooler.SpawnFromPool("Blood Spatter", hit.point, Quaternion.LookRotation(hit.normal));
                }
                else
                {
                    pooler.SpawnFromPool("Impact Effect", hit.point, Quaternion.LookRotation(hit.normal));
                }

                //Uncomment if I want to add force to shot
                //if(hit.rigidbody != null)
                //{
                //    hit.rigidbody.AddForce(-hit.normal * hitForce);
                //}
            }
        }
    }
}
