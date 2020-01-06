using System.Collections.Generic;
using Scripts.Models;
using Scripts.Views;
using UnityEngine;

namespace Scripts.Presenters
{
    public interface IProjectilePresenter
    {
        GameObject CreateObject(string path);
        void Remove(ProjectileView projectileView);
    }

    public class ProjectilePresenter : PresenterBase, IProjectilePresenter
    {
        /// <summary>
        /// View
        /// </summary>
        private Dictionary<ProjectileView, ProjectileModel> _dictionary;

        public ProjectilePresenter()
        {
            _dictionary = new Dictionary<ProjectileView, ProjectileModel>();
        }

        public void Remove(ProjectileView projectileView)
        {
            _dictionary.Remove(projectileView);
        }

        public GameObject CreateObject(string path)
        {
            var instance = GamePresenter.Instance.CreateGameObjectFromObject(path);
            var model = new ProjectileModel();
            _dictionary.Add(instance.GetComponentInChildren<ProjectileView>(), model);
            return instance;
        }
    }
}