using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponPlayable : MonoBehaviour {

    public AudioClip weaponSound;

    protected AudioSource source;

    public int AttackType = 0;
    public float Damage = 8;

    // TODO: abstract these.
    public ClickyMove PlayerCharController;
    public Enemy belongsToEnemy;

    public Transform attackTarget;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public virtual void TriggerAttack()
    {
        Debug.Log("Nothing implemented yet.");
    }
}
