using UnityEngine;

public class BattleZone : MonoBehaviour
{
    [SerializeField] private ParticleSystemRenderer[] _particles;
    [SerializeField] private Color[] _color;

    private int _colorHash = Shader.PropertyToID("_TintColor");

    public void Loop(bool flag) {
        if(flag) {
            for(int i = 0; i < _particles.Length; ++i) {
                ParticleSystem p = _particles[i].GetComponent<ParticleSystem>();
                p.Play();
            }
        }
        else {
            for(int i = 0; i < _particles.Length; ++i) {
                ParticleSystem p = _particles[i].GetComponent<ParticleSystem>();
                p.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
    
    public void SetColor(int index) {
        for(int i = 0; i < _particles.Length; ++i) {
            _particles[i].material.SetColor(_colorHash, _color[index] * 3f);
        }
    }
}
