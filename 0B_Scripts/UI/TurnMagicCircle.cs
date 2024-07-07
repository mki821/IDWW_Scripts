using UnityEngine;

public class TurnMagicCircle : MonoBehaviour
{
    private float currentAngle = 0f;

    private void Update() {
        currentAngle += Time.deltaTime * -60f;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
