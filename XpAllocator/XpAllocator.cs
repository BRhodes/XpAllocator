using System;

namespace XpAllocator
{
    internal class XpAllocator
    {
        PlayerConfiguration _config { get; set; }
        TraitManager _traitManager { get; set; }
        RaiseAttempt _lastRaiseAttempt { get; set; } = null;

        public bool _inErrorState { get; private set; }
        public bool _isRunning { get; private set; }
        private int _skillPointCount { get; set; }
        private long _expectedRaiseCost { get; set; }
        public bool _isRaiseCostCalculated { get; private set; }
        public DateTime _runTimeout { get; private set; }

        public XpAllocator(PlayerConfiguration config)
        {
            _config = config;
            Reset();
        }

        internal void AllocateXp(bool skillRaiseSuccess = false)
        {
            if (skillRaiseSuccess) _lastRaiseAttempt = null;

            if (_lastRaiseAttempt != null && _runTimeout > DateTime.UtcNow)
                Util.WriteToChat($"Failed to raise {_lastRaiseAttempt.Trait} by spending {_lastRaiseAttempt.XpAllocated}.");

            if (_inErrorState || !_config.Enabled || _lastRaiseAttempt != null) return;

            // Recalculate Skills & Weights on skillpoint change
            if (_skillPointCount != Globals.Core.CharacterFilter.SkillPoints)
            {
                _skillPointCount = Globals.Core.CharacterFilter.SkillPoints;
                _traitManager = new TraitManager(_config);
                _isRaiseCostCalculated = false;
            }

            // Calculate the expected raise cost
            if (!_isRaiseCostCalculated)
            {
                _expectedRaiseCost = _traitManager.ExpectedRaiseCost();
                _isRaiseCostCalculated = true;
            }

            // Attempt to raise a skill if we can
            if (Globals.Core.CharacterFilter.UnassignedXP - ReservedXp() > _expectedRaiseCost)
            {
                _lastRaiseAttempt = _traitManager.RaiseTrait();
                _isRaiseCostCalculated = false;
            }

            if (_lastRaiseAttempt != null)
                _runTimeout = DateTime.UtcNow.AddSeconds(2);
        }

        public long ReservedXp()
        {
            var reserve = _config.Reserve;
            var reservePercent = (long)(_config.ReservePercent * Globals.Core.CharacterFilter.TotalXP);

            reserve = Math.Max(reserve, reservePercent);
            reserve = Math.Min(reserve, _config.ReserveMax);

            return reserve;
        }

        void Reset()
        {
            _isRunning = false;
            _inErrorState = false;
            _traitManager = new TraitManager(_config);
        }

        internal string Weights()
        {
            return _traitManager.DumpWeights();
        }
    }
}
