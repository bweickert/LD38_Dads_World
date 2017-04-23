using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [SerializeField]
    public GameObject[] dangerousPlaces;
    public float daddyValue = 100f;
    public bool won = false;

    private bool gameFinished = false;
    [SerializeField]
    private float timeLeft = 10f;
    [SerializeField]
    private int numeberOfTaskForWin = 5;
    private int taskDone = 0;
    [SerializeField]
    private float daddyReduce = 10f;
    [SerializeField]
    private float daddyReduceTicker = 1f;
    [SerializeField]
    private float timerAfterLastReduce = 0f;
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Toggle toiletToggle;
    [SerializeField]
    private Toggle laundryToggle;
    [SerializeField]
    private Toggle kitchenToggle;
    [SerializeField]
    private Toggle plantToggle;
    [SerializeField]
    private Toggle mealToggle;
    [SerializeField]
    private Text timeLeftText;

    // Use this for initialization
    void Start () {

        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameFinished)
            return;
        timeLeft -= Time.deltaTime;
        timeLeftText.text = Mathf.CeilToInt(timeLeft).ToString();
        if (timeLeft <= 10)
            timeLeftText.color = Color.red;

        if (daddyValue < 0 || (timeLeft <= 0f && !won))
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
            gameFinished = true;
        }
            

        if (taskDone >= numeberOfTaskForWin)
        {
            Debug.Log("You have Won");
            won = true;
            SceneManager.LoadScene("GameOver");
            gameFinished = true;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
            
	}

    public void BabyInTheDangerZone()
    {
        if (timerAfterLastReduce > daddyReduceTicker)
        {
            Debug.Log("Reduce");
            daddyValue = daddyValue - daddyReduce;
            healthBar.value = daddyValue;
            timerAfterLastReduce = 0f;
            return;
        }
        timerAfterLastReduce = timerAfterLastReduce + Time.deltaTime;
    }

    public void FinishedTask(string taskName)
    {
        taskDone++;
        switch (taskName)
        {
            case "laundry":
                laundryToggle.isOn = true;
                break;
            case "toilet":
                toiletToggle.isOn = true;
                break;
            case "meal":
                mealToggle.isOn = true;
                break;
            case "kitchen":
                kitchenToggle.isOn = true;
                break;
            case "plant":
                plantToggle.isOn = true;
                break;
            default:
                break;
        }
    }
}
