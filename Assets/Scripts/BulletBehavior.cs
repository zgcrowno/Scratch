using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Eventually use object pooling to conserve memory
public class BulletBehavior : MonoBehaviour {

    public GameObject bloodSpatter;
    public GameObject impactEffect;
    public float damage = 5f;
    public float speed = 120f;

    private void OnCollisionEnter(Collision collision)
    {
        //"Remove" bullet from scene without destroying prefab
        gameObject.SetActive(false);

        //Activate bloodSplatter, and deal damage to enemy
        var collidedObj = collision.gameObject;
        if(collidedObj.tag.Equals("Enemy"))
        {
            //Activate bloodSpatter
            bloodSpatter = Instantiate(bloodSpatter, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));

            //Deal damage, and destroy if dead
            Enemy enemyScript = collidedObj.GetComponent<Enemy>();
            enemyScript.TakeDamage(damage);
        }
        else
        {
            //Instantiate(impactEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}
