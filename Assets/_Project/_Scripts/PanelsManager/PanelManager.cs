using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace _Scripts.Panels
{
    public class PanelManager : Singleton<PanelManager>
    {

        private List<PanelInstanceModel> _panelInstanceModels = new List<PanelInstanceModel>();
        public List<GameObject> PrefabsForPool;
        private List<GameObject> _pooledObjects = new List<GameObject>();

        public void ShowPanel(string panelId, PanelShowBehaviour behaviour = PanelShowBehaviour.HIDE_PREVIOUS)
        {
            GameObject panelInstance = GetObjectFromPool(panelId);

            if (panelInstance != null)
            {
                if (behaviour == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelsInQueue() > 0)
                {
                    var lastPanel = GetLastPanel();
                    lastPanel?.PanelInstance.SetActive(false);
                }

                _panelInstanceModels.Add(new PanelInstanceModel
                {
                    PanelId = panelId,
                    PanelInstance = panelInstance
                });
            }
            else
            {
                Debug.LogWarning($"Trying to use panelId = {panelId}, but this is not found in the ObjectPool");
            }
        }

        public void HideLastPanel()
        {
            if (AnyPanelShowing())
            {
                var lastPanel = GetLastPanel();
                _panelInstanceModels.Remove(lastPanel);
                PoolObject(lastPanel.PanelInstance);
                if (GetAmountPanelsInQueue() > 0)
                {
                    lastPanel = GetLastPanel();
                    if (lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                    {
                        lastPanel.PanelInstance.SetActive(true);
                    }
                }
            }
        }

        PanelInstanceModel GetLastPanel()
        {
            return _panelInstanceModels[_panelInstanceModels.Count - 1];
        }

        private bool AnyPanelShowing()
        {
            return GetAmountPanelsInQueue() > 0;
        }

        private int GetAmountPanelsInQueue()
        {
            return _panelInstanceModels.Count;
        }

        private GameObject GetObjectFromPool(string objectName)
        {
            var instance = _pooledObjects.FirstOrDefault(obj => obj.name == objectName);

            if (instance != null)
            {
                _pooledObjects.Remove(instance);

                instance.SetActive(true);

                return instance;
            }

            var prefab = PrefabsForPool.FirstOrDefault(obj => obj.name == objectName);
            if (prefab != null)
            {
                var newInstace = Instantiate(prefab, Vector3.zero, Quaternion.identity, this.transform/*.parent.transform*/);
                newInstace.name = objectName;
                newInstace.transform.localPosition = Vector3.zero;
                if (!newInstace.gameObject.activeSelf) newInstace.gameObject.SetActive(true);
                return newInstace;
            }
            return null;
        }

        private void PoolObject(GameObject obj)
        {
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public class PanelInstanceModel
    {
        public string PanelId;
        public GameObject PanelInstance;
    }
}
