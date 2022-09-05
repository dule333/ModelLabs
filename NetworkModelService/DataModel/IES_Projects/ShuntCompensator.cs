using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class ShuntCompensator : RegulatingCondEq
    {
        public ShuntCompensator(long globalId) : base(globalId)
        {
        }

        private float aVRDelay;
        private float b0PerSection;
        private float bPerSection;
        private float g0PerSection;
        private float gPerSection;
        private WindingConnection grounded;
        private int maximumSections;
        private float nomU;
        private int normalSections;
        private PhaseShuntConnectionKind phaseConnection;
        private int switchOnCount;
        private DateTime switchOnDate;
        private float voltageSensitivity;

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				ShuntCompensator x = obj as ShuntCompensator;
				return ((x.aVRDelay == aVRDelay) &&
						(x.b0PerSection == b0PerSection) &&
						(x.bPerSection == bPerSection) &&
						(x.g0PerSection == g0PerSection) &&
						(x.gPerSection == gPerSection) &&
						(x.grounded == grounded) &&
						(x.maximumSections == maximumSections) &&
						(x.nomU == nomU) &&
						(x.normalSections == normalSections) &&
						(x.phaseConnection == phaseConnection) &&
						(x.switchOnCount == switchOnCount) &&
						(x.switchOnDate == switchOnDate) &&
						(x.voltageSensitivity == voltageSensitivity));
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
                case ModelCode.SHUNTCOMPENSATOR_AVRDELAY:                    
                case ModelCode.SHUNTCOMPENSATOR_B0PERSECTION:                    
                case ModelCode.SHUNTCOMPENSATOR_BPERSECTION:                    
                case ModelCode.SHUNTCOMPENSATOR_G0PERSECTION:                    
                case ModelCode.SHUNTCOMPENSATOR_GPERSECTION:                    
                case ModelCode.SHUNTCOMPENSATOR_GROUNDED:                    
                case ModelCode.SHUNTCOMPENSATOR_MAXIMUMSECTIONS:                    
                case ModelCode.SHUNTCOMPENSATOR_NOMU:                    
                case ModelCode.SHUNTCOMPENSATOR_NORMALSECTIONS:                    
                case ModelCode.SHUNTCOMPENSATOR_PHASECONNECTION:                    
                case ModelCode.SHUNTCOMPENSATOR_SWITCHONCOUNT:                    
                case ModelCode.SHUNTCOMPENSATOR_SWITCHONDATE:                    
                case ModelCode.SHUNTCOMPENSATOR_VOLTAGESENS:
					return true;
                default:
					return base.HasProperty(property);
            }
        }

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.SHUNTCOMPENSATOR_AVRDELAY:
					property.SetValue(aVRDelay);
					break;
				case ModelCode.SHUNTCOMPENSATOR_B0PERSECTION:
					property.SetValue(b0PerSection);
					break;
				case ModelCode.SHUNTCOMPENSATOR_BPERSECTION:
					property.SetValue(bPerSection);
					break;
				case ModelCode.SHUNTCOMPENSATOR_G0PERSECTION:
					property.SetValue(g0PerSection);
					break;
				case ModelCode.SHUNTCOMPENSATOR_GPERSECTION:
					property.SetValue(gPerSection);
					break;
				case ModelCode.SHUNTCOMPENSATOR_GROUNDED:
					property.SetValue((short)grounded);
					break;
				case ModelCode.SHUNTCOMPENSATOR_MAXIMUMSECTIONS:
					property.SetValue(maximumSections);
					break;
				case ModelCode.SHUNTCOMPENSATOR_NOMU:
					property.SetValue(nomU);
					break;
				case ModelCode.SHUNTCOMPENSATOR_NORMALSECTIONS:
					property.SetValue(normalSections);
					break;
				case ModelCode.SHUNTCOMPENSATOR_PHASECONNECTION:
					property.SetValue((short)phaseConnection);
					break;
				case ModelCode.SHUNTCOMPENSATOR_SWITCHONCOUNT:
					property.SetValue(switchOnCount);
					break;
				case ModelCode.SHUNTCOMPENSATOR_SWITCHONDATE:
					property.SetValue(switchOnDate);
					break;
				case ModelCode.SHUNTCOMPENSATOR_VOLTAGESENS:
					property.SetValue(voltageSensitivity);
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
				case ModelCode.SHUNTCOMPENSATOR_AVRDELAY:
					aVRDelay = property.AsFloat();
					break;
				case ModelCode.SHUNTCOMPENSATOR_B0PERSECTION:
					b0PerSection = property.AsFloat();
					break;
				case ModelCode.SHUNTCOMPENSATOR_BPERSECTION:
					bPerSection = property.AsFloat();
					break;
				case ModelCode.SHUNTCOMPENSATOR_G0PERSECTION:
					g0PerSection = property.AsFloat();
					break;
				case ModelCode.SHUNTCOMPENSATOR_GPERSECTION:
					gPerSection = property.AsFloat();
					break;
				case ModelCode.SHUNTCOMPENSATOR_GROUNDED:
					grounded = (WindingConnection)property.AsEnum();
					break;
				case ModelCode.SHUNTCOMPENSATOR_MAXIMUMSECTIONS:
					maximumSections = property.AsInt();
					break;
				case ModelCode.SHUNTCOMPENSATOR_NOMU:
					nomU = property.AsFloat();
					break;
				case ModelCode.SHUNTCOMPENSATOR_NORMALSECTIONS:
					normalSections = property.AsInt();
					break;
				case ModelCode.SHUNTCOMPENSATOR_PHASECONNECTION:
					phaseConnection = (PhaseShuntConnectionKind)property.AsEnum();
					break;
				case ModelCode.SHUNTCOMPENSATOR_SWITCHONCOUNT:
					switchOnCount = property.AsInt();
					break;
				case ModelCode.SHUNTCOMPENSATOR_SWITCHONDATE:
					switchOnDate = property.AsDateTime();
					break;
				case ModelCode.SHUNTCOMPENSATOR_VOLTAGESENS:
					voltageSensitivity = property.AsFloat();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

	}
}
