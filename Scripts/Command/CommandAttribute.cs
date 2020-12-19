namespace UnityDebugConsole.CommandTool{
    public class CommandAttribute : System.Attribute
    {
        public CommandAttribute(string Name){
            this.Name = Name;
        }
        public CommandAttribute(string Name , string Description):this(Name){
            this.Description = Description;
        }
        public CommandAttribute(string Name , string Description, string args):this(Name , Description){
            this.ArgsDescription = args;
        }

        public string Name{get;} = zeroValue;
        public string Description {get;} = zeroValue;
        public string ArgsDescription {get;}

        public static readonly string zeroValue = "undefined"; 
    }
}
