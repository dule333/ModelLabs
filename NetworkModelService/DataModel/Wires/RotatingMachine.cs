using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RotatingMachine : RegulatingCondEq
    {
        public RotatingMachine(long globalId) : base(globalId)
        {
        }

		private float damping;
		private float inertia;
		private float ratedS;
		private float saturationFactor;
		private float saturationFactor120;
		private float statorLeakageReactance;
		private float statorResistance;

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				RotatingMachine x = obj as RotatingMachine;
				return ((x.damping == damping) &&
						(x.inertia == inertia) &&
						(x.ratedS == ratedS) &&
						(x.saturationFactor == saturationFactor) &&
						(x.saturationFactor120 == saturationFactor120) &&
						(x.statorLeakageReactance == statorLeakageReactance) &&
						(x.statorResistance == statorResistance));
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
				case ModelCode.ROTATINGMACHINE_DAMPING:
				case ModelCode.ROTATINGMACHINE_INERTIA:
				case ModelCode.ROTATINGMACHINE_SATFACTOR:
				case ModelCode.ROTATINGMACHINE_SATFACTOR120:
				case ModelCode.ROTATINGMACHINE_RATEDS:
				case ModelCode.ROTATINGMACHINE_STATORLEAKAGERES:
				case ModelCode.ROTATINGMACHINE_STATORRES:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.ROTATINGMACHINE_DAMPING:
					property.SetValue(damping);
					break;
				case ModelCode.ROTATINGMACHINE_INERTIA:
					property.SetValue(inertia);
					break;
				case ModelCode.ROTATINGMACHINE_SATFACTOR:
					property.SetValue(saturationFactor);
					break;
				case ModelCode.ROTATINGMACHINE_SATFACTOR120:
					property.SetValue(saturationFactor120);
					break;
				case ModelCode.ROTATINGMACHINE_RATEDS:
					property.SetValue(ratedS);
					break;
				case ModelCode.ROTATINGMACHINE_STATORLEAKAGERES:
					property.SetValue(statorLeakageReactance);
					break;
				case ModelCode.ROTATINGMACHINE_STATORRES:
					property.SetValue(statorResistance);
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
				case ModelCode.ROTATINGMACHINE_DAMPING:
					damping = property.AsFloat();
					break;
				case ModelCode.ROTATINGMACHINE_INERTIA:
					inertia = property.AsFloat();
					break;
				case ModelCode.ROTATINGMACHINE_SATFACTOR:
					saturationFactor = property.AsFloat();
					break;
				case ModelCode.ROTATINGMACHINE_SATFACTOR120:
					saturationFactor120 = property.AsFloat();
					break;
				case ModelCode.ROTATINGMACHINE_RATEDS:
					ratedS = property.AsFloat();
					break;
				case ModelCode.ROTATINGMACHINE_STATORLEAKAGERES:
					statorLeakageReactance = property.AsFloat();
					break;
				case ModelCode.ROTATINGMACHINE_STATORRES:
					statorResistance = property.AsFloat();
					break;


				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
