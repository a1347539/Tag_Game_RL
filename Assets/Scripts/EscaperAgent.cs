using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UIElements;

public class EscaperAgent : GameObjectAgent
{
    [SerializeField] private MoverAgent mover;
    [SerializeField] private Transform moverTransform;
    [SerializeField] private Background background;
    [SerializeField] private GameManager gameManager;

    private float moveSpeed = 8;

    private void Start()
    {
        gameManager.onTimerEnd += handleTimerEnd;
    }

    private void OnDestroy()
    {
        gameManager.onTimerEnd -= handleTimerEnd;
    }

    public override void OnEpisodeBegin()
    {
        gameManager.resetTimer();
        base.OnEpisodeBegin();
        transform.localPosition = vs[0];
        moverTransform.localPosition = vs[1];
    }

    public void handleTimerEnd()
    {
        background.swapColor(Color.red);
        AddReward(1f);
        EndEpisode();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(moverTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, moveY) * Time.deltaTime * moveSpeed;
        AddReward(1f / gameManager.durationInStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;
        actions[0] = Input.GetAxisRaw("Horizontal");
        actions[1] = Input.GetAxisRaw("Vertical");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            //background.swapColor(Color.yellow);
            AddReward(-0.5f);
            //SetReward(-1f);
            //EndEpisode();
        }
        else if (collision.collider.CompareTag("Mover"))
        {
            SetReward(-1f);
            //EndEpisode();
        }


/*        if (collision.collider.CompareTag("Mover"))
        {
            SetReward(-1f);
            EndEpisode();
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            //background.swapColor(Color.yellow);
            AddReward(-5f / gameManager.durationInStep);
            //SetReward(-1f);
            //EndEpisode();
        }
    }
}
