using UnityEngine;

public class Indicator : MonoBehaviour
{

    // state
    int count;
    Color color;

    public void Indicator(int n, Color c)
    {
        count = n;
        color = c;
    }

}