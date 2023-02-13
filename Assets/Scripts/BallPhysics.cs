using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallPhysics : MonoBehaviour
{

    Rigidbody rb;
    float speed = 15f;
    private int LeftPlayerScore = 0;
    private int RightPlayerScore = 0;
    private GameObject leftPaddle;
    private GameObject rightPaddle;

    private Renderer ballRender;
    private float red, green, blue;
    private Color ballColor;


    public TextMeshProUGUI LScore;
    public TextMeshProUGUI RScore;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Waiter());
        SetScoreText();
        rb = GetComponent<Rigidbody>();
        ballRender = rb.GetComponent<Renderer>();
        rb.velocity = Vector3.left * speed;

    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(4);
    }

    private void OnTriggerEnter(Collider o)
    {
        if (o.gameObject.CompareTag("LeftWall"))
        {
            RightPlayerScore++;
            Debug.Log($" Right Player Scored! Score is: {LeftPlayerScore} | {RightPlayerScore}");
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            resetRound(2);
        }

        if (o.gameObject.CompareTag("RightWall"))
        {
            LeftPlayerScore++;
            Debug.Log($" Left Player Scored! Score is: {LeftPlayerScore} | {RightPlayerScore}");
            rb.gameObject.GetComponent<TrailRenderer>().enabled = false;
            resetRound(1);
        }

        SetScoreText();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftPaddle" || collision.gameObject.tag == "RightPaddle")
        {

            BoxCollider bc = collision.rigidbody.GetComponent<BoxCollider>();
            Bounds b = bc.bounds;
            float maxY = b.max.y;
            float minY = b.min.y;

            Quaternion rotation = Quaternion.Euler(0, 0, -60f);
            Vector3 bounceDirection = rotation * Vector3.left;

            if (collision.gameObject.tag == "LeftPaddle")
            {
                bounceDirection = rotation * Vector3.right;
            }

            changeBallColor();
            rb.AddForce(bounceDirection * 30f * speed++, ForceMode.Force);

        }
    }

    private void resetRound(int decider)
    {
        rb.position = new Vector3(0, 0, 1);
        speed = 15f;
        rb.velocity = Vector3.zero;
        rightPaddle = GameObject.Find("LeftPaddle");
        leftPaddle = GameObject.Find("RightPaddle");

        leftPaddle.GetComponent<Rigidbody>().position = new Vector3(-8, 0, 1);
        rightPaddle.GetComponent<Rigidbody>().position = new Vector3(-8, 0, 1);

        StartCoroutine(Waiter());
        rb.gameObject.GetComponent<TrailRenderer>().enabled = true;

        if (decider == 2) rb.velocity = Vector3.left * speed;
        else rb.velocity = Vector3.right * speed;
    }


    private void restartGame()
    {
        RightPlayerScore = 0;
        LeftPlayerScore = 0;
        StartCoroutine(Waiter());

    }

    private void SetScoreText()
    {
        LScore.text = LeftPlayerScore.ToString();
        RScore.text = RightPlayerScore.ToString();
        updateColorText();

        if (RightPlayerScore == 11)
        {
            Debug.Log("Right Player Wins!");
            restartGame();
        }

        if (LeftPlayerScore == 11)
        {
            Debug.Log("Left Player Wins!");
            restartGame();
        }


    }

    private void updateColorText()
    {



        if (RightPlayerScore > 1 && RightPlayerScore < 4)
        {
            RScore.color = Color.green;
        }

        if (LeftPlayerScore > 1 && LeftPlayerScore < 4)
        {
            LScore.color = Color.green;

        }

        if (RightPlayerScore > 4 && RightPlayerScore < 9)
        {
            RScore.color = Color.yellow;
        }

        if (LeftPlayerScore > 4 && LeftPlayerScore < 9)
        {
            LScore.color = Color.yellow;

        }

        if (RightPlayerScore > 9)
        {
            RScore.color = Color.red;
        }

        if (LeftPlayerScore > 9)
        {
            LScore.color = Color.red;

        }

        if(RightPlayerScore < 1 && LeftPlayerScore < 1)
        {
            LScore.color = Color.white;
            RScore.color = Color.white;
        }

    }


    private void changeBallColor()
    {
        red = Random.Range(0f, 1f);
        green = Random.Range(0f, 1f);
        blue = Random.Range(0f, 1f); 

        ballColor = new Color(red, green, blue, 1f);

        ballRender.material.SetColor("_Color", ballColor);
    }

    

}
