using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }
    public void DamageTargets() => entity.DamageTargets();
    //Call method from Player script to Stop the player movement while attacking.
    private void DisableJumpAndMovemnet() => entity.EnableJumpAndMovemnet(false);

    private void EnableJumpAndMovemnet() => entity.EnableJumpAndMovemnet(true);

}

