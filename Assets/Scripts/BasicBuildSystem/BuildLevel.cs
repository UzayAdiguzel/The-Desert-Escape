using System;
using UnityEngine;

namespace BasicBuildSystem
{
    [Serializable]
    public class BuildLevel
    {
        [SerializeField] private float exp;
        [SerializeField] private BuildObject bp;


        public float Exp => exp;
        public BuildObject Bp => bp;
    }
}