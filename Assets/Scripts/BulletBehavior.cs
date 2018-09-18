using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Eventually use object pooling to conserve memory
public class BulletBehavior : MonoBehaviour {
    
    public float speed = 120f;

    private void OnCollisionEnter(Collision collision)
    {
        //"Remove" bullet from scene without destroying prefab
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}
