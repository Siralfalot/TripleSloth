using SimpleExampleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoat : MonoBehaviour
{
    public System.Func<Bullet> GetBullet;
    public float FishSpeed = 2.0f;
    public int m_ScreenID = -1;
    private Vector2 inputDirection = Vector2.zero;
    [SerializeField] private float[] newPosition;
    [SerializeField] private int targetPosition;
    [SerializeField] private float targetX;

    private Minigame1 minigame;

    public int playerNumber;

    public float movementCooldown = 0.5f;
    private float lastUsedTime;

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
        
        if (inputDirection.x < 0 && Time.time > lastUsedTime + movementCooldown)
        {
            targetPosition = Mathf.Clamp(targetPosition - 1, 0, 2);
            targetX = minigame.m_Paths[playerNumber].pathPositions[targetPosition];
            Vector3 temp = new Vector3(targetX,0,0);
            transform.position += temp;
            lastUsedTime = Time.time;
        }
        else if (inputDirection.x > 0 && Time.time > lastUsedTime + movementCooldown) 
        { 
            targetPosition = Mathf.Clamp(targetPosition + 1, 0, 2);
            targetX = minigame.m_Paths[playerNumber].pathPositions[targetPosition];
            Vector3 temp = new Vector3(targetX,0,0);
            transform.position += temp;
            lastUsedTime = Time.time;
        }

        //transform.position += (Vector3)moveDirection * FishSpeed * Time.deltaTime; // Time.deltaTime makes our movement consistent regardless of framerate
        transform.position = ScreenUtility.ClampToScreen(transform.position, m_ScreenID, 0.5f);
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
}
