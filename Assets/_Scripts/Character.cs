using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public List<Transform> goalPoints;
    public int idGoal = 0;
    public bool isLoop = false;
    public bool isEnd = false;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        character = GetComponent<ThirdPersonCharacter>();
        SetGoal(idGoal);
    }

    public void SetGoal(int id)
    {
        if (goalPoints.Count == 0)
            return;
        
        agent.SetDestination(goalPoints[id].position);
        character.Move(Vector3.zero, false, false);
    }

    void Update()
    {
        if (isEnd)
        {
            character.Move(Vector3.zero, false, false);
            return;
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    idGoal++;
                    if (idGoal >= goalPoints.Count)
                    {
                        GoingEnd();
                    }
                    else
                    {
                        SetGoal(idGoal);
                    }
                }
            }
            else
            {
                character.Move(agent.desiredVelocity, false, false);
            }
        }
    }
    

    public void GoingEnd()
    {
        isEnd = true;
        if (isLoop)
        {
            SetGoal(idGoal = 0);
            isEnd = false;
        }
    }
}
