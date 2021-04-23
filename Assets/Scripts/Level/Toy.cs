using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    protected enum TOY_STATE{
        MOVING,
        AFFECTING,
        EFFECT_END
    }
    protected LevelManager _levelManager;
    protected Player _player;
    protected float speedMod;
    protected bool useTimer;
    protected float timerCurrent;
    protected float timerMax;
    protected bool effectReady;
    protected bool effectOneShot;
    protected TOY_STATE state;
    
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
        effectOneShot = true;
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

            // default sprite animation
            spriteTimer -= Time.deltaTime;
            if (spriteTimer < 0){
                if (_renderer.sprite == sprites[0]){
                    _renderer.sprite = sprites[1];
                    transform.Translate(0f,0.05f,0f);
                }
                else if (_renderer.sprite == sprites[1]){
                    _renderer.sprite = sprites[0];
                    transform.Translate(0f,-0.05f,0f);
                }
                else if (_renderer.sprite == sprites[2]){
                    _renderer.sprite = sprites[3];
                }
                else if (_renderer.sprite == sprites[3]){
                    _renderer.sprite = sprites[2];
                }
                spriteTimer = 0.25f;
            }

            // special effect movement/size animation
            switch (state){
                case TOY_STATE.AFFECTING:
                if (transform.localPosition.x > 2.5f){
                    transform.Translate(Time.deltaTime*5f,0f,0f);
                }
                else if (transform.localScale.x < 2.5f){
                    Vector3 scale = transform.localScale;
                    scale.x += Time.deltaTime*1.5f;
                    scale.y += Time.deltaTime*1.5f;
                    transform.localScale = scale;

                    if (transform.localScale.x > 2.5f && effectOneShot){
                        state = TOY_STATE.EFFECT_END;
                        _renderer.sprite = sprites[0];
                    }
                }
                break;
                case TOY_STATE.EFFECT_END:
                if (transform.localPosition.x > -2.5f){
                    transform.Translate(Time.deltaTime*-7.5f,0f,0f);
                    if (transform.localPosition.x < -2.5f){
                        transform.localPosition = new Vector3(-2.5f,0f,-5f);
                    }
                }
                if (transform.localScale.x > 2.5f){
                    transform.localScale = new Vector3(1.5f,1.5f,1f);
                }
                break;
                default:
                break;
            }
        }
    }
    public void SetPlayer(Player _p){
        _player = _p;
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
        state = TOY_STATE.AFFECTING;
        _renderer.sprite = sprites[2];
        Vector3 pos = transform.localPosition;
        pos.x = 2.5f;
        pos.y = 0f;
        transform.localPosition = pos;
        effectReady = false;
    }
}
