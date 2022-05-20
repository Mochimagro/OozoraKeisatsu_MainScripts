using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class ParticleEffecterInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<ParticleEffecter.ParticleEffecterModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.ParticleEffecterPresenter>().AsCached().NonLazy();
		}
	}
}