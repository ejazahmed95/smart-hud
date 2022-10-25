using System;
using System.Collections;
using System.Collections.Generic;
using FiaMaze.TestLevels;
using RangerRPG.AI;
using RangerRPG.AI.Tools;
using RangerRPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace FiaMaze.Characters {
    public class HostageBehaviour : MonoBehaviour {
        public GameObject playerRef;
        public List<GameObject> patrolPoints;
        public int currentPointIndex = -1;

        public GameObject winEffect;
        public Transform goalTransform;
        
        private BehaviourTree hostageTree;
        private NavMeshAgent navMeshAgent;
        
        public AISight aiSight;

        private void Start() {
            transform.LookAt(playerRef.transform);
            navMeshAgent = GetComponent<NavMeshAgent>();
            aiSight = GetComponent<AISight>();
            BuildTree();
        }

        #region Behaviour

        enum ActionState {
            IDLE,
            WORKING
        }

        private ActionState patrolState = ActionState.IDLE;
        private ActionState followState = ActionState.WORKING;
        
        public bool canFollow = true;
        public bool foundPlayer = false;
        private bool reachedGoal = false;

        private void BuildTree() {
            hostageTree = new BehaviourTree();

            var canPatrol = new BTLeaf("Can_Patrol", CanPatrol);
            var pickRandomLocation = new BTLeaf("Pick_Random_Location", PickRandomLocation);
            var goToLocation = new BTLeaf("Go_To_Location", GoToLocation);
            var seq_Patrol = new BTSequence("Patrol_Sequence").AddChildren(canPatrol, pickRandomLocation, goToLocation);
            
            var followPlayer = new BTLeaf("Follow_Player", FollowPlayer);
            var stayInPlace = new BTLeaf("Is_Player_In_Range", StayInPlace);
            var sel_Follow = new BTSelector("Follow_Sequence").AddChildren(followPlayer, stayInPlace);

            var playRescueAnim = new BTLeaf("Play_Rescue_Anim", PlayRescueAnim);
            var disappear = new BTLeaf("Disappear", Disappear);
            var seq_Exit = new BTSequence("Shoot_Sequence").AddChildren(playRescueAnim, disappear);

            var seq_Rescued = new BTSequence("Rescue_Sequence").AddChildren(sel_Follow, seq_Exit);
            hostageTree.root.AddChildren(seq_Patrol, seq_Rescued);
            hostageTree.PrintTree();
        }

        private BTNode.Status CanPatrol() {
            if (foundPlayer) {
                return BTNode.Status.FAILURE;
            }
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
                foundPlayer = true;
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
        
        private BTNode.Status FollowPlayer() {
            if (canFollow == false) {
                navMeshAgent.ResetPath();
                return BTNode.Status.FAILURE;
            }
            switch (followState) {
                case ActionState.IDLE:
                    navMeshAgent.SetDestination(playerRef.transform.position);
                    followState = ActionState.WORKING;
                    return BTNode.Status.RUNNING;
                case ActionState.WORKING:
                    navMeshAgent.SetDestination(playerRef.transform.position);
                    float dist = Vector3.Distance(playerRef.transform.position, transform.position);
                    if (dist < 2f) {
                        navMeshAgent.ResetPath();
                    }
                    float goalDist = Vector3.Distance(goalTransform.position, transform.position);
                    if (goalDist < 4f) {
                        canFollow = false;
                        return BTNode.Status.SUCCESS;
                    }
                    return BTNode.Status.RUNNING;
                default:
                    return BTNode.Status.FAILURE;
            }
        }
        
        private BTNode.Status StayInPlace() {
            if (reachedGoal) {
                Debug.Log("Reached GOAL!");
                return BTNode.Status.SUCCESS;
            }
            if (canFollow) {
                return BTNode.Status.FAILURE;
            }
            return BTNode.Status.RUNNING;
        }
        
        private BTNode.Status PlayRescueAnim() {
            Instantiate(winEffect, transform);
            return BTNode.Status.SUCCESS;
        }
        
        private BTNode.Status Disappear() {
            TestAILevelManager.Instance.GameWon();
            Destroy(gameObject, 2f);
            return BTNode.Status.SUCCESS;
        }

        #endregion


        private void Update() {
            hostageTree?.Run();

        }
    }
}