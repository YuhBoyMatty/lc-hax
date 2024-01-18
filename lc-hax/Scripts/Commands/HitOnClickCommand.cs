using Hax;

[Command("/hitclick")]
public class HitOnClickCommand : ICommand {
    public void Execute(StringArray _) {
        Setting.EnableHitOnLeftClick = !Setting.EnableHitOnLeftClick;
        Chat.Print($"Hitclick: {(Setting.EnableHitOnLeftClick ? "Enabled" : "Disabled")}");
    }
}
