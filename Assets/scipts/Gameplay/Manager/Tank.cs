using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tank : MonoBehaviour,IHitable
{
    [Header("ClassTank")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject canonPoint;
    [SerializeField] GameObject pivHead;
    [SerializeField] GameObject deathTank;
    [SerializeField] float shootForce;
    [Range(2, 0)]
    [SerializeField] float rotationSpeed;
    [SerializeField] int lives;
    [SerializeField] int damage;
    private Vector3 target;
    public Vector3 Target { set { target = value; } get { return target; } }
    private bool cr_running=false;
    public bool CR_running { set { cr_running = value; } get { return cr_running; } }
    public System.Action OnStartShoot;
    public System.Action OnDeath;


    public void Shoot()
    {
        GameObject go = Instantiate(bullet, canonPoint.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce((target - transform.position) * shootForce, ForceMode.Impulse);
    }

    public void TakeDamage(int damage)
    {
        lives-=damage;
        Debug.Log(transform.name + " life: " + lives);
        if (lives<0)
        {
            OnDeath?.Invoke();
            death();
        }
    }

    public IEnumerator headRotate(Vector3 target)
    {
        CR_running = true;
        Quaternion a = pivHead.transform.rotation;
        Quaternion b = Quaternion.LookRotation(target - transform.position, transform.up);
        float t = 0;
        while (0.05f <= Quaternion.Angle(pivHead.transform.rotation, b))
        {
            pivHead.transform.rotation = Quaternion.Lerp(a, b, t);
            t += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        OnStartShoot?.Invoke();
        CR_running = false;
    }

    public void death()
    {
        Destroy(gameObject);
        GameObject go = Instantiate(deathTank, transform.position, transform.rotation, null);
    }
}
