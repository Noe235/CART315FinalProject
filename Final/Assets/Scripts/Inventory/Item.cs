using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject {
    [Header("Only Gameplay")]
    public ItemType type;
    public ActionType actiontype;

    [Header("Only UI")] 
    public bool stackable = true;

    [Header("Both")] 
    public Sprite image;

}

public enum ItemType {
    Ressource,
    Tool
}

public enum ActionType {
    Dig,
    Gather
}