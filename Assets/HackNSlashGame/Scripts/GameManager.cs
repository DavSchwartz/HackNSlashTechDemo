using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerConfig config;

    public GameObject MainCamera;

    public CharacterSetup MainPlayer;

    public Text gameDialogWindow;

    public List<NPC> npcGuys;
    public List<Enemy> enemyGuys;

    public ClickyMove ClickyCamera;

    public Canvas awesomeUI;

    public Slider playerHealthBar;

	// Use this for initialization
	void Awake() {
        npcGuys = new List<NPC>();
        enemyGuys = new List<Enemy>();
        config.manager = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
