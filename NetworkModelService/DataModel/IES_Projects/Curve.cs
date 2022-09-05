using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class Curve : IdentifiedObject
    {
        public Curve(long globalId) : base(globalId)
        {
        }
        private CurveStyle curveStyle;
        private UnitMultiplier xMultiplier;
        private UnitSymbol xUnit;
        private UnitMultiplier y1Multiplier;
        private UnitSymbol y1Unit;
        private UnitMultiplier y2Multiplier;
        private UnitSymbol y2Unit;
        private UnitMultiplier y3Multiplier;
        private UnitSymbol y3Unit;

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Curve x = (Curve)obj;
				return (x.curveStyle == curveStyle &&
						x.xMultiplier == xMultiplier &&
						x.xUnit == xUnit &&
						x.y1Multiplier == y1Multiplier &&
						x.y1Unit == y1Unit &&
						x.y2Multiplier == y2Multiplier &&
						x.y2Unit == y2Unit &&
						x.y3Multiplier == y3Multiplier &&
						x.y3Unit == y3Unit && base.Equals(obj));
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
				case ModelCode.CURVE_CURVESTYLE:
				case ModelCode.CURVE_XMULT:
				case ModelCode.CURVE_XUNIT:
				case ModelCode.CURVE_Y1MULT:
				case ModelCode.CURVE_Y1UNIT:
				case ModelCode.CURVE_Y2MULT:
				case ModelCode.CURVE_Y2UNIT:
				case ModelCode.CURVE_Y3MULT:
				case ModelCode.CURVE_Y3UNIT:
					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.CURVE_CURVESTYLE:
					property.SetValue((short)curveStyle);
					break;
				case ModelCode.CURVE_XMULT:
					property.SetValue((short)xMultiplier);
					break;
				case ModelCode.CURVE_XUNIT:
					property.SetValue((short)xUnit);
					break;
				case ModelCode.CURVE_Y1MULT:
					property.SetValue((short)y1Multiplier);
					break;
				case ModelCode.CURVE_Y1UNIT:
					property.SetValue((short)y1Unit);
					break;
				case ModelCode.CURVE_Y2MULT:
					property.SetValue((short)y2Multiplier);
					break;
				case ModelCode.CURVE_Y2UNIT:
					property.SetValue((short)y2Unit);
					break;
				case ModelCode.CURVE_Y3MULT:
					property.SetValue((short)y3Multiplier);
					break;
				case ModelCode.CURVE_Y3UNIT:
					property.SetValue((short)y3Unit);
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
				case ModelCode.CURVE_CURVESTYLE:
					curveStyle = (CurveStyle)property.AsEnum();
					break;
				case ModelCode.CURVE_XMULT:
					xMultiplier = (UnitMultiplier)property.AsEnum();
					break;
				case ModelCode.CURVE_XUNIT:
					xUnit = (UnitSymbol)property.AsEnum();
					break;
				case ModelCode.CURVE_Y1MULT:
					y1Multiplier = (UnitMultiplier)property.AsEnum();
					break;
				case ModelCode.CURVE_Y1UNIT:
					y1Unit = (UnitSymbol)property.AsEnum();
					break;
				case ModelCode.CURVE_Y2MULT:
					y2Multiplier = (UnitMultiplier)property.AsEnum();
					break;
				case ModelCode.CURVE_Y2UNIT:
					y2Unit = (UnitSymbol)property.AsEnum();
					break;
				case ModelCode.CURVE_Y3MULT:
                    y3Multiplier = (UnitMultiplier)property.AsEnum();
					break;
				case ModelCode.CURVE_Y3UNIT:
					y3Unit = (UnitSymbol)property.AsEnum();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
