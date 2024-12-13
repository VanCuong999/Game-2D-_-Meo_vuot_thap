using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeathNPC_Follow : MonoBehaviour
{
    public static HeathNPC_Follow Instance;
    private float _maxHeath = 100;
    private float _currentHeath;
    [SerializeField] private Image _heathBarFill;
    [SerializeField] private float _damageAmont;
    [SerializeField] private Transform _healthBarTranForm;
    private Camera _camera;


    private void Awake() {
        _currentHeath = _maxHeath;
        _camera = Camera.main;
        Instance = this;
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }
        _healthBarTranForm.rotation = _camera.transform.rotation;   
    }
    
    public void TakeDamage(float amount)
    {
        _currentHeath -= amount;
        _currentHeath = Mathf.Clamp(_currentHeath,0,_maxHeath);
        if (_currentHeath == 0)
        {
            Debug.Log("NPC DIE");
            UIManager.Intance.ActiveOver();
        }
        UPdateHeathBar();
    }
    private void UPdateHeathBar()
    {
        _heathBarFill.fillAmount = _currentHeath / _maxHeath;
    }
}
