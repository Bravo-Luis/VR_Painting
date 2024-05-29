using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIText : MonoBehaviour
{

    public TMPro.TextMeshPro instructionText;

    public int index = 0;

    public List<string> instructions = new List<string>(10);

    
    // Start is called before the first frame update
    void Start()
    {
        instructions.Add("To start tracing the Soccer Ball model please click next and click next after you finish each step");
        instructions.Add("First, change your brush color to white by pressing the trigger on your left controller");
        instructions.Add("Position yourself close to the transparent model of the soccer ball");
        instructions.Add("Adjust the size of your brush using the (    ) button on the left controller so that it is easy to make small paint strokes");
        instructions.Add("Use your right controller to fill in the white portions of the soccer ball");
        instructions.Add("Change your brush color to black");
        instructions.Add("Now fill in the black portions of the soccer ball");
        instructions.Add("Great work! If you want to try a more difficult model you can try tracing the mushroom by selecting (    )");
        instructionText.text = instructions[index];
    }

    public void next()
    {
        if (index < instructions.Count - 1)
        {
            index++;
            instructionText.text = instructions[index];
        }
        else
        {
            return;
        }
    }

    public void back()
    {
        if (index > 0)
        {
            index--;
            instructionText.text = instructions[index];
        }
        else
        {
            return;
        }
    }
}
