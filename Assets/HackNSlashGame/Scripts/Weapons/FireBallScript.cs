using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : WeaponPlayable {

    public GameObject fireball;


    public float speed;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void TriggerAttack()
    {
        var gb = Instantiate(fireball, transform.position, Quaternion.identity);

        if(PlayerCharController.character is Enemy)
        {
            Vector3 direction =(((Enemy)PlayerCharController.character).transform.position - PlayerCharController.transform.position ).normalized;
            gb.GetComponent<Rigidbody>().velocity = direction* speed;
        }


        source.PlayOneShot(weaponSound);
    }
}
