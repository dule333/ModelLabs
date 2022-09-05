using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Equipment : PowerSystemResource
	{		
		private bool isAggregate;
		private bool isNormallyInService;
						
		public Equipment(long globalId) : base(globalId) 
		{
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Equipment x = obj as Equipment;
				return ((x.isAggregate == isAggregate) &&
						(x.isNormallyInService == isNormallyInService));
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
				case ModelCode.EQUIPMENT_AGGREGATE:
				case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.EQUIPMENT_AGGREGATE:
					property.SetValue(isAggregate);
					break;

				case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
					property.SetValue(isNormallyInService);
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
				case ModelCode.EQUIPMENT_AGGREGATE:
					isAggregate = property.AsBool();
					break;

				case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
					isNormallyInService = property.AsBool();
					break;
			
				default:
					base.SetProperty(property);
					break;
			}
		}		

		#endregion IAccess implementation
	}
}
