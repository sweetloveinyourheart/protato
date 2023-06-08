using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthbar : MonoBehaviour
{
    [SerializeField] Canvas healthBarCanvas;
    [SerializeField] Slider slider;
    [SerializeField] float offset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        HideHealthbar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthbar(float current, float max)
    {
        slider.value = current / max;
        ShowHealthbar();
    }

    public void Setup(Transform pos)
    {
        transform.position = new Vector3(pos.position.x, pos.position.y + offset, 0);
    }

    private void ShowHealthbar()
    {
        healthBarCanvas.enabled = true;
    }

    private void HideHealthbar()
    {
        healthBarCanvas.enabled = false;
    }
}
