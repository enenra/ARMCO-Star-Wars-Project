using System;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRageMath;

namespace EquinoxStuff
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_MedicalRoom), false, "rebel_spawn_base")]
    public class MedicalSpinner : EnabledSpinner
    {
        // Spin SubpartName at 10 degrees per second
        public MedicalSpinner() : base("spinners", 10 * (float) Math.PI / 180.0f * MyEngineConstants.UPDATE_STEP_SIZE_IN_SECONDS)
        {
        }
    }

     public abstract class EnabledSpinner : MyGameLogicComponent
    {
        private readonly float _idleSpeed;
        private readonly string _subpart;

        protected EnabledSpinner(string subpart, float idleSpeed)
        {
            _subpart = subpart;
            _idleSpeed = idleSpeed;
        }

        public override string ComponentTypeDebugString => nameof(EnabledSpinner);

        public override void OnAddedToContainer()
        {
            if (MyAPIGateway.Utilities.IsDedicated)
                return;
            base.OnAddedToContainer();
            var block = Entity as IMyFunctionalBlock;
            if (block != null)
                block.EnabledChanged += EnabledChanged;
        }

        public override void OnBeforeRemovedFromContainer()
        {
            if (MyAPIGateway.Utilities.IsDedicated)
                return;
            var block = Entity as IMyFunctionalBlock;
            if (block != null)
                block.EnabledChanged -= EnabledChanged;
            base.OnBeforeRemovedFromContainer();
        }

        private void EnabledChanged(IMyTerminalBlock obj)
        {
            NeedsUpdate = ((IMyFunctionalBlock) obj).Enabled ? MyEntityUpdateEnum.EACH_FRAME : MyEntityUpdateEnum.NONE;
        }

        private IMyModel _activeModel;
        private MyEntitySubpart _activeSubpart;

        public override void UpdateAfterSimulation()
        {
            if (_activeModel != Entity?.Model)
            {
                _activeModel = Entity?.Model;
                Entity?.TryGetSubpart(_subpart, out _activeSubpart);
            }

            if (_activeSubpart?.PositionComp == null) return;

            var m = _activeSubpart.PositionComp.LocalMatrix;
            m *= Matrix.CreateRotationY(_idleSpeed);
            _activeSubpart.PositionComp.LocalMatrix = m;
        }
    }
}