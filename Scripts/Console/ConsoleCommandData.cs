namespace UnityDebugConsole.CommandTool{
    public class ConsoleCommandData{
        public ConsoleCommandData (string Name , string Description , string ArgsDescription , System.Reflection.MethodInfo Action){
            this.Name = Name;
            this.Description = Description;
            this.Action = Action;
            this.ArgsDescription = ArgsDescription;
        }
        public string Name;
        public string Description;
        public string ArgsDescription;
        public System.Reflection.MethodInfo Action;
    }
}