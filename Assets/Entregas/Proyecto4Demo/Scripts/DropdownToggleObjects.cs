using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class DropdownToggleObjects : MonoBehaviour
{
    [Header("Referencias")]
    public TMP_Dropdown dropdown;
    public GameObject hannahBanana;
    public GameObject theNotGoat;

    void Start()
    {
        // Asegurarse de que haya un listener
        dropdown.onValueChanged.AddListener(OnDropdownChanged);

        // Aplicar el valor inicial
        OnDropdownChanged(dropdown.value);
    }

    void OnDropdownChanged(int index)
    {
        switch (index)
        {
            case 0:
                hannahBanana.SetActive(true);
                theNotGoat.SetActive(false);
                break;
            case 1:
                hannahBanana.SetActive(false);
                theNotGoat.SetActive(true);
                break;
        }
    }

    void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
    }
}
