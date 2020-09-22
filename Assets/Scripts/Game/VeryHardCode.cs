using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeryHardCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Difficulty.currentDifficultyLevel != Difficulty.DifficultyLevel.VeryHard)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
