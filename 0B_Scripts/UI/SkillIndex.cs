using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillIndex : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;

    [SerializeField] private SkillBook _prefab;

    private SkillSO _skill;
    private Transform _ilustratedBookTrm;

    public void Init(SkillSO skill, Transform trm) {
        _skill = skill;
        _ilustratedBookTrm = trm;

        _icon.sprite = _skill.icon;
        _name.text = _skill.skillName;
    }

    public void OnPointerClick(PointerEventData eventData) {
        Open();
    }

    public void Open() {
        Vector3 position = new Vector3(_ilustratedBookTrm.position.x, transform.position.y, 0f);
        if(_ilustratedBookTrm.position.x < Screen.width / 2)
            position.x += 493f;
        else
            position.x -= 493f;

        SkillBook skillBook = Instantiate(_prefab, position, Quaternion.identity, transform.root);
        skillBook.Init(_skill);
    }
}
