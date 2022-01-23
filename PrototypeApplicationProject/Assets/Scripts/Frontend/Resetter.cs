using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows for reset sequences to be configured in the inspector.
/// </summary>
public class Resetter : MonoBehaviour
{
    /// <summary>
    /// UnityEvent representing the reset sequence. Can be configured in the 
    /// inspector.
    /// </summary>
    public UnityEvent Reset;

    /// <summary>
    /// Starts the reset sequence of the attached GameObject.
    /// </summary>
    public void InvokeReset()
    {
        Reset.Invoke();
    }
}
