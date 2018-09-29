using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shot : MonoBehaviour {

	public float health = 50f;


	public void takeDamage (float amount)
	{
		health -= amount;
	}

	// Use this for initialization
	public void GotShot()
	{
		StartCoroutine(Die());
	}

	private IEnumerator Die()
	{
		this.transform.Rotate(-75,0,0);
		yield return new WaitForSeconds (1.5f);

		Destroy (this.gameObject);
	}
}
