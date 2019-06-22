using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    static CharacterController instance;

    public AudioSource source;

    public bool isLive = true;

    [Header("Character Moving Speed")]
    public float speed = 3f;
    public float runSpeed = 2f;
    public bool isRunning = false;

    [SerializeField]
    float curSpeed = 0;

    Transform MC;
    float mouseAxisX = 0;
    float mouseAxisY = 0;

    [Header("Character Moving Sound Speed")]
    public float walkingSoundLoopSpeed = 0.3f;
    public float runSoundLoopSpeed = 0.7f;

    [Header("Running Cam Effect")]
    public float movingAdd = 1f;
    public float camMoving = 10f;
    float curMoving = 0;

    Quaternion curQua;
    Vector3 curRot;

    static public CharacterController Getinstance()
    {
        if (instance == null)
        {
            Debug.Log("CharacterController is havn't instance!");
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        MC = Camera.main.transform;
        source = GetComponent<AudioSource>();
        curSpeed = speed;
        curQua = transform.localRotation;
        curRot = transform.localEulerAngles;
    }

    void Update()
    {
        if (isLive)
        {
            Movement();
        }
    }

    private void Movement()
    {
        Accelation();
        MouseRotate();
    }

    private void Accelation()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
            WalkingSound();
            WalkCamera();
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * curSpeed);
            WalkingSound();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * curSpeed);
            WalkingSound();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * curSpeed);
            WalkingSound();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W))
        {
            isRunning = false;
            curSpeed = speed;
            source.pitch = walkingSoundLoopSpeed;
        }
    }

    public void WalkingSound()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(source.clip);
        }
    }

    private void MouseRotate()
    {
        mouseAxisX = Input.GetAxis("Mouse Y");
        mouseAxisY = Input.GetAxis("Mouse X");
        float y = MC.transform.rotation.eulerAngles.x + (-mouseAxisX);
        if (y > 300 || y < 65)
        {
            MC.transform.Rotate(-mouseAxisX, 0, 0);
        }
        transform.Rotate(0, mouseAxisY, 0);
    }

    private void WalkCamera()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            source.pitch = runSoundLoopSpeed;
            curSpeed = speed * runSpeed;
            if (Mathf.Abs(curMoving) > camMoving)
            {
                movingAdd *= -1;
            }
            curMoving += movingAdd;
            MC.Translate(0, movingAdd * Time.deltaTime, 0);
        }
    }
}
