using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class TutorialBlink : MonoBehaviour
{
    Animator _animator;
    P_ShootLogic _P_shoot;
    bool _triggerSlowMoUI;
    float _slowMoHoldTimer;
    [SerializeField] GameObject SlowMoCanvas;
    SpriteRenderer _p_SR;
    TMP_Text _snowmanShootText;
    enum TutorialBlinkObjectType 
    {
        ShootTutorial,
        SlowMoTutorial,
        TorchTutorial,
        ShootTorchText,
        SnowManShootingTutorial
    }
    [SerializeField] TutorialBlinkObjectType _objType;


    private void Awake()
    {
        _animator = this.GetComponent<Animator>(); //get animator component off of self
        if (TutorialBlinkObjectType.ShootTutorial == _objType) 
        {
            _P_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ShootLogic>();
        }
        if (TutorialBlinkObjectType.SnowManShootingTutorial == _objType)
        {
            _snowmanShootText = this.GetComponent<TMP_Text>();
            _p_SR = transform.parent.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        switch (_objType)
        {
            case TutorialBlinkObjectType.ShootTutorial:
                ShootTutorialBlink();
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
            case TutorialBlinkObjectType.ShootTorchText:
                if (SlowMoCanvas == null) this.GetComponent<TMP_Text>().alpha = 1.0f;
                    break;
            case TutorialBlinkObjectType.SnowManShootingTutorial:
                _snowmanShootText.alpha = _p_SR.color.a;
                break;
        }
    }
    void ShootTutorialBlink()
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
