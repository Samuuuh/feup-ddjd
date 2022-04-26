using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverlay;
    [SerializeField] private CrystalWheelController _crystalWheel;
    [SerializeField] private InventoryController _inventory;
    [SerializeField] private GameObject _pauseMenu;
    
    void Awake()
    {   
        Events.OnToggleInventory.AddListener(OnToggleInventory);
        Events.OnToggleCrystalWheel.AddListener(OnToggleCrystalWheel);
        Events.OnTogglePauseMenu.AddListener(OnTogglePauseMenu);
    }

    void Update()
    {
        
    }

    public void OnToggleInventory() {
        _inventory.OnToggleInventory();
    }

    public void OnToggleCrystalWheel() {
        _crystalWheel.OnToggleCrystalWheel();
    }

    public void OnTogglePauseMenu() {
        _pauseMenu.SetActive(true);
    }
}