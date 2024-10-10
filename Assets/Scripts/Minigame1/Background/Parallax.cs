using SimpleExampleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollSpeed;
    public bool canScroll = true;
    private Vector3 startPosition;
    private float textureHeight;
    public int layer;
    public Minigame1 minigameRef;
    Sprite sprite;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        minigameRef.MinigameLoaded.AddListener(Initialise);
        minigameRef.MinigameFinished.AddListener(DisableBG);
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
        
        spriteRenderer.enabled = false;
    }

    void Initialise()
    {
        spriteRenderer.enabled = true;
        textureHeight = (sprite.texture.height / sprite.pixelsPerUnit)*transform.localScale.y;

        //startPosition = transform.position;
        startPosition = Vector3.zero;

        SetScrollSpeed(layer);
    }

    void DisableBG()
    {
        spriteRenderer.enabled = false;

        textureHeight = (sprite.texture.height / sprite.pixelsPerUnit) * transform.localScale.y;

        SetScrollSpeed(layer);
    }

    // Update is called once per frame
    void Update()
    {
        if (canScroll)
        {
            float direction = scrollSpeed * Time.deltaTime;
            transform.position += new Vector3(0, -direction, 0);
        }

        if ((Mathf.Abs(transform.position.y) - textureHeight) > 0)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            Debug.Log("ScrollBG reset");
        }
    }

    private void SetScrollSpeed(int layer)
    {
        switch (layer)
        {
            case 1:
                scrollSpeed = minigameRef.gameSpeed;
                break;
            case 2:
                scrollSpeed = minigameRef.gameSpeed - 1f;
                break;
            case 3:
                scrollSpeed = minigameRef.gameSpeed - 1.5f;
                break;
            default:
                scrollSpeed = 0f;
                Debug.LogWarning("ScrollSpeed not set. Layer missing.");
                break;
        }
    }
}
