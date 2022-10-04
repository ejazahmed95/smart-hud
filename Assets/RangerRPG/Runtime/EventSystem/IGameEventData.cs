namespace RangerRPG.EventSystem {
    public interface IGameEventData {
    }

    public struct EmptyGameEventData : IGameEventData {
    }

    public struct ItemAcquireEventData: IGameEventData {
        public ItemData ItemData;
    }
}