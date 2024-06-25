using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Keychain _keychain;
    [SerializeField] private Text _text;
    void Update()
    {
        _text.text = "Keys: " + string.Join(", ", _keychain.m_KeyTypeOwned.ToArray());
    }
}
