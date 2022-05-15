using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _PlayerInputActions;
    private Animator _Animator;
    private Vector2 TouchPosition;
    private Vector2 OldTouchPosition = new Vector2();

    public float SmoothMoving = 4f;

    private void FixedUpdate()
    {
        TouchPosition = _PlayerInputActions.Player.Move.ReadValue<Vector2>();

        if (TouchPosition != Vector2.zero)
        {
            Move();
            SetAnimation();
        }

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void Awake()
    {
        _Animator = GetComponent<Animator>();
        _PlayerInputActions = new PlayerInputActions();
        _PlayerInputActions.Enable();
    }
    private void Move()
    {
        Vector3 ScreenCoordinates = new Vector3(TouchPosition.x, TouchPosition.y, Camera.main.nearClipPlane); // То, куда нажали пальцем в игровых координатах
        Vector3 WorldCoordinates = Camera.main.ScreenToWorldPoint(ScreenCoordinates);
        WorldCoordinates.z = 0;

        Vector3 SmoothPosition = Vector3.Lerp(transform.position, WorldCoordinates,
            SmoothMoving * Time.fixedDeltaTime);
        transform.position = SmoothPosition;
        
    }

    private void SetAnimation()
    {
        Vector2 offset = TouchPosition - OldTouchPosition;
        if (offset != Vector2.zero)
        {
            if (offset.y > 0.1) _Animator.SetInteger("Animation",0);
            if (offset.y < -0.1) _Animator.SetInteger("Animation",1);
            if (offset.x > 5) _Animator.SetInteger("Animation", 2);
            if (offset.x < -5) _Animator.SetInteger("Animation", 3);
        }
        else _Animator.SetInteger("Animation", 0);

        OldTouchPosition = TouchPosition;
    }
}
