using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SynchronousMachine : RotatingMachine
    {
        public SynchronousMachine(long globalId) : base(globalId)
        {
        }
        private float aVRToManualLag;
        private float aVRToManualLead;
        private float baseQ;
        private float condenserP;
        private float coolantCondition;
        private CoolantType coolantType;
        private float manualToAVR;
        private SynchronousMachineOperatingMode operatingMode;
        private SynchronousGeneratorType synchronousGeneratorType;
        private SynchronousMachineType type;
        private long reactiveCapabilityCurve;

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				SynchronousMachine x = obj as SynchronousMachine;
				return ((x.aVRToManualLag == aVRToManualLag) &&
						(x.aVRToManualLead == aVRToManualLead) &&
						(x.baseQ == baseQ) &&
						(x.condenserP == condenserP) &&
						(x.coolantCondition == coolantCondition) &&
						(x.coolantType == coolantType) &&
						(x.manualToAVR == manualToAVR) &&
						(x.operatingMode == operatingMode) &&
						(x.synchronousGeneratorType == synchronousGeneratorType) &&
						(x.type == type) &&
						(x.reactiveCapabilityCurve == reactiveCapabilityCurve));
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLAG:
				case ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLEAD:
				case ModelCode.SYNCHRONOUSMACHINE_BASEQ:
				case ModelCode.SYNCHRONOUSMACHINE_CONDENSERP:
				case ModelCode.SYNCHRONOUSMACHINE_COOLANTCOND:
				case ModelCode.SYNCHRONOUSMACHINE_COOLANTTYPE:
				case ModelCode.SYNCHRONOUSMACHINE_MANUALTOAVR:
				case ModelCode.SYNCHRONOUSMACHINE_OPMODE:
				case ModelCode.SYNCHRONOUSMACHINE_GENTYPE:
				case ModelCode.SYNCHRONOUSMACHINE_TYPE:
				case ModelCode.SYNCHRONOUSMACHINE_REACURVE:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLAG:
					property.SetValue(aVRToManualLag);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLEAD:
					property.SetValue(aVRToManualLead);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_BASEQ:
					property.SetValue(baseQ);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_CONDENSERP:
					property.SetValue(condenserP);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_COOLANTCOND:
					property.SetValue(coolantCondition);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_COOLANTTYPE:
					property.SetValue((short)coolantType);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_MANUALTOAVR:
					property.SetValue(manualToAVR);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_OPMODE:
					property.SetValue((short)operatingMode);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_GENTYPE:
					property.SetValue((short)synchronousGeneratorType);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_TYPE:
					property.SetValue((short)type);
					break;
				case ModelCode.SYNCHRONOUSMACHINE_REACURVE:
					property.SetValue(reactiveCapabilityCurve);
					break;

				default:
					base.GetProperty(property);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLAG:
					aVRToManualLag = property.AsFloat();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLEAD:
					aVRToManualLead = property.AsFloat();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_BASEQ:
					baseQ = property.AsFloat();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_CONDENSERP:
					condenserP = property.AsFloat();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_COOLANTCOND:
					coolantCondition = property.AsFloat();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_COOLANTTYPE:
					coolantType = (CoolantType)property.AsEnum();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_MANUALTOAVR:
					manualToAVR = property.AsFloat();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_OPMODE:
					operatingMode = (SynchronousMachineOperatingMode)property.AsEnum();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_GENTYPE:
					synchronousGeneratorType = (SynchronousGeneratorType)property.AsEnum();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_TYPE:
					type = (SynchronousMachineType)property.AsEnum();
					break;
				case ModelCode.SYNCHRONOUSMACHINE_REACURVE:
					reactiveCapabilityCurve = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (reactiveCapabilityCurve != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
				references[ModelCode.SYNCHRONOUSMACHINE_REACURVE] = new List<long>() { reactiveCapabilityCurve };
			base.GetReferences(references, refType);
		}

		#endregion IReference implementation	

	}
}
