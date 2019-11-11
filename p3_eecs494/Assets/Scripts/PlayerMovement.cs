using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.InputSystem;

[RequireComponent(typeof(BeatHitDetector))]
public class PlayerMovement : MonoBehaviour, Controls.IPlayerControlsActions
{
    public float stepSize = 1;

    private Quaternion target_rot;
    private Vector3 target_pos;
    private BeatHitDetector bhd;

    private Vector2 moveDir;

    private Controls controls;

    private bool update;

    void Start()
    {
        target_rot = transform.rotation;
        target_pos = transform.position;
    }

    void Awake()
    {
        bhd = GetComponent<BeatHitDetector>();
    }

    void OnEnable() {
        if(controls == null) {
            controls = new Controls();
            controls.PlayerControls.SetCallbacks(this);
        }
        controls.PlayerControls.Enable();
    }

    void OnDisable() {
        controls.PlayerControls.Disable();
    }

    public void SetUpdate(bool update) {
        this.update = update;
    }

    void Update()
    {
        if (GetComponent<Health>().dead())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rot, Time.deltaTime * 18);
        transform.position = Vector3.Lerp(transform.position, target_pos, Time.deltaTime * 15);
    }

    public void OnBeatHit((ButtonPress, BeatInfo) info)
    {
        if(!update) {
            return;
        }

        ButtonPress press = info.Item1;
        if (press == ButtonPress.MOVE)
        {
            if(moveDir.magnitude > 0.2F) {
                if(Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y)) {
                    target_rot = Quaternion.AngleAxis(90 * Mathf.Sign(moveDir.x), Vector3.up) * target_rot;
                } else {
                    Vector3 move = transform.forward * stepSize * Mathf.Sign(moveDir.y);
                    if(!Physics.Raycast(transform.position, move, move.magnitude, ~0, QueryTriggerInteraction.Ignore)) {
                        target_pos += move;
                    }
                    
                }
            }
        }
        else if (press == ButtonPress.ATTACK)
        {
            //this.gameObject.transform.GetChild(1).gameObject.GetComponent<PlayerAttack>().spinAttack();
            Debug.Log("attack");
            StartCoroutine(this.gameObject.transform.Find("Spin").gameObject.GetComponent<PlayerAttack>().spinAttack());
        }
    }

    void Controls.IPlayerControlsActions.OnAttackButton(InputAction.CallbackContext context) {
        if(!context.performed) {
            return;
        }
        bhd.PressButton(ButtonPress.ATTACK);
    }

    void Controls.IPlayerControlsActions.OnMoveButton(InputAction.CallbackContext context) {
        if(!context.performed) {
            return;
        }
        bhd.PressButton(ButtonPress.MOVE);
    }

    void Controls.IPlayerControlsActions.OnMoveDir(InputAction.CallbackContext context) {
        moveDir = context.ReadValue<Vector2>();
    }
}
