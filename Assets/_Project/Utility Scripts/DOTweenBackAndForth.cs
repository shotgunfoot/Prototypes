using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenBackAndForth : MonoBehaviour
{    
    Sequence mySequence;

    private void Start() {
        mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOLocalMoveX(15, 2))
        .Append(transform.DOLocalMoveX(-5, 2));
        mySequence.SetLoops(-1);;
        mySequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
