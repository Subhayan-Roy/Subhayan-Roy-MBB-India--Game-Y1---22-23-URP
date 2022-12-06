using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRoutineClass : MonoBehaviour
{
    public bool Tree;
    
    IEnumerator ToggleBool()
    {   
        
        yield return new WaitForSeconds(3f); //This is the syntax to remember. Put the time you want to wait for within the parantheses 
        Tree = !Tree;
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToggleBool());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
