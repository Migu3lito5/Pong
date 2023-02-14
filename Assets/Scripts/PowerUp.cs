using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    private Vector3 defaultSize = new Vector3(0.5f, 0.5f, 0.5f);
    public GameObject ball;

    public GameObject Lpaddle;
    public GameObject Rpaddle;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(135, 55, 45) * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            activatePowerUp(other);
        }
    }
    void activatePowerUp(Collider collider)
    {
        int rnd = Random.Range(0, 3);
        gameObject.transform.position = new Vector3(0f, -10f, 0f);

        if (rnd == 0)
        {
            StartCoroutine(BigBallPowerUp());
        } 
        else if(rnd == 1)
        {
           StartCoroutine(ChangePaddleColor());
        } else if(rnd == 2)
        {
            StartCoroutine(MakePaddleBigger());
        }
          
    }

    IEnumerator BigBallPowerUp()
    {
        ball.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        yield return new WaitForSeconds(6);
        ball.transform.localScale = defaultSize;
        StartCoroutine(spawnPowerUp());
    }

    IEnumerator spawnPowerUp()
    {
        float y = Random.Range(0, 3);
        float x = Random.Range(0, 6);
        yield return new WaitForSeconds(3);
        gameObject.transform.position = new Vector3(x, y, 0f);

    }

    IEnumerator ChangePaddleColor()
    {

        Lpaddle.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        Rpaddle.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        yield return new WaitForSeconds(5);
        Lpaddle.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        Rpaddle.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

        StartCoroutine(spawnPowerUp());
    }

    IEnumerator MakePaddleBigger()
    {
        Lpaddle.transform.localScale = new Vector3(.25f, 2.5f, 1f);
        Rpaddle.transform.localScale = new Vector3(.25f, 2.5f, 1f);
        yield return new WaitForSeconds(7);
        Lpaddle.transform.localScale = new Vector3(.25f, 1.5f, 1f);
        Rpaddle.transform.localScale = new Vector3(.25f, 1.5f, 1f);

        StartCoroutine(spawnPowerUp());
    }
}