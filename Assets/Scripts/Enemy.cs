using UnityEngine;

public class Enemy : MonoBehaviour
{
   public void TakeDamage()
   {
      // Implement damage logic here (e.g., reduce health, play hit animation, etc.)
      Debug.Log(gameObject+"took some damage!");
      Destroy(gameObject); // For demonstration, we destroy the enemy immediately.
    }
  
}
