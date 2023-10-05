using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Win_Lose_Check : MonoBehaviour
{
    public GameObject Win_img1;
    public GameObject Lose_img1;
    public GameObject Win_img2;
    public GameObject Lose_img2;
    int goal = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("p1") && goal == 0)
        {
            Win_img1.SetActive(true);
            Lose_img2.SetActive(true);
            goal = 1;
        }
        if(other.CompareTag("p2") && goal == 0)
        {
            Win_img2.SetActive(true);
            Lose_img1.SetActive(true);
            goal = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
