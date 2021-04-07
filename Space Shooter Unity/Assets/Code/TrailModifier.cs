using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailModifier : MonoBehaviour
{
    /*
     
    Has: 
        -collision sprite prefab: "TrailCollision"
            -2D circle collider
        -frame amount until next spawn
        -collision sprite prefab: "TrailDamage"
            -2D circle collider
       
    Does:
        -detects for a collision between TrailCollision and PlayerShip 
        -spawn a TrailCollision prefab every x frames
        -detects time of life for colliding TrailCollion
            -detect Trail Collision with half of that time, 
             3/4 of that time, and 1/4 of that time: "A", "B", and "C"
            -detect distance between A and playerShip: "D" 
            -detect distance between B and C: "E"
            -find the average distance between D and E: "F"
        -create TrailDamage with a diameter of F
        -damage EnemyShips inside of TrailDamage
        





    */
}
