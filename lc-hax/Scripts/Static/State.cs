static class State {
    internal static char CommandPrefix { get; set; } = '/';
    internal static int ShovelHitForce { get; set; } = 1;
    internal static bool DisconnectedVoluntarily { get; set; }
    internal static ConnectedLobby? ConnectedLobby { get; set; }
}
