using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOption : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnEnter.Invoke();
        }
    }
}
