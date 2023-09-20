using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFPS : MonoBehaviour
{
    [SerializeField]
    Text text;

    float frames = 0f;
    float timeElap = 0f;
    float frameTime = 0f;

    private void Update()
    {
        frames++;
        timeElap += Time.unscaledDeltaTime;

        if (timeElap > 1f)
        {
            frameTime = timeElap / (float)frames;
            timeElap -= 1f;
            UpdateText();
            frames = 0;
        }
    }
    void UpdateText()
    {
        text.text = string.Format(
            "FPS : {0}, FrameTime : {1:F2} ms, {2}p",
            frames, frameTime * 1000f, Screen.height);
    }
}
