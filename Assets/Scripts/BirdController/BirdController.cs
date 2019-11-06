using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance; // đồng bộ hoá các biến trong các script

    public float bounceForce; // lực nảy của Bird
    private Rigidbody2D myBody;
    private Animator anim;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField] // hiện lên những thành phần private
    private AudioClip flyClip, pingClip, diedClip;

    private bool isAlive;
    private bool didFlap;

    private GameObject spawner;

    public float flag = 0;
    public int score = 0;

    // Ham khoi tao
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isAlive = true;
        _MakeInstance();
        spawner = GameObject.Find("Spawner Pipe");
    }

    void _MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _BirdMovenment();
    }

    void _BirdMovenment()
    {
        // Function click Button
        if (isAlive)
        {
            if (didFlap)
            {
                didFlap = false;
                myBody.velocity = new Vector2(myBody.velocity.x, bounceForce); // truyền vận tốc hiện tại của x 
                audioSource.PlayOneShot(flyClip); // truyền âm thanh khi bay
            }
        }

        // Funtion rotation of bird
        if(myBody.velocity.y > 0) // khi click chuột
        {
            float angel = 0;
            angel = Mathf.Lerp(0, 90, myBody.velocity.y / 7); // nội suy giữa a và theo t
            transform.rotation = Quaternion.Euler(0,0,angel ); // đại lượng để xoay Quaternion, xoay theo chiều x,y,z
        } 
        else if (myBody.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // đại lượng để xoay Quaternion, xoay theo chiều x,y,z
        }
        else if (myBody.velocity.y < 0) // khi nhả chuột
        {
            float angel = 0;
            angel = Mathf.Lerp(0, -90, -myBody.velocity.y / 7); // nội suy giữa a và theo t
            transform.rotation = Quaternion.Euler(0, 0, angel); // đại lượng để xoay Quaternion, xoay theo chiều x,y,z
        }
    }

    // hàm bắt sự kiện button
    public void FlapButton()
    {
        didFlap = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PipeHolder") // pipeHolder là đường nằm giữa 2 Pipe
        {
            score++;
            if(GamePlayController.instance != null)
            {
                GamePlayController.instance._SetScore(score);
            }
            audioSource.PlayOneShot(pingClip);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Ground")
        {
            flag = 1;
            if (isAlive)
            {
                isAlive = false;
                Destroy(spawner);
                audioSource.PlayOneShot(diedClip);
                anim.SetTrigger("Died");
            }
            if(GamePlayController.instance != null)
            {
                GamePlayController.instance._BirdDiedShowPanel(score);
            }
        }
    }
}
