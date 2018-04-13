using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDamage : MonoBehaviour
{

    int damagepillar = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void OncollisionEnter(Collision _collision)
    {
        if (_collision.Fire Pillar.tag == "damage by fire" ) {
            Vida_Player -= damagepillar;
            print(Vida_Player)
          }
    }
}