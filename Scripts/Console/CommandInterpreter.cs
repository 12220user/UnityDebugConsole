using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityDebugConsole.CommandTool;

namespace UnityDebugConsole{
    public class CommandInterpreter : MonoBehaviour
    {
        // singletone pattern
        public static CommandInterpreter instance;

        public ConsoleColorConfiguration colorConfig;
        private List<(string , List<ConsoleCommandData>)> Commands;
        public IReadOnlyCollection<(string , List<ConsoleCommandData>)> CommandsPacks {get{return Commands;}}
        public IReadOnlyCollection<ConsoleCommandData> CommandsCollection {get
            {
                var list = new List<ConsoleCommandData>();
                foreach(var comPack in Commands){
                    list.AddRange(comPack.Item2.ToArray());
                }
                return list;
            }
        }

        private void Awake(){
            // in scene only object this type
            if(instance == null) instance = this;
            else Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

            Commands = CommandParser.Parse();
        }


        public string Interpret(string command){
            //Debug.Log(command);
            try{
                // zero request
                if(command.Trim() == "")
                    return "";
                var commandData = CommandParser.RequestParse(command);
                
                //Debug.Log(commandData + "   "  + commandData.Item2.Length);
                var com = CommandsCollection
                    .First(x=> x.Name == commandData.Item1 //);
                    && x.Action.GetParameters().Length == commandData.Item2.Length);
                
                // Change parametrs type to current 
                var args = new List<object>();
                for(int i = 0; i < commandData.Item2.Length; i++){
                    args.Add(CommandParser.ConvertToCurrentType(commandData.Item2[i], 
                        com.Action.GetParameters()[i].ParameterType));
                } 

                // Succsessfull
                return (string)com.Action.Invoke(null , args.ToArray());
            }
            catch (Exception e){
                // any exeptions out to console
                var hexColor = ConsoleColorConfiguration.HEXColor(colorConfig.errorColor);
                return $"<color={hexColor}>{e.Message}</color>";
            }
        }
    }

    #region Color Config
    [Serializable]
    public struct ConsoleColorConfiguration{
        public Color standartColor;
        public Color headerColor;
        public Color importantColor;
        public Color secondImportantColor;
        public Color warningColor;
        public Color errorColor;

        ///<summary>
        /// Change rgb color to hex system
        ///</summary>
        public static string HEXColor(Color c)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
        }
        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }
    }
    #endregion
}
