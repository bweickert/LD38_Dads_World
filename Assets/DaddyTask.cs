using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DaddyTask : MonoBehaviour {

    public float taskProgress = 0f;

    [SerializeField]
    private float progressPerHit = 0.02f;
    [SerializeField]
    private GameController gameManager;
    [SerializeField]
    private string taskName;
    [SerializeField]
    private Slider taskProgressSlider;
    [SerializeField]
    private AudioSource finishTaskAudio;
    private bool taskDone = false;
    private float timeAfterLastDoTheTask = 0;

    private float timeAfterLastProgressReducement = 0;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        taskProgressSlider.value = taskProgress;
        timeAfterLastDoTheTask += Time.deltaTime;

        timeAfterLastProgressReducement += Time.deltaTime;

        if (timeAfterLastDoTheTask >= 5 && !taskDone)
            ReduceProgress();
	}

    public void DoTheTask ()
    {
        timeAfterLastDoTheTask = 0;
        if (taskDone)
            return;
        if (taskProgress >= 100)
        {
            taskProgress = 100;
            gameManager.FinishedTask(taskName);
            taskDone = true;

            var fill = (taskProgressSlider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            if (fill != null)
            {
                fill.color = Color.Lerp(Color.red, Color.green, 0.5f);
            }

            finishTaskAudio.Play();
        }
        else
        {
            taskProgress += progressPerHit;
        }

    }

    void ReduceProgress()
    {
        if (timeAfterLastProgressReducement >= 1f)
        {
            taskProgress -= 5f;
            if (taskProgress <= 0)
                taskProgress = 0;
            timeAfterLastProgressReducement = 0;
        }
            

    }
}
