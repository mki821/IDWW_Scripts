using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBook : MonoBehaviour
{
    [SerializeField] private Image _skillIcon;
    [SerializeField] private TextMeshProUGUI _commandText;

    private SkillSO _skill;

    public void Init(SkillSO skill) {
        _skill = skill;

        _skillIcon.sprite = _skill.icon;

        StringBuilder command = new StringBuilder();
        for(int i = 0; i < _skill.command.Length - 1; ++i) {
            command.Append(_skill.command[i] + "-");
        }
        command.Append(_skill.command[_skill.command.Length - 1]);
        _commandText.text = command.ToString();
    }

    public void Close() {
        Destroy(gameObject);
    }
}
