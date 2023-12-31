using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 10.0f;

    public GameObject projectilePrefab;

    public float zMin;
    public float zMax;
    public float verticalInput;

    public Transform projectileSpawnPoint;

    public string inputID;
    public KeyCode throwFood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        transform.Translate(Vector3.right *  horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxis("Vertical" + inputID);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        if(transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if(transform.position.z < zMin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
        }

        if(transform.position.z > zMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
        }

        // 'F' 누르면 Player1이 음식을 던짐, 'M'누르면 Player2가 던짐
        if (Input.GetKeyDown(throwFood))
        {
            // Launch a projectile from the player 어떤 탄도체를 플레이어로부터 발사하게 하는 것
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
        }
    }
}
