using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Collectible Data", menuName = "New Collectible")]
public class CollectibleScriptableObject : ScriptableObject
{
    public Color color;
    public int points;
    public CollectibleType type;
}
