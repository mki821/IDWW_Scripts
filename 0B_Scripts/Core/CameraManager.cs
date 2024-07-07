using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private Transform _camera;
    [SerializeField] private LayerMask _whatIsGround;

    private CinemachineVirtualCamera _npcCam;

    private CinemachineImpulseSource _source;

    protected override void Awake() {
        base.Awake();

        _npcCam = _camera.GetChild(1).GetComponent<CinemachineVirtualCamera>();

        _source = GetComponent<CinemachineImpulseSource>();
    }

    public bool ScreenToWorld(Vector3 screenPos, out Vector3 worldPos) {
        Camera mainCam = Camera.main;
        Ray ray = mainCam.ScreenPointToRay(screenPos);

        bool result = Physics.Raycast(ray, out RaycastHit hit, mainCam.farClipPlane, _whatIsGround);
        
        worldPos = result ? hit.point : Vector3.zero;
        return result;
    }

    public Vector3 TowardMouseDirection(Transform trm, Vector3 mousePos) {
        bool isHit = ScreenToWorld(mousePos, out Vector3 worldPos);
        if(isHit) {
            Vector3 direction = worldPos - trm.position;
            direction.y = 0;
            return direction.normalized;
        }

        return trm.forward;
    }

    public void Shake(float impulsePower, float time = 0.2f) {
        _source.GenerateImpulse(impulsePower);
        _source.m_ImpulseDefinition.m_ImpulseDuration = time;
    }

    public void NpcCam(bool flag) {
        _npcCam.Priority = flag ? 15 : 5;
    }
}
