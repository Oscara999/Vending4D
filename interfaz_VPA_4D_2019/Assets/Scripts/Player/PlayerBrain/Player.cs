using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class Player : Singleton<Player>
{
    public Ray ray;
    public int lifes;
    public bool isImmune;
    [SerializeField] float currentTimeSpawn;
    [SerializeField] float timeToSpawn;
    [SerializeField] bool isActive;
    [SerializeField] CameraShakeSimpleScript shake;
    [SerializeField] ObjectPooling pooling;
    [SerializeField] GameObject poder_invierno;
    public Movimiento_UI_Control_Juego movingController;
    public Sound[] fx_Sound;
    public bool IsActivate { get { return isActive; } }

    void Start()
    {
        lifes = 3;
        movingController = GetComponentInChildren<Movimiento_UI_Control_Juego>();
        currentTimeSpawn = timeToSpawn;
    }

    public void StateController()
    {
        isActive = !isActive;
    }

    void Update()
    {
        if (isActive)
        {
            //ray = new Ray(transform.position, Enemy.Instance.transform.position);
            //Debug.DrawRay(transform.position, ray.direction, Color.green);
            movingController.Selected();
            //Debug.Log("aqui " + movimiento.crossFire.transform.position);
            currentTimeSpawn++;
        }
    }

    public void Attack()
    {
        if (!StatesManager.Instance.ui.crossFire.activeInHierarchy)
            return;

        if (isActive && currentTimeSpawn > timeToSpawn)
        {
            Destroy(Instantiate(poder_invierno, StatesManager.Instance.leapMotionMovementController.ray.origin, Quaternion.identity), 20f);
            currentTimeSpawn = 0;
            //ProjectileMoveScript poder = pooling.GetPooledObjects().GetComponent<ProjectileMoveScript>();
            //poder.Start();
            //poder.gameObject.SetActive(true);
        }
    }

    public void Damage()
    {
        shake.SetShakeTime();

        if (!isImmune && lifes != 0)
        {
            SoundManager.Instance.PlayNewSound(Player.Instance.fx_Sound[0].name);
            lifes--;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Selectable")
        {
            Damage();
            other.gameObject.SetActive(false);
        }
    }
}
