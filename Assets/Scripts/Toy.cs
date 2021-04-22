using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    private enum TOY_STATE{
        MOVING,
        EFFECTING
    }
    protected LevelManager _levelManager;
    protected float speedMod;
    protected bool useTimer;
    protected float timerCurrent;
    protected float timerMax;
    protected bool effectReady;
    private TOY_STATE state;
    
    [SerializeField]
    private Sprite[] sprites;
    private static float spriteTimer;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        _levelManager.SetToy(this);

        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = sprites[0];
        spriteTimer = 0.25f;
        state = TOY_STATE.MOVING;

        speedMod = 1f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GameManager.GetFlag(GameManager.GAME_FLAGS.PAUSED)){
            // advance effect timer
            if (useTimer){
                timerCurrent -= Time.deltaTime;
                if (timerCurrent < 0){
                    effectReady = true;
                    timerCurrent += timerMax;
                }
            }

            // animate sprite
            spriteTimer -= Time.deltaTime;
            if (spriteTimer < 0){
                if (_renderer.sprite == sprites[0]){
                    _renderer.sprite = sprites[1];
                }
                else if (_renderer.sprite == sprites[1]){
                    _renderer.sprite = sprites[0];
                }
                else if (_renderer.sprite == sprites[2]){
                    _renderer.sprite = sprites[3];
                }
                else if (_renderer.sprite == sprites[3]){
                    _renderer.sprite = sprites[2];
                }
                spriteTimer = 1f;
            }

            // animate movement and size
            if (state == TOY_STATE.EFFECTING){
                if (transform.localPosition.x > 2.5f){
                    Vector3 scale = transform.localScale;
                    scale.x += Time.deltaTime*1.5f;
                    scale.y += Time.deltaTime*1.5f;
                    transform.localScale = scale;
                    if (transform.localScale.x > 2.5f){
                        state = TOY_STATE.MOVING;
                        _renderer.sprite = sprites[0];
                    }
                }
                else
                    transform.Translate(Time.deltaTime*5f,0f,0f);
            }
            else {
                if (transform.localPosition.x > -2.5f){
                    transform.Translate(Time.deltaTime*-7.5f,0f,0f);
                    if (transform.localPosition.x < -2.5f){
                        transform.localPosition = new Vector3(-2.5f,0f,0f);
                    }
                }
                if (transform.localScale.x > 2.5f){
                    transform.localScale = new Vector3(1.5f,1.5f,1f);
                }
            }
        }
    }
    public float GetSpeedModifier(){
        return speedMod;
    }
    public virtual bool OnPointGain(){
        return false;
    }
    public virtual bool OnFall(){
        return false;
    }
    public virtual bool OnHit(){
        return false;
    }
    protected void OnEffectStart(){
        state = TOY_STATE.EFFECTING;
        _renderer.sprite = sprites[2];
        effectReady = false;
    }
}
