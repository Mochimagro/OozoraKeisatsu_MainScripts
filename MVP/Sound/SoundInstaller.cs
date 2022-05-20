using UnityEngine;
using Zenject;

namespace OozoraKeisatsu.Game.Installer
{
	public class SoundInstaller : MonoInstaller 
	{
		public override void InstallBindings()
		{
            Container.BindInterfacesAndSelfTo<Sound.SoundModel>().FromNew().AsCached();
            Container.BindInterfacesAndSelfTo<Presenter.SoundPresenter>().AsCached().NonLazy();
		}
	}
}