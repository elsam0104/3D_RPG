using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    [SerializeField]
    float _rotate = 20.0f;

    //패싱위한 변수
    Animator anim;

    Define.State _state = Define.State.Idle;
    Vector3 _destPos; //목적지 위치
    int _mask = (1 << (int)Define.Layer.Ground);
    //비트연산 = 고유한 layer번호 가지려고. 1 2 4 8 (2의 n승씩) 함께 체크하려면 10 + 100 으로 체크하면 되니까 편함

    void Start()
    {
        anim = GetComponent<Animator>();

        Managers.Input.KeyAction -= OnKeyboard; //중복 방지
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    void Update()
    {
        switch (_state)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Die:
                UpdateDie();
                break;

        }
    }
    void UpdateIdle()
    {
        anim.SetFloat("Movement", 0f);
    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        dir.y = 0f;
        if (dir.magnitude < 0.001f)
        {
            _state = Define.State.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _rotate * Time.deltaTime);
        }
        anim.SetFloat("Movement", dir.magnitude);
    }
    void UpdateDie()
    {

    }
        GameObject prefab = null;
    void OnKeyboard()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(Vector3.forward), 0.2f);
            dir = Vector3.forward * Time.deltaTime * _speed;
            transform.position += dir;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(Vector3.back), 0.2f);
            dir = Vector3.back * Time.deltaTime * _speed;
            transform.position += dir;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(Vector3.left), 0.2f);
            dir = Vector3.left * Time.deltaTime * _speed;
            transform.position += dir;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(Vector3.right), 0.2f);
            dir = Vector3.right * Time.deltaTime * _speed;
            transform.position += dir;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Managers.UI.ShowPopUpUI<UIButton>();
            anim.SetTrigger("Jump");
            //prefab = Managers.Resource.Instantiate("Knight");
        }
        bool run =false;
        if (Input.GetKey(KeyCode.LeftShift))
            run = true;
        anim.SetBool("Run", run);

        if(Input.GetKeyDown(KeyCode.V))
        {
            Managers.UI.ClosePopUpUI();
            //Managers.Resource.Destroy(prefab);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            Managers.Sound.Play("Player/univ0001");
        }
        if (dir != Vector3.zero)
        {
            float move = dir.magnitude;
            anim.SetFloat("Movement", move);
        }

    }
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == Define.State.Die) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 10000f, Color.red, 1.0f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, _mask))
        {
            _destPos = hit.point;
            _state = Define.State.Moving;
        }
    }
}
