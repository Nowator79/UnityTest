using Scripts;

public class Successful : BaseCommand
{
    public override string Start(CommandTemplate command)
    {
        string result = new CommandTemplate()
        {
            TypeCommandStr = "Successful",
        }.ToString();
        return result;
    }
}
