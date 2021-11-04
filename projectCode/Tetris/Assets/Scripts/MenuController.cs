using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] frames;
    [SerializeField]
    private float frameDelay = 0.1f;
    [SerializeField]
    private float repeatDelay = 2f;
    private float nextFrameTime;
    private float nextCycleTime;
    private int frameIndex = 0;

    [SerializeField]
    private Image titleImage;

    // Start is called before the first frame update
    void Start()
    {
        nextFrameTime = Time.time + frameDelay;
        nextCycleTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        RenderTitleGif();
    }

    // Since Unity does not support gifs, I created this method that loops through all of the sprite frames, then waits for a delay before repeating
    private void RenderTitleGif()
    {
        if (Time.time < nextCycleTime)
        {
            return;
        }

        if (Time.time >= nextFrameTime)
        {
            frameIndex++;
            if (frameIndex >= frames.Length)
            {
                frameIndex = 0;
                nextCycleTime = Time.time + repeatDelay;
            }
            else
            {
                titleImage.sprite = frames[frameIndex];
                nextFrameTime = Time.time + frameDelay;
            }
        }
    }
}
