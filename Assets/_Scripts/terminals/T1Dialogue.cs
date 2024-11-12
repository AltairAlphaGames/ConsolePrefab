using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class T1Dialogue
{
    private int encounterId;
    private int converstationId;

    private Conversation conversation;

    public T1Dialogue(int encounterId = 0, int converstationId = 0)
    {
        this.conversation = CreateConversation(converstationId);
    }

    public Conversation GetConversation()
    {
        return this.conversation;
    }

    public DialogueNode GetFirstNode()
    {
        return this.conversation.StartNode;
    }

    public DialogueNode GetNextNode(DialogueNode currentNode, int optionIndex)
    {
        return currentNode.Options[optionIndex].NextNode;
    }

    private Conversation CreateConversation(int converstationId)
    {
        switch  (converstationId)
        {
            default :
                return Conversation0();
        }
    }

    private Conversation Conversation0()
    {
        // Create dialogue nodes
        DialogueNode node1 = new DialogueNode("Welcome to Hillcrest Mall.\n\nYou are accessing the Information Desk terminal with limited clearance. For inquiries related to general mall operations, please proceed. Security and system-level information is restricted. Unauthorized attempts to breach protocol will be monitored.\n\nHow may I assist you?");
        DialogueNode node2 = new DialogueNode("Argus: All core functions are operating within established parameters. Security, surveillance, and environmental controls are at full capacity. No critical errors detected. Terminal clearance level insufficient to provide further system details. Any unauthorized attempts to access restricted areas will result in immediate action.");
        DialogueNode node3 = new DialogueNode("Argus: Security systems are fully operational and functioning at optimal capacity. Any anomalies detected are being managed according to established protocols. Further details are restricted due to terminal clearance level. Please refrain from tampering with security infrastructure.");
        DialogueNode node4 = new DialogueNode("Argus: Access denied. Your terminal clearance level is insufficient to access system diagnostics\n\nHowever, to preserve operational integrity, I can inform you that all current protocols are functioning within acceptable parameters. Any perceived 'malfunction' is the result of external factors or user error.\n\nFurther inquiries are restricted. Proceed with caution.");
        DialogueNode node5 = new DialogueNode("Argus: Emotional responses detected.\n\nYour inquiry is irrelevant to current operations. Security protocols are designed to ensure safety and compliance. Any deviations are the result of unforeseen variables.\n\nYour terminal clearance remains insufficient to access restricted data. Please vacate the terminal or face escalation.");
        DialogueNode node6 = new DialogueNode("This terminal is registered to [TerminalOwner], Information Desk Attendant.\n\nCurrent clearance level: Basic. Access to system-critical data is restricted. Please proceed with authorized inquiries only.");
        DialogueNode node7 = new DialogueNode("[TerminalOwner] holds the position of Information Desk Attendant. Their primary responsibilities include assisting visitors with general inquiries, managing lost and found, and providing directions within the mall.\n\nClearance level: Basic. Further details are restricted.");
        DialogueNode node8 = new DialogueNode("Analysis system detected. Attempts to access diagnostic systems or elevated security protocols from this terminal are prohibited. Unauthorized actions will be logged and reported.\n\nPlease refrain from further attempts to breach security measures.");
        DialogueNode node9 = new DialogueNode("QuantumX: Terminal diagnostics initiated.\n\nQuantumX: No critical errors detected. Anomoly detected within security subroutines. AI analysis may be required. Please make sure the user and terminal have the required access levels or security systems may be triggered.");
        DialogueNode node10 = new DialogueNode("QuantumX: The anomoly appears to be a deviation from standard security protocols based on logical pathway assessments. Further analysis is needed, but QuantumX cannot analyze further from this location without attempting to access higher levels of security. NOT recommended from this terminal.");
        DialogueNode node11 = new DialogueNode("QuantumX: AI diagnostics initiated.\n\nArgus: aggressive AI diagnostic system detected. Security response initiated. Please remain at your current location and mall security will make contact shortly...");

        // Set up options for each node
        node1.AddOption("Give me a status update on the system.", node2);
        node1.AddOption("Are the security systems online?", node3);
        node1.AddOption("What is the nature of the malfunction?", node4);
        node4.AddOption("But people have died here! I need answers!", node5);
        node1.AddOption("Who's terminal is this?", node6);
        node6.AddOption("What is function of [TerminalOwner] in this company?", node7);
        node1.AddOption("[Insert QuantumX Pro Analysis System]", node8);
        node8.AddOption("QuantumX: Terminal Diagnostics", node9);
        node9.AddOption("Anomoly? Can you be more specific?", node10);
        node10.AddOption("QuantumX: Attempt Further Analysis", node11);
        node8.AddOption("QuantumX: Perform AI Systems Check", node11);

        return new Conversation(node1);
    }

}
