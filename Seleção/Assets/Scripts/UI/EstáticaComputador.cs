using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaticEffect : MonoBehaviour
{
    [Header("Referências")]
    public Image staticImage;

    [Header("Frames da estática")]
    public Sprite[] frames;

    [Header("Velocidade")]
    public float minTime = 0.03f;
    public float maxTime = 0.08f;

    [Header("Transparência")]
    [Range(0f, 1f)]
    public float minAlpha = 0.15f;

    [Range(0f, 1f)]
    public float maxAlpha = 0.3f;

    private int currentFrame = -1;

    void OnEnable()
    {
        StartCoroutine(StaticLoop());
    }

    IEnumerator StaticLoop()
    {
        while (true)
        {
            int nextFrame = Random.Range(0, frames.Length);

            // impede repetição do mesmo frame
            while (nextFrame == currentFrame)
            {
                nextFrame = Random.Range(0, frames.Length);
            }

            currentFrame = nextFrame;

            // troca sprite
            staticImage.sprite = frames[currentFrame];

            // alpha aleatório
            Color color = staticImage.color;
            color.a = Random.Range(minAlpha, maxAlpha);
            staticImage.color = color;

            // tempo aleatório
            yield return new WaitForSeconds(
                Random.Range(minTime, maxTime)
            );
        }
    }
}