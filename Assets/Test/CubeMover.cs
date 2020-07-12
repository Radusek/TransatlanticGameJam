using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public float speed = 1f;


    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(xMove, 0f, zMove).normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
