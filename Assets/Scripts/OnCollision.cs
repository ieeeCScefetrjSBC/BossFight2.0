using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnCollision : MonoBehaviour { 

    void Update()
    {
    CharacterController controller = GetComponent<CharacterController>();
        if (controller.collisionFlags == CollisionFlags.None)
            
            print("Free floating!");

        if ((controller.collisionFlags & CollisionFlags.Sides) != 0)
            
           print("Touching sides!");

        if (controller.collisionFlags == CollisionFlags.Sides)
            print("Only touching sides, nothing else!");

        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
            print("Touching sides!");

        if (controller.collisionFlags == CollisionFlags.Above)
            print("Only touching Ceiling, nothing else!");

        if ((controller.collisionFlags & CollisionFlags.Below) != 0)
            
        print("Touching ground!");

        if (controller.collisionFlags == CollisionFlags.Below)
            
            print("Only touching ground, nothing else!");
}

   
}