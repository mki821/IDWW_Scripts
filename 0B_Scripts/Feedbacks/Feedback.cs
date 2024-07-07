using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    //각 피드백ㅁㅏㄷㅏ 해야 할 일ㅇㅣ 다르ㄹ 것
    public abstract void CreateFeedback();
    public abstract void FinishFeedback();

    protected Agent _owner;

    protected virtual void Awake()
    {
        _owner = transform.parent.GetComponent<Agent>();
    }

    private void OnDisable()
    {
        //Tween이 원격 제어라서
        FinishFeedback();
    }
}
