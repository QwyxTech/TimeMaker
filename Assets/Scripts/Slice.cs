using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice
{
    
    // state
    Indicator[] myIndicators;
    Color[] myPixels;

    // construct a slice from an array of pixels
    public Slice(Color[] pixels)
    {
        myPixels = pixels;
        InitIndicators();
    }

    // construct a slice of all pixels at a specific
    // index across an array of other slices
    public Slice(Slice[] slices, int index)
    {
        List<Color> colorList = new List<Color>();

        foreach (Slice s in slices)
        {
            colorList.Add(s.ColorAt(index));
        }

        myPixels = colorList.ToArray();
        InitIndicators();
    }

    public Color ColorAt(int index)
    {
        return myPixels[index];
    }

    private void InitIndicators()
    {
        List<Indicator> indicatorList = new List<Indicator>();

        Color currentColor = Color.clear;
        int currentCount = 0;

        foreach (Color p in myPixels)
        {
            if (p == currentColor)
            {
                currentCount++;
            }
            else 
            {
                if (currentColor != Color.clear)
                {
                    indicatorList.Add(new Indicator(currentCount, currentColor));
                }

                currentColor = p;
                currentCount = 1;
            }
        }

        // Check to add the last color if needed
        if (currentColor != Color.clear)
        {
            indicatorList.Add(new Indicator(currentCount, currentColor));
        }

        myIndicators = indicatorList.ToArray();
    }

}