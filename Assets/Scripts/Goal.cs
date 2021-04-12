using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool touched = false;
    public int counter = 0;
    public bool IsAchieved()
    {
        return touched;
    }
    public void Complete()
    {

    }
    private void Update()
    {
        if(counter == 0 && touched)
        {
            touched = false;
        }
        if(touched)
        {
            counter--;
        }
    }
}
