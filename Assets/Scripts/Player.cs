using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private GameManager _gameManager;
    
    private Vector2 touchStart;
    private Vector2 touchEnd;
    private float jumpForce;
    private bool isAlive;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();
        animator.SetBool("isJumping",false);
        animator.SetBool("isSliding",false);

        rb = GetComponent<Rigidbody2D>();

        jumpForce = 10f;

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // input
        if (Input.touchCount > 0){
            Touch currentTouch = Input.GetTouch(0);
            // all controls: stop sliding when screen isn't touched
            if (animator.GetBool("isSliding") && currentTouch.phase == TouchPhase.Ended) {
                animator.SetBool("isSliding",false);
            }
            // single finger tap to jump, double finger hold to slide
            if (_gameManager.GetControls() == GameManager.CONTROL_SCHEME.TOUCH){
                // slide
                if (Input.touchCount == 2){
                    if (currentTouch.phase == TouchPhase.Began && !animator.GetBool("isSliding"))
                        Slide();
                }
                // jump
                else {
                    if (currentTouch.phase == TouchPhase.Began && !animator.GetBool("isJumping"))
                        Jump();
                }
            }
            // swipe up to jump, down + hold to slide
            else if (_gameManager.GetControls() == GameManager.CONTROL_SCHEME.SWIPE){
                if (currentTouch.phase == TouchPhase.Began)
                    touchStart = currentTouch.position;
                else if (currentTouch.phase == TouchPhase.Moved){
                    touchEnd = currentTouch.position;
                    // swipe up
                    if (touchEnd.y - touchStart.y > 0 && !animator.GetBool("isJumping"))
                        Jump();
                    // swipe down
                    else if (touchEnd.y - touchStart.y < 0 && !animator.GetBool("isSliding"))
                        Slide();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Danger"){
            isAlive = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        if (animator.GetBool("isJumping"))
            animator.SetBool("isJumping",false);
    }

    public void Jump(){
        if (!animator.GetBool("isJumping")){
            animator.SetBool("isJumping",true);
            rb.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
        }
    }
    public void Slide(){
        if (!animator.GetBool("isSliding")){
            animator.SetBool("isSliding",true);
        }
    }
    public bool IsAlive(){
         return isAlive;
    }
}