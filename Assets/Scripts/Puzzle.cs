using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class Puzzle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParsePuzzleFile("5x5 9.c");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ParsePuzzleFile(string filename)
    {
        int counter = 0;  
        string line;  
  
        StreamReader file = new StreamReader(@"Assets\Puzzles\" + filename);
        while((line = file.ReadLine()) != null)  
        {  
            Debug.Log(line);  
            counter++;  
        }  
        
        file.Close();  
        Debug.Log("There were " + counter + " lines.");
    }
}
