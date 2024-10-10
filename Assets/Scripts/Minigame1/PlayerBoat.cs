using SimpleExampleGame;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerBoat : MonoBehaviour
{
    public System.Func<Bullet> GetBullet;
    public float boatSpeed = 2.0f;
    public int m_ScreenID = -1;
    private Vector2 inputDirection = Vector2.zero;
    private Vector3 newPosition;
    [SerializeField] private int targetPosition;
    [SerializeField] private float targetX;

    private Minigame1 minigame;

    public Paths path;

    public int playerNumber;

    public float movementCooldown = 0.25f;

    public float movementTime = 1f;
    private float xVelocity = 1.0f;
    private float lastUsedTime;
    private float newPositionX;
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool isMoving = false;

    private float duration = 0.25f;


    public Animator animator;
    public bool isDamaged = false;
    public float damageTime = 2f;


    //private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = 1;

        minigame = GetComponent<Minigame1>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = inputDirection;
        moveDirection.y = 0; //We don't want to move up and down
        
        if (inputDirection.x < 0 && Time.time > lastUsedTime + movementCooldown)
        {   
            targetPosition = Mathf.Clamp(targetPosition - 1, 0, 2);
            
            lastUsedTime = Time.time;
            StartCoroutine(DoMovement());
        }
        else if (inputDirection.x > 0 && Time.time > lastUsedTime + movementCooldown) 
        { 
            //coroutine = DoMovementLeft();
            

            targetPosition = Mathf.Clamp(targetPosition + 1, 0, 2);
            
            lastUsedTime = Time.time;
            StartCoroutine(DoMovement());
        }
        else
        {
            movingLeft = false;
            movingRight = false;
        }

        //transform.position += (Vector3)moveDirection * FishSpeed * Time.deltaTime; // Time.deltaTime makes our movement consistent regardless of framerate
        transform.position = ScreenUtility.ClampToScreen(transform.position, m_ScreenID, 0.5f);

        //Just for animation testing

        //if (Input.GetKeyDown(KeyCode.B) && !isDamaged)
        //{
        //    StartCoroutine(DamageTest());
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    CollectAnimTest();
        //}
    }

    private IEnumerator DoMovement()
    {
        //Make sure there is only one instance of this function running
        if (isMoving)
        {
            yield break; ///exit if this is still running
        }
        isMoving = true;

        float counter = 0;

        //Get the current position of the object to be moved
        Vector3 startPos = transform.position;

        targetX = path.pathPositions[targetPosition];
        newPositionX = targetX;
        newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);

        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, newPosition, counter / duration);
            yield return null;
        }

        isMoving = false;
    }

    private IEnumerator DoMovementLeft(float movementTime)
    {
        movingLeft = true;
        targetPosition = Mathf.Clamp(targetPosition - 1, 0, 2);
        targetX = path.pathPositions[targetPosition];
            
        newPositionX = targetX;
        newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
            
        var step = boatSpeed * Time.deltaTime;
            
        transform.position = Vector3.MoveTowards(transform.position, newPosition, step);

        yield return new WaitForSeconds(movementTime);
        lastUsedTime = Time.time;

        movingLeft = false;
    }

    public void HandleDirectionalInput(Vector2 direction)
    {
        //Save the direciton to use later
        inputDirection = direction;
    }
    public void HandleButtonInput(int buttonID)
    {
        if (buttonID == 0)
        {
            Bullet bullet = GetBullet();
            if (bullet != null)
            {
                bullet.Fire(transform.position);
            }
        }
    }

    private IEnumerator DamageTest()
    {
        isDamaged = true;

        animator.SetBool("IsDamage", true);

        yield return new WaitForSeconds(damageTime);

        animator.SetBool("IsDamage", false);

        isDamaged = false;
    }

    private void CollectAnimTest()
    {
        animator.SetTrigger("WhaleCollectTrigger");
    }
}
