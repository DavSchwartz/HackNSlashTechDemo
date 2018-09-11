using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        public float enemyEngageDistance = 2;

        private void Start()
        {

            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
            var charSetup = GetComponent<CharacterSetup>();

            //DEBUG is this only for enemies? or NPCs too
            if (charSetup && charSetup.GetComponent<ClickyMove>().character is Enemy)
            {
                var enemy = (Enemy)charSetup.GetComponent<ClickyMove>().character;

                if (enemy && Vector3.Distance(enemy.transform.position, transform.position) < enemyEngageDistance)
                {
                    var rotateVect = Vector3.Normalize(enemy.transform.position - transform.position);
                    character.Move(rotateVect * 0.01f, false, false);
                }
            }

            var enemyCheck = GetComponent<Enemy>();

            if (enemyCheck)
            {
                var player = enemyCheck.PlayerTarget;

                if (player && Vector3.Distance(player.transform.position, transform.position) < enemyEngageDistance)
                {
                    var rotateVect = Vector3.Normalize(player.transform.position - transform.position);
                    character.Move(rotateVect * 0.01f, false, false);
                }
            }
    
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
