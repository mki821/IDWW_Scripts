using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour, IMovement
{
    private Agent _agent;
    private NavMeshAgent _navAgent;
    private Rigidbody _rigidbody;

    public Vector3 Velocity => _navAgent.velocity;

    public bool IsGround { get; private set; }

    [SerializeField] private float _knockBackThreshold;
    [SerializeField] private float _maxKncokBackTime;

    private float _knockBackTime;
    private bool _isKnockBack;

    public void Initialize(Agent agent) {
        _agent = agent;

        _rigidbody = GetComponent<Rigidbody>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        DisableRigidbody();
    }

    public void SetDestination(Vector3 destination) {
        if(!_navAgent.enabled) return;

        _navAgent.isStopped = false;
        _navAgent.SetDestination(destination);
    }

    public void StopImmediately() {
        if(!_navAgent.enabled) return;

        _navAgent.isStopped = true;
        _navAgent.destination = transform.position;
    }

    public bool IsMove() {
        return Vector3.Distance(transform.position, _navAgent.destination) > _navAgent.stoppingDistance;
    }

    public void GetKnockBack(Vector3 force) {
        StartCoroutine(ApplyKnockBack(force));
    }

    private IEnumerator ApplyKnockBack(Vector3 force) {
        _navAgent.enabled = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        _knockBackTime = Time.time;

        if(_isKnockBack) yield break;

        _isKnockBack = true;

        yield return new WaitForFixedUpdate();

        yield return new WaitUntil(CheckKnockBackEnd);

        DisableRigidbody();

        _navAgent.Warp(transform.position);
        _isKnockBack = false;

        if(!_agent.isDead) _navAgent.enabled = true;
    }

    private bool CheckKnockBackEnd() {
        return _rigidbody.velocity.magnitude < _knockBackThreshold || Time.time > _knockBackTime + _maxKncokBackTime;
    }

    public void DisableMovementAgent() {
        StopAllCoroutines();
        DisableRigidbody();
        _navAgent.enabled = false;
    }

    private void DisableRigidbody() {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    public void SetMovement(Vector3 movement, bool isRotation = true) {
        
    }

    public void GetKnockback(Vector3 force) {
        
    }
}
