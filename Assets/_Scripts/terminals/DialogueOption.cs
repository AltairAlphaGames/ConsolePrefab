public class DialogueOption
{
    public string Text { get; private set; }
    public DialogueNode NextNode { get; private set; }

    public DialogueOption(string text, DialogueNode nextNode)
    {
        Text = text;
        NextNode = nextNode;
    }
}
