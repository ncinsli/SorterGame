using UnityEngine;
public class ShootData : MonoBehaviour{
    public float power;
    private void OnCollisionEnter2D(Collision2D collision){ if (collision.gameObject.GetComponent<MoveableByJoystick>() == null) Destroy(gameObject); }
}