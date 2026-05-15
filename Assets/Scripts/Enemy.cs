using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected string EnemyName;
   [SerializeField] protected float moveSpeed;

    public void Update()
    {
       // MoveAround();

        if(Input.GetKeyDown(KeyCode.F))
        Attack();
    }
      /*Protected method is used to define common behavior for all enemy types, 
       while allowing derived classes to add their own specific behavior.*/
      //Protected method can be accessed by derived classes, but not from outside the class hierarchy.
    protected void MoveAround()
    {
        Debug.Log(EnemyName  +  "moves at speed"  +  moveSpeed);
    } 
    //Protected virtual method allows derived classes to override the Attack method
    //  to provide specific behavior for different enemy types.
    protected virtual void Attack()
    {
        Debug.Log(EnemyName  +  "attacks!");
    }
     public void TakeDamage()
    {
        
    }
     public string GetEnemyName()
    {
         return EnemyName;
     }
}
