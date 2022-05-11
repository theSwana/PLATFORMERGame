using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SKRIP : MonoBehaviour
{
    public Text timer;
    public Text coinCounterText;
    private Rigidbody rigidBodyComponent;
    public static int coinCounter = 0;
    bool jump;
    bool touchGround;
    private float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && touchGround)
            jump = true;
        horizontal = Input.GetAxis("Horizontal");
        if (coinCounter < 6)
        {
            timer.text = "Time: " + Mathf.Round(Time.time).ToString() + " s";
        }
        if(coinCounter == 18)
        {

            StartCoroutine(StopLOL());

        }
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            rigidBodyComponent.AddForce(6 * Vector3.up, ForceMode.VelocityChange);
            jump = false;
            touchGround = false;
        }
        rigidBodyComponent.velocity = new Vector3(horizontal * 3f, rigidBodyComponent.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        touchGround = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        coinCounter++;
        Destroy(other.gameObject);
        GetComponent<AudioSource>().Play();
    }

    public IEnumerator StopLOL()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        coinCounter = 0;


    }

    
}
