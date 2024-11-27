using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemGenerator : MonoBehaviour
{
    private string _current;
    // public Dictionary<char, string> rules = new Dictionary<char, string>();

  
    // Start is called before the first frame update
    void Start()
    {
        // GenerateLSystem();
        
    }

    public void GenerateLSystem()
    {
        //set main camera position
        Camera.main.transform.position = UIConfig.cameraPosition;
        
        //make camera look at vector 3 0 offset by uiconfig vertical offset
        Camera.main.transform.LookAt(Vector3.zero + Vector3.up * UIConfig.cameraVerticalOffset);
        
        _current = UIConfig.Axiom; // Start with Axiom
        // rules.Add('F', "FF+[+F-F-F]-[-F+F+F]"); // Rule 1 tried from " Nature of Code " Tutorial linked in learn.gold

        for (int i = 0; i < UIConfig.Iterations; i++) // Number of generations
        {
            string nextString = "";
            foreach(char c in _current)
            {
                if(UIConfig.Rules.ContainsKey(c))
                {
                    nextString += UIConfig.Rules[c]; // If the character is in the rules dictionary, replace it with the value
                }
                else
                {
                    nextString += c.ToString();
                }
            }
            _current = nextString;
        }
        // Debug.Log(_current);

        LSystemPen pen = GetComponent<LSystemPen>();
        pen.DrawLSystem(_current);
    }

    

    

   
}
