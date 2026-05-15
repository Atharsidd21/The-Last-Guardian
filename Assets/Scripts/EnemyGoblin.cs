using UnityEngine;

public class EnemyGoblin : Enemy
{
   /*Inheritance:- It refers to the ability of a class to inherit properties and methods from another class.
    In this example, the EnemyGoblin class inherits from the Enemy class, allowing it to reuse the MoveAround and Attack methods defined in the Enemy class. 
    This promotes code reusability and helps to create a hierarchical relationship between classes.
   Polymophism:- It refers to the ability of a class to take on many forms.
    In this example, the EnemyGoblin class inherits from the Enemy class 
    and can override the Attack method to provide specific behavior for goblins.
    Encapsulation:- It refers to the bundling of data and methods that operate on that data within a single unit.
    In this example, the EnemyGoblin class encapsulates its own properties and methods, 
    providing a clean interface for interacting with goblin-specific functionality.
    */

    public int MoneySteal;
    //Can override the Attack method to add additional behavior specific to the Goblin enemy type.
   protected override void Attack() 
    {
        base.Attack();
        StealMoney();
    }
    private void StealMoney()
    {

        Debug.Log(EnemyName + " steals " + MoneySteal + " gold coins!");
    }

}

