using System;
using UtilityBelt.Common.Messages.Events;

namespace XpAllocator
{
    internal class XpAllocator
    {
        PlayerConfiguration _config { get; set; }
        public TraitManager _traitManager { get; set; }
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
            _traitManager = new TraitManager(_config);
            Reset();
        }

        internal void AllocateXp(bool skillRaiseSuccess = false)
        {
            if (_config == null) return;

            if (skillRaiseSuccess) _lastRaiseAttempt = null;

            if (_lastRaiseAttempt != null && _runTimeout > DateTime.UtcNow)
                Util.WriteToChat($"Failed to raise {_lastRaiseAttempt.Trait} by spending {_lastRaiseAttempt.XpAllocated}.");

            if (_inErrorState || !_config.Enabled || _lastRaiseAttempt != null) return;

            // Recalculate Skills & Weights on skillpoint change
            if (_skillPointCount != Globals.Core.CharacterFilter.SkillPoints)
            {
                _skillPointCount = Globals.Core.CharacterFilter.SkillPoints;
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

        private long ReservedXp()
        {
            var million = 1000000L;
            var reserve = _config.Reserve * million;
            var reservePercent = (long)(_config.ReservePercent/100.0 * Globals.Core.CharacterFilter.TotalXP);

            reserve = Math.Max(reserve, reservePercent);
            reserve = Math.Min(reserve, _config.ReserveMax * million);

            return reserve;
        }

        public void Reset()
        {
            _isRunning = false;
            _inErrorState = false;
            _isRaiseCostCalculated = false;
        }

        internal string Weights()
        {
            return _traitManager.DumpWeights();
        }
    }
}
