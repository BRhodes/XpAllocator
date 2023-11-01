namespace XpAllocator
{
    internal interface ITrait
    {
        double Weight { get; set; }
        double EffectiveWeight { get; }

        long RaiseCost();
        RaiseAttempt Raise();
        bool CanBeRaised();
        double AllocationWeight();
    }
}