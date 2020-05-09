using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class Puzzle : MonoBehaviour
{

    // state
    string name = "";
    int rowCount = 0;
    int colCount = 0;

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
        char[] charSeparators = new char[] { ' ', '_', ',' };

        StreamReader file = new StreamReader(@"Assets\Puzzles\" + filename);
        while((line = file.ReadLine()) != null)  
        {  
            string[] words = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);  
            counter++;  
        }  
        
        file.Close();  
        Debug.Log("There were " + counter + " lines.");
    }
}
