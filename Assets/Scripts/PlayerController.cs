using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1,10)]
    [Tooltip("ËÙ¶È")]
    public float r = 5f;
    [Range(0, 1)]
    [Tooltip("·ù¶È")]
    public float amplitude = .5f;
    private float m_Time = 0;
    private Vector3 origTransform;

    public float gravity = -9.8f;
    public float jumpHeight = 1.3f;
    public Vector3 vector3 = Vector3.zero;
    public float maxValue = -10;

    public float rotationAngle = 0;
    public float rotationSpeed = 4;
    private float jumpVelocity;

    public UIManager ui;

    public State state;
    public long score=0;
    void Start()
    {
        origTransform = transform.position;
    }
    void Update()
    {
        state = GameStrateManager.GetState();
        switch (GameStrateManager.GetState())
        {
            case State.Playin:
                if (Input.GetMouseButtonDown(0))
                {
                    vector3.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                    jumpVelocity = vector3.y;
                    rotationAngle = 30;
                }
                vector3.y += gravity * Time.deltaTime;
                if (vector3.y < maxValue) vector3.y = maxValue;
                transform.position += vector3 * Time.deltaTime;

                if(vector3.y < -jumpVelocity * 0.2)
                {
                    rotationAngle -= rotationSpeed * Time.deltaTime * Mathf.Rad2Deg;
                    rotationAngle = Mathf.Max(-90,rotationAngle);
                }
                transform.eulerAngles = new Vector3(0, 0, rotationAngle);
                break;
            case State.Win:

                break;
            case State.Lose:
                float dis = -Camera.main.orthographicSize + 1.28f + 0.64f * 0.5f;
                if(transform.position.y > dis)
                {
                    transform.position += new Vector3(0, dis, 0) * Time.deltaTime;
                    transform.eulerAngles = new Vector3(0, 0, -90);
                }
                break;
            case State.None:
                m_Time += r * Time.deltaTime;
                var hight = Mathf.Sin(m_Time) * amplitude;
                transform.position = origTransform + new Vector3(0, hight, 0);
                transform.eulerAngles = new Vector3(0,0,0);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("ground") || collider.gameObject.CompareTag("pipe"))
        {
            GameStrateManager.SetGameState(State.Lose);
            ui.ShowReStartPanel();
        }
        if (collider.gameObject.CompareTag("score"))
        {
            score++;
            ui.AddScore(score);
        }
    }

    public void ReStart()
    {
        transform.position = origTransform;
        transform.eulerAngles = Vector3.zero;
        rotationAngle = 0;
        vector3 = Vector3.zero;
    }
}
