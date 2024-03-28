using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneLine;

namespace Cards
{
	[System.Serializable]
	public struct CardPropertyData
	{
		[SerializeField]
		private int _cost;
		[SerializeField]
		private Texture _image;
		[SerializeField]
		private string _name;
		[SerializeField]
		private string _description;
		[SerializeField]
		private int _attack;
		[SerializeField]
		private int _health;
		[SerializeField]
		private CardUnitType _type;
	}

	[Serializable]
	public struct CardPropertiesData
	{
		[Width(30)]
		public uint Id;
		[NonSerialized]
		public ushort Cost;
        [Width(150)]
        public string Name;
		public string Description;
        [Width(100)]
        public CardAbility Ability;
		[Width(125)]
		public Texture Texture;
		[Width(40)]
		public ushort Attack;
		[Width(40)]
		public ushort Health;
		[Width(65)]
		public CardUnitType Type;

		public CardParamsData GetParams()
		{
			return new CardParamsData(Cost, Attack, Health);
		}
	}

	public struct CardParamsData
	{
		public ushort Cost;
		public ushort Attack;
		public ushort Health;

		public CardParamsData(ushort cost, ushort attack, ushort health)
		{
			Cost = cost; Attack = attack; Health = health;
		}
	}
}
