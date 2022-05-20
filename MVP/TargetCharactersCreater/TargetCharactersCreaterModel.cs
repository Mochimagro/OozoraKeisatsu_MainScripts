using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

namespace OozoraKeisatsu.Game.TargetCharactersCreater
{
	public interface ITargetCharactersCreaterModel
    {
		Component.ITargetCharacterComponent ActiveRandomCharacter();
		void AllDisable();
		void AllEscapeCharacter();
		void DisactiveCharacter(Component.ITargetCharacterComponent target);
		void SetRandomWantedCharacter();
		bool CheckWantedCharacter(Component.ITargetCharacterComponent catched);
		IReadOnlyList<Component.ITargetCharacterComponent> TargetCharacters { set; }
		IObservable<Component.ITargetCharacterComponent> OnCatchedTarget { get; }
		IObservable<Component.ITargetCharacterComponent> OnWantedCharacter { get; }
	}


	public class TargetCharactersCreaterModel : ITargetCharactersCreaterModel
	{
		
		private List<Component.ITargetCharacterComponent> _activeCharacters = new List<Component.ITargetCharacterComponent>();
		private List<Component.ITargetCharacterComponent> _disactiveCharacters = new List<Component.ITargetCharacterComponent>(); 

		public IObservable<Component.ITargetCharacterComponent> OnCatchedTarget => _onCatchedTarget;
		private Subject<Component.ITargetCharacterComponent> _onCatchedTarget = new Subject<Component.ITargetCharacterComponent>();

		private ReactiveProperty<Component.ITargetCharacterComponent> _wantedCharacter = new ReactiveProperty<Component.ITargetCharacterComponent>();
        public IObservable<Component.ITargetCharacterComponent> OnWantedCharacter => _wantedCharacter;


		public TargetCharactersCreaterModel()
		{

		}

		public IReadOnlyList<Component.ITargetCharacterComponent> TargetCharacters
		{
			set
			{

				foreach (var component in value)
				{
					component.Init();

					component.OnCatched.Subscribe(target =>
					{
						// DisactiveCharacter(target);
						_activeCharacters.Remove(target);

						_onCatchedTarget.OnNext(target);
					});

					component.OnEndAnimation.Subscribe(target =>
                    {
						target.RemoveField();
						_disactiveCharacters.Add(target);
                    });

					component.OnEscape.Subscribe(target =>
					{
						DisactiveCharacter(component);
					});

					DisactiveCharacter(component);

				}
			}
		}


        public Component.ITargetCharacterComponent ActiveRandomCharacter()
        {
			var select = _disactiveCharacters[UnityEngine.Random.Range(0, _disactiveCharacters.Count)];

			ActiveCharacter(select);

			return select;

        }

		private void ActiveCharacter(Component.ITargetCharacterComponent target)
        {
			_disactiveCharacters.Remove(target);
			target.EntryField();
			_activeCharacters.Add(target);
        }

		public void DisactiveCharacter(Component.ITargetCharacterComponent target)
        {
			_activeCharacters.Remove(target);
			target.RemoveField();
			_disactiveCharacters.Add(target);
		}


        public void AllDisable()
        {
			while(_activeCharacters.Count > 0)
            {
				DisactiveCharacter(_activeCharacters[0]);
            }
        }

		public void SetRandomWantedCharacter()
        {
			_wantedCharacter.Value = _activeCharacters[UnityEngine.Random.Range(0, _activeCharacters.Count)];
        }

        public bool CheckWantedCharacter(Component.ITargetCharacterComponent catched)
        {
			return _wantedCharacter.Value == catched;
        }

        public void AllEscapeCharacter()
        {
			foreach(var character in _activeCharacters)
            {
				character.EscapeField();
            }
        }
    }
}