using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

[Command("suit")]
class SuitCommand : ICommand {
    internal static Dictionary<string, Unlockable> SuitUnlockables =>
        Enum.GetValues(typeof(Unlockable))
            .Cast<Unlockable>()
            .Where(u => u.ToString().EndsWith("_SUIT"))
            .ToDictionary(suit => suit.ToString().Replace("_SUIT", "").ToLower(), suit => suit);

    public async Task Execute(string[] args, CancellationToken cancellationToken) {
        if (args[0] is not string suit) {
            Chat.Print("Usage: suit <suit>");
            return;
        }

        if (!suit.FuzzyMatch(SuitCommand.SuitUnlockables.Keys, out string key)) {
            Chat.Print("Suit is not found!");
            return;
        }

        Unlockable selectedSuit = SuitCommand.SuitUnlockables[key];
        Helper.BuyUnlockable(selectedSuit);

        Helper
            .FindObjects<UnlockableSuit>()
            .First(suit => selectedSuit.Is(suit.suitID))?
            .SwitchSuitToThis(Helper.LocalPlayer);

        Chat.Print($"Wearing {string.Join(" ", selectedSuit.ToString().Split('_').Select(s => s.ToLower())).ToTitleCase()}!");
    }
}
