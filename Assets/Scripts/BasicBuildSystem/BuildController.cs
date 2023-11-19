using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BasicBuildSystem
{
    public class BuildController : MonoBehaviour
    {
        [SerializeField] private List<Transform> parentPointList;
        [SerializeField] private List<BuildLevel> buildLevelList;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private List<ParticleSystem> particleList;
        [SerializeField] private float particleSpeedPerLevel = -25f;
        
        
        
        

        private float CurrentExp { get; set; }
        private int _buildLevelIndex;
        private Dictionary<BuildObject, Transform> _objToParentDict = new Dictionary<BuildObject, Transform>();

        private void Awake()
        {
            foreach (var system in particleList)
            {
                var emission = system.emission;
                emission.rateOverTime = 500f;
            }
            
            BuildEvents.OnAddExp += OnAddExp;
            BuildEvents.OnChangeBuildLevel += OnChangeBuildLevel;
        }

        [Sirenix.OdinInspector.Button]
        private void OnAddExp(float obj)
        {
            CurrentExp += obj;
            if (parentPointList.Count <= 0)
                return;

            for (int i = 0; i < 10; i++)
            {
                var levelIndex = _buildLevelIndex % buildLevelList.Count;
                var buildLevel = buildLevelList[levelIndex];
                if(CurrentExp < buildLevel.Exp)
                    break;
                
                if (parentPointList.Count <= 0)
                    break;
                _buildLevelIndex++;
                CurrentExp -= buildLevel.Exp;
                

                var bp = buildLevel.Bp;
                var parent = parentPointList.First();
                parentPointList.Remove(parent);

                var instance = Instantiate(bp, spawnParent);
                instance.transform.localPosition = Vector3.zero;
                instance.Initialize(parent);
                _objToParentDict.Add(instance, parent);
                
                BuildEvents.ChangeBuildLevel(_buildLevelIndex);
            }

            
        }

        private void OnChangeBuildLevel(int obj)
        {
            foreach (var system in particleList)
            {
                // var main = system.main;
                // var speed = 1 + obj * particleSpeedPerLevel;
                // if (speed <= 0.05f)
                //     speed = 0.05f;
                // main.simulationSpeed = speed;
                
                var emission = system.emission;
                var rateCount = 500 + obj * particleSpeedPerLevel;
                if (rateCount <= 10f)
                    rateCount = 10f;
                emission.rateOverTime = rateCount;
            }
        }
    }
}
