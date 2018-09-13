using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{

    public Vector3 restPosition; //local position where your weapon would rest when it's not bobbing.
    public float transitionSpeed = 6f; //smooths out the transition from moving to not moving.
    public float bobSpeed = 6f; //how quickly the player's weapon bobs.
    public float bobAmount = 0.04f; //how dramatic the bob is. Increasing this in conjunction with bobSpeed gives a nice effect for sprinting.

    float timer = Mathf.PI * 2; //initialized as this value because this is where cos = 1, thus smoothing the transition from not walking to walking.
    Vector3 weaponPos;

    void Awake()
    {
        restPosition = transform.localPosition;
        weaponPos = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //moving
        {
            timer += bobSpeed * Time.deltaTime;
            
            //use the timer value to set the position
            Vector3 newPosition = new Vector3((restPosition.x - bobAmount) + Mathf.Cos(timer) * bobAmount, restPosition.y + Mathf.Abs(Mathf.Sin(timer) * bobAmount), restPosition.z); //restPosition - bobAmount for x so the parabola starts at a position in which the weapon will already be. abs val of y for a parabolic path
            weaponPos = newPosition;
        }
        else
        {
            timer = Mathf.PI * 2; //reinitialize

            Vector3 newPosition = new Vector3(Mathf.Lerp(weaponPos.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(weaponPos.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(weaponPos.z, restPosition.z, transitionSpeed * Time.deltaTime)); //transition smoothly from walking to stopping.
            weaponPos = newPosition;
        }

        if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
            timer = 0;

        transform.localPosition = weaponPos;
    }
}
