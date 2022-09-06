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
    public class RegulatingCondEq : ConductingEquipment
    {
        public RegulatingCondEq(long globalId) : base(globalId)
        {
        }
		private List<long> controls = new List<long>();
		private long regulatingControl = 0;
		public override bool Equals(object obj)
		{
			RegulatingCondEq temp = obj as RegulatingCondEq;
			if (base.Equals(obj) && regulatingControl == temp.regulatingControl && CompareHelper.CompareLists(controls, temp.controls))
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
				case ModelCode.REGULATINGCONDEQ_CONTROLS:
				case ModelCode.REGULATINGCONDEQ_REGULATINGCONT:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.REGULATINGCONDEQ_CONTROLS:
					property.SetValue(controls);
					break;
				case ModelCode.REGULATINGCONDEQ_REGULATINGCONT:
					property.SetValue(regulatingControl);
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
				case ModelCode.REGULATINGCONDEQ_REGULATINGCONT:
					regulatingControl = property.AsReference();
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
				return controls.Count != 0 || base.IsReferenced;
            }
		}

        public override void AddReference(ModelCode referenceId, long globalId)
        {
			switch(referenceId)
			{ 
				case ModelCode.CONTROL_REGULATINGCONDEQ:
					controls.Add(globalId); 
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
                case ModelCode.CONTROL_REGULATINGCONDEQ:
					if(controls.Contains(globalId))
						controls.Remove(globalId);
					break;
                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (regulatingControl != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
				references[ModelCode.REGULATINGCONDEQ_REGULATINGCONT] = new List<long>() { regulatingControl };
			if (controls != null && controls.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
				references[ModelCode.REGULATINGCONDEQ_CONTROLS] = controls.GetRange(0, controls.Count);
			base.GetReferences(references, refType);
		}

		#endregion IReference implementation	
	}
}
