using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class StaticVarCompensator : RegulatingCondEq
    {
        public StaticVarCompensator(long globalId) : base(globalId)
        {
        }
        private float capacitiveRating;
        private float inductiveRating;
        private float slope;
        private SVCControlMode sVCControlMode;
        private float voltageSetPoint;

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				StaticVarCompensator x = obj as StaticVarCompensator;
				return ((x.capacitiveRating == capacitiveRating) &&
						(x.inductiveRating == inductiveRating) &&
						(x.slope == slope) &&
						(x.sVCControlMode == sVCControlMode) &&
						(x.voltageSetPoint == voltageSetPoint));
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
				case ModelCode.STATICVARCOMPENSATOR_CAPACITIVERAT:
				case ModelCode.STATICVARCOMPENSATOR_INDUCTIVERAT:
				case ModelCode.STATICVARCOMPENSATOR_SLOPE:
				case ModelCode.STATICVARCOMPENSATOR_SVCCONTROLMODE:
				case ModelCode.STATICVARCOMPENSATOR_VOLTAGESETPNT:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.STATICVARCOMPENSATOR_CAPACITIVERAT:
					property.SetValue(capacitiveRating);
					break;
				case ModelCode.STATICVARCOMPENSATOR_INDUCTIVERAT:
					property.SetValue(inductiveRating);
					break;
				case ModelCode.STATICVARCOMPENSATOR_SLOPE:
					property.SetValue(slope);
					break;
				case ModelCode.STATICVARCOMPENSATOR_SVCCONTROLMODE:
					property.SetValue((short)sVCControlMode);
					break;
				case ModelCode.STATICVARCOMPENSATOR_VOLTAGESETPNT:
					property.SetValue(voltageSetPoint);
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
				case ModelCode.STATICVARCOMPENSATOR_CAPACITIVERAT:
					capacitiveRating = property.AsFloat();
					break;
				case ModelCode.STATICVARCOMPENSATOR_INDUCTIVERAT:
					inductiveRating = property.AsFloat();
					break;
				case ModelCode.STATICVARCOMPENSATOR_SLOPE:
					slope = property.AsFloat();
					break;
				case ModelCode.STATICVARCOMPENSATOR_SVCCONTROLMODE:
					sVCControlMode = (SVCControlMode)property.AsEnum();
					break;
				case ModelCode.STATICVARCOMPENSATOR_VOLTAGESETPNT:
					voltageSetPoint = property.AsFloat();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
