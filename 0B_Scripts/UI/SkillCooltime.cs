using UnityEngine;

public class SkillCooltime : MonoBehaviour
{
    private void Start() {
        SkillManager.Instance.skillCooltime = transform;
    }
}
