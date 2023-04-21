using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public enum NodeType
    {
        DeadEnd,
        Path,
        Junction
    }

    public NodeType nodeType;
    public List<Node> connectedNodes = new List<Node>();

    // Additional properties and methods can be added as needed
}
