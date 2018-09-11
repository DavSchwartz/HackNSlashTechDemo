using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour {

    // TODO: change this to a generic weapon.
    public WeaponPlayable weaponScript;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerWeapon()
    {
        weaponScript.TriggerAttack();
    }
}
