using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOption : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnEnter.Invoke();
        }
    }
}
