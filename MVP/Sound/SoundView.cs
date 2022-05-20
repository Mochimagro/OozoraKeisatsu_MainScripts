using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;
using UnityEngine.Audio;

namespace OozoraKeisatsu.Game.Sound
{
	public interface ISoundView
    {
		void Init(bool defaultValue);

		void PlaySoundEffect(AudioClip clip);
		void PlayBGM(AudioClip clip);
		void StopBGM();
        bool MasterMixiser { set; }
        IObservable<bool > OnSoundSwitch { get; }
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class SoundView : MonoBehaviour ,ISoundView
	{
        [SerializeField] private AudioSource _soundEffect = null;
        [SerializeField] private AudioSource _bgm = null;
        [SerializeField] private Component.ImageToggleComponent _soundSwitch = null;
        [SerializeField] private AudioMixer _audioMixiser = null;

        public IObservable<bool> OnSoundSwitch => _onSoundSwitch;

        public bool MasterMixiser { set => _audioMixiser.SetFloat("MasterVolume",value ? 0 : -80); }

        private Subject<bool> _onSoundSwitch = new Subject<bool>();

        public void Init(bool defaultValue)
		{
            _soundSwitch.Init(defaultValue);

            _soundSwitch.OnSwitch.Subscribe(value =>
            {
                _onSoundSwitch.OnNext(value);
            });

            Debug.Log($"View Default : {defaultValue}");

		}

        public void PlayBGM(AudioClip clip)
        {
            _bgm.clip = clip;
            _bgm.Play();
        }

        public void PlaySoundEffect(AudioClip clip)
        {
            _soundEffect .clip = clip;
            _soundEffect.Play();
        }

        public void StopBGM()
        {
            _bgm.Stop();
        }
    }
}