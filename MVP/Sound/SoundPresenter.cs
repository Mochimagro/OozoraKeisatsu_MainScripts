using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using Sound;

	public interface ISoundPresenter
	{
		void PlaySound(string soundName);
		void StopBGM();

		void SetDefault();
	}

	public class SoundPresenter : ISoundPresenter
	{
		private ISoundView _soundView = null;
		private ISoundModel _soundModel = null;

		public SoundPresenter(ISoundView view,ISoundModel model) 
		{
			_soundView = view ?? throw new ArgumentNullException(nameof(view));
			_soundModel = model ?? throw new ArgumentNullException(nameof(model));

			_soundView.Init(_soundModel.MasterSound);
			Bind();
		}

        public void PlaySound(string soundName)
        {
			_soundModel.SearchSound(soundName);
        }

        public void SetDefault()
        {
			_soundView.MasterMixiser = _soundModel.MasterSound;
        }

        public void StopBGM() => _soundView.StopBGM();

        private void Bind () 
		{
			_soundModel.OnSoundEffect.Subscribe(clip =>
			{
				_soundView.PlaySoundEffect(clip);
			});

			_soundModel.OnBGM.Subscribe(clip =>
			{
				_soundView.PlayBGM(clip);
			});

			_soundView.OnSoundSwitch.Subscribe(value =>
			{
				_soundModel.MasterSound = value;
			});

			_soundModel.OnMasterSound.Subscribe(value =>
			{
				_soundView.MasterMixiser = value;
			});
		}
	}
}