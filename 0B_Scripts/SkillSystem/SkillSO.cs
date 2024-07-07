using UnityEngine;

[CreateAssetMenu(menuName = "SO/MagicCircle/Spell")]
public class SkillSO : ScriptableObject
{
    public string code;
    public Sprite icon;
    public string skillName;
    public string command;
    public PlayerSkill skillType;
}
