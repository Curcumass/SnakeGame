using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Block))]
public class BlockView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;
    
    private Block _block;

    private void Awake()
    {
        _block = GetComponent<Block>();
    }

    private void OnEnable()
    {
        _block.FillingUpdate += OnFilingUpdate;
    }

    private void OnDisable()
    {
        _block.FillingUpdate -= OnFilingUpdate;
    }

    private void OnFilingUpdate(int leftToFill)
    {
        _view.text = leftToFill.ToString();
    }

}
