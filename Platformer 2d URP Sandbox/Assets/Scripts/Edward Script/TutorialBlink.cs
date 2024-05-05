using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class TutorialBlink : MonoBehaviour
{
    Animator _animator;
    P_ShootLogic _P_shoot;
    bool _triggerSlowMoUI,_triggerShootUI;
    enum TutorialBlinkObjectType 
    {
        SnowmanTutorial,
        SlowMoTutorial,
        ShootTutorial,
    }
    [SerializeField] TutorialBlinkObjectType _objType;
    private void Awake()
    {
        _animator = this.GetComponent<Animator>(); //get animator component off of self
        if (TutorialBlinkObjectType.SnowmanTutorial == _objType) 
        {
            _P_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ShootLogic>();
        }
    }

    void Update()
    {
        switch (_objType)
        {
            case TutorialBlinkObjectType.SnowmanTutorial:
                SnowmanTutorialBlink();
                break;
            case TutorialBlinkObjectType.SlowMoTutorial:
                if(_triggerSlowMoUI)
                {
                    _animator.SetBool("TriggerTutorial", true);
                    Destroy(this);//destroys script not gameobject
                }
                break;
            case TutorialBlinkObjectType.ShootTutorial:
                if (_animator != null) // double check
                {
                    if (_P_shoot.CanShoot && _P_shoot.BulletNum != 0 && !_triggerShootUI)
                    {
                        _animator.SetBool("TriggerTutorial", true);
                        _triggerShootUI = true;
                    }
                    if(Input.GetKeyDown(KeyCode.Mouse0) && _triggerShootUI)
                    {
                        Destroy(this.gameObject);
                    }
                }
                else Debug.Log("Object isnt here dummy it got destroyed");
                break;
        }
    }
    void SnowmanTutorialBlink()
    {
        if (_animator != null) // double check
        {
            if (_P_shoot.CanShoot && _P_shoot.BulletNum != 0)
            {
                _animator.SetBool("TriggerTutorial", true);
                Destroy(this);//destroys script not gameobject
            }
        }
        else Debug.Log("Object isnt here dummy it got destroyed");
    }
    private void OnTriggerEnter2D(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            _triggerSlowMoUI = true;
        }
    }
}
