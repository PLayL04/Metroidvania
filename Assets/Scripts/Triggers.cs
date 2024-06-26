using UnityEngine;
using UnityEngine.SceneManagement;

enum Type
{
    Finish,
    Restart
}
public class Triggers : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private GameObject _canvasGUI;
    [SerializeField] private GameObject _canvasHUD;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (_type == Type.Finish && collider2D.gameObject.name == "Player")
        {
            Debug.Log("Finish");
            _canvasGUI.SetActive(true);
            _canvasHUD.SetActive(false);
            
        }
        else if (_type == Type.Restart &&  collider2D.gameObject.name == "Player")
        {
            Debug.Log("Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
