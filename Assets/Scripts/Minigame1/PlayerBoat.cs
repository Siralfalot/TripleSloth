using SimpleExampleGame;
using System.Collections;
using System.Collections.Generic;
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

    public float movementCooldown = 0.5f;
    private float xVelocity = 0.0f;
    private float lastUsedTime;
    private float newPositionX;
    private bool movingLeft = false;
    private bool movingRight = false;

    //private IEnumerator coroutine;
    

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = 1;

        minigame = GetComponent<Minigame1>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = inputDirection;
        moveDirection.y = 0; //We don't want to move up and down
        
        if (inputDirection.x < 0 && Time.time > lastUsedTime + movementCooldown || movingLeft == true)
        {
            //coroutine = DoMovementRight();
            //StartCoroutine(DoMovementLeft(movementCooldown));
            targetPosition = Mathf.Clamp(targetPosition - 1, 0, 2);
            targetX = path.pathPositions[targetPosition];
            
            newPositionX = Mathf.SmoothDamp(transform.position.x, targetX, ref xVelocity, movementCooldown);
            newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
            
            var step = boatSpeed * Time.deltaTime;
            
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);

            if (Vector3.Distance(transform.position, newPosition) >0.01f)
            {
                movingLeft = true;
            }
        }
        else if (inputDirection.x > 0 && Time.time > lastUsedTime + movementCooldown || movingRight == true) 
        { 
            //coroutine = DoMovementLeft();
            //StartCoroutine(DoMovementRight(movementCooldown));
            targetPosition = Mathf.Clamp(targetPosition + 1, 0, 2);
            targetX = path.pathPositions[targetPosition];
            
            newPositionX = Mathf.SmoothDamp(transform.position.x, targetX, ref xVelocity, movementCooldown);
            newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
            
            var step = boatSpeed * Time.deltaTime;
            
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        }
        else
        {
            movingLeft = false;
            movingRight = false;
        }

        //transform.position += (Vector3)moveDirection * FishSpeed * Time.deltaTime; // Time.deltaTime makes our movement consistent regardless of framerate
        transform.position = ScreenUtility.ClampToScreen(transform.position, m_ScreenID, 0.5f);
    }

    /*private IEnumerator DoMovementRight(float movementCooldown)
    {
        targetPosition = Mathf.Clamp(targetPosition + 1, 0, 2);
        targetX = path.pathPositions[targetPosition];
        newPositionX = Mathf.SmoothDamp(transform.position.x, targetX, ref xVelocity, movementCooldown);
        transform.position = Vector3.MoveTowards(newPositionX, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(movementCooldown);
        lastUsedTime = Time.time;
    }*/

    /*private IEnumerator DoMovementLeft(float movementCooldown)
    {
        targetPosition = Mathf.Clamp(targetPosition - 1, 0, 2);
        targetX = path.pathPositions[targetPosition];
        newPositionX = Mathf.SmoothDamp(transform.position.x, targetX, ref xVelocity, movementCooldown);
        newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
        var step = boatSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        yield return new WaitForSeconds(movementCooldown);
        lastUsedTime = Time.time;
    }*/

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
}
