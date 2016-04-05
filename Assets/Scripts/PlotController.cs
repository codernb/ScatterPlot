using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Collections.Generic;

public class PlotController : MonoBehaviour
{

    public GameObject Dot;

    private char[] newLineDelimiter = new char[] {'\n'};
    private char[] commaDelimiter = new char[] {','};

    private string[] axisKeys;
    private int[] selectedAxisKeys = new int[3];

    private List<GameObject> dots = new List<GameObject>();

    private int axisToSelect = -1;

    public void SelectData()
    {
        foreach (var d in dots) {
            Destroy(d);
            dots.Remove(d);
        }
        var filePath = EditorUtility.OpenFilePanel("Select *.data File", "", "data");
        if (filePath.Length == 0)
            return;
        var text = File.ReadAllText(filePath);
        var lines = text.Split(newLineDelimiter);
        axisKeys = lines[0].Split(commaDelimiter);
        for (int i = 1; i < lines.Length; i++) {
            var values = lines[i].Split(commaDelimiter);
            var vals = new float[values.Length];
            for (int j = 0; j < values.Length; j++)
                if (values[j].Length > 0) {
                    vals[j] = float.Parse(values[j]);
                    GameObject dot = (GameObject) Instantiate(Dot);
                    dot.GetComponent<DotController>().Values = vals;
                    dots.Add(dot);
                }
        }
    }

    public void SelectXAxis()
    {
        if (axisKeys == null)
            return;
        axisToSelect = 0;
    }
    
    public void SelectYAxis()
    {
        if (axisKeys == null)
            return;
        axisToSelect = 1;
    }
    
    public void SelectZAxis()
    {
        if (axisKeys == null)
            return;
        axisToSelect = 2;
    }

    void OnGUI()
    {
        if (axisToSelect > -1) {
            var selection = GUI.SelectionGrid(new Rect(10, 10, 600, 300), -1, axisKeys, 4);
            selectedAxisKeys[axisToSelect] = selection;
            if (selection > -1) {
                foreach (var d in dots)
                    d.GetComponent<DotController>().SetAxis(axisToSelect, selection);
                axisToSelect = -1;
            }
        }
    }

}
