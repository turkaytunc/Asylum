using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int currentHealthPoints = 100;
    private int maxHealthPoints = 100;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealthPoints -= 10;
        }
    }





}
