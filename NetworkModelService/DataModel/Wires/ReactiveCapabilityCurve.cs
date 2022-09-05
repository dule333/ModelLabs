using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.IES_Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ReactiveCapabilityCurve : Curve
    {
        public ReactiveCapabilityCurve(long globalId) : base(globalId)
        {
        }
        private float coolantTemperature;
        private float hydrogenPressure;
		private List<long> synchronousMachines;

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				ReactiveCapabilityCurve x = (ReactiveCapabilityCurve)obj;
				return ((x.coolantTemperature == coolantTemperature) &&
						(x.hydrogenPressure == hydrogenPressure) &&
						CompareHelper.CompareLists(x.synchronousMachines, synchronousMachines) && base.Equals(obj));
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
				case ModelCode.REACTIVECAPABILITYCURVE_COOLANTTEMP:
				case ModelCode.REACTIVECAPABILITYCURVE_HYDROGENP:
				case ModelCode.REACTIVECAPABILITYCURVE_SYNCHS:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{

				case ModelCode.REACTIVECAPABILITYCURVE_COOLANTTEMP:
					property.SetValue(coolantTemperature);
					break;
				case ModelCode.REACTIVECAPABILITYCURVE_HYDROGENP:
					property.SetValue(hydrogenPressure);
					break;
				case ModelCode.REACTIVECAPABILITYCURVE_SYNCHS:
					property.SetValue(synchronousMachines);
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
				case ModelCode.REACTIVECAPABILITYCURVE_COOLANTTEMP:
					coolantTemperature = property.AsFloat();
					break;
				case ModelCode.REACTIVECAPABILITYCURVE_HYDROGENP:
					hydrogenPressure = property.AsFloat();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return synchronousMachines.Count != 0 || base.IsReferenced;
			}
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.SYNCHRONOUSMACHINE_REACURVE:
					synchronousMachines.Add(globalId);
					break;
				default:
					base.AddReference(referenceId, globalId);
					break;
			}
		}

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.SYNCHRONOUSMACHINE_REACURVE:
					if (synchronousMachines.Contains(globalId))
						synchronousMachines.Remove(globalId);
					break;
				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (synchronousMachines != null && synchronousMachines.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
				references[ModelCode.REACTIVECAPABILITYCURVE_SYNCHS] = synchronousMachines.GetRange(0, synchronousMachines.Count);
			base.GetReferences(references, refType);
		}

		#endregion IReference implementation	
	}
}
