﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootScript : MonoBehaviour {

	public Rigidbody projectile;
    public Transform bulletSpawn;
    public float projectileForce = 500f;
    public float fireRate = .25f;

    private float nextFireTime;

	// Update is called once per frame
	void Update () {

		 if (Input.GetButtonDown ("Fire1") && Time.time > nextFireTime) 
        {
            Rigidbody cloneRb = Instantiate (projectile, bulletSpawn.position, Quaternion.identity) as Rigidbody;
            cloneRb.AddForce(bulletSpawn.transform.forward * projectileForce);
            nextFireTime = Time.time + fireRate;
        }
	}
}
