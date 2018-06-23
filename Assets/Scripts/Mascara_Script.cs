using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara_Script : MonoBehaviour {
    private GameObject player;
    private GameObject mascara_1;
    private GameObject mascara_2;
    private GameObject mascara_3;
    private GameObject core;
    private GameObject boss;
    private Vida_Boss vidaBoss;
    private bool setMascara = true;

    private Quaternion rotateQuat;
    public float speed = 1f;

    private GameObject particle_1;
    private GameObject particle_2;
    private GameObject particle_3;

    private bool Masc1;
    private bool Masc2;
    private bool Masc3;
    private bool BossMorto;

    private GameObject[] masks = new GameObject[3];
    private GameObject[] particles = new GameObject[3];
    private GameObject activeMask;
    private int currentMaskIdx = 0;

    // Use this for initialization 
    void Start () {
        vidaBoss = this.gameObject.GetComponent<Vida_Boss>();
        player = GameObject.FindGameObjectWithTag("Player");
        mascara_1 = GameObject.FindGameObjectWithTag("Mascara1"); //Objeto mascara 1 atribuido
        mascara_2 = GameObject.FindGameObjectWithTag("Mascara2"); //Objeto mascara 2 atribuido
        mascara_3 = GameObject.FindGameObjectWithTag("Mascara3"); //Objeto mascara 3 atribuido
        core = GameObject.Find("Core");
        boss = GameObject.FindGameObjectWithTag("Boss");

        Masc1 = true;
        Masc2 = true;
        Masc3 = true;
        BossMorto = false;
        
        masks[0] = GameObject.FindGameObjectWithTag("Mascara1");
        masks[1] = GameObject.FindGameObjectWithTag("Mascara2");
        masks[2] = GameObject.FindGameObjectWithTag("Mascara3");
        activeMask = masks[0];
        
        particles[0] = GameObject.FindGameObjectWithTag("particle1");
        particles[1] = GameObject.FindGameObjectWithTag("particle2");
        particles[2] = GameObject.FindGameObjectWithTag("particle3");

        particles[0].SetActive(false);
        particles[1].SetActive(false);
        particles[2].SetActive(false);

        ChooseMask(1);
    }

    public bool GetMasc1 ()
    {
        return Masc1;
    }
    public bool GetMasc2()
    {
        return Masc2;
    }
    public bool GetMasc3()
    {
        return Masc3;
    }
    public bool GetBossMorto()
    {
        return BossMorto;
    }
    public void SetMasc1(bool val)
    {
        Masc1 = val;
    }
    public void SetMasc2(bool val)
    {
        Masc2 = val;
    }
    public void SetMasc3(bool val)
    {
        Masc3 = val;
    }


    public GameObject GetActiveMask()
    {
        return activeMask;
    }

    public void ChooseMask(int maskNum)
    {
        GameObject mask;
        int maskIdx = maskNum - 1;

        if (maskIdx < 0 || maskIdx > 2)
            //Debug.Log("ESSA MASCARA NAO EXISTE OTARIO");

        if (masks[maskIdx] == null)
            return;
        
        if (activeMask != null)
            particles[currentMaskIdx].SetActive(false);
        particles[maskIdx].SetActive(true);

        mask = masks[maskIdx];
        activeMask = mask;
        currentMaskIdx = maskIdx;

        transform.DetachChildren();
        transform.rotation = Quaternion.LookRotation(mask.transform.forward, mask.transform.up);
        core.transform.SetParent(this.transform);

        for (int idx = 0; idx < 3; idx++)
        {
            if (masks[idx] != null)
                masks[idx].transform.SetParent(this.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
      /*  if (Input.GetKeyDown(KeyCode.Alpha1))
            ChooseMask(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChooseMask(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChooseMask(3);*/
        
        if (activeMask == null)
        {
            for (int idx = 0; idx < 3; idx++)
            {
                if (masks[idx] != null)
                {
                    ChooseMask(idx + 1);
                    break;
                }
            }
        }

        if(!Masc1 && !Masc2 && !Masc3)
        {
            BossMorto = true;
        }

        float vida = vidaBoss.getvida();
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, 
                                                  Quaternion.LookRotation(this.transform.position - player.transform.position),
                                                  Time.time * speed);
    }
}
