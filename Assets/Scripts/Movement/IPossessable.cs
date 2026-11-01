using Ignite.Movement.States.Controller;
using UnityEngine;

namespace Ignite
{
    public interface IPossessable
    {
        public ILimbController Controller
        { get; }
    }
}
