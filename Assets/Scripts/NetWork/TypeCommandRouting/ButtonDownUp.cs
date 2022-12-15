public class ButtonDownUp : BaseCommand
{
    public override string Start(CommandTemplate command, string ipAddress)
    {
        ButtonNetWork buttonNetWork = command.GetJsonBody<ButtonNetWork>();
        UIDebug.Log($"{buttonNetWork.Name} is {buttonNetWork.IsClick}");
        return "";
    }
}
