using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    public void DamageEnemies() => player.DamageEnemies();
        //Call method from Player script to Stop the player movement while attacking.
    private void DisableJumpAndMovemnet() => player.EnableJumpAndMovemnet(false);
    
    private void EnableJumpAndMovemnet() => player.EnableJumpAndMovemnet(true);

}

