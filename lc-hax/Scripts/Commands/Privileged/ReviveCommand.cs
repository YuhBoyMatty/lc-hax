using System.Threading;
using System.Threading.Tasks;

[PrivilegedCommand("revive")]
class ReviveCommand : ICommand {
    public async Task Execute(string[] args, CancellationToken cancellationToken) => Helper.StartOfRound?.Debug_ReviveAllPlayersServerRpc();
}
