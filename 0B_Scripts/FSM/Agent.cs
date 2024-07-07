using System;
using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    #region Components

    public Animator AnimatorCompo { get; protected set; }
    public IMovement MovementCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }

    #endregion

    [SerializeField] protected AgentStat _agentStat;
    
    public AgentStat Stat => _agentStat;

    public bool CanStateChangeable { get; protected set; } = true;

    public bool isDead = false;

    protected virtual void Awake() {
        AnimatorCompo = transform.Find("Visual").GetComponent<Animator>();

        MovementCompo = GetComponent<IMovement>();
        MovementCompo.Initialize(this);

        HealthCompo = GetComponent<Health>();
        HealthCompo.Initialize(this);

        _agentStat = Instantiate(_agentStat);
    }

    public Coroutine StartDelayCoroutine(float time, Action callback) {
        return StartCoroutine(DelayCoroutine(time, callback));
    }

    private IEnumerator DelayCoroutine(float time, Action callback) {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    public abstract void SetDead();
}
