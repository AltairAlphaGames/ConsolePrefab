using UnityEngine;

public class DialogueSetup : MonoBehaviour
{
    private Conversation conversation;

    void Start()
    {
        // Create dialogue nodes
        DialogueNode node1 = new DialogueNode("Hello, traveler! Welcome to our village. What brings you here?");
        DialogueNode node2 = new DialogueNode("Adventure, you say? There are many quests around here.");
        DialogueNode node3 = new DialogueNode("Just passing through? Safe travels, stranger.");
        DialogueNode node4 = new DialogueNode("Good luck, brave one. Return when you’ve found the treasure.");
        DialogueNode node5 = new DialogueNode("Very well. The cave isn’t going anywhere.");

        // Set up options for each node
        node1.AddOption("I'm looking for adventure.", node2);
        node1.AddOption("Just passing through.", node3);
        node2.AddOption("I'm ready for danger!", node4);
        node2.AddOption("Maybe I’ll come back later.", node5);

        // Initialize the conversation with the root node
        conversation = new Conversation(node1);

        // Display the first node
        DisplayNode(conversation.StartNode);
    }

    void DisplayNode(DialogueNode node)
    {
        // Display node text
        Debug.Log(node.Text);

        // Display options
        foreach (DialogueOption option in node.Options)
        {
            Debug.Log("Option: " + option.Text);
        }
    }
}
