using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    private FieldScript underField;
    private GameRunner runner;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    void OnMouseDown()
    {
        if (underField is not null)
            underField.canDig = true;
        runner.AddPoints();
        Destroy(gameObject);
    }

    public void Init(GameRunner runner, FieldScript field)
    {
        underField = field;
        this.runner = runner;
    }
}
