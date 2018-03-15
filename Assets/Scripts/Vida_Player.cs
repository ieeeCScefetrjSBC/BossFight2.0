using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Class1
{
    public float vida = 100f;

    void onCollisionEnter3D(Collision col) // quando o golpe do inimigo atingir o personagem
    {
        if (col.transform.tag == "Dano1") // se for o dano fraco, dá 10 de dano
        {
            vida -= 10f;
        }
        if (col.transform.tag == "Dano2") // se for o dano forte, dá 30 de dano
        {
            vida -= 30f;
        }
    }

    void Update()
    {
        vida = Mathf.Clamp(vida, 0, 100); // indica o mínimo e máximo da vida do personagem

        if (vida == 0) // se a vida chegar a 0 chama a tela de gameover
        {
            Application.LoadLevel("GameOver");
        }
    }



}