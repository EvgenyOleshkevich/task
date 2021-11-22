using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    private bool hasGold = false ;
    public bool canDig = false;
    private FieldScript underField;
    private GameRunner runner;

    void OnMouseDown()
	{
        if (canDig && runner.ÑanDig())
        {
            if (hasGold)
			{
                var position = transform.position;
                position.y -= 0.5f;
                var gold = Instantiate(runner.goldPrefab, position, Quaternion.identity).GetComponent<GoldScript>();
                gold.Init(runner, underField);
            }
            else
                if (underField is not null)
                    underField.canDig = true;
            runner.useTry();
            Destroy(gameObject);
        }
	}

    public void InstantiateGold()
    {
        hasGold = true;
    }

    public void AddUnderField(FieldScript field)
    {
        underField = field;
    }

    public void InitRunner(GameRunner runner)
    {
        this.runner = runner;
    }
}
