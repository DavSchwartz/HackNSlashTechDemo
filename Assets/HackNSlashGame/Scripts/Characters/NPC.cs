using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, ICharacter
{

    public PlayerConfig playerConfig;
    public int talkRadius;
    public bool talking;

    private GameObject playerCharacter;
    private GameObject mainCamera;
    private Text dialog;

	// Use this for initialization
	void Start () {
        playerCharacter = playerConfig.manager.MainPlayer.gameObject;
        mainCamera = playerConfig.manager.MainCamera;
        dialog = playerConfig.manager.gameDialogWindow;

        playerConfig.manager.npcGuys.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
        //NOT COMPLETE
        if ((playerCharacter.transform.position - transform.position).magnitude < talkRadius)
        {
            //Debug.Log();
        }
	}

    //unused code, should this be deleted?
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.gameObject == playerCharacter)
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody.gameObject == playerCharacter)
        {
            
        }
    }

    //speak with player
    public void Interact()
    {
        dialog.text = "WHAT UP DAWG ? ";
    }

    //stop speaking
    public void cancel()
    {
        dialog.text = "";
    }
}
