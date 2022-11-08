using System.Collections;
using System.Collections.Generic;
using RangerRPG.AI;
using RangerRPG.AI.Tools;
using RangerRPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace FiaMaze.Characters {
    public class EnemyBehaviour : MonoBehaviour {

        public HealthComponent healthComponent;
        public HealthBar healthBar;

        public ProjectileMover shootProjectile;
        public GameObject spawnPoint;
        
        public GameObject playerRef;
        public List<GameObject> patrolPoints = new();
        public int currentPointIndex = -1;
        public float attackRange = 5;
        public float sightDistance = 10;
        public float shootCooldown = 3;
        public AISight aiSight;
        private bool canShoot = true;
        
        private BehaviourTree enemyTree;
        private NavMeshAgent navMeshAgent;

        private AudioSource source;
        public AudioClip shootClip;

        public EnemyBehaviour Init(GameObject player) {
            playerRef = player;
            return this;
        }
        
        private void Start() {
            transform.LookAt(playerRef.transform);
            navMeshAgent = GetComponent<NavMeshAgent>();
            source = GetComponent<AudioSource>();
            //navMeshAgent.SetDestination(playerRef.transform.position);
            BuildTree();
        }

        #region Behaviour

        enum ActionState {
            IDLE,
            WORKING
        }

        private ActionState patrolState = ActionState.IDLE;
        private ActionState chaseState = ActionState.IDLE;
        private ActionState shootState = ActionState.IDLE;
        
        private void BuildTree() {
            enemyTree = new BehaviourTree();

            var canPatrol = new BTLeaf("Can_Patrol", CanPatrol);
            var pickRandomLocation = new BTLeaf("Pick_Random_Location", PickRandomLocation);
            var goToLocation = new BTLeaf("Go_To_Location", GoToLocation);
            var seq_Patrol = new BTSequence("Patrol_Sequence").AddChildren(canPatrol, pickRandomLocation, goToLocation);
            
            var chasePlayer = new BTLeaf("Chase_Player", ChasePlayer);
            var isPlayerInRange = new BTLeaf("Is_Player_In_Range", IsPlayerInRange);
            var waitForShootCooldown = new BTLeaf("Wait_For_Cooldown", WaitForShootCooldown);
            var shootPlayer = new BTLeaf("Shoot_Player", ShootPlayer);
            var seq_ChaseAndShoot = new BTSequence("Chase_Sequence").AddChildren(chasePlayer, waitForShootCooldown, shootPlayer);
            
            
            //var seq_Shoot = new BTSequence("Shoot_Sequence").AddChildren(waitForShootCooldown, shootPlayer);

            enemyTree.root.AddChildren(seq_Patrol, seq_ChaseAndShoot);
            enemyTree.PrintTree();
        }

        private BTNode.Status CanPatrol() {
            return BTNode.Status.SUCCESS;
        }

        private BTNode.Status PickRandomLocation() {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
            return BTNode.Status.SUCCESS;
        }

        private BTNode.Status GoToLocation() {
            if (aiSight.IsInSight(playerRef.transform)) {
                Log.Info("Found Player");
                patrolState = ActionState.IDLE;
                return BTNode.Status.FAILURE;
            }
            
            switch (patrolState) {
                case ActionState.IDLE:
                    navMeshAgent.SetDestination(patrolPoints[currentPointIndex].transform.position);
                    patrolState = ActionState.WORKING;
                    return BTNode.Status.RUNNING;
                case ActionState.WORKING:
                    float dist = Vector3.Distance(patrolPoints[currentPointIndex].transform.position, transform.position);
                    if (dist < 2f) {
                        patrolState = ActionState.IDLE;
                        return BTNode.Status.SUCCESS;
                    }
                    return BTNode.Status.RUNNING;
                default:
                    return BTNode.Status.FAILURE;
            }
        }
        
        private BTNode.Status ChasePlayer() {
            switch (chaseState) {
                case ActionState.IDLE:
                    navMeshAgent.SetDestination(playerRef.transform.position);
                    chaseState = ActionState.WORKING;
                    return BTNode.Status.RUNNING;
                case ActionState.WORKING:
                    navMeshAgent.SetDestination(playerRef.transform.position);
                    float dist = Vector3.Distance(playerRef.transform.position, transform.position);
                    if (dist < attackRange) {
                        chaseState = ActionState.IDLE;
                        return BTNode.Status.SUCCESS;
                    }
                    return BTNode.Status.RUNNING;
                default:
                    return BTNode.Status.FAILURE;
            }
        }
        
        private BTNode.Status IsPlayerInRange() {
            return BTNode.Status.SUCCESS;
        }
        
        private BTNode.Status WaitForShootCooldown() {
            if (aiSight.IsInSight(playerRef.transform) == false) {
                shootState = ActionState.IDLE;
                return BTNode.Status.FAILURE;
            }
            
            switch (shootState) {
                case ActionState.IDLE:
                    shootState = ActionState.WORKING;
                    return BTNode.Status.RUNNING;
                case ActionState.WORKING:
                    if (canShoot) {
                        shootState = ActionState.IDLE;
                        return BTNode.Status.SUCCESS;
                    }
                    return BTNode.Status.RUNNING;
                default:
                    shootState = ActionState.IDLE;
                    return BTNode.Status.FAILURE;
            }
        }
        
        private BTNode.Status ShootPlayer() {
            Log.Info("Enemy Shooting Player!");
            Instantiate(shootProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
            if (source.isPlaying == false) {
                source.clip = shootClip;
                source.Play();
            }
            return BTNode.Status.SUCCESS;
        }

        #endregion


        private void Update() {
            enemyTree?.Run();
        }
        
        private void OnCollisionEnter(Collision other) {
            Log.Info("Collision Detected!");
            if (other.gameObject.CompareTag("PlayerAttack")) {
                healthComponent.Damage(20);
                var percent = healthComponent.GetPercent();
                if (percent == 0) StartCoroutine(Die());
                healthBar.SetPercent(healthComponent.GetPercent());
            }
        }
        
        private IEnumerator Die() {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}