using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBB : MonoBehaviour
{
    
    public GameObject shotPrefab;
    public float shotInterval;
    
    void Start() {
        InvokeRepeating("Shoot", 0f, shotInterval);
    }
    
    void Shoot() {
        Instantiate(shotPrefab, transform.position, Quaternion.identity);
    }
}
