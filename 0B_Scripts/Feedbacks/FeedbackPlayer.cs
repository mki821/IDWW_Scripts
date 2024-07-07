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

        //UniRX << 신기한거
        //_feedbackToPlay = new List<Feedback>();
        //Getcomponents<Feedback>(_feedbackToPlay);
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        _feedbackToPlay.ForEach(f => f.CreateFeedback());
        //모바일ㅇㅔㅅㅓㄴㅡㄴ for문 써라
    }

    public void FinishFeedback()
    {
        _feedbackToPlay.ForEach(f => f.FinishFeedback());
    }
}
