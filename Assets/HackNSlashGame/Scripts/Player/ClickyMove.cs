using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickyMove : MonoBehaviour {

   public LayerMask layerMask;

   public  Transform navpoint;

    //public Transform playerCharacter;

    public Vector3 position;

    public float heightOffset = 1;

    public ICharacter character = null;
   // public int AttackType = 0; //removal causes compiler error, why?

    private bool interacting = false;

    // Use this for initialization
    void Start () {
     //   position = playerCharacter.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition) ;
            // Debug.DrawRay(r.origin,r.direction * 100, Color.green, 10);

            if(Physics.Raycast(r,out hit, 50, layerMask)) {
                navpoint.position = hit.point;
                // Debug.Log("terrain hit! " + hit.point);
                Debug.DrawRay(hit.point, Vector3.up, Color.green, 10);

                //reset when clicking on new object
                if (character != null)
                {
                    character.cancel();
                    character = null;
                    interacting = false;
                }

                character = hit.transform.gameObject.GetComponent<ICharacter>();
                if (character != null)
                {
                    interacting = true;
                    //character.Interact();
                }
            }
        }

        if(character != null && interacting)
        {
            character.Interact();
        }


        if (character is Enemy && ((Enemy)character).Dead)
        {
            character = null;
        }

        //if(_animator.GetInteger("ActionIndex") == 0)
        //{
        //    _animator.SetInteger("ActionIndex", -1);
        //}

        //  transform.position = playerCharacter.position - position;
    }
}
