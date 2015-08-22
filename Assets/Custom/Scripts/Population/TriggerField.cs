using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerField : MonoBehaviour {
    public MoveBehaviorType Type;
    private MoveBehaviorType currentType;

    public int FieldSize = 50;

    public Dictionary<Vector2, float> Field { get; private set; }

	// Use this for initialization
	void Start () {
        Field = new Dictionary<Vector2, float>();
        GenerateField();
    }
	
	// Update is called once per frame
	void Update () {
	    if(currentType != Type)
        {
            currentType = Type;
            GenerateField();
        }
	}

    void GenerateField()
    {
        Field.Clear();
        var fieldSizeFactor = (FieldSize / 2) / (2*Mathf.PI); 
        for(int x = -FieldSize / 2; x < FieldSize / 2; x++)
        {
            for (int y = -FieldSize / 2; y < FieldSize / 2; y++)
            {
                var position = new Vector2(x, y);
                var force = 0f;
                switch (Type)
                {
                    case MoveBehaviorType.Attraction:
                        force = Mathf.Sin(Mathf.Sqrt(Mathf.Pow(x / fieldSizeFactor, 2) + Mathf.Pow(y / fieldSizeFactor, 2)));
                        break;
                    case MoveBehaviorType.Repulsion:
                        force = 1 - Mathf.Sin(Mathf.Sqrt(Mathf.Pow(x / fieldSizeFactor, 2) + Mathf.Pow(y / fieldSizeFactor, 2)));
                        break;
                }
                force++;
                force /= 2;
                Field.Add(position, force);
            }
            //var line = "";
            //for (int y = -FieldSize / 2; y < FieldSize / 2; y++)
            //{
            //    line += Field[new Vector2(x, y)].ToString("f2") + "|";
            //}
            //Debug.Log(line);
        }
    }
}
