using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPunch : WeaponPlayable {

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
    }

    public override void TriggerAttack()
    {

        //other.GetComponent<Enemy>().TakeDamage(10);
        Debug.Log("Hit triggered!!!!");

        // instantiate blood effect.
        if(PlayerCharController && PlayerCharController.character is Enemy)
        { 
            ((Enemy)PlayerCharController.character).TakeDamage(Damage);
            source.PlayOneShot(weaponSound);
        }

        if(belongsToEnemy && belongsToEnemy.PlayerCharAttacking)
        {
            belongsToEnemy.PlayerCharAttacking.GetComponent<CharacterSetup>().TakeDamage(Damage);
            source.PlayOneShot(weaponSound);
        }
    }
}
