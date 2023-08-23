using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 10;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Animator uiAnimator;
    [SerializeField]
    private Animator playerAnimator;

    private bool isJumping;
    private float yVelocity;
    private float gravity = 0.9f;
    private int hpNow;
    private IEnumerator damageRecover;

    public float hpBar
    {
        get
        {
            return (float)hpNow / maxHp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        hpNow = maxHp;
        uiAnimator.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, (float)hpNow / maxHp, Time.deltaTime);

        if (!GameManagerUI.gameStart)
        {
            return;
        }
        if (hpNow < 1)
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (characterController.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
            }
            yVelocity = 0f;
        }

        //Debug.Log(yVelocity);

        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        Vector3 dir = new Vector3(h, 0, v);
        playerAnimator.SetFloat("Blend", dir.magnitude);
        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity -= gravity * Time.deltaTime;
        dir.y = yVelocity;

        characterController.Move(dir * speed * Time.deltaTime);
    }

    public void DamageFoo(int damage)
    {
        if (damageRecover == null)
        {
            hpNow -= damage;
            if (hpNow > 0)
            {
                uiAnimator.Play("Idle");
                uiAnimator.Play("Damage");
                damageRecover = DamageRecover();
                StartCoroutine(damageRecover);
            }
            else
            {
                uiAnimator.Play("Damage");
                GameManagerUI.GameEnd();
            }
        }
    }

    private IEnumerator DamageRecover()
    {
        yield return new WaitForSeconds(0.3f);
        uiAnimator.Play("Recover");
        yield return new WaitForSeconds(0.3f);
        uiAnimator.Play("Idle");

        StopCoroutine(damageRecover);
        damageRecover = null;
    }
}
