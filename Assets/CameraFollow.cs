using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown2D.CameraTools
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        #region Enum

        public enum FollowType
        {
            SNAP,
            LERP,
            SMOOTHDAMP
        }

        public enum UpdateType
        {
            LATE_UPDATE,
            FIXED_UPDATE,
        }

        #endregion


        #region Show in inspector

        [Header("Parameters")]
        [SerializeField] private UpdateType _updateType;
        [SerializeField] private FollowType _followType;
        [SerializeField] private float _stopDistance;

        [Header("Lerp parameters")]
        [SerializeField] private float _speed;

        [Header("SmoothDamp parameters")]
        [SerializeField] private float _smoothTime;

        #endregion


        #region Init

        private void Awake()
        {
            _transform = transform;

            GameObject playerGO = GameObject.FindGameObjectWithTag("CameraTarget");
            if (playerGO == null)
            {
                Debug.LogError("There is no active \"CameraTarget\" tagged GameObject in the scene");
            }
            else
            {
                _target = playerGO.transform;
            }
        }

        #endregion


        #region Updates

        // Si le joueur bouge avec son Transform sur le cycle Update 
        // OU avec un Rigidbody sur le cycle FixedUpdate mais avec l'option interpolate activée sur le Rigidbody, 
        // alors la caméra doit le suivre sur LateUpdate

        private void LateUpdate()
        {
            if (_updateType != UpdateType.LATE_UPDATE)
            {
                return;
            }

            DoCameraUpdate();
        }

        // Si le joueur bouge avec un Rigidbody sur le cycle FixedUpdate, alors
        // la caméra doit le suivre sur FixedUpdate
        // ATTENTION, dans ce cas, tous les objets qui bougent sur le cycle Update pourront être légèrement désynchronisés 
        // vis-à-vis de la caméra

        private void FixedUpdate()
        {
            if (_updateType != UpdateType.FIXED_UPDATE)
            {
                return;
            }

            DoCameraUpdate();
        }

        private void DoCameraUpdate()
        {
            switch (_followType)
            {
                case FollowType.SNAP:
                    FollowWithSnap();
                    break;
                case FollowType.LERP:
                    FollowWithLerp();
                    break;
                case FollowType.SMOOTHDAMP:
                    FollowWithSmoothDamp();
                    break;
            }
        }

        #endregion


        #region Follow methods

        /// <summary>
        /// Suit le joueur en se collant directement à sa position
        /// </summary>
        private void FollowWithSnap()
        {
            Vector3 newPosition = _target.position;
            newPosition.z = _transform.position.z;

            _transform.position = newPosition;
        }

        /// <summary>
        /// Suit le joueur en interpolant sa position en fonction d'une vitesse par la fonction Lerp
        /// </summary>
        private void FollowWithLerp()
        {
            if (Vector2.Distance(_transform.position, _target.position) < _stopDistance)
            {
                return;
            }

            Vector3 newPosition = Vector2.Lerp(_transform.position, _target.position, _speed * (_updateType == UpdateType.LATE_UPDATE ? Time.deltaTime : Time.fixedDeltaTime));
            newPosition.z = _transform.position.z;
            _transform.position = newPosition;
        }

        /// <summary>
        /// Suit le joueur en calculant une vitesse de déplacement relative par la fonction SmoothDamp
        /// </summary>
        private void FollowWithSmoothDamp()
        {
            if (Vector2.Distance(_transform.position, _target.position) < _stopDistance)
            {
                return;
            }

            Vector3 newPosition = Vector2.SmoothDamp(_transform.position, _target.position, ref _velocity, _smoothTime, Mathf.Infinity, _updateType == UpdateType.LATE_UPDATE ? Time.deltaTime : Time.fixedDeltaTime);
            newPosition.z = _transform.position.z;
            _transform.position = newPosition;
        }

        #endregion


        #region Private

        private Transform _transform;
        private Transform _target;
        private Vector2 _velocity;

        #endregion
    }
}
