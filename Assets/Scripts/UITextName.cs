using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UITextName : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputText;

    public void LoadGame()
    {
        TempName.instance.playerName = inputText.text;
        SceneManager.LoadScene(1);
    }
}
