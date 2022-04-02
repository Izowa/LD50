using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatBar : MonoBehaviour
{

    public float maxHeight;

    private RectTransform trans;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
        maxHeight = trans.sizeDelta.y;
    }

    public void updateBar(float value, float max)
    {
        trans.sizeDelta = new Vector2(trans.sizeDelta.x, maxHeight * (value / max));
        trans.localPosition = new Vector3(trans.localPosition.x, (maxHeight - maxHeight * (value / max)) / -2, trans.localPosition.z);
    }
}
