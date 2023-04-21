using UnityEngine;

[System.Serializable]
public class Message
{
    public enum MessageType
    {
        Command,
        Request,
        Report,
        // Add any other message types as needed
    }

    public SoldierAgent sender;
    public SoldierAgent receiver;
    public MessageType type;
    public string content;
    public Vector3 position;

    public Message(SoldierAgent sender, SoldierAgent receiver, MessageType type, string content, Vector3 position = default)
    {
        this.sender = sender;
        this.receiver = receiver;
        this.type = type;
        this.content = content;
        this.position = position;
    }
}
