using System.Collections;
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
    private PLAYER_STATE currentState;
    private float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        jumpForce = 10f;

        currentState = PLAYER_STATE.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
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
        // collision shape changes go here
    }
    private void SetState(PLAYER_STATE state){
        // sprite animation goes here
        switch(state){
            case PLAYER_STATE.JUMPING:
                rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
            break;
            default:
            break;
        }
        currentState = state;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Danger"){
            SetState(PLAYER_STATE.DEAD);
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
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
            SetState(PLAYER_STATE.JUMPING);
        }
    }
    public bool IsAlive(){
         return currentState != PLAYER_STATE.DEAD;
    }
}