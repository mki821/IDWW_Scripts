using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/MagicCircle/List")]
public class SkillListSO : ScriptableObject
{
    public List<SkillSO> list = new List<SkillSO>();
}
