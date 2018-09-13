using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Eventually use object pooling to conserve memory
public class BulletBehavior : MonoBehaviour {

    public static float MAX_DIVERGENCE = 5;

    public GameObject bloodSpatter;
    public float damage = 5f;
    public float speed = 60f;
    public Vector3 divergence = Vector3.zero;

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
    }

    // Use this for initialization
    void Start () {
        divergence.x = (1 - 2 * Random.value) * MAX_DIVERGENCE;
        divergence.y = (1 - 2 * Random.value) * MAX_DIVERGENCE;
        GetComponent<Transform>().Rotate(divergence);
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}
