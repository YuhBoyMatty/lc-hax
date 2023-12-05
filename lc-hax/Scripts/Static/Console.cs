using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace Hax;

public static class Console {
    static Dictionary<string, ICommand> Commands { get; } = new() {
        { "/god", new GodCommand() },
        { "/shovel", new ShovelCommand() },
        { "/tp", new TeleportCommand() },
        { "/money", new MoneyCommand() },
        { "/unlock", new UnlockCommand() },
        { "/kill", new KillCommand() },
        { "/players", new PlayersCommand() },
        { "/home", new HomeCommand() },
        { "/end", new EndCommand() },
        { "/explode", new ExplodeCommand() },
        { "/noise", new NoiseCommand() },
        { "/random", new RandomCommand() }
    };

    static HUDManager? HUDManager => HaxObjects.Instance?.HUDManager.Object;

    static Reflector? HUDManagerReflector => Console.HUDManager == null ? null : Reflector.Target(Console.HUDManager);

    public static void Open() {
        Console.HUDManager?.chatTextField.Select();
        Console.HUDManager?.PingHUDElement(Console.HUDManager.Chat, 0.1f, 1.0f, 1.0f);
    }

    public static void Print(string name, string? message) {
        if (string.IsNullOrWhiteSpace(message)) return;
        if (Console.HUDManager == null) return;

        _ = Console.HUDManagerReflector?.InvokeInternalMethod("AddChatMessage", message, name);

        if (Console.HUDManager.localPlayer.isTypingChat) {
            Console.HUDManager.localPlayer.isTypingChat = false;
            Console.HUDManager.typingIndicator.enabled = false;
            Console.HUDManager.chatTextField.text = "";
            Console.HUDManager.PingHUDElement(Console.HUDManager.Chat, 1.0f, 1.0f, 0.2f);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public static void ExecuteCommand(string command) {
        Console.Print("USER", command);
        Console.ExecuteCommand(command.Split(' '));
    }

    public static void ExecuteCommand(string[] args) {
        if (args.Length < 1) {
            Console.Print("SYSTEM", "Usage: /<command> <args>");
            return;
        }

        if (!Console.Commands.ContainsKey(args[0])) {
            Console.Print("SYSTEM", "Command not found!");
            return;
        }

        Console.Commands[args[0]].Execute(args.Skip(1).ToArray());
    }
}


