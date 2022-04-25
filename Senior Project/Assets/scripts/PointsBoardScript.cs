using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsBoardScript : MonoBehaviour
{
    private Text pointsText;
    private static int pointsInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        pointsText = gameObject.GetComponent<Text>();
        pointsText.text = pointsInt.ToString();
    }

    public void AddPoints(int pointsToAdd)
    {
        pointsInt += pointsToAdd;
        pointsText.text = pointsInt.ToString();
    }
}
