using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
	public class ConductingEquipment : Equipment
	{		
			
		public ConductingEquipment(long globalId) : base(globalId) 
		{
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
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
			return base.HasProperty(property);
		}
		 
		public override void GetProperty(Property prop)
		{
			base.GetProperty(prop);
		}

		public override void SetProperty(Property property)
		{
			base.SetProperty(property);
		}	

		#endregion IAccess implementation

		#region IReference implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			base.GetReferences(references, refType);
		}

		#endregion IReference implementation
	}
}
