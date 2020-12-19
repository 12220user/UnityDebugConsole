namespace UnityDebugConsole.CommandTool{
    public class CommandPackageAttribute : System.Attribute{
        public CommandPackageAttribute(string Name){
            this.Name = Name;
        }
        public string Name {get;}
    }
}
