using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Sound
{
	public interface ISoundModel
    {
		void SearchSound(string soundName);

		IObservable<AudioClip> OnSoundEffect { get; }
		IObservable<AudioClip> OnBGM { get; }
		IObservable<bool> OnMasterSound { get; }
		bool MasterSound { get; set; }
    }


	public class SoundModel : ISoundModel
	{
        public IObservable<AudioClip> OnSoundEffect => _onSoundEffect;
		private Subject<AudioClip> _onSoundEffect = new Subject<AudioClip>();

        public IObservable<AudioClip> OnBGM => _onBGM;
        private Subject<AudioClip> _onBGM = new Subject<AudioClip>();

        public bool MasterSound { 
			get => _masterSound;
			set 
			{
				_masterSound = value;
				PlayerPrefs.SetInt("MasterSound", value ? 1 : 0);
				_onMasterSound.OnNext(value);
			}
		}

		public IObservable<bool> OnMasterSound => _onMasterSound;
		private Subject<bool> _onMasterSound = new Subject<bool>();
		private bool _masterSound = false;

		private Dictionary<string,Data.ISoundClipData> _clips = new Dictionary<string,Data.ISoundClipData>();

		public SoundModel(Data.ISoundClipsListData listData)
		{
			foreach(var clip in listData.ClipsLists)
            {
				_clips.Add(clip.SoundName, clip);
            }

			_masterSound = PlayerPrefs.GetInt("MasterSound", 1) == 1;

		}

        public void SearchSound(string soundName)
        {
			var clip = _clips[soundName];

			if(clip.Type == Data.SoundType.SE)
            {
				_onSoundEffect.OnNext(clip.AudioClip);
				return;
            }
			if(clip.Type == Data.SoundType.BGM)
            {
				_onBGM.OnNext(clip.AudioClip);
				return;
            }
			Debug.LogError("サウンドが見当たらない事故");
        }
    }
}