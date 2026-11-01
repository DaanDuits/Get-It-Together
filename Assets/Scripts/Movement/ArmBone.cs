using Ignite.Movement.States.Controller;
using UnityEngine;

namespace Ignite
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class ArmBone : MonoBehaviour, IPossessable
    {
        [SerializeField] private ArmController controller;

        public ILimbController Controller 
        { get => controller; }
    }
}
