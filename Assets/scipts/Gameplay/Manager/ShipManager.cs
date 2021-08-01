using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField]ShipController shipController;
    [SerializeField] PlayerFx playerFx;
    [SerializeField] Animator animator;
    public ShipController getShipController => shipController;

    private void Start()
    {
        shipController.OnStartMove += StartAnimationMove;
        shipController.OnStartShoot += StartAnimationShoot;
        playerFx.OnShoot += shipController.Shoot;
    }
    void StartAnimationShoot()
    {
        animator.Play("shoot");
    }
    void StartAnimationMove(bool state)
    {
        animator.SetBool("OnMove", state);
    }
}
