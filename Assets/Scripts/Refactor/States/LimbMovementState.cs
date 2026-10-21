using UnityEngine;

namespace Ignite
{
    public abstract class LimbMovementState
    {
        protected LimbStateController Context
        { get; private set; }

        public LimbMovementState(LimbStateController context)
        {
            Context = context;
        }
    }
}
