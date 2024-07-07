using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField] private NavMeshSurface _navSurface;
    [SerializeField] private BattleZone _battleZone;

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsBattleZone;

    public void StartBattleZone(Vector3 pos, int index) {
        _battleZone.SetColor(index);
        _battleZone.gameObject.SetActive(true);
        _battleZone.Loop(true);
        _battleZone.transform.position = pos;

        _navSurface.layerMask = _whatIsBattleZone;
        _navSurface.BuildNavMesh();
    }

    public void EndBattleZone() {
        _battleZone.Loop(false);

        StartCoroutine(EndCoroutine());
    }

    private IEnumerator EndCoroutine() {
        yield return new WaitForSeconds(2f);

        _battleZone.gameObject.SetActive(true);
        _navSurface.layerMask = _whatIsGround;
        _navSurface.BuildNavMesh();
    }
}
