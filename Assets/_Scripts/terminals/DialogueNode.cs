using System.Collections.Generic;

public class DialogueNode
{
    public string Text { get; private set; }
    public List<DialogueOption> Options { get; private set; }

    public DialogueNode(string text)
    {
        Text = text;
        Options = new List<DialogueOption>();
    }

    public void AddOption(string optionText, DialogueNode nextNode)
    {
        Options.Add(new DialogueOption(optionText, nextNode));
    }
}
