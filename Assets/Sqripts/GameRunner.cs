using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    public GameObject fieldPrefab;
    public GameObject goldPrefab;
    public Text countPickedGoldText;
    public Text remainTriesText;
    private int countPickedGold = 0;
    private int remainTries;
    private int minCamHeight = 1;
    private int maxCamHeight = 50;
    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        FieldGeneration(gameData);
        countPickedGoldText.text = $"собрано {countPickedGold} из {gameData.countGold}";
        remainTries = gameData.countTries;
        remainTriesText.text = $"осталось {remainTries} попыток";
    }

    // Update is called once per frame
    void Update()
    {
        var mw = -Input.GetAxis("Mouse ScrollWheel") * 4;
        if (transform.position.y + mw > minCamHeight
            && transform.position.y + mw < maxCamHeight)
            transform.position += new Vector3(0, mw, 0);
        
        if (Input.GetKeyDown(KeyCode.W)
            && transform.position.z < gameData.sizeField * 2)
		{
            transform.position += new Vector3(0, 0, 2);
        }
        if (Input.GetKeyDown(KeyCode.S)
            && transform.position.z > -gameData.sizeField * 2)
        {
            transform.position += new Vector3(0, 0, -2);
        }
        if (Input.GetKeyDown(KeyCode.A)
            && transform.position.x > -gameData.sizeField * 2)
        {
            transform.position += new Vector3(-2, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D)
            && transform.position.x < gameData.sizeField * 2)
        {
            transform.position += new Vector3(2, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

    private void FieldGeneration(GameData gameData)
    {
        var instantPosition = new Vector3(0, 0, 0);
        int fieldRemain = gameData.heightsField * gameData.sizeField * gameData.sizeField;
        int goldRemain = gameData.countGold;
        var upperField = new FieldScript[gameData.sizeField][];

        for (int i = 0; i < gameData.sizeField; ++i)
            upperField[i] = new FieldScript[gameData.sizeField];

        for (int height = 0; height < gameData.heightsField; ++height)
		{
            instantPosition.Set(-2 * (gameData.sizeField - 1), -height, -2 * (gameData.sizeField - 1));
            for (int x = 0; x < gameData.sizeField; ++x)
            {
                instantPosition.z = -2 * (gameData.sizeField - 1);
                for (int z = 0; z < gameData.sizeField; ++z)
                {
                    var field = Instantiate(fieldPrefab, instantPosition, Quaternion.identity).GetComponent<FieldScript>();
                    if (goldRemain > 0 && Random.value <= ((float)goldRemain) / fieldRemain)
                    {
                        --goldRemain;
                        field.InstantiateGold();
                    }
                    if (upperField[x][z] is not null)
                        upperField[x][z].AddUnderField(field);
                    else
                        field.canDig = true;

                    field.InitRunner(this);
                    upperField[x][z] = field;
                    instantPosition.z += 4;
                    --fieldRemain;
                }
                instantPosition.x += 4;
            }
        }
    }

    public void AddPoints()
	{
        ++countPickedGold;
        countPickedGoldText.text = $"собрано {countPickedGold} из {gameData.countGold}";
    }

    public void useTry()
    {
        --remainTries;
        remainTriesText.text = $"осталось {remainTries} попыток";
    }

    public bool СanDig()
    {
        return remainTries > 0;
    }
}
