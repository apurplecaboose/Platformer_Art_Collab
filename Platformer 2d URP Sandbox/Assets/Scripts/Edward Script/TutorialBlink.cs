using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class TutorialBlink : MonoBehaviour
{
    Animator _animator;
    P_ShootLogic _P_shoot;
    bool _triggerSlowMoUI;
    float _slowMoHoldTimer;
    [SerializeField] GameObject SlowMoCanvas;
    enum TutorialBlinkObjectType 
    {
        Snowman_ShootTutorial,
        SlowMoTutorial,
        TorchTutorial
    }
    [SerializeField] TutorialBlinkObjectType _objType;


    private void Awake()
    {
        _animator = this.GetComponent<Animator>(); //get animator component off of self
        if (TutorialBlinkObjectType.Snowman_ShootTutorial == _objType) 
        {
            _P_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ShootLogic>();
        }
    }

    void Update()
    {
        switch (_objType)
        {
            case TutorialBlinkObjectType.Snowman_ShootTutorial:
                Snowman_ShootTutorialBlink();
                break;
            case TutorialBlinkObjectType.SlowMoTutorial:
                if(_triggerSlowMoUI)
                {
                    _animator.SetBool("TriggerTutorial", true);
                    if(Input.GetKey(KeyCode.Mouse1))
                    {
                        _slowMoHoldTimer += Time.unscaledDeltaTime;
                    }
                    if(_slowMoHoldTimer >= 0.75f)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;
            case TutorialBlinkObjectType.TorchTutorial:
                if (SlowMoCanvas.activeSelf) this.transform.GetChild(0).gameObject.SetActive(true);
                else this.transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
    }
    void Snowman_ShootTutorialBlink()
    {
        if (_animator != null) // double check
        {
            if (_P_shoot.CanShoot && _P_shoot.BulletNum != 0)
            {
                _animator.SetBool("TriggerTutorial", true);
                if(this.transform.childCount > 0)
                {
                    this.transform.GetChild(0).GetComponent<Animator>().SetBool("TriggerTutorial", true);
                }
                Destroy(this);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _triggerSlowMoUI = true;
        }
    }
}
