using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace OozoraKeisatsu.Game.ParticleEffecter
{
	public enum ParticleEffectType
    {
		normal,
		super
    }

	public interface IParticleEffecterView
    {
		void Init();
		void CreateParticle(ParticleEffectType type,Vector3 position);
	}

	[RequireComponent(typeof(ZenjectBinding))]
	public class ParticleEffecterView : MonoBehaviour ,IParticleEffecterView
	{
		[SerializeField] private GameObject _normalParticle = null;
		[SerializeField] private GameObject _superParticle = null;

        public void CreateParticle(ParticleEffectType type, Vector3 position)
        {
			GameObject effect = null;

			if (type == ParticleEffectType.normal)
			{
				effect = _normalParticle;
			}
			else if(type == ParticleEffectType.super){
				effect = _superParticle;
            }

			Instantiate(effect,position,Quaternion.identity);
        }

        public void Init()
		{
			

		}

		
		
	}
}