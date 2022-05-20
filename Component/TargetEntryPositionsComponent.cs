using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OozoraKeisatsu.Game.Component
{
	public interface ITargetEntryPositionsComponent
    {
		void Init();
    }


	public class TargetEntryPositionsComponent : MonoBehaviour ,ITargetEntryPositionsComponent
	{
		[SerializeField] private List<EntryPositionsPair> _positions = new List<EntryPositionsPair>();

		public void Init () 
		{
			

		}
		
	}

	[System.Serializable]public class EntryPositionsPair
    {
		[SerializeField] private Transform _start;
		[SerializeField] private Transform _end;

		public Transform Start => _start;
		public Transform End => _end;

    }

}