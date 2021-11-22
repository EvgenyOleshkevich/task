using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
	public Text height;
	public Text size;
	public Text countGold;
	public Text countTries;
	public Text error;
	public GameData gameData;

	void Start()
	{
		gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		if (int.TryParse(height.text, out gameData.heightsField))
		{
			if (gameData.heightsField < 1 || gameData.heightsField > 10)
			{
				error.text = "Высота должна быть от 1 до 10";
				return;
			}
		}
		else
		{
			error.text = "Высота должна целым числом";
			return;
		}

		if (int.TryParse(size.text, out gameData.sizeField))
		{
			if (gameData.sizeField < 2 || gameData.sizeField > 10)
			{
				error.text = "Размер поля должен быть от 2 до 10";
				return;
			}
		}
		else
		{
			error.text = "Размер поля должен целым числом";
			return;
		}

		int countField = gameData.sizeField * gameData.sizeField * gameData.heightsField;

		if (int.TryParse(countGold.text, out gameData.countGold))
		{
			if (gameData.countGold < 1 || gameData.countGold > countField)
			{
				error.text = $"число золота должно быть от 1 до {countField}";
				return;
			}
		}
		else
		{
			error.text = "число золота должно целым числом";
			return;
		}

		if (int.TryParse(countTries.text, out gameData.countTries))
		{
			if (gameData.countTries < 1 || gameData.countTries > countField)
			{
				error.text = $"число попыток должно быть от 1 до {countField}";
				return;
			}
		} else {
			error.text = "число попыток должно целым числом";
			return;
		}

		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}
}
