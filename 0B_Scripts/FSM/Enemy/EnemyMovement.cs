using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IMovement
{
    private Enemy _enemy;
     public NavMeshAgent navAgnet;
    private Rigidbody _rigidbody;

    public Vector3 Velocity => navAgnet.velocity;

    public bool IsGround { get; private set; }

    [SerializeField] private float _knockBackThreshold;
    [SerializeField] private float _maxKnockBackTime;

    private float _knockBackTime; // �˹� �ð� ����
    private bool _isKnockBack; // �˹�����?

    public void GetKnockback(Vector3 force)
    {
        StartCoroutine(ApplyKnockback(force));
    }

    private IEnumerator ApplyKnockback(Vector3 force)
    {
        navAgnet.enabled = false; // �׺�޽ð� �ڲ� ���ڸ��� ��
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        _knockBackTime = Time.time;

        if (_isKnockBack)
        {
            yield break; // �ڷ�ƾ ����
        }

        _isKnockBack = true;
        yield return new WaitForFixedUpdate(); // ���� �����Ӹ�ŭ ���

        yield return new WaitUntil(CheckKnockbackEnd);

        DisableRigidbody();

        navAgnet.Warp(transform.position);
        _isKnockBack = false;

        if (!_enemy.isDead)
        {
            navAgnet.enabled = true;
        }

        yield return new WaitForFixedUpdate(); // ���� �����Ӹ�ŭ ���
    }

    public void DisableMovementAgent()
    {
        StopAllCoroutines();
        DisableRigidbody();
        navAgnet.enabled = false;
    }

    private void DisableRigidbody()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    public void EnableMovementAgent()
    {
        EnableRigidbody();
        navAgnet.enabled = true;
    }

    private void EnableRigidbody()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }

    //�ܶ�ȸ�� ����
    //shot
    private bool CheckKnockbackEnd()
    {
        return _rigidbody.velocity.magnitude < _knockBackThreshold ||
                Time.time > _knockBackTime + _maxKnockBackTime;
    }

    public void Initialize(Agent agent)
    {
        _enemy = agent as Enemy;
        navAgnet = GetComponent<NavMeshAgent>();
        navAgnet.speed = _enemy.moveSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetDestination(Vector3 destination)
    {
        if (navAgnet.enabled == false) return;
        navAgnet.speed = _enemy.Stat.moveSpeed.GetValue();
        navAgnet.isStopped = false;
        navAgnet.SetDestination(destination);
    }

    public void SetMovement(Vector3 movement, bool isRotation = true)
    {
        //�̰� Enemy���� �Ƚ�.
    }

    public void StopImmediately()
    {
        if (navAgnet.enabled == false) return;
        navAgnet.isStopped = true;
    }

    public bool IsMove()
    {
        return Vector3.Distance(transform.position, navAgnet.destination) > navAgnet.stoppingDistance;
    }
}
