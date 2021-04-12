using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MovetoGoalAgent : Agent
{
    [SerializeField] private Goal[] goals;
    [SerializeField] private Objectives man;
    private int counter = 0;
 
    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(goals[counter].transform.localPosition);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[0];
        float moveY = vectorAction[1];

        float moveSpeed = 6f;
        transform.localPosition += new Vector3(moveX, 0, moveY) * Time.deltaTime * moveSpeed;
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal)) {
            if (!goal.touched && goal.Equals(goals[counter]))
            {
                AddReward(+10f);
                counter++;
                goal.touched = true;
                goal.counter = 9000;
            }
            if (!goal.touched)
            {
                AddReward(+1f);
                goal.touched = true;
                goal.counter = 1000;
            }
        }
        if (other.TryGetComponent<Infected>(out Infected kid))
        {
            SetReward(-1f);
            Debug.Log("Infected Touched");
            EndEpisode();
            //man.restart();
        }
        if(other.TryGetComponent<MovetoGoalAgent>(out MovetoGoalAgent agent))
        {
            //SetReward(-1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wal))
        {
            //SetReward(-1f);
            EndEpisode();
        }
    }
}
