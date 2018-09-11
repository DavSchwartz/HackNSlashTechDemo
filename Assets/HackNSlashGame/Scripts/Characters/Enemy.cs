using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ICharacter
{

    public float detectionRadius;
    public Transform navpoint;
    
    public float maxHealth;
    public GameObject healthBar;
    public GameObject bloodPrefab;
    public float healthBarHeightOffset;

    private Camera cam;
    private float currentHealth;
    private Canvas healthBarCanvas;
    private Slider bar;
    private RectTransform rect;
    private bool dead = false;
    private RectTransform canvasRect;

    public bool attacking = false;

    public float AttackDistance = 2;
    public int AttackType = 1;

    private Animator _animator;

    public PlayerConfig config;

    GameObject playerCharacter;
    Animator _playerAnimator;


    public GameObject PlayerCharAttacking
    {
        get
        {
            return playerCharacter;
        }
    }

    // todo make this dynamic to who he's attacking
    public Transform PlayerTarget
    {
        get {
            return playerCharacter ? playerCharacter.transform : null;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    public bool Dead
    {
        get { return dead; }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (amount > 0)
        {
            // again, there are better methds, using this for prototype
            var gb = GameObject.Instantiate(bloodPrefab, transform);
            gb.transform.localPosition = Vector3.zero;
            bar.value = CalculatedHealth();
        }

        Debug.Log("currentHealth " + currentHealth);
        if (currentHealth <= 0 && !dead)
        {
            Died();
            this.enabled = false;
        }
    }

    // Use this for initialization
    void Start() {
        playerCharacter = config.manager.MainPlayer.gameObject;
        currentHealth = maxHealth;
        healthBar = Instantiate(healthBar) as GameObject;
        rect = healthBar.GetComponent<RectTransform>();
        bar = healthBar.GetComponent<Slider>();
        healthBarCanvas = config.manager.awesomeUI; // GameObject.Find("AwesomeUI").GetComponent<Canvas>();

        canvasRect = healthBarCanvas.GetComponent<RectTransform>();
        cam = Camera.main;
        healthBar.transform.SetParent(healthBarCanvas.transform);

        _animator = GetComponent<Animator>();

        //get value from player
        _playerAnimator = config.manager.MainPlayer.GetComponent<Animator>();

        config.manager.enemyGuys.Add(this);
    }
	
	// Update is called once per frame
	void Update () {


        //if within detection radius of player, follow player
		if ((playerCharacter.transform.position - transform.position).magnitude <= detectionRadius)
        {
            navpoint.position = playerCharacter.transform.position;
            //attacking = true;
        }
        else
        {
            //attacking = false;
        }

        //Vector3 screenPos = cam.WorldToScreenPoint(this.transform.position);
        // Debug.Log(screenPos);

        //moving health bar to track an offset position of the character
        Vector2 viewport = cam.WorldToViewportPoint(transform.position + Vector3.up*healthBarHeightOffset);
        
        // TODO: figure out ways to hide the health bar, or not render off screen.
        Vector2 screenPosition = new Vector2(
            ((viewport.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((viewport.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f))
        );

        rect.anchoredPosition = screenPosition;

        AttackPlayerDetect();
    }

    void AttackPlayerDetect()
    {
        if (attacking && Vector3.Distance(transform.position, PlayerTarget.position) < AttackDistance)
        {
            _animator.SetInteger("ActionIndex", AttackType);
            //Debug.Log("Setting action index" + AttackType);

            // disabling rotate to face, since this needs to be a feature to third person controller.
            // character.rotation = Quaternion.LookRotation(character.position - attackTarget.position);
        }
        else if (_animator.GetInteger("ActionIndex") != -1)
        {
            _animator.SetInteger("ActionIndex", -1);
        }
    }
    float CalculatedHealth()
    {
        return currentHealth / maxHealth;
    }

    private void Died()
    {
        Animator a = GetComponent<Animator>();
        //a.SetBool("Dead", true);
        //Debug.Log("Is Dead!");

        a.CrossFadeInFixedTime("Dead", 0.2f);
        var agent = GetComponent<NavMeshAgent>();//.enabled = false;

        agent.updateRotation = false;
        agent.updatePosition = false;

        var ai = GetComponent<AICharacterControl>();
        ai.enabled = false;

        dead = true;
        bar.gameObject.SetActive(false);
        //a.Play("Dead", 0, 0f);
    }

    //make player character attack enemy
    //copy and pasted from ClickyMove, may need changes?
    public void Interact()
    {
        if (Dead)
        {
            _playerAnimator.SetInteger("ActionIndex", -1);
        }
        else if (Vector3.Distance(playerCharacter.transform.position, transform.position) < AttackDistance)
        {
            // This should come from player.
            _playerAnimator.SetInteger("ActionIndex", config.manager.MainPlayer.AttackType);

            
            Debug.Log("Setting action index" + config.manager.MainPlayer.AttackType);

            // disabling rotate to face, since this needs to be a feature to third person controller.
            // playerCharacter.rotation = Quaternion.LookRotation(playerCharacter.position - attackTarget.position);
        }
        else if (_playerAnimator.GetInteger("ActionIndex") != -1)
        {
            Debug.Log("disable player attack" );
            _playerAnimator.SetInteger("ActionIndex", -1);
        }
    }
    
    //stop attacking
    public void cancel()
    {
        _playerAnimator.SetInteger("ActionIndex", -1);
    }
}
