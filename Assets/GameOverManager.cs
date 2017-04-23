using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    private string winText;
    [SerializeField]
    private string looseText;
    [SerializeField]
    private Text winLooseText;
    [SerializeField]
    private AudioSource winSound;
    [SerializeField]
    private AudioSource looseSound;


    public bool win = true;

	// Use this for initialization
	void Start () {
        GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
        win = gc.won;
        if (!win)
        {
            winLooseText.text = looseText;
            looseSound.PlayDelayed(0.5f);
        }

        else
        {
            winLooseText.text = winText;
            winSound.PlayDelayed(0.5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void BackToMainMenuPressed ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
