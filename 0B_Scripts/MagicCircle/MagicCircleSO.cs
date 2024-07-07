using UnityEngine;

[CreateAssetMenu(menuName = "SO/MagicCircle/MagicCircle")]
public class MagicCircleSO : ScriptableObject
{
    public Texture2D texture;
    [ColorUsage(false, true)] public Color color;
    public bool multiply;
}
