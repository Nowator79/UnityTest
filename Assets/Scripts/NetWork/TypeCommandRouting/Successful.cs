using Scripts;

public class Successful : BaseCommand
{
    public override void Process(CommandTemplate command, string ipAddress)
    {
        string result = new CommandTemplate()
        {
            TypeCommandStr = "Successful",
        }.ToString();
    }
}
