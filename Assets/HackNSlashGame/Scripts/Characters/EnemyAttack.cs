using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, InteractCharacter {

    public AudioClip punch;

    public Enemy enemy;

    private AudioSource source;

    public float Damage = 8;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        source = GetComponent<AudioSource>();

    }

    public void TriggerPunchHit()
    {

        //other.GetComponent<Enemy>().TakeDamage(10);
        Debug.Log("Hit triggered!!!!");

        // instantiate blood effect.
        if (enemy.PlayerCharAttacking)
        {
            enemy.PlayerCharAttacking.GetComponent<CharacterSetup>().TakeDamage(Damage);
            source.PlayOneShot(punch);
        }
    }

    public void Interact(CharacterSetup player)
    {
        throw new System.NotImplementedException();
    }
}

public interface InteractCharacter{
    void Interact(CharacterSetup player);
    }