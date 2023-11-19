using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

namespace BasicBuildSystem
{
    public class BuildController : MonoBehaviour
    {
        [SerializeField] private List<Transform> parentPointList;
        [SerializeField] private List<BuildLevel> buildLevelList;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private List<ParticleSystem> particleList;
        [SerializeField] private float particleSpeedPerLevel = -25f;
        [SerializeField] private VisualEffect smokeVfx;
        
        
        
        
        

        private float CurrentExp { get; set; }
        private int _buildLevelIndex;
        private Dictionary<BuildObject, Transform> _objToParentDict = new Dictionary<BuildObject, Transform>();

        private float _defaultSpeed1;
        private float _defaultSpeed2;

        private void Awake()
        {
            foreach (var system in particleList)
            {
                var emission = system.emission;
                emission.rateOverTime = 500f;
            }

            var speed1 = smokeVfx.GetVector3("Speed1");
            _defaultSpeed1 = speed1.z;
            var speed2 = smokeVfx.GetVector3("Speed2");
            _defaultSpeed2 = speed2.z;
            
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
            
            var speed1 = smokeVfx.GetVector3("Speed1");//30
            speed1.z = _defaultSpeed1 + ((30 - _defaultSpeed1) / buildLevelList.Count) * obj;
            smokeVfx.SetVector3("Speed1", speed1);
            var speed2 = smokeVfx.GetVector3("Speed2");//40
            speed2.z = _defaultSpeed2 + ((40 - _defaultSpeed2) / buildLevelList.Count) * obj;
            smokeVfx.SetVector3("Speed2", speed2);
        }
    }
}
