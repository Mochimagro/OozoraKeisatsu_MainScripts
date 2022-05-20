using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace OozoraKeisatsu.Game.MainPlayerCharacter
{
	public interface IMainPlayerCharacterView
    {
		void Init();
		void SetStart();
		void Move(Vector2 inputVector);
		IObservable<Component.ITargetCharacterComponent> OnCatchTarget { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class MainPlayerCharacterView : MonoBehaviour ,IMainPlayerCharacterView
	{
		[Inject(Id = "IMainPlayerCharacterComponent")] private Component.IMainPlayerCharacterComponent _mainPlayerCharacterComponent = null;
		[Inject(Id = "IPlayerTriggerComponent")]private Component.IPlayerTriggerComponent _playerTriggerComponent = null;

		public IObservable<Component.ITargetCharacterComponent> OnCatchTarget => _playerTriggerComponent.OnCatchTarget;

        public void Init()
		{
			_mainPlayerCharacterComponent.Init();
			_playerTriggerComponent.Init();
		}

        public void Move(Vector2 inputVector)
        {
			_mainPlayerCharacterComponent.Move(inputVector);
        }

        public void SetStart()
        {
			_mainPlayerCharacterComponent.SetStartPosition();
        }
    }
}