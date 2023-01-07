using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;
using UnityEngine.UIElements;

public class MoverAgent : GameObjectAgent
{
    [SerializeField] private Transform goalTransform;
    [SerializeField] private Background background;
    [SerializeField] private GameManager gameManager;

    private float moveSpeed = 3.5f;

    public override void OnEpisodeBegin()
    {
        gameManager.resetTimer();
        base.OnEpisodeBegin();
        transform.localPosition = vs[0];
        goalTransform.localPosition = vs[1];
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(goalTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, moveY) * Time.deltaTime * moveSpeed;

        AddReward(-1f / gameManager.durationInStep);
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
            background.swapColor(Color.yellow);
            SetReward(-1f);
            EndEpisode();
        }
        else if (collision.collider.CompareTag("Goal"))
        {
            background.swapColor(Color.green);
            SetReward(1f);
            EndEpisode();
        }
    }

}
