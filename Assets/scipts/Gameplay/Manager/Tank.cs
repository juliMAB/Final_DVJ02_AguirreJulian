using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Tank : MonoBehaviour,IHitable
{
    [Header("ClassTank")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject canonPoint;
    [SerializeField] GameObject pivHead;
    [SerializeField] float shootForce;
    [SerializeField] float rotationSpeed;
    [SerializeField] int lives;
    [SerializeField] int damage;
    private Vector3 target;
    public Vector3 Target { set { target = value; } get { return target; } }
    private bool cr_running;
    public bool CR_running { set { cr_running = value; } get { return cr_running; } }
    public System.Action OnStartShoot;


    public void Shoot()
    {
        GameObject go = Instantiate(bullet, canonPoint.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce((target - transform.position) * shootForce, ForceMode.Impulse);
    }

    public void TakeDamage(int damage)
    {
        lives-=damage;
    }

    public IEnumerator headRotate(Vector3 target)
    {
        CR_running = true;
        Quaternion a = pivHead.transform.rotation;
        Quaternion b = Quaternion.LookRotation(target - transform.position, transform.up);
        float t = 0;
        while (0.05f < Quaternion.Angle(pivHead.transform.rotation, b))
        {
            pivHead.transform.rotation = Quaternion.Lerp(a, b, t);
            t += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        OnStartShoot?.Invoke();
        CR_running = false;
    }
}
