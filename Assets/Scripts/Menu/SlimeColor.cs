using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeColor : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private float speed = 0.05f;
    private float t;
    private Image slime;
    private void Start()
    {
        slime = GetComponent<Image>();
    }
    void Update()
    {
        t = Mathf.Clamp01(t + Time.deltaTime * speed);
        if (t == 1) t = 0;

        slime.color = gradient.Evaluate(t);

    }
}
