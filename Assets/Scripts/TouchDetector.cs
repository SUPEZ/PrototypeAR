using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    [SerializeField]
    private GameObject _dummy = null;

    private Touch touch;

    private Animator _dummyAnimator = null;
    
    private void Awake()
    {
        if (_dummy)
        _dummyAnimator = _dummy.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (_dummy)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    ChangeAnimationState();
            }
            else
            if (Input.anyKeyDown)
            {
                ChangeAnimationState();
            }
        }
    }

    private void ChangeAnimationState()
    {
        if (_dummyAnimator.GetBool("OnAnimation") == false)
            _dummyAnimator.SetBool("OnAnimation", true);
        else
        if (_dummyAnimator.GetBool("OnAnimation") == true)
            _dummyAnimator.SetBool("OnAnimation", false);
    }
}
