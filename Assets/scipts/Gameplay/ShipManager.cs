using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField]ShipController shipController;
    public ShipController getShipController => shipController;

}
