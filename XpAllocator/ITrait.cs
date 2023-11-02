namespace XpAllocator
{
    internal interface ITrait
    {
        int Weight { get; }
        double EffectiveWeight { get; }

        long RaiseCost();
        RaiseAttempt Raise();
        bool CanBeRaised();
        double AllocationWeight();
    }
}