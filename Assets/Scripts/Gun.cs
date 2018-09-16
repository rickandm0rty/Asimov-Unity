using UnityEngine;

public class Gun : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;
	public Camera fpsCam;
	public GameObject bulletPrefab;
	//public Transform bulletSpawn;
	//public ParticleSystem muzzleFlash;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			//Debug.Log("Space Key Hit");
			Shoot();		
		}
	}

	void Shoot()
	{
	//muzzleFlash.Play();

	// Create the Bullet from the Bullet Prefab
    	var bullet = (GameObject)Instantiate (bulletPrefab, transform.position,transform.rotation);

    // Add velocity to the bullet
    	bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;

    // Destroy the bullet after 2 seconds
    	Destroy(bullet, 2.0f);
		
		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			Debug.Log(hit.transform.name);

			Target target = hit.transform.GetComponent<Target>();
			if (target != null)
			{
				target.takeDamage(damage);
			}
		}

	}
}
