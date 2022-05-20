using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Presenter
{
	using ParticleEffecter;

	public interface IParticleEffecterPresenter
	{
		void SuperCatchEffect(Vector3 pos);
		void NormalCatchEffect(Vector3 pos);
	}

	public class ParticleEffecterPresenter : IParticleEffecterPresenter
	{
		private IParticleEffecterView _particleEffecterView = null;
		private IParticleEffecterModel _particleEffecterModel = null;

		public ParticleEffecterPresenter(IParticleEffecterView view,IParticleEffecterModel model) 
		{
			_particleEffecterView = view ?? throw new ArgumentNullException(nameof(view));
			_particleEffecterModel = model ?? throw new ArgumentNullException(nameof(model));
			

			Bind();
		}

		public void NormalCatchEffect(Vector3 pos) => _particleEffecterView.CreateParticle(ParticleEffectType.normal, pos);

		public void SuperCatchEffect(Vector3 pos) => _particleEffecterView.CreateParticle(ParticleEffectType.super, pos);

        private void Bind () 
		{
			

		}
	}
}