using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheatcodes : MonoBehaviour
{
    [SerializeField]
    private string nextLevel = "ascend";

    private FrogController frogController;

    private KeyCode[] nextLevelKeySequence;

    private int cheatIndex = 0;

    private void ParseCheat(ref KeyCode[] keySequence, string cheatText)
    {
        for (int codeLetter = 0; codeLetter < cheatText.Length; ++codeLetter)
        {
            keySequence[codeLetter] = (KeyCode)System.Enum.Parse(typeof(KeyCode), cheatText.Substring(codeLetter, 1).ToUpper());
        }
    }

    private void NextLevelCheat()
    {
        frogController.WinLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(nextLevelKeySequence[cheatIndex]))
        {
            cheatIndex++;
            if (cheatIndex >= nextLevelKeySequence.Length)
            {
                cheatIndex = 0;
                NextLevelCheat();
            }
        }
        else if (Input.GetKeyDown(nextLevelKeySequence[0]))
        {
            cheatIndex = 1;
        }
        else if (Input.anyKeyDown)
        {
            cheatIndex = 0;
        }
    }

    private void Awake()
    {
        frogController = GetComponent<FrogController>();
        nextLevelKeySequence = new KeyCode[nextLevel.Length];
        ParseCheat(ref nextLevelKeySequence, nextLevel);
    }
}