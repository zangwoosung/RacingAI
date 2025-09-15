using LootLocker.Requests;

namespace LootLocker {
public static class LootLockerSettingsOverrider
{
    public static void OverrideSettings()
    {
        LootLockerSDKManager.Init("dev_68013677c9be49fb86bda9f4df23bbb7", "0.0.0.1", "itfcoqvh");
        LootLocker.LootLockerConfig.current.currentDebugLevel = LootLocker.LootLockerConfig.DebugLevel.All;
    }
}
}
