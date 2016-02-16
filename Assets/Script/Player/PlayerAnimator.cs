using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
	
	}

	public void goDown()
	{
		anim.SetBool("Dead", true);
	}

	public void revive()
	{
		anim.SetBool("Dead", false);
	}
}
