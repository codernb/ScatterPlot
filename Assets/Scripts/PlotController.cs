using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlotController : MonoBehaviour
{

    public GameObject Dot;
    public Canvas Options;
    public int GridWidth = 600;
    public int GridHeight = 300;
    public Slider[] ScaleSliders = new Slider[3];

    public float XScale {
        set {
            if (selectedAxisKeys[0] < 0)
                return;
            foreach (var d in dots) {
                var position = d.transform.position;
                position.x = d.GetComponent<DotController>().Values[selectedAxisKeys[0]] * value;
                d.transform.position = position;
            }
        }
    }
    
    public float YScale {
        set {
            if (selectedAxisKeys[1] < 0)
                return;
            foreach (var d in dots) {
                var position = d.transform.position;
                position.y = d.GetComponent<DotController>().Values[selectedAxisKeys[1]] * value;
                d.transform.position = position;
            }
        }
    }
    
    public float ZScale {
        set {
            if (selectedAxisKeys[2] < 0)
                return;
            foreach (var d in dots) {
                var position = d.transform.position;
                position.z = d.GetComponent<DotController>().Values[selectedAxisKeys[2]] * value;
                d.transform.position = position;
            }
        }
    }

    private char[] newLineDelimiter = new char[] {'\n'};
    private char[] commaDelimiter = new char[] {','};
    private string[] axisKeys;
    private int[] selectedAxisKeys = new int[3] {-1, -1, -1};
    private List<GameObject> dots = new List<GameObject>();
    private int axisToSelect = -1;

    public void SelectData()
    {
        Options.enabled = false;
        FileSelector.GetFile("/sdcard", LoadData, "data");
    }

    private void LoadData(FileSelector.Status status, string filePath)
    {
        Options.enabled = true;
        if (status != FileSelector.Status.Successful)
            return;
        foreach (var d in dots)
            Destroy(d);
        dots.Clear();
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
            Options.enabled = false;
            var gridRectange = new Rect(Screen.width / 2 - GridWidth / 2, Screen.height / 2 - GridHeight / 2, GridWidth, GridHeight);
            var selection = GUI.SelectionGrid(gridRectange, -1, axisKeys, 4);
            selectedAxisKeys[axisToSelect] = selection;
            if (selection > -1) {
                foreach (var d in dots)
                    d.GetComponent<DotController>().SetAxis(axisToSelect, selection);
                ScaleSliders[axisToSelect].value = 1;
                Options.enabled = true;
                axisToSelect = -1;
            }
        }
    }

}
