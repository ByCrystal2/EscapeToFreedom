using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : MonoBehaviour
{
    private float Vertical;
    private float RepetitionAmount;
    Vector3 startPosition;
    Vector3 endPosition;
    float lerpSpeed = 2f;
    float zControl;
    float delayTime;
    float perlinSpeed;
    float perlinScale;

    private void Awake()
    {
        startPosition = transform.localPosition;
        endPosition = new Vector3(Random.Range(startPosition.x, startPosition.x + 30), startPosition.y, startPosition.z);
        perlinScale = Random.Range(0, 10);
        Vertical = Random.Range(-1.2f, 2f);
        while (Vertical == 0)
        {
            Vertical = Random.Range(-1.2f, 2f);
        }
        RepetitionAmount = Random.Range(1, 6);
        zControl = Random.Range(0.5f, 1f);
        perlinSpeed = Random.Range(5, 12);
    }

    private void Update()
    {
        BallHzStart();
    }

    private void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * lerpSpeed, 1.0f);
        transform.localPosition = new Vector3(Mathf.Lerp(startPosition.x, endPosition.x, t), transform.localPosition.y, transform.localPosition.z);

        float x = transform.position.x + Mathf.PerlinNoise(Time.time * perlinSpeed, 0.1f) * perlinScale;
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    void BallHzStart()
    {
        delayTime = Random.Range(0.5f, 1f);
        float x = transform.localPosition.x;
        float y = Mathf.Sin(Time.time * RepetitionAmount) * Vertical;
        float z = transform.localPosition.z + Mathf.Sin(Time.time * zControl) * delayTime;
        transform.localPosition = new Vector3(x, y, z);
    }
}
