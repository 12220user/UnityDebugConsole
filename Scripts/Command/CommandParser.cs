using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityDebugConsole.CommandTool;

namespace UnityDebugConsole.CommandTool{
    public class CommandParser
    {
        public static List<(string , List<ConsoleCommandData>)> Parse(){
            var classes = GetTypesWithMyAttribute(Assembly.GetExecutingAssembly());
            var array = new List<(string , List<ConsoleCommandData>)>();
            
            foreach(var item in classes){
                var classAtr = (CommandPackageAttribute)Attribute.GetCustomAttribute( item , typeof(CommandPackageAttribute));
                var package = new List<ConsoleCommandData>();

                foreach(var field in item.GetMethods()){
                    if( field.IsStatic && field.IsPublic &&
                        Attribute.IsDefined(field, typeof(CommandAttribute)) && 
                        field.ReturnType == typeof(String)){
                        var atr = field.GetCustomAttribute(typeof(CommandAttribute)) as CommandAttribute;
                        package.Add(new ConsoleCommandData(atr.Name , atr.Description , atr.ArgsDescription , field));
                    }
                }
                array.Add(( classAtr.Name , package));
            }

            return array;
        }

        //  Parse string request and make cortege with command name and args array
        public static (string, object[]) RequestParse(string request){
            request = request.Replace("  " , " ");
            //var values = request.Trim().Split(new char[] {' '});
            var values = AdvancedSplit(request);

            if(values.Length < 1){
                throw new Exception("Oops, your request is null");
            }
            var name = values[0];
            var objs = new object[values.Length - 1];
            for(int i = 0; i < values.Length - 1; i++){
                objs[i] = values[i+1];
            }
            
            return (name , objs);
        }

        // Change type to current
        public static object ConvertToCurrentType( object obj , Type t){
            var value = obj.ToString();
            if      (t == typeof(int))      // Int
                return int.Parse(value);
            else if (t == typeof(float))    // Float
                return float.Parse(value);
            else if (t == typeof(bool))     // Bool
                return bool.Parse(value);
            else if (t == typeof(byte))     // Byte
                return byte.Parse(value);
            else if (t == typeof(short))    // Short
                return short.Parse(value);
            else if (t == typeof(long))     // long
                return long.Parse(value);
            else if (t == typeof(double))   // double
                return double.Parse(value);
            else if (t == typeof(decimal))  // decimal
                return decimal.Parse(value);
            else if (t == typeof(char))     // char
                return char.Parse(value);
            else if (t == typeof(string))   // string
                return value;
            else
                return obj;
        }

        public static string[] AdvancedSplit(string value){
            var words = new List<string>();
            bool newWord = true;
            bool ignoreSpace = false;
            string word = "";
            for(int i = 0; i <= value.Length; i++){
                var c = ' ';
                if(i < value.Length) c  = value[i];
                else if(i == value.Length) c = ' ';

                var isQuotes = c == '\"'; 
                if(!ignoreSpace){
                    if(c == ' '){
                        if(!newWord){
                            words.Add(word);
                            word = "";
                        }

                        newWord = true;
                    }
                    else{
                        newWord = false;
                        if(isQuotes) ignoreSpace = true;
                        else word += c;
                    }
                }
                else{
                    if(isQuotes){
                        ignoreSpace = false;
                        words.Add(word);
                        word = "";
                        newWord = true;
                    }
                    else{
                        word += c;
                    }
                }
            }
            return words.ToArray();
        }
        
        // Get all classes with CommandPackageAttribute
        public static IEnumerable<Type> GetTypesWithMyAttribute(Assembly assembly)
        {
            foreach(Type type in assembly.GetTypes())
            {
                if (Attribute.IsDefined(type, typeof(CommandPackageAttribute)))
                    yield return type;
            }
        }
    }
}
