using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    [SerializeField] public Goal[] goals;
    [SerializeField] public MovetoGoalAgent[] agents;
    private bool completed = false;

    private void Awake()
    {
        foreach (var agent in agents)
        {
            //Debug.Log(agent);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        foreach(var goal in goals)
        {
            if (goal.IsAchieved())
            {
                goal.Complete();
                completed = true;
            }
            else
            {
                completed = false;
                break;
            }
        }
        if(completed)
        {
            foreach(var agent in agents)
            {
                agent.EndEpisode();
            }
            foreach(var goal in goals)
            {
                goal.touched = false;
            }
        }
    }
    public void restart()
    {
       
        foreach (var goal in goals)
        {
            goal.touched = false;
        }
    }
}
