using System.Linq;
using UnityEngine;
using UnityDebugConsole;
using UnityDebugConsole.CommandTool;

[CommandPackage("Standart command package")]
public static class StandartCommandPack
{
    [Command("debug" , "tested command")]
    public static string Debug(){
        return "youchooo, all work";
    }

    [Command("print" , "out any data in console", "string value")]
    public static string Print(string value){
        return string.Join(" " , value);
    }

    [Command("help" , "return all commands")]
    public static string Help(){
        var result = "";
        var hex = ConsoleColorConfiguration.HEXColor(CommandInterpreter.instance.colorConfig.importantColor);
        //UnityEngine.Debug.Log(CommandInterpreter.instance.CommandsCollection.Count());
        var collection = CommandInterpreter.instance.CommandsCollection;
        collection = collection.OrderBy(x => x.Name).ToList();
        foreach(var command in collection){
            result += $"<color={hex}>"+command.Name+"</color>" + "   - " + command.Description +"\n";
        }
        return result;
        
    }

    [Command("help" , "wiht flag argument" , "flag")]
    public static string Help(string flag){
        var result = "";
        if(flag == "-a" || flag == "-advanced"){
            var hex = ConsoleColorConfiguration.HEXColor(CommandInterpreter.instance.colorConfig.importantColor);
            var hex2 = ConsoleColorConfiguration.HEXColor(CommandInterpreter.instance.colorConfig.secondImportantColor);
            var headHex = ConsoleColorConfiguration.HEXColor(CommandInterpreter.instance.colorConfig.headerColor);
            //UnityEngine.Debug.Log(CommandInterpreter.instance.CommandsCollection.Count());
            var packs = CommandInterpreter.instance.CommandsPacks.ToArray();
            for(int i =0 ; i < packs.Length; i++){
                var commands = packs[i].Item2;
                result += $"<b><color={headHex}>{packs[i].Item1}</color></b> \n";
                for(int n = 0; n < commands.Count; n++){
                    var argsValue = commands[n].ArgsDescription != null?$"   args: <color={hex2}>( {commands[n].ArgsDescription} )</color>":"";
                    result += $"    <color={hex}>"+commands[n].Name+ "</color>"+
                    argsValue+ 
                    "  - " +
                    commands[n].Description +"\n";
                }
            }
        }
        else return Help();
        
        return result;
        
    }

    [Command("info" , "Return conrectly help information some command." , "Name object to scene")]
    public static string Info(string name){
        //UnityEngine.Debug.Log(name);
        var hex = ConsoleColorConfiguration.HEXColor(CommandInterpreter.instance.colorConfig.importantColor);
        var command = CommandInterpreter.instance.CommandsCollection.First(x=> x.Name == name);
        var result = $"<color={hex}>{command.Name}</color>  - {command.Description}";
        return result;
    }

    [Command("sum" , "sum 2 numbers" , "first num | second num")]
    public static string Sum(float x , float y){
        return (x + y).ToString();
    }

    [Command("clear" , "clear console field")]
    public static string Clear(){
        var obj = GameObject.FindGameObjectWithTag("ConsoleTextField");
        obj.GetComponent<UnityEngine.UI.Text>().text = "";
        return "";
    }

    [Command("test" , "")]
    public static string Test(int i){
        return i.ToString();
    }

    [Command("tr" , "change transform values (position , rotation , scale)." , "Name | Flag (-t , -r , -s) | x | y | z")]
    public static string Transform(string Name , string flag , float x , float y , float z){
        // Get scene object
        var obj = GameObject.Find(Name);
        //UnityEngine.Debug.Log(Name);
        if(Name[0] == '#')
            obj = GameObject.FindGameObjectWithTag(Name.Remove(0 , 1));

        Vector3 value = new Vector3(x , y , z);

        // action
        switch(flag){
            case "-t":
                obj.transform.position = value;
                break;
            case "-r":
                obj.transform.rotation = Quaternion.Euler(x , y , z);
                break;
            case "-s":
                obj.transform.localScale = value;
                break;
            default:
                return "Don't correct flag type: \n" + 
                "   -t  - transform coordinate. \n"+
                "   -r  - rotate. \n" +
                "   -s  - change scale.";
        }
        
        return "";
    }

    [Command("coml" , "Command Line - this command run array with commands" , "line with commands")]
    public static string CommandLine(string value){
        var commands = value.Split(';');
        var inter = CommandInterpreter.instance;
        var result = "";
        foreach(var command in commands){
            var line = inter.Interpret(command);
            if(line != null)if(line != "")result += line +"\n";
        }
        return result;
    }

    [Command("title" , "Change console title" , "new Name")]
    public static string Title(string name){
        var text = GameObject.FindGameObjectWithTag("ConsoleTitle").GetComponent<UnityEngine.UI.Text>();
        if(text != null)text.text = name;
        return "";
    }
}
