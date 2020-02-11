using System;
using Framework;
using Scripts.Models;
using UnityEngine;

namespace Scripts.Views
{
    public class GameProgressPointView : ViewBase
    {
        [SerializeField] private int _id;
        public void OnCollisionEnter(Collision other)
        {
            GameModel.Instance.GameProgressModel.PassingPoint(_id);

        }
    }
}