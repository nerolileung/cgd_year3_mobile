﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PLAYER_STATE{
        RUNNING,
        JUMPING,
        SLIDING,
        DEAD
    }
    private Rigidbody2D rb;    
    private Vector2 touchStart;
    private Vector2 touchEnd;
    private float jumpForce;
    private PLAYER_STATE currentState;
    private SpriteRenderer image;
    private Dictionary<string, Sprite> sprites;
    private PolygonCollider2D coll;
    private List<Vector2> physicsShape;
    private bool spriteChanged;
    private float spriteTimerMax;
    private float spriteTimerCurrent;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
        coll = GetComponent<PolygonCollider2D>();

        // initialise sprites
        Sprite[] spritesheet = Resources.LoadAll<Sprite>("player_"+GameManager.GetSkinColour()+GameManager.GetClothesColour());
        sprites = new Dictionary<string, Sprite>();
        for (int i = 0; i < spritesheet.Length; i++){
            sprites[spritesheet[i].name.Substring(10)] = spritesheet[i];
        }
        image.sprite = sprites["run1"];
        physicsShape = new List<Vector2>();
        image.sprite.GetPhysicsShape(0,physicsShape);
        coll.SetPath(0,physicsShape);

        jumpForce = 10f;
        currentState = PLAYER_STATE.RUNNING;

        spriteTimerMax = GameManager.GetCurrentLevel().startSpeed / 4f;
        if (spriteTimerMax < 0.1f) spriteTimerMax = 0.1f;
        spriteTimerCurrent = spriteTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        // fell in a hole
        if (transform.position.y < -13){
            SetState(PLAYER_STATE.DEAD);
        }

        // advance animation timer
        spriteTimerCurrent -= Time.deltaTime;
        if (spriteTimerCurrent < 0) {
            // split current sprite name into state and frame
            char index = image.sprite.name[image.sprite.name.Length-1];
            string name = image.sprite.name.Substring(10);
            name = name.Substring(0,name.Length-1);
            if (index == '1')
                image.sprite = sprites[name+'2'];
            else image.sprite = sprites[name+'1'];
            spriteTimerCurrent = spriteTimerMax;
        }

        // input
        if (Input.touchCount > 0){
            Touch currentTouch = Input.GetTouch(0);
            // all controls: stop sliding when screen isn't touched
            if (currentState == PLAYER_STATE.SLIDING && currentTouch.phase == TouchPhase.Ended) {
                SetState(PLAYER_STATE.RUNNING);
            }
            // single finger tap to jump, double finger hold to slide
            if (GameManager.GetControls() == GameManager.CONTROL_SCHEME.TOUCH){
                // slide
                if (Input.touchCount == 2){
                    if (currentTouch.phase == TouchPhase.Began && currentState!=PLAYER_STATE.SLIDING)
                        SetState(PLAYER_STATE.SLIDING);
                }
                // jump
                else {
                    if (currentTouch.phase == TouchPhase.Began && currentState!=PLAYER_STATE.JUMPING)
                        SetState(PLAYER_STATE.JUMPING);
                }
            }
            // swipe up to jump, down + hold to slide
            else if (GameManager.GetControls() == GameManager.CONTROL_SCHEME.SWIPE){
                if (currentTouch.phase == TouchPhase.Began)
                    touchStart = currentTouch.position;
                else if (currentTouch.phase == TouchPhase.Moved){
                    touchEnd = currentTouch.position;
                    // swipe up
                    if (touchEnd.y - touchStart.y > 0 && currentState!=PLAYER_STATE.JUMPING)
                        SetState(PLAYER_STATE.JUMPING);
                    // swipe down
                    else if (touchEnd.y - touchStart.y < 0 && currentState!=PLAYER_STATE.SLIDING)
                        SetState(PLAYER_STATE.SLIDING);
                }
            }
        }
    }
    void LateUpdate(){
        if (spriteChanged){
            image.sprite.GetPhysicsShape(0,physicsShape);
            coll.SetPath(0,physicsShape);
            spriteChanged = false;
        }
    }
    private void SetState(PLAYER_STATE state){
        if (state == currentState) return;
        switch(state){
            case PLAYER_STATE.RUNNING:
                image.sprite = sprites["run1"];
            break;
            case PLAYER_STATE.JUMPING:
                image.sprite = sprites["jump1"];
                rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
            break;
            case PLAYER_STATE.SLIDING:
                image.sprite = sprites["slide1"];
            break;
            default:
            break;
        }
        currentState = state;
        spriteChanged = true;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Danger"){
            SetState(PLAYER_STATE.DEAD);
        }
        if (currentState == PLAYER_STATE.JUMPING)
            SetState(PLAYER_STATE.RUNNING);
    }

    public void Jump(){
        if (currentState != PLAYER_STATE.JUMPING){
            SetState(PLAYER_STATE.JUMPING);
        }
    }
    public void Slide(){
        if (currentState != PLAYER_STATE.SLIDING){
            SetState(PLAYER_STATE.SLIDING);
        }
    }
    public bool IsAlive(){
         return currentState != PLAYER_STATE.DEAD;
    }
}