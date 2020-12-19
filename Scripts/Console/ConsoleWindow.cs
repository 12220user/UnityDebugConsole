using UnityEngine;
using UnityEngine.UI;
using UnityDebugConsole;

public class ConsoleWindow : MonoBehaviour
{
    [SerializeField]private Text outField;
    [SerializeField]private InputField inField;
    [SerializeField]private Button SubmitButton;

    private CommandInterpreter interpreter;

    private void Start(){
        interpreter = CommandInterpreter.instance;
        // Enter text events
        inField.onEndEdit.AddListener(ConsoleWritedMessenge);
        //SubmitButton.onClick.AddListener(()=>{ConsoleWritedMessenge(inField.text);});
    }

    // Write any data to consol UI
    private void Write(object text){
        outField.text += text.ToString() + "\n";
    }

    private void ConsoleWritedMessenge(string messenge){
        inField.text = "";
        // print command
        Write("> "+messenge);
        // print result
        var msg = interpreter.Interpret(messenge);
        if(msg != null)if(msg != "")Write(msg);
    }
}
