using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * 5, rb.linearVelocityY);
    }
}
