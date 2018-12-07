using UnityEngine;
using UnityEngine.UI;

public class LimbUIListener : MonoBehaviour
{

    public Image _img;
    public FloatVariable _value;

    private void Update() {
        _img.color = Color.Lerp(new Color(1, 0, 0), new Color(0, 1, 0), _value.Value / 100);
    }

}