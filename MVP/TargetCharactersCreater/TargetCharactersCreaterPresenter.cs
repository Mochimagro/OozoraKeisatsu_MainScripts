using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using TargetCharactersCreater;

	public interface ITargetCharactersCreaterPresenter
	{
		void ReadyCreate(int count);
		void AllEscape();
	}

	public class TargetCharactersCreaterPresenter : ITargetCharactersCreaterPresenter
	{
		private ITargetCharactersCreaterView _targetCharactersCreaterView = null;
		private ITargetCharactersCreaterModel _targetCharactersCreaterModel = null;
		private IScorePresenter _scorePresenter = null;
		private ISoundPresenter _soundPresenter = null;
		private IParticleEffecterPresenter _particleEffecterPresenter = null;

		public TargetCharactersCreaterPresenter(
			ITargetCharactersCreaterView view,ITargetCharactersCreaterModel model,
			IScorePresenter score,
			ISoundPresenter sound,
			IParticleEffecterPresenter particleEffecter) 
		{
			_targetCharactersCreaterView = view ?? throw new ArgumentNullException(nameof(view));
			_targetCharactersCreaterModel = model ?? throw new ArgumentNullException(nameof(model));
			
			_scorePresenter = score ?? throw new ArgumentNullException(nameof(score));
			_soundPresenter = sound ?? throw new ArgumentNullException(nameof(sound));
			_particleEffecterPresenter= particleEffecter ?? throw new ArgumentNullException(nameof(particleEffecter));

			_targetCharactersCreaterView.Init();
			_targetCharactersCreaterModel.TargetCharacters = _targetCharactersCreaterView.Characters;

			Bind();
		}
        public void ReadyCreate(int count)
        {
			_targetCharactersCreaterModel.AllDisable();

			for (int i = 0; i < count; i++)
            {
				_targetCharactersCreaterModel.ActiveRandomCharacter();
			}

			_targetCharactersCreaterModel.SetRandomWantedCharacter();

		}

		public void AllEscape()
		{
			_targetCharactersCreaterModel.AllEscapeCharacter();
		}


		private void Bind () 
		{
			// TODO : コンボ時のアニメーション
			// TODO : 結果画面で捕まえた数

			_targetCharactersCreaterModel.OnCatchedTarget.Subscribe(target =>
			{
				_scorePresenter.AddScore(100);

				_scorePresenter.AddCatchCount();

				_targetCharactersCreaterModel.ActiveRandomCharacter();

				if (_targetCharactersCreaterModel.CheckWantedCharacter(target))
                {
					_particleEffecterPresenter.SuperCatchEffect(target.Position);

					_soundPresenter.PlaySound("CatchSuper");

					_scorePresenter.AddWantedScore();

					_targetCharactersCreaterModel.SetRandomWantedCharacter();
                }
                else
                {
					_soundPresenter.PlaySound("CatchNormal");
					_particleEffecterPresenter.NormalCatchEffect(target.Position);
					_scorePresenter.ResetComboCount();
                }

			});

			_targetCharactersCreaterModel.OnWantedCharacter.Subscribe(wanted =>
			{
				_targetCharactersCreaterView.WantedImageSprite = wanted?.CharacterIcon;
			});
		}
	}
}