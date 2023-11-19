using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlebTester : MonoBehaviour
{
    [SerializeField] private GameObject a;
    
    
    void Start()
    {
        "Test Log".Debug();
        "Hello Space".Debug(Color.red);
        
        
    }
}
