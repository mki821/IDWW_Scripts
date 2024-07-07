using UnityEngine;

public class CodeFormat : MonoBehaviour
{
    public static string PlayerSkillFormat =
    @"public enum PlayerSkill {{
        None = 0, {0}
    }}";

    public static string PoolTypeFormat =
    @"namespace ObjectPooling
    {{
        public enum PoolingType
        {{
            {0}    
        }}
    }}";
}
