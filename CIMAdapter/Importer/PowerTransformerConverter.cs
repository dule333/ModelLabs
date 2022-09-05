namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
        {
			if ((cimEquipment != null) && (rd != null))
			{
				PopulateIdentifiedObjectProperties(cimEquipment, rd);

				if (cimEquipment.NormallyInServiceHasValue)
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				if (cimEquipment.AggregateHasValue)
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PopulateEquipmentProperties(cimConductingEquipment, rd);
			}
		}

		public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if((cimTerminal != null) && (rd != null))
            {
				PopulateIdentifiedObjectProperties(cimTerminal, rd);

				if (cimTerminal.ConnectedHasValue)
					rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTED, cimTerminal.Connected));
				if (cimTerminal.PhasesHasValue)
					rd.AddProperty(new Property(ModelCode.TERMINAL_PHASES, (short)GetDMSPhaseCode(cimTerminal.Phases)));
				if (cimTerminal.SequenceNumberHasValue)
					rd.AddProperty(new Property(ModelCode.TERMINAL_SEQNUMBER, cimTerminal.SequenceNumber));
			}
        }

		public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd)
        {
			if(cimCurve != null && rd != null)
            {
				PopulateIdentifiedObjectProperties(cimCurve, rd);

				if (cimCurve.CurveStyleHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_CURVESTYLE, (short)GetDMSCurveStyle(cimCurve.CurveStyle)));
				if (cimCurve.XMultiplierHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_XMULT, (short)GetDMSUnitMultiplier(cimCurve.XMultiplier)));
				if(cimCurve.XUnitHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_XUNIT, (short)GetDMSUnitSymbol(cimCurve.XUnit)));
				if (cimCurve.Y1MultiplierHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_Y1MULT, (short)GetDMSUnitMultiplier(cimCurve.Y1Multiplier)));
				if (cimCurve.Y1UnitHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_Y1UNIT, (short)GetDMSUnitSymbol(cimCurve.Y1Unit)));
				if (cimCurve.Y2MultiplierHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_Y2MULT, (short)GetDMSUnitMultiplier(cimCurve.Y2Multiplier)));
				if (cimCurve.Y2UnitHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_Y2UNIT, (short)GetDMSUnitSymbol(cimCurve.Y2Unit)));
				if (cimCurve.Y3MultiplierHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_Y3MULT, (short)GetDMSUnitMultiplier(cimCurve.Y3Multiplier)));
				if (cimCurve.Y3UnitHasValue)
					rd.AddProperty(new Property(ModelCode.CURVE_Y3UNIT, (short)GetDMSUnitSymbol(cimCurve.Y3Unit)));
			}
        }

		public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if(cimRegulatingControl != null && rd != null)
            {
				PopulatePowerSystemResourceProperties(cimRegulatingControl, rd);

				if (cimRegulatingControl.DiscreteHasValue)
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_DISCRETE, cimRegulatingControl.Discrete));
				if(cimRegulatingControl.ModeHasValue)
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MODE, (short)GetDMSRegulatingControlModeKind(cimRegulatingControl.Mode)));
				if (cimRegulatingControl.MonitoredPhaseHasValue)
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MONITOREDPHASE, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));
				if (cimRegulatingControl.TargetRangeHasValue)
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETRANGE, cimRegulatingControl.TargetRange));
				if (cimRegulatingControl.TargetValueHasValue)
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETVALUE, cimRegulatingControl.TargetValue));


				if (cimRegulatingControl.TerminalHasValue)
				{
					long gid = importHelper.GetMappedGID(cimRegulatingControl.Terminal.ID);
					if (gid < 0)
					{
						transformAndLoadReport.Report.Append("WARNING: Convert ").Append(cimRegulatingControl.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulatingControl.ID);
						transformAndLoadReport.Report.Append("\" - Failed to set reference to Terminal: rdfID \"").Append(cimRegulatingControl.Terminal.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TERMINAL, gid));
				}
			}
        }

		public static void PopulateReactiveCapabilityCurveProperties(FTN.ReactiveCapabilityCurve cimReactiveCapabilityCurve, ResourceDescription rd)
        {
			if(cimReactiveCapabilityCurve != null && rd != null)
            {
				PopulateCurveProperties(cimReactiveCapabilityCurve, rd);

				if (cimReactiveCapabilityCurve.CoolantTemperatureHasValue)
					rd.AddProperty(new Property(ModelCode.REACTIVECAPABILITYCURVE_COOLANTTEMP, cimReactiveCapabilityCurve.CoolantTemperature));
				if (cimReactiveCapabilityCurve.HydrogenPressureHasValue)
					rd.AddProperty(new Property(ModelCode.REACTIVECAPABILITYCURVE_HYDROGENP, cimReactiveCapabilityCurve.HydrogenPressure));
			}
        }

		public static void PopulateRegulatingCondEqProperties(FTN.RegulatingCondEq cimRegulatingCondEq, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if(cimRegulatingCondEq != null && rd != null)
            {
				PopulateConductingEquipmentProperties(cimRegulatingCondEq, rd);
				if (cimRegulatingCondEq.RegulatingControlHasValue)
				{
					long gid = importHelper.GetMappedGID(cimRegulatingCondEq.RegulatingControl.ID);
					if (gid < 0)
					{
						transformAndLoadReport.Report.Append("WARNING: Convert ").Append(cimRegulatingCondEq.GetType().ToString()).Append(" rdfID = \"").Append(cimRegulatingCondEq.ID);
						transformAndLoadReport.Report.Append("\" - Failed to set reference to RegulatingControl: rdfID \"").Append(cimRegulatingCondEq.RegulatingControl.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.REGULATINGCONDEQ_REGULATINGCONT, gid));
				}
			}
        }

		public static void PopulateControlProperties(FTN.Control cimControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if(cimControl != null && rd != null)
            {
				PopulateIdentifiedObjectProperties(cimControl, rd);

				if (cimControl.OperationInProgressHasValue)
					rd.AddProperty(new Property(ModelCode.CONTROL_OPERATIONINPROGRESS, cimControl.OperationInProgress));
				if(cimControl.TimeStampHasValue)
					rd.AddProperty(new Property(ModelCode.CONTROL_TIMESTAMP, cimControl.TimeStamp));
				if (cimControl.UnitMultiplierHasValue)
					rd.AddProperty(new Property(ModelCode.CONTROL_UNITMULTIPLIER, (short)GetDMSUnitMultiplier(cimControl.UnitMultiplier)));
				if(cimControl.UnitSymbolHasValue)
					rd.AddProperty(new Property(ModelCode.CONTROL_UNITSYMBOL, (short)GetDMSUnitSymbol(cimControl.UnitSymbol)));

				if(cimControl.RegulatingCondEqHasValue)
                {
					long gid = importHelper.GetMappedGID(cimControl.RegulatingCondEq.ID);
					if(gid < 0)
                    {
						transformAndLoadReport.Report.Append("WARNING: Convert ").Append(cimControl.GetType().ToString()).Append(" rdfID = \"").Append(cimControl.ID);
						transformAndLoadReport.Report.Append("\" - Failed to set reference to RegulatingCondEq: rdfID \"").Append(cimControl.RegulatingCondEq.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.CONTROL_REGULATINGCONDEQ, gid));
				}
            }
        }

		public static void PopulateShuntCompensatorProperties(FTN.ShuntCompensator cimShuntCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if(cimShuntCompensator != null && rd != null)
            {
				PopulateRegulatingCondEqProperties(cimShuntCompensator, rd, importHelper, transformAndLoadReport);

				if (cimShuntCompensator.AVRDelayHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_AVRDELAY, cimShuntCompensator.AVRDelay));
				if(cimShuntCompensator.B0PerSectionHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_B0PERSECTION, cimShuntCompensator.B0PerSection));
				if (cimShuntCompensator.BPerSectionHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_BPERSECTION, cimShuntCompensator.BPerSection));
				if (cimShuntCompensator.G0PerSectionHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_G0PERSECTION, cimShuntCompensator.G0PerSection));
				if (cimShuntCompensator.GPerSectionHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_GPERSECTION, cimShuntCompensator.GPerSection));
				if (cimShuntCompensator.GroundedHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_GROUNDED, (short)GetDMSWindingConnection(cimShuntCompensator.Grounded)));
				if (cimShuntCompensator.MaximumSectionsHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_MAXIMUMSECTIONS, cimShuntCompensator.MaximumSections));
				if (cimShuntCompensator.NomUHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_NOMU, cimShuntCompensator.NomU));
				if (cimShuntCompensator.NormalSectionsHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_NORMALSECTIONS, cimShuntCompensator.NormalSections));
				if (cimShuntCompensator.PhaseConnectionHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_PHASECONNECTION, (short)GetDMSPhaseShuntConnectionKind(cimShuntCompensator.PhaseConnection)));
				if (cimShuntCompensator.SwitchOnCountHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_SWITCHONCOUNT, cimShuntCompensator.SwitchOnCount));
				if (cimShuntCompensator.SwitchOnDateHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_SWITCHONDATE, cimShuntCompensator.SwitchOnDate));
				if (cimShuntCompensator.VoltageSensitivityHasValue)
					rd.AddProperty(new Property(ModelCode.SHUNTCOMPENSATOR_VOLTAGESENS, cimShuntCompensator.VoltageSensitivity));
			}
        }

		public static void PopulateStaticVarCompensatorProperties(FTN.StaticVarCompensator cimStaticVarCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if(cimStaticVarCompensator != null && rd != null)
            {
				PopulateRegulatingCondEqProperties(cimStaticVarCompensator, rd, importHelper, transformAndLoadReport);

				if (cimStaticVarCompensator.CapacitiveRatingHasValue)
					rd.AddProperty(new Property(ModelCode.STATICVARCOMPENSATOR_CAPACITIVERAT, cimStaticVarCompensator.CapacitiveRating));
				if (cimStaticVarCompensator.InductiveRatingHasValue)
					rd.AddProperty(new Property(ModelCode.STATICVARCOMPENSATOR_INDUCTIVERAT, cimStaticVarCompensator.InductiveRating));
				if (cimStaticVarCompensator.SlopeHasValue)
					rd.AddProperty(new Property(ModelCode.STATICVARCOMPENSATOR_SLOPE, cimStaticVarCompensator.Slope));
				if (cimStaticVarCompensator.SVCControlModeHasValue)
					rd.AddProperty(new Property(ModelCode.STATICVARCOMPENSATOR_SVCCONTROLMODE, (short)GetDMSSVCControlMode(cimStaticVarCompensator.SVCControlMode)));
				if (cimStaticVarCompensator.VoltageSetPointHasValue)
					rd.AddProperty(new Property(ModelCode.STATICVARCOMPENSATOR_VOLTAGESETPNT, cimStaticVarCompensator.VoltageSetPoint));
			}
        }

		public static void PopulateRotatingMachineProperties(FTN.RotatingMachine cimRotatingMachine, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if(cimRotatingMachine != null && rd != null)
            {
				PopulateRegulatingCondEqProperties(cimRotatingMachine, rd, importHelper, transformAndLoadReport);

				if (cimRotatingMachine.DampingHasValue)
					rd.AddProperty(new Property(ModelCode.ROTATINGMACHINE_DAMPING, cimRotatingMachine.Damping));
				if (cimRotatingMachine.InertiaHasValue)
					rd.AddProperty(new Property(ModelCode.ROTATINGMACHINE_INERTIA, cimRotatingMachine.Inertia));
				if (cimRotatingMachine.RatedSHasValue)
					rd.AddProperty(new Property(ModelCode.ROTATINGMACHINE_RATEDS, cimRotatingMachine.RatedS));
				if (cimRotatingMachine.SaturationFactorHasValue)
					rd.AddProperty(new Property(ModelCode.ROTATINGMACHINE_SATFACTOR, cimRotatingMachine.SaturationFactor));
				if (cimRotatingMachine.SaturationFactor120HasValue)
					rd.AddProperty(new Property(ModelCode.ROTATINGMACHINE_SATFACTOR120, cimRotatingMachine.SaturationFactor120));
				if (cimRotatingMachine.StatorLeakageReactanceHasValue)
					rd.AddProperty(new Property(ModelCode.ROTATINGMACHINE_STATORLEAKAGERES, cimRotatingMachine.StatorLeakageReactance));
				if (cimRotatingMachine.StatorResistanceHasValue)
					rd.AddProperty(new Property(ModelCode.ROTATINGMACHINE_STATORRES, cimRotatingMachine.StatorResistance));
            }
        }

		public static void PopulateSynchronousMachineProperties(FTN.SynchronousMachine cimSynchronousMachine, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport transformAndLoadReport)
        {
			if(cimSynchronousMachine != null && rd != null)
            {
				PopulateRotatingMachineProperties(cimSynchronousMachine, rd, importHelper, transformAndLoadReport);

				if(cimSynchronousMachine.AVRToManualLagHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLAG, cimSynchronousMachine.AVRToManualLag));
				if (cimSynchronousMachine.AVRToManualLeadHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_AVRTOMANLEAD, cimSynchronousMachine.AVRToManualLead));
				if (cimSynchronousMachine.BaseQHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_BASEQ, cimSynchronousMachine.BaseQ));
				if (cimSynchronousMachine.CondenserPHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_CONDENSERP, cimSynchronousMachine.CondenserP));
				if (cimSynchronousMachine.CoolantConditionHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_COOLANTCOND, cimSynchronousMachine.CoolantCondition));
				if (cimSynchronousMachine.CoolantTypeHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_COOLANTTYPE, (short)GetDMSCoolantType(cimSynchronousMachine.CoolantType)));
				if (cimSynchronousMachine.ManualToAVRHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_MANUALTOAVR, cimSynchronousMachine.ManualToAVR));
				if (cimSynchronousMachine.OperatingModeHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_OPMODE, (short)GetDMSSynchronousMachineOperatingMode(cimSynchronousMachine.OperatingMode)));
				if (cimSynchronousMachine.SynchronousGeneratorTypeHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_GENTYPE, (short)GetDMSSynchronousGeneratorType(cimSynchronousMachine.SynchronousGeneratorType)));
				if (cimSynchronousMachine.TypeHasValue)
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_TYPE, (short)GetDMSSynchronousMachineType(cimSynchronousMachine.Type)));

				if (cimSynchronousMachine.ReactiveCapabilityCurvesHasValue)
				{
					long gid = importHelper.GetMappedGID(cimSynchronousMachine.ReactiveCapabilityCurves.ID);
					if (gid < 0)
					{
						transformAndLoadReport.Report.Append("WARNING: Convert ").Append(cimSynchronousMachine.GetType().ToString()).Append(" rdfID = \"").Append(cimSynchronousMachine.ID);
						transformAndLoadReport.Report.Append("\" - Failed to set reference to ReactiveCapabilityCurve: rdfID \"").Append(cimSynchronousMachine.ReactiveCapabilityCurves.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SYNCHRONOUSMACHINE_REACURVE, gid));
				}
			}
        }
		#endregion Populate ResourceDescription

		#region Enums convert
		public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.Unknown;
			}
		}

		public static CurveStyle GetDMSCurveStyle(FTN.CurveStyle curveStyle)
        {
            switch (curveStyle)
            {
                case FTN.CurveStyle.constantYValue:
                    return CurveStyle.constantYValue;
                case FTN.CurveStyle.formula:
                    return CurveStyle.formula;
                case FTN.CurveStyle.rampYValue:
                    return CurveStyle.rampYValue;
				default:
                    return CurveStyle.straightLineYValues;
            }
        }

		public static SynchronousMachineOperatingMode GetDMSSynchronousMachineOperatingMode(FTN.SynchronousMachineOperatingMode synchronousMachineOperatingMode)
        {
            switch (synchronousMachineOperatingMode)
            {
                case FTN.SynchronousMachineOperatingMode.condenser:
                    return SynchronousMachineOperatingMode.condenser;
                default:
                    return SynchronousMachineOperatingMode.generator;
            }
        }

		public static SynchronousGeneratorType GetDMSSynchronousGeneratorType(FTN.SynchronousGeneratorType synchronousGeneratorType)
        {
            switch (synchronousGeneratorType)
            {
                case FTN.SynchronousGeneratorType.roundRotor:
                    return SynchronousGeneratorType.roundRotor;
                case FTN.SynchronousGeneratorType.salientPole:
                    return SynchronousGeneratorType.salientPole;
                case FTN.SynchronousGeneratorType.transient:
                    return SynchronousGeneratorType.transient;
                case FTN.SynchronousGeneratorType.typeF:
                    return SynchronousGeneratorType.typeF;
                default:
                    return SynchronousGeneratorType.typeJ;
            }
        }

		public static SynchronousMachineType GetDMSSynchronousMachineType(FTN.SynchronousMachineType synchronousMachineType)
        {
            switch (synchronousMachineType)
            {
                case FTN.SynchronousMachineType.condenser:
                    return SynchronousMachineType.condenser;
                case FTN.SynchronousMachineType.generator:
                    return SynchronousMachineType.generator;
                default:
					return SynchronousMachineType.generator_or_condenser;
            }
        }

		public static PhaseShuntConnectionKind GetDMSPhaseShuntConnectionKind(FTN.PhaseShuntConnectionKind phaseShuntConnectionKind)
        {
            switch (phaseShuntConnectionKind)
            {
                case FTN.PhaseShuntConnectionKind.D:
                    return PhaseShuntConnectionKind.D;
                case FTN.PhaseShuntConnectionKind.I:
                    return PhaseShuntConnectionKind.I;
                case FTN.PhaseShuntConnectionKind.Y:
                    return PhaseShuntConnectionKind.Y;
                default:
                    return PhaseShuntConnectionKind.Yn;
            }
        }

		public static CoolantType GetDMSCoolantType(FTN.CoolantType coolantType)
        {
            switch (coolantType)
            {
                case FTN.CoolantType.air:
                    return CoolantType.air;
                case FTN.CoolantType.hydrogenGas:
                    return CoolantType.hydrogenGas;
                default:
                    return CoolantType.water;
            }
        }

		public static RegulatingControlModeKind GetDMSRegulatingControlModeKind(FTN.RegulatingControlModeKind regulatingControlModeKind)
        {
            switch (regulatingControlModeKind)
            {
                case FTN.RegulatingControlModeKind.activePower:
                    return RegulatingControlModeKind.activePower;
                case FTN.RegulatingControlModeKind.admittance:
                    return RegulatingControlModeKind.admittance;
                case FTN.RegulatingControlModeKind.currentFlow:
                    return RegulatingControlModeKind.currentFlow;
				case FTN.RegulatingControlModeKind.@fixed:
                    return RegulatingControlModeKind.@fixed;
                case FTN.RegulatingControlModeKind.powerFactor:
                    return RegulatingControlModeKind.powerFactor;
                case FTN.RegulatingControlModeKind.reactivePower:
                    return RegulatingControlModeKind.reactivePower;
                case FTN.RegulatingControlModeKind.temperature:
                    return RegulatingControlModeKind.temperature;
                case FTN.RegulatingControlModeKind.timeScheduled:
                    return RegulatingControlModeKind.timeScheduled;
                default:
                    return RegulatingControlModeKind.voltage;
            }
        }

		public static SVCControlMode GetDMSSVCControlMode(FTN.SVCControlMode sVCControlMode)
        {
            switch (sVCControlMode)
            {
                case FTN.SVCControlMode.off:
                    return SVCControlMode.off;
                case FTN.SVCControlMode.reactivePower:
                    return SVCControlMode.reactivePower;
                default:
                    return SVCControlMode.voltage;
            }
        }

		public static UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier unitMultiplier)
        {
            switch (unitMultiplier)
            {
                case FTN.UnitMultiplier.c:
                    return UnitMultiplier.c;
                case FTN.UnitMultiplier.d:
                    return UnitMultiplier.d;
                case FTN.UnitMultiplier.G:
                    return UnitMultiplier.G;
                case FTN.UnitMultiplier.k:
                    return UnitMultiplier.k;
                case FTN.UnitMultiplier.m:
                    return UnitMultiplier.m;
                case FTN.UnitMultiplier.M:
                    return UnitMultiplier.M;
                case FTN.UnitMultiplier.micro:
                    return UnitMultiplier.micro;
                case FTN.UnitMultiplier.n:
                    return UnitMultiplier.n;
                case FTN.UnitMultiplier.none:
                    return UnitMultiplier.none;
                case FTN.UnitMultiplier.p:
                    return UnitMultiplier.p;
                default:
					return UnitMultiplier.T;
            }
        }

		public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
        {
            switch (unitSymbol)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;
                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.none:
                    return UnitSymbol.none;
                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh:
                    return UnitSymbol.VArh;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
				default:
                    return UnitSymbol.Wh;
            }
        }

		public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
		{
			switch (windingConnection)
			{
				case FTN.WindingConnection.D:
					return WindingConnection.D;
				case FTN.WindingConnection.I:
					return WindingConnection.I;
				case FTN.WindingConnection.Z:
					return WindingConnection.Z;
				case FTN.WindingConnection.Y:
					return WindingConnection.Y;
				default:
					return WindingConnection.Y;
			}
		}
		#endregion Enums convert
	}
}
