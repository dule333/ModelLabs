using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Meas
{
    public class Control : IdentifiedObject
    {
        public Control(long globalId) : base(globalId)
        {
        }
        private long regulatingCondEq = 0;
        private bool operationInProgress;
        private DateTime timeStamp;
        private UnitMultiplier unitMultiplier;
        private UnitSymbol unitSymbol;

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Control temp = (Control)obj;
				return regulatingCondEq == temp.regulatingCondEq &&
					operationInProgress == temp.operationInProgress &&
					timeStamp == temp.timeStamp &&
					unitMultiplier == temp.unitMultiplier &&
					unitSymbol == temp.unitSymbol;
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
				case ModelCode.CONTROL_OPERATIONINPROGRESS:
				case ModelCode.CONTROL_REGULATINGCONDEQ:
				case ModelCode.CONTROL_TIMESTAMP:
				case ModelCode.CONTROL_UNITMULTIPLIER:
				case ModelCode.CONTROL_UNITSYMBOL:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.CONTROL_OPERATIONINPROGRESS:
					property.SetValue(operationInProgress);
					break;
				case ModelCode.CONTROL_REGULATINGCONDEQ:
					property.SetValue(regulatingCondEq);
					break;
				case ModelCode.CONTROL_TIMESTAMP:
					property.SetValue(timeStamp);
					break;
				case ModelCode.CONTROL_UNITMULTIPLIER:
					property.SetValue((short)unitMultiplier);
					break;
				case ModelCode.CONTROL_UNITSYMBOL:
					property.SetValue((short)unitSymbol);
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
				case ModelCode.CONTROL_OPERATIONINPROGRESS:
					operationInProgress = property.AsBool();
					break;
				case ModelCode.CONTROL_REGULATINGCONDEQ:
					regulatingCondEq = property.AsReference();
					break;
				case ModelCode.CONTROL_TIMESTAMP:
					timeStamp = property.AsDateTime();
					break;
				case ModelCode.CONTROL_UNITMULTIPLIER:
					unitMultiplier = (UnitMultiplier)property.AsEnum();
					break;
				case ModelCode.CONTROL_UNITSYMBOL:
					unitSymbol = (UnitSymbol)property.AsEnum();
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
			if (regulatingCondEq != 0 && (refType == TypeOfReference.Both || refType == TypeOfReference.Reference))
				references[ModelCode.CONTROL_REGULATINGCONDEQ] = new List<long>() { regulatingCondEq };
			base.GetReferences(references, refType);
		}
		#endregion IReference implementation		
	}
}
