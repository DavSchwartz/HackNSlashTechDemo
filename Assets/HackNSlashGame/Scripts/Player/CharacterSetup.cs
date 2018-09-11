using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class CharacterSetup : MonoBehaviour {

    // I guess this can be a weapon or watever, we'll figure it out.
    public List<GameObject> weapons = new List<GameObject>();
    public Transform handBone;
    public WeaponType weaponSelected = WeaponType.Fist;
     ClickyMove PlayerCharController;
    //public float healthBarHeightOffset = 1;
    public float maxHealth = 100;
    public GameObject bloodPrefab;

    public PlayerConfig config;

    //private FireBallScript spellScript;

    //private Canvas healthBarCanvas;
    private float currentHealth;
    //private Camera cam;
    private bool dead = false;
    //private RectTransform canvasRect;
    private Slider healthBar;

    public int AttackType;
    //{
    //    get;set;
    //}

    // This class is designed to act as temporary 'glue' between enemy and player.
    private AttackBehavior attackBehavior;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    // Use this for initialization
    void Start()
    {
        attackBehavior = GetComponent<AttackBehavior>();
        PlayerCharController = GetComponent<ClickyMove>();
        healthBar = config.manager.playerHealthBar;
        if (handBone)
        {
            // TODO: make switch at runtime.
            foreach(var w in weapons)
            {
                var gb = GameObject.Instantiate(w, handBone);
                 gb.SetActive(weapons.IndexOf(w) == (int)weaponSelected);

                // 1 is punch, 0 is swing sword, more action types will be added here.
                AttackType = (int)weaponSelected;

                if (gb.activeInHierarchy)
                {
                    attackBehavior.weaponScript = gb.GetComponent<WeaponPlayable>();
                    attackBehavior.weaponScript.PlayerCharController = this.PlayerCharController;
                   
                }
            }
        }

        //set health stuff
        currentHealth = maxHealth;
        //healthBarCanvas = GameObject.Find("AwesomeUI").GetComponent<Canvas>();

        //canvasRect = healthBarCanvas.GetComponent<RectTransform>();
        //cam = Camera.main;
        //healthBar.transform.SetParent(healthBarCanvas.transform);
    }
	
	// Update is called once per frame
	void Update () {
        // draw health bar.
        //Vector2 viewport = cam.WorldToViewportPoint(transform.position + Vector3.up * healthBarHeightOffset);

        //Vector2 screenPosition = new Vector2(
        //    ((viewport.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        //    ((viewport.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f))
        //);

        //rect.anchoredPosition = screenPosition;
    }




    //public void CastAttack()
    //{
    //    spellScript.CastFireball();
    //}


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (amount > 0)
        {
            // again, there are better methds, using this for prototype
            var gb = GameObject.Instantiate(bloodPrefab, transform);
            gb.transform.localPosition = Vector3.zero;
            healthBar.value = CalculatedHealth();
        }

      //  Debug.Log("currentHealth " + currentHealth);
        if (currentHealth <= 0 && !dead)
        {
            Died();
            this.enabled = false;
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
        healthBar.gameObject.SetActive(false);
        //a.Play("Dead", 0, 0f);
    }
}


public enum WeaponType
{
    Fist = 0,
    Melee = 1,
    Magic = 2,

    //TODO
    Bow =4
}