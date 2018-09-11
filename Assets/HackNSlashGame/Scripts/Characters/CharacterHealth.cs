using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {


    public HealthBar bar;
    public float CurrentHealth;
    public float MaxHealth;

    // Use this for initialization
    void Start () {
	}
	
    void TakeDamage(float damagevalue)
    {
        CurrentHealth -= damagevalue;
    }
}
