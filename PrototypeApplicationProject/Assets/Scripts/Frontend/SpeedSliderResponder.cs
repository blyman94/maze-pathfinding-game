using TMPro;
using UnityEngine;

/// <summary>
/// Intercepts signals from the speed slider and converts the resulting float to
/// values readable by interested UI elements.
/// </summary>
public class SpeedSliderResponder : MonoBehaviour
{
    /// <summary>
    /// The text displayed on the slider handle that shows the current value
    /// of the slider.
    /// </summary>
    [Tooltip("The text displayed on the slider handle that shows " +
        "the current value of the slider.")]
    [SerializeField] private TextMeshProUGUI currentSliderText;

    /// <summary>
    /// Integer variable dictating how fast the visualization runs.
    /// </summary>
    [Tooltip("Integer variable dictating the row count of the grid.")]
    [SerializeField] private IntVariable framesPerSecond;

    /// <summary>
    /// Converts the incoming float value to an integer and updates the current
    /// slider text accordingly.
    /// </summary>
    /// <param name="newValue">Float value from slider.</param>
    public void UpdateCurrentSliderText(float newValue)
    {
        currentSliderText.text = ((int)newValue).ToString();
    }

    /// <summary>
    /// Converts the incoming float value to an integer and updates the current
    /// frames per second accordingle.
    /// </summary>
    /// <param name="newValue">Float value from slider.</param>
    public void UpdateCurrentFPS(float newValue)
    {
        framesPerSecond.Value = (int)newValue;
    }
}
