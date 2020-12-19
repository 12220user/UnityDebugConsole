using UnityEngine;
using UnityDebugConsole.CommandTool;

///<summary>
/// Make any commands to unity PlayerPrefs object.
///</summary>

[CommandPackage("PlayerPrefs command package")]
public static class PlayerPrefsCommandPack 
{
    [Command("pprf-g" , "console method to get PlayerPresfs value" , "flag(-i (int) , -f (float), -s(string)) | key")]
    public static string GetValue(string flag , string key){
        var result = "";
        switch(flag){
            case "-i":
                result = PlayerPrefs.GetInt(key , 0).ToString();
                break;
            case "-f":
                result = PlayerPrefs.GetFloat(key , 0).ToString();
                break;
            default:
                result = PlayerPrefs.GetString(key , "undefiend");
                break;
        }
        return result;
    }
    [Command("pprf-s" , "console method to set PlayerPresfs value" , "flag(-i (int) , -f (float), -s(string)) | key | value")]
    public static string SetValue(string flag , string key , string value){
        switch(flag){
            case "-i":
                PlayerPrefs.SetInt( key ,int.Parse(value));
                break;
            case "-f":
                PlayerPrefs.SetFloat( key , float.Parse(value));
                break;
            default:
                PlayerPrefs.SetString( key , value);
                break;
        }
        return "save completed";
    }

    [Command("pprf-d" , "delete all saves to PlayerPrefs")]
    public static string DeleteAll(){
        PlayerPrefs.DeleteAll();
        return "All delite";
    }

    [Command("pprf-dk" , "delete one saves to PlayerPrefs" , "key")]
    public static string DeleteKey(string key){
        PlayerPrefs.DeleteKey(key);
        return "delite " + key;
    }
    
    [Command("pprf-hash" , "return true if key is not empty" , "key")]
    public static string Hash(string key){
        return PlayerPrefs.HasKey(key).ToString();
    }
    
    [Command("pprf-save" , "save all changes" )]
    public static string Save(){
        PlayerPrefs.Save();
        return "all saved";
    }
}
