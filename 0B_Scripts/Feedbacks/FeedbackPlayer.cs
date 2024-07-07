using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbackToPlay;

    private void Awake()
    {
        _feedbackToPlay = GetComponents<Feedback>().ToList();

        //UniRX << �ű��Ѱ�
        //_feedbackToPlay = new List<Feedback>();
        //Getcomponents<Feedback>(_feedbackToPlay);
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        _feedbackToPlay.ForEach(f => f.CreateFeedback());
        //����Ϥ��Ĥ��ä��Ѥ� for�� ���
    }

    public void FinishFeedback()
    {
        _feedbackToPlay.ForEach(f => f.FinishFeedback());
    }
}
