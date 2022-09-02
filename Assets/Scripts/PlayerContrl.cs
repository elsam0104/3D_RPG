using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContrl : MonoBehaviour
{
    private Transform playerTransform;
    private Animator anim;
    private Vector3 moveDir;

    [SerializeField]
    private float moveSpeed = 5.0f;


    void Start()
    {
        playerTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(moveDir != Vector3.zero)
        {
            playerTransform.rotation = Quaternion.LookRotation(moveDir);
            playerTransform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }

    #region Send_Massage
    private void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        moveDir = new Vector3(dir.x, 0, dir.y);

        anim.SetFloat("Movement", dir.magnitude);
    }

    void OnAttack()
    {
        anim.SetTrigger("Attack");
    }
    #endregion

    #region Unity_Actions
    //public void OnAnimatorMove(InputAction.CallbackContext ctx)
    //{
    //    Vector2 dir = ctx.ReadValue<Vector2>();
    //    moveDir = new Vector3(dir.x, 0, dir.y);

    //    anim.SetFloat("Move", dir.magnitude);
    //}
    //public void OnAttack(InputAction.CallbackContext ctx)
    //{
    //    if(ctx.performed)
    //    {
    //        anim.SetTrigger("Attack");
    //    }
    //}
    #endregion
}
