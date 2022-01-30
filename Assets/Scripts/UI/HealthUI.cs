using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    private void OnEnable()
    {
        playerScript.OnTakeDamage.AddListener(GetData);
    }

    private void OnDisable()
    {
        playerScript?.OnTakeDamage.RemoveListener(GetData);
    }

    void GetData(Entity entity, DamageData data) {
        UpdateHealth();
    }

    [SerializeField] private Slider healthSlider;
    Player playerScript;

    private void Awake()
    {
        playerScript = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
        healthSlider.minValue = 0;
        healthSlider.maxValue = playerScript.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealth() {
        healthSlider.value = playerScript.CurrentHealth;
    }
}
