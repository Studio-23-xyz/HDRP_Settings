using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{
    public interface ISettings
    {
        void Initialized();
        void Apply();

    }
}