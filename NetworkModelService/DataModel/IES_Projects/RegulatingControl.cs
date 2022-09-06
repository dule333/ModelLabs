using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class RegulatingControl : PowerSystemResource
    {
        public RegulatingControl(long globalId) : base(globalId)
        {
        }
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
		private List<long> regulatingCondEqs = new List<long>();
		private long terminal = 0;

		public override bool Equals(object obj)
		{
			RegulatingControl other = obj as RegulatingControl;
			if (base.Equals(obj) && other.discrete == discrete && other.mode == mode && other.monitoredPhase == monitoredPhase && other.targetValue == targetValue
				&& other.targetRange == targetRange && other.terminal == terminal && CompareHelper.CompareLists(other.regulatingCondEqs, regulatingCondEqs))
			{
				return true;
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
				case ModelCode.REGULATINGCONTROL_CONDEQS:
				case ModelCode.REGULATINGCONTROL_MODE:
				case ModelCode.REGULATINGCONTROL_DISCRETE:
				case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
				case ModelCode.REGULATINGCONTROL_TARGETRANGE:
				case ModelCode.REGULATINGCONTROL_TARGETVALUE:
				case ModelCode.REGULATINGCONTROL_TERMINAL:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{

				case ModelCode.REGULATINGCONTROL_MODE:
					property.SetValue((short)mode);
					break;
				case ModelCode.REGULATINGCONTROL_DISCRETE:
					property.SetValue(discrete);
					break;
				case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
					property.SetValue((short)monitoredPhase);
					break;
				case ModelCode.REGULATINGCONTROL_TARGETRANGE:
					property.SetValue(targetRange);
					break;
				case ModelCode.REGULATINGCONTROL_TARGETVALUE:
					property.SetValue(targetValue);
					break;
				case ModelCode.REGULATINGCONTROL_TERMINAL:
					property.SetValue(terminal);
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
				case ModelCode.REGULATINGCONTROL_MODE:
					mode = (RegulatingControlModeKind)property.AsEnum();
					break;
				case ModelCode.REGULATINGCONTROL_DISCRETE:
					discrete = property.AsBool();
					break;
				case ModelCode.REGULATINGCONTROL_MONITOREDPHASE:
					monitoredPhase = (PhaseCode)property.AsEnum();
					break;
				case ModelCode.REGULATINGCONTROL_TARGETRANGE:
					targetRange = property.AsFloat();
					break;
				case ModelCode.REGULATINGCONTROL_TARGETVALUE:
					targetValue = property.AsFloat();
					break;
				case ModelCode.REGULATINGCONTROL_TERMINAL:
					terminal = property.AsReference();
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
				return regulatingCondEqs.Count != 0 || base.IsReferenced;
			}
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.REGULATINGCONDEQ_REGULATINGCONT:
					regulatingCondEqs.Add(globalId);
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
				case ModelCode.REGULATINGCONDEQ_REGULATINGCONT:
					if (regulatingCondEqs.Contains(globalId))
						regulatingCondEqs.Remove(globalId);
					break;
				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (terminal != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
				references[ModelCode.REGULATINGCONTROL_TERMINAL] = new List<long>() { terminal };
			if (regulatingCondEqs != null && regulatingCondEqs.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
				references[ModelCode.REGULATINGCONTROL_CONDEQS] = regulatingCondEqs.GetRange(0, regulatingCondEqs.Count);
			base.GetReferences(references, refType);
		}

		#endregion IReference implementation	
	}
}
