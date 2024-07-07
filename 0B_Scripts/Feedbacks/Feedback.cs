using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    //�� �ǵ�餱������ �ؾ� �� �Ϥ��� �ٸ��� ��
    public abstract void CreateFeedback();
    public abstract void FinishFeedback();

    protected Agent _owner;

    protected virtual void Awake()
    {
        _owner = transform.parent.GetComponent<Agent>();
    }

    private void OnDisable()
    {
        //Tween�� ���� �����
        FinishFeedback();
    }
}
