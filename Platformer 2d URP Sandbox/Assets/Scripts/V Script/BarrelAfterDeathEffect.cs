using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelAfterDeathEffect : MonoBehaviour
{
    [SerializeField] float _destroyCountDown = 0;
    bool startProcedure;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            startProcedure = true;
        }
        if (startProcedure)
        {
            BeforeDestroyProcedure(1.5f);

        }
    }


    void BeforeDestroyProcedure(float timeTillDestroy)
    {



        _destroyCountDown += 1 * Time.deltaTime;
        if (_destroyCountDown >= timeTillDestroy)
        {
            Destroy(gameObject);
        }
        Debug.Log(_destroyCountDown);
    }


}
