using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button gridRebuildButton;
    [SerializeField] private TMP_InputField gridResizeInputField;
    [SerializeField] private TextMeshProUGUI matchCountText;
    
    private void OnEnable()
    {
        Actions.OnMatchDataChanged += CheckMatchCount;
    }

    private void OnDisable()
    {
        Actions.OnMatchDataChanged -= CheckMatchCount;
    }

    private void Awake()
    {
        gridRebuildButton.onClick.AddListener(SetGridSize);
    }

    private void Start()
    {
        CheckMatchCount();
    }

    public void SetGridSize()
    {
        int.TryParse(gridResizeInputField.text, out var result);
        GridManager.i.size = result;
        GridManager.i.CreateGrid();
    }

    private void CheckMatchCount()
    {
        matchCountText.text = "Match Count :" + " " + GridManager.i.matchCount;
    }
}
