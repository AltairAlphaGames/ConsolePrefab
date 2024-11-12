using System.Collections.Generic;
using UnityEngine;

public class Conversation
{
    public DialogueNode StartNode { get; private set; }

    public Conversation(DialogueNode startNode)
    {
        StartNode = startNode;
    }
}
