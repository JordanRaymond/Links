using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float range = 15f;
    public float fireRate = 1f;

    public string enemyTag = "Enemy";

    private float fireContdown = 0f;
    private Transform target;


    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);	
	}

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }
	
	void Update () {
        if (target == null) {
            return;
        }

        if (fireContdown <= 0f) {
            Shoot();
            fireContdown = 1f / fireRate;
        }

        fireContdown -= Time.deltaTime;
	}

    void Shoot() {
        Debug.Log("Shoot");
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
