using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConsoleController : MonoBehaviour
{

    public int ConsoleSize = 5;
    public Text Console;
    public Canvas ConsoleCanvas;

    private static List<string> Messages = new List<string>();

    public static void Log(object message)
    {
        Messages.Add(message.ToString());
    }

    void OnGUI()
    {
        if (Messages.Count == 0)
            return;
        if (Messages.Count > ConsoleSize)
            Messages.RemoveAt(0);
        Console.text = string.Join("\n", Messages.ToArray());
    }

}
