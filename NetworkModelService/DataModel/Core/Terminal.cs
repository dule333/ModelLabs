using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using FTN.Common;



namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Terminal : IdentifiedObject
	{
		private bool connected;
		private int sequenceNumber;
		private PhaseCode phases;
		private List<long> regulatingControls;

        public Terminal(long globalId)
			: base(globalId)
		{
		}	


		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Terminal temp = (Terminal)obj;
				return connected == temp.connected && 
					sequenceNumber == temp.sequenceNumber && 
					phases == temp.phases && CompareHelper.CompareLists(regulatingControls, this.regulatingControls);
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
				case ModelCode.TERMINAL_CONNECTED:
				case ModelCode.TERMINAL_PHASES:
				case ModelCode.TERMINAL_SEQNUMBER:
				case ModelCode.TERMINAL_CONTROLS:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.TERMINAL_CONNECTED:
					property.SetValue(connected);
					break;
				case ModelCode.TERMINAL_PHASES:
					property.SetValue((short)phases);
					break;
				case ModelCode.TERMINAL_SEQNUMBER:
					property.SetValue(sequenceNumber);
					break;
				case ModelCode.TERMINAL_CONTROLS:
					property.SetValue(regulatingControls);
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
				case ModelCode.TERMINAL_CONNECTED:
					connected = property.AsBool();
                    break;
				case ModelCode.TERMINAL_PHASES:
					phases = (PhaseCode)property.AsEnum();
                    break;
				case ModelCode.TERMINAL_SEQNUMBER:
					sequenceNumber = property.AsInt();
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
				return regulatingControls.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (regulatingControls != null && regulatingControls.Count != 0 && (refType == TypeOfReference.Both || refType == TypeOfReference.Target))
				references[ModelCode.TERMINAL_CONTROLS] = regulatingControls.GetRange(0, regulatingControls.Count);
			base.GetReferences(references, refType);			
		}

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATINGCONTROL_TERMINAL:
					regulatingControls.Add(globalId);
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
                case ModelCode.REGULATINGCONTROL_TERMINAL:
					if(regulatingControls.Contains(globalId))
						regulatingControls.Remove(globalId);
                    break;
                default:
					base.RemoveReference(referenceId, globalId);
					break;
            }
        }

        #endregion IReference implementation		
    }
}
