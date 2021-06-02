using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player P_instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private AudioSource jumpAudio;
    public AudioClip jumpSound;

    private AudioSource boosterAudio;
    public AudioClip boosterSound;


    float baseSpeed; // 이동 속도 지정
    float realSpeed;
    public float _realSpeed { get { return realSpeed; } set { realSpeed = value; } }
    float rotationSpeed; // 회전 속도 지정

    private Rigidbody rigidBody;

    bool isJumpAble;
    bool isWallLeftContact;
    bool isWallRightContact;
    bool isWallForwardContact;

    float jumpCoolDownTime;

    //Animator animator;


    void Awake()
    {
        instance = this;
        //Debug.Log(instance.transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.jumpAudio = this.gameObject.AddComponent<AudioSource>();
        this.jumpAudio.loop = false;
        this.jumpAudio.clip = this.jumpSound;
        this.jumpAudio.volume = 0.5f;

        this.boosterAudio = this.gameObject.AddComponent<AudioSource>();
        this.boosterAudio.loop = false;
        this.boosterAudio.clip = this.boosterSound;
        this.boosterAudio.volume = 0.5f;

        baseSpeed = 10f;
        realSpeed = baseSpeed;
        rotationSpeed = 360f * 5.0f;

        rigidBody = GetComponent<Rigidbody>();

        isJumpAble = false;
        isWallLeftContact = false;
        isWallRightContact = false;
        isWallForwardContact = false;

        jumpCoolDownTime = 0;
        //animator = GetComponentInChildren<Animator>();
        //animator.SetFloat("Move", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        TimerManager();
        cheatClear();
    }

    void cheatClear()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MyGameManager.P_instance.endingTime = Timer.P_instance.TimeCost;
            SceneManager.LoadScene("StageClear");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            MyGameManager.P_instance.stage = 1;
            SceneManager.LoadScene("StartScene");
        }
    }

    void TimerManager()
    {
        if (jumpCoolDownTime > 0.0f)
            jumpCoolDownTime -= Time.deltaTime;
    }

    void Move()
    {
        if (isWallForwardContact)
        {
            Vector3 _direction = new Vector3(0, 1, 0);

            Transform modelTransform = transform.GetChild(0);
            Vector3 forward = Vector3.Slerp( // 메소드를 조합해 플레이어의 방향 변환
            modelTransform.forward,
            Vector3.forward,
            rotationSpeed * Time.deltaTime / Vector3.Angle(modelTransform.forward, Vector3.forward)
            );
            modelTransform.LookAt(modelTransform.position + forward * 50);

            rigidBody.MovePosition(transform.position + _direction * realSpeed * 20.0f * Time.deltaTime);
            rigidBody.velocity = Vector3.zero;
            return;
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, 1);
        Vector3 vel;
        if (((isWallLeftContact && direction.x > 0) || (isWallRightContact && direction.x < 0)) && jumpCoolDownTime <= 0.0f)
        {
            direction.x = 0;
            vel = rigidBody.velocity;
            vel.x = 0;
            vel.y = 0;
            rigidBody.velocity = vel;
        }

        if (direction.sqrMagnitude > 0.01f)
        {
            Transform modelTransform = transform.GetChild(0);
            Vector3 forward = Vector3.Slerp( // 메소드를 조합해 플레이어의 방향 변환
            modelTransform.forward,
            direction,
            rotationSpeed * Time.deltaTime / Vector3.Angle(modelTransform.forward, direction)
            );
            modelTransform.LookAt(modelTransform.position + forward * 50);

        }
        // Move()를 이용해 이동, 충돌 처리, 속도 값 얻기 가능
        //rigidBody.MovePosition(transform.position + direction * realSpeed * Time.deltaTime);
        rigidBody.velocity += direction * realSpeed * Time.deltaTime * 10.0f;

        vel = rigidBody.velocity;
        if (vel.x > realSpeed)
            vel.x -= realSpeed * Time.deltaTime * 10.0f;
        else if (vel.x < -realSpeed)
            vel.x += realSpeed * Time.deltaTime * 10.0f;

        if (vel.z > realSpeed)
            vel.z = realSpeed;

        if (Input.GetAxis("Horizontal") == 0)
        {
            Vector3 vel_x_zero = vel;
            vel_x_zero.x = 0;

            vel = Vector3.Slerp( // 메소드를 조합해 플레이어의 방향 변환
               vel,
               vel_x_zero,
               Time.deltaTime * 3.0f
               );
        }
        rigidBody.velocity = vel;

        Jump();
        //animator.SetFloat("Move", characterController.velocity.magnitude);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumpAble)
        {
            this.jumpAudio.Play(); // audio clip 음원을 재생.

            Vector3 jumpPower = new Vector3(0, 15, 0);

            if (isWallLeftContact)
            {
                jumpPower.x = -10 * 4;
            }
            else if (isWallRightContact)
            {
                jumpPower.x = 10 * 4;
            }

            rigidBody.velocity += jumpPower;

            jumpCoolDownTime = 0.3f;
            isJumpAble = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isJumpAble = true;
    }

    private void OnCollisionExit(Collision other)
    {
        isJumpAble = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isJumpAble = true;

        if (other.tag == "WallLeft")
        {
            isWallLeftContact = true;
        }
        else if (other.tag == "WallRight")
        {
            isWallRightContact = true;
        }
        else if (other.tag == "WallForward")
        {
            isWallForwardContact = true;
            realSpeed = baseSpeed;
        }

        if (other.tag == "Booster")
        {
            this.boosterAudio.Play(); // audio clip 음원을 재생.
            realSpeed *= 1.2f;
            if (realSpeed >= 35.0f)
                realSpeed = 35.0f;
            Debug.Log("Speed UP" + realSpeed);
        }

        if (other.tag == "KillArea")
        {
            SceneManager.LoadScene("StageFail");
        }

        if (other.tag == "FinishLine")
        {
            MyGameManager.P_instance.endingTime = Timer.P_instance.TimeCost;
            Debug.Log("엔딩 시간 : " + MyGameManager.P_instance.endingTime);
            SceneManager.LoadScene("StageClear");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WallLeft")
        {
            isWallLeftContact = false;
        }
        else if (other.tag == "WallRight")
        {
            isWallRightContact = false;
        }
        else if (other.tag == "WallForward")
        {
            isWallForwardContact = false;
        }
    }

}
