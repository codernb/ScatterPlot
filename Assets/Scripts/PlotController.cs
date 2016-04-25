using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlotController : MonoBehaviour {

	public GameObject UICameras;
    public GameObject Dot;
    public Canvas Options;
    public int GridWidth = 600;
    public int GridHeight = 300;
    public TextMesh[] Labels = new TextMesh[3];
    public Slider[] ScaleSliders = new Slider[3];
    public Transform[] Axes = new Transform[3];
    private char[] newLineDelimiter = new char[] { '\n' };
    private char[] commaDelimiter = new char[] { ',' };
    private string[] axisKeys;
    private int[] selectedAxisKeys = new int[3] { -1, -1, -1 };
    private List<GameObject> dots = new List<GameObject>();
    private int axisToSelect = -1;

	public void Start() {
		UICameras.SetActive (false);
	}
    public float XScale {
        set {
            if (selectedAxisKeys[0] < 0)
                return;
            var max = 0f;
            foreach (var d in dots) {
                var position = d.transform.position;
                position.x = d.GetComponent<DotController>().Values[selectedAxisKeys[0]] * value;
                max = Mathf.Max(max, position.x);
                d.transform.position = position;
            }
            var scale = Axes[0].localScale;
            var pos = Axes[0].position;
            scale.x = max;
			pos.x = max / 2;
            Axes[0].localScale = scale;
            Axes[0].position = pos;
        }
    }

    public float YScale {
        set {
            if (selectedAxisKeys[1] < 0)
                return;
            var max = 0f;
            foreach (var d in dots) {
                var position = d.transform.position;
                position.y = d.GetComponent<DotController>().Values[selectedAxisKeys[1]] * value;
                max = Mathf.Max(max, position.y);
                d.transform.position = position;
            }
            var scale = Axes[1].localScale;
            var pos = Axes[1].position;
            scale.y = max;
            pos.y = max / 2;
            Axes[1].localScale = scale;
            Axes[1].position = pos;
        }
    }

    public float ZScale {
        set {
            if (selectedAxisKeys[2] < 0)
                return;
            var max = 0f;
            foreach (var d in dots) {
                var position = d.transform.position;
                position.z = d.GetComponent<DotController>().Values[selectedAxisKeys[2]] * value;
                max = Mathf.Max(max, position.z);
                d.transform.position = position;
            }
            var scale = Axes[2].localScale;
            var pos = Axes[2].position;
            scale.z = max;
			pos.z = max / 2;
            Axes[2].localScale = scale;
            Axes[2].position = pos;
        }
    }

    public void SelectData() {
        Options.enabled = false;
        FileSelector.GetFile("/sdcard", LoadData, "data");
    }

    private void LoadData(FileSelector.Status status, string filePath) {
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

    public void SelectXAxis() {
        if (axisKeys == null)
            return;
        axisToSelect = 0;
    }

    public void SelectYAxis() {
        if (axisKeys == null)
            return;
        axisToSelect = 1;
    }

    public void SelectZAxis() {
        if (axisKeys == null)
            return;
        axisToSelect = 2;
    }

	public void SelectColor() {
		if (axisKeys == null)
			return;
		axisToSelect = 3;
	}

    void OnGUI() {
        if (axisToSelect > -1) {
            Options.enabled = false;
            var gridRectange = new Rect(Screen.width / 2 - GridWidth / 2, Screen.height / 2 - GridHeight / 2, GridWidth, GridHeight);
            var selection = GUI.SelectionGrid(gridRectange, -1, axisKeys, 4);
            if (selection > -1) {
				if (axisToSelect > 2) {
					float min = float.MaxValue, max = float.MinValue;
					foreach (var d in dots) {
						var val = d.GetComponent<DotController>().Values [selection];
						min = Mathf.Min(min, val);
						max = Mathf.Max(max, val);
					}
					max -= min;
					foreach (var d in dots) {
						var val = d.GetComponent<DotController>().Values [selection] - min;
						var ratio = val / max;
						var hsb = new HSBColor(0.908333333f, ratio, 0.65f);
						d.GetComponent<SpriteRenderer>().color = hsb.ToColor();
					}
				} else {
					selectedAxisKeys[axisToSelect] = selection;
					foreach (var d in dots)
						d.GetComponent<DotController>().SetAxis(axisToSelect, selection);
					ScaleSliders[axisToSelect].value = 1;
					Labels[axisToSelect].text = axisKeys[selection];
					switch (axisToSelect) {
					case 0:
						XScale = 1;
						break;
					case 1:
						YScale = 1;
						break;
					case 2:
						ZScale = 1;
						break;
					}
                }
				Options.enabled = true;
                axisToSelect = -1;
            }
        }
    }
}
