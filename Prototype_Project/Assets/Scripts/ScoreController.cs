using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _pointsValue;
    [SerializeField]
    private TextMeshProUGUI _TurnsValue;
    // Start is called before the first frame update
    void Start()
    {
        Transform pointsGameObject = FindChildWithTag(this.transform, "pointsvalue");
        _pointsValue = pointsGameObject.GetComponent<TextMeshProUGUI>();

        Transform turnsGameObject = FindChildWithTag(this.transform, "turnsvalue");
        _TurnsValue = turnsGameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateValues(int points, int turns)
    {
        _pointsValue.text = points.ToString();
        _TurnsValue.text = turns.ToString();
    }

    // searching for the text manually since there is not a long list of gameobjects
    Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
                return child;

            // recursively search in nested children
            Transform result = FindChildWithTag(child, tag);
            if (result != null)
                return result;
        }
        return null;
    }
}
