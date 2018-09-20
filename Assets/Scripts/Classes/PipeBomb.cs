using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBomb : MonoBehaviour {

    public float delay = 3f;
    public float damage = 80f;
    public float blastRadius = 5f;
    public float blastForce = 300f;
    public GameObject explosion;
    public ObjectPooler pooler;

    private float countdown;

	// Use this for initialization
	void Start () {
        countdown = delay;
        pooler = GameObject.Find("/World/Object Pooler").GetComponent<ObjectPooler>();
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            Explode();
        }
	}

    void Explode()
    {
        pooler.SpawnFromPool("Explosion", transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius); //Get all the nearby objects with which the explosion is colliding

        foreach(Collider nearbyObject in colliders)
        {
            Entity entity = nearbyObject.GetComponent<Entity>();
            if(entity != null)
            {
                entity.TakeDamage(damage);

                pooler.SpawnFromPool("Blood Spatter", entity.transform.position, Quaternion.identity);
            }

            //Uncomment if I want to add force to explosion
            //Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            //if(rb != null)
            //{
            //    rb.AddExplosionForce(blastForce, transform.position, blastRadius);
            //}
        }

        gameObject.SetActive(false);
    }
}
