using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveButton : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Data data;

    [Header("Buttons")]
    [SerializeField] Button buttonSave;
    [SerializeField] Button buttonLoad;
    void Start()
    {
        buttonSave.onClick.AddListener(() => data.Save());
        buttonLoad.onClick.AddListener(() => data.Load());
    }

    
}
