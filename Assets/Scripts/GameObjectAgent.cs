using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameObjectAgent : Agent
{

    public Action<List<Vector2>> onNewEpisode = delegate { };

    protected List<Vector2> vs;

    public override void OnEpisodeBegin()
    {
        vs = getSpawnPoints();
    }

    public List<Vector2> getSpawnPoints()
    {
        Vector2 p1;
        Vector2 p2;
        do
        {
            p1 = new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
            p2 = new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
        }
        while (Vector2.Distance(p1, p2) < 2);

        return new List<Vector2> { p1, p2 };

    }
}
