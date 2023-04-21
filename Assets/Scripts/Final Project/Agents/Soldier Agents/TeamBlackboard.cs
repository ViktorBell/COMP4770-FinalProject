using System.Collections.Generic;
using UnityEngine;

public class TeamBlackboard
{
    public List<Node> DiscoveredNodes = new List<Node>();
    public List<Node> ExploredNodes = new List<Node>();
    public List<Vector3> TaggedEnemyData = new List<Vector3>();
    public List<Vector3> PickUpPositions = new List<Vector3>();

    // You can add more team-wide data here if needed
}
