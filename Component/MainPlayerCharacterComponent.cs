using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.AI;

namespace OozoraKeisatsu.Game.Component
{
	public interface IMainPlayerCharacterComponent
    {
		void Init();
		void Move(Vector2 vector);
		void SetStartPosition();
    }


	public class MainPlayerCharacterComponent : MonoBehaviour ,IMainPlayerCharacterComponent
	{
		[SerializeField] private float _speed = 1.0f;
		[SerializeField] private NavMeshAgent _navMeshAgent = null;
		[SerializeField]private Transform _transform = null;
		[SerializeField] private Animator _animator = null;

		private Vector3 _moveDirection = new Vector3();

		public void Init () 
		{
			

		}

        public void Move(Vector2 vector)
        {
			_moveDirection.Set(vector.x, 0, vector.y);
			_navMeshAgent.Move(_moveDirection * _speed);
			_transform.LookAt(_moveDirection + _transform.position);
			_animator.SetFloat("Move", _moveDirection.magnitude);
        }

        public void SetStartPosition()
        {
			_transform.position = new Vector3(0, 0, -10);
			_transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}