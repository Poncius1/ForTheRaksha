using UnityEngine;

public class Mission : ScriptableObject
{
     public string missionName;
    [TextArea] public string description;
    public bool isCompleted = false;
    
    public virtual bool IsCompleted()
    {
        return isCompleted;
    }

    public void MarkCompleted()
    {
        isCompleted = true;
    }
}
