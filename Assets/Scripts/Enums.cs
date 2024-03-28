using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
	public enum CardUnitType : byte
	{
		None = 0,
		Murloc = 1,
		Beast = 2,
		Elemental = 3,
		Mech = 4
	}

	public enum SideType : byte
	{
		Common = 0,
		Mage = 1,
		Warrior = 2
	}

	public enum StepType : byte
	{
		FirstPlayer = 0,
		SecondPlayer = 1
	}

	public enum CardAbility : byte
	{
		None = 0,
		Taunt = 1,
		BattleCry = 2,
		PassiveBuff = 3,
		Charge = 4
	}
}
