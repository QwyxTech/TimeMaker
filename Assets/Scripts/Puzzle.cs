using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class Puzzle : MonoBehaviour
{

    // state
    string myTitle = "";
    int myRowCount = 0;
    int myColCount = 0;

    Slice[] myRows = null;
    Slice[] myCols = null;

    // Start is called before the first frame update
    void Start()
    {
        ParsePuzzleFile("Color Piskel.c");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ParsePuzzleFile(string filename)
    {
        string line;  
        char[] charSeparators = new char[] { ' ', '_', ',' };
        bool inPixelData = false;
        List<Slice> rowList = new List<Slice>();

        StreamReader file = new StreamReader(@"Assets\Puzzles\" + filename);
        while((line = file.ReadLine()) != null)  
        {
            string[] words = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0)
            {
                if (inPixelData && words[0] == "}")
                {
                    inPixelData = false;
                }

                if (inPixelData)
                {
                    rowList.Add(EvalPixels(words));
                }
                else
                {
                    EvalMeta(words);
                }

                if (words[0] == "{")
                {
                    inPixelData = true;
                }
            }
        }  
        
        file.Close();

        myRows = rowList.ToArray();

        List<Slice> colList = new List<Slice>();
        for (int index = 0; index < myColCount; index++)
        {
            colList.Add(new Slice(myRows, index));
        }
        myCols = colList.ToArray();
    }

    private void EvalMeta(string[] words)
    {
        // Get the puzzle's title
        if (myTitle == "" && words[0] == "#define")
        {
            int index = 1;
            while(words[index] != "FRAME")
            {
                myTitle += words[index];
                index++;
            }
        }

        for(int index = 0; index < words.Length; index++)
        {
            if(words[index] == "WIDTH")
            {
                myColCount = int.Parse(words[index + 1]);
            }
            else if(words[index] == "HEIGHT")
            {
                myRowCount = int.Parse(words[index + 1]);
            }
        }
    }

    private Slice EvalPixels(string[] words)
    {
        List<Color> pixelList = new List<Color>();

        foreach (string word in words)
        {
            pixelList.Add(ConvertToColor(word));
        }

        return new Slice(pixelList.ToArray());
    }

    private Color ConvertToColor(string code)
    {
        int r = int.Parse(code.Substring(8, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(code.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(code.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        int a = int.Parse(code.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);

        if(r==0 && g==0 && b==0 && a==0)
        {
            return Color.clear;
        }
        else
        {
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
        }
    }
}
