using UnityEngine;
using UnityEngine.UI;

public class ConsoleWindowControler : MonoBehaviour
{
    [SerializeField]private RectTransform console;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            console.gameObject.SetActive(!console.gameObject.activeSelf);
        }
    }
}
