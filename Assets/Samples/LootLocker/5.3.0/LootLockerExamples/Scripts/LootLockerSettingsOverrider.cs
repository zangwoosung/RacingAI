using LootLocker.Requests;

namespace LootLocker {
public static class LootLockerSettingsOverrider
{
    public static void OverrideSettings()
    {
            
        LootLockerSDKManager.Init("dev_a51c404c18604088a883973122a3cbb3", "0.0.0.1", "5fd3u4li");
            
            LootLocker.LootLockerConfig.current.currentDebugLevel = LootLocker.LootLockerConfig.DebugLevel.All;
    }
}
}
