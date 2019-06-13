using System;
using System.Collections;
using EZCameraShake;
using UnityEngine;
/*
    Melee_Weapon_Medium_Controller
    The controller for using medium melee weapons.
    The feeling we are going for are meaty swings with medium slowdowns.
    -Limits on the player-
     -When building up a swing can only move slowly.
     -Swinging the weapon commits to a full swing animation.
     -Does more damage than light, but less than heavy.
     
    -The pseudo process-
     Player has weapon in hand
     Player holds down attack key and begins building up a swing
        -if player lets go of swing before full buildup then go back to idle anim slowly (the cost of a fake out is slow progression back to idle)
        -if player holds swing then releases, do a full swing animation (if player hits something then make it bounce back quicker, a miss takes longer). Think Dark Souls.
 */
public class Melee_Weapon_Medium_Controller : MonoBehaviour
{

    [SerializeField] private Animator playerAnim;
    [SerializeField] private float windUpTimeRequired = 1f;
    [SerializeField] private float timeBetweenSwings = 2f;
    [SerializeField] private float transitionScale = .2f;//Controls how fast the animation will blend back and forth from idle to buildup.
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeRoughness;
    [SerializeField] private float shakeFadeIn;
    [SerializeField] private float shakeFadeOut;
    [SerializeField] private Vector3 shakePos;
    [SerializeField] private Vector3 shakeRotation;
    private Animator anim;
    private float windUpTime = 0f;
    private float idling = 0;
    private float timeSinceLastSwing = 0f;
    private bool swinging = false;    
    
    private CameraShaker shaker;

    private void Start()
    {
        anim = GetComponent<Animator>();
        shaker = FindObjectOfType<CameraShaker>();        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Shake();
        }

        if (!swinging)
        {
            if (Input.GetButtonDown("LeftHand"))
            {
                windUpTime = 0;
                idling = 0;
            }

            if (Input.GetButton("LeftHand"))
            {
                //begin wind up and play build up anim
                windUpTime += Time.deltaTime * transitionScale;
                idling += Time.deltaTime * transitionScale * 2;
            }
            else
            {
                //unwind and go back to idle anim
                windUpTime -= Time.deltaTime * transitionScale;
                idling -= Time.deltaTime * transitionScale * 2;
            }

            if (Input.GetButtonUp("LeftHand") && windUpTime >= windUpTimeRequired)
            {

                //Play swinging animation based on movement direction
                Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                FigureOutWhichSwing(direction);

            }
        }
        idling = Mathf.Clamp01(idling);
        UpdateAnimParams();
    }

    private void Shake()
    {
        shaker.ShakeOnce(shakeMagnitude, shakeRoughness, shakeFadeIn, shakeFadeOut, shakePos, shakeRotation);
    }

    private void FigureOutWhichSwing(Vector2 direction)
    {
        if (direction.y == 0 && direction.x < 0)
        {
            //swing right to left
            Debug.Log("Swinging left to right!");
            return;
        }
        else if (direction.y == 0 && direction.x > 0)
        {
            //swing left to right
            Debug.Log("Swinging right to left!");
            return;
        }
        else
        {
            //default overhead swing
            StartCoroutine(OverheadSwing());        
            Shake();
            return;
        }
    }

    private IEnumerator OverheadSwing()
    {
        swinging = true;
        windUpTime = 0;
        idling = 0;
        anim.Play("Overhead_Medium_Swing");
        yield return new WaitForSeconds(timeBetweenSwings);
        swinging = false;
    }

    private void UpdateAnimParams()
    {
        anim.SetFloat("BuildUp", windUpTime);
        anim.SetFloat("Idling", idling);
    }
}
