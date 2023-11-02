namespace XpAllocator
{
    internal interface ITrait
    {
        int Weight { get; }
        double EffectiveWeight { get; }
        long CurrentXp { get; }

        long RaiseCost();
        RaiseAttempt Raise();
        double AllocationWeight();
    }
}