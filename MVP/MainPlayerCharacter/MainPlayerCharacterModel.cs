using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.MainPlayerCharacter
{
	public interface IMainPlayerCharacterModel
    {

		bool EnableControll { get; set; }

	}


	public class MainPlayerCharacterModel : IMainPlayerCharacterModel
	{
		public bool EnableControll { get => _enableControll; set => _enableControll = value; }
		private bool _enableControll = false;

		public MainPlayerCharacterModel()
		{
			

		}

	}
}