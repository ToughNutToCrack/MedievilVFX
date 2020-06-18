using UnityEngine;

public class SpiritMovement : MonoBehaviour
{
    public float rate = 2;
    public float ySpeed = 1, xRange = 1f;
    public float incrementSpeed = 1;
    public Transform finalPosition;
    float elapsedTime;
    TrailRenderer tRenderer;

    private void Start()
    {
        tRenderer = GetComponent<TrailRenderer>();
        transform.position = finalPosition.position;
        Destroy(gameObject, 5);
    }


    void Update()
    {
        elapsedTime += Time.deltaTime / incrementSpeed;
        tRenderer.time = Mathf.Lerp(0.1f, 1.5f, elapsedTime);

        float x = Mathf.Cos(rate * Time.time);

        gameObject.transform.localPosition = new Vector3(transform.position.x, gameObject.transform.localPosition.y + Time.deltaTime * ySpeed, x * xRange);
    }
}
