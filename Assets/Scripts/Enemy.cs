using UnityEngine;

public class Enemy : MonoBehaviour
{
   private SpriteRenderer SR;
    // Time before the enemy is destroyed after taking damage.
   [SerializeField] private float ColorDuration = 0.5f;
   public float currentTimeinGame;
   public float LastTimeDamaged;
   private void Awake()
   {
      SR = GetComponent<SpriteRenderer>();
   }
    public void Update()
    {
        ChangeColorIfNeeded();

    }

    private void ChangeColorIfNeeded()
    {
        currentTimeinGame += Time.time;
        if (currentTimeinGame > LastTimeDamaged + ColorDuration)
        {
            if (SR.color != Color.black)
            {
                TurnBlack();
            }
        }
    }

    [ContextMenu("Update Timer")]
    //private void  UpdateTimer()=> Timer = ColorDuration;
   
    public void TakeDamage()
   {
      // Implement damage logic here (e.g., reduce health, play hit animation, etc.)
      Debug.Log(gameObject + "took some damage!");
      // Destroy(gameObject, DestructTime); // For demonstration, we destroy the enemy immediately.
      SR.color = Color.white; // Change color to red to indicate damage (for demonstration).
      LastTimeDamaged = Time.time;
   }

   private void TurnBlack()
   {
      SR.color = Color.black; // Reset color to white (for demonstration).
   }

}
