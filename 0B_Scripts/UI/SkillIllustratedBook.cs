using UnityEngine;

public class SkillIllustratedBook : MonoBehaviour
{
    [SerializeField] private Transform _content;

    [SerializeField] private SkillListSO _skillList;

    [SerializeField] private SkillIndex _prefab;

    private void Awake() {
        Create();
    }

    public void Create() {
        int count = _skillList.list.Count;

        RectTransform contentTrm = _content.GetComponent<RectTransform>();
        contentTrm.sizeDelta = new Vector3(0, count * 110f - 10f);

        for(int i = 0; i < count; ++i) {
            SkillIndex skillIndex = Instantiate(_prefab, _content);
            skillIndex.Init(_skillList.list[i], transform);
        }
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
