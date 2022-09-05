using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		Unknown = 0x0,
		N = 0x1,
		C = 0x2,
		CN = 0x3,
		B = 0x4,
		BN = 0x5,
		BC = 0x6,
		BCN = 0x7,
		A = 0x8,
		AN = 0x9,
		AC = 0xA,
		ACN = 0xB,
		AB = 0xC,
		ABN = 0xD,
		ABC = 0xE,
		ABCN = 0xF,
		s1 = 0x10,
		s12 = 0x11,
		s12N = 0x12,
		s1N = 0x13
	}
	public enum WindingConnection : short
	{
		Y = 1,		// Wye
		D = 2,		// Delta
		Z = 3,		// ZigZag
		I = 4,		// Single-phase connection. Phase-to-phase or phase-to-ground is determined by elements' phase attribute.
		Yn = 5,   // Scott T-connection. The primary winding is 2-phase, split in 8.66:1 ratio
		A = 6,		// 2-phase open wye. Not used in Network Model, only as result of Topology Analysis.
		Zn = 7		// 2-phase open delta. Not used in Network Model, only as result of Topology Analysis.
	}

	public enum CoolantType : short
    {
		air = 1,
		hydrogenGas = 2,
		water = 3
    }

	public enum CurveStyle : short
    {
		constantYValue = 1,
		formula = 2,
		rampYValue = 3,
		straightLineYValues = 4
    }

	public enum PhaseShuntConnectionKind : short
    {
		D = 1,
		Y = 2,
		I = 3,
		Yn = 4
    }

	public enum RegulatingControlModeKind : short
    {
		activePower = 1,
		admittance = 2,
		currentFlow = 3,
		@fixed = 4,
		powerFactor = 5,
		reactivePower = 6,
		temperature = 7,
		timeScheduled = 8,
		voltage = 9
    }

	public enum SVCControlMode : short
	{
		off = 1,
		reactivePower = 2,
		voltage = 3
	}

	public enum SynchronousGeneratorType : short
    {
		roundRotor = 1,
		salientPole = 2,
		transient = 3,
		typeF = 4,
		typeJ = 5
    }

	public enum SynchronousMachineOperatingMode : short
    {
		condenser = 1,
		generator = 2
    }

	public enum SynchronousMachineType : short
	{
		condenser = 1,
		generator = 2,
		generator_or_condenser = 3
	}

	public enum UnitMultiplier : short
    {
        G = 1,
		M = 2,
		T = 3,
		c = 4,
		d = 5,
		k = 6,
		m = 7,
		micro = 8,
		n = 9,
		none = 10,
		p = 11
    }

	public enum UnitSymbol : short
    {
		A = 1,
		F = 2,
		H = 3,
		Hz = 4,
		J = 5,
		N = 6,
		Pa = 7,
		S = 8,
		V = 9,
		VA = 10,
		VAh = 11,
		VAr = 12,
		VArh = 13,
		W = 14,
		Wh = 15,
		deg = 16,
		degC = 17,
		g = 18,
		h = 19,
		m = 20,
		m2 = 21,
		m3 = 22,
		min = 23,
		none = 24,
		ohm = 25,
		rad = 26,
		s = 27
    }
}
