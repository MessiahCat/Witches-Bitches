using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer_UI : MonoBehaviour
{
    #region data
    [SerializeField]
    private int time;
    Text text;
    private float t = 1;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Canvas/TimerCanvas/Timer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {  
        TimeCount();
        JumpSense();
    }

    #region Function
    private void TimeCount() 
    {
        t = t - Time.deltaTime;
        if (t <= 0)
        {
            time -= 1;
            t = 1;
        }

        if ( time<= 10)
        {
            text.color = Color.red;
        }
        text.text = "Time: "+time.ToString();
    }

    private void JumpSense() 
    {
        if (time <= 0)
        {
            SceneManager.LoadScene("Win");
        }

        //else
        //{
       //     SceneManager.LoadScene("Lose");
      //  }
    }
    #endregion
}
