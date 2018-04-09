using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	private bool Jumping = false; // Está pulando
	private bool JumpRequest = false;
	private float JumpForce = 0.5f;
	private float FallForce = -0.01f;
	private GameObject Grounder;

	void Start ()
	{
		
		Grounder = GameObject.FindWithTag("Grounder");
	}
	
	void Update () 
	{
	
		if(Input.GetKeyDown(KeyCode.Space) && Grounder.GetComponent<Grounded>().getGrounded())
			JumpRequest = true;

		if(JumpRequest)
		{
			if(!Jumping)
			{
				GetComponent<Mov>().setExtra_Y(JumpForce);
				Jumping = true;
			}
			else
			{
				if(GetComponent<Rigidbody>().velocity.y <= 0)
						FallForce -= Time.deltaTime/15;
						
				GetComponent<Mov>().setExtra_Y(FallForce);

				if(Grounder.GetComponent<Grounded>().getGrounded() && FallForce <= -0.02f)
				{
					JumpRequest = false;
					Jumping = false;
					GetComponent<Mov>().setExtra_Y(-GetComponent<Mov>().getExtra_Y());
					FallForce = -0.01f;
				}
			}
		}
	}
}
