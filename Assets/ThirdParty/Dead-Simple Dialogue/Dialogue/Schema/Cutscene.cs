using UnityEngine;
using System.Collections.Generic;
using System;
using Dossamer.Utilities;

namespace Dossamer.Dialogue.Schema
{
    [Serializable]
    public class Cutscene : ScriptableObject
    {
        public string Title;
        public List<Speech> Exchanges;
        public SerializableGuid Guid;

        public void Initialize(string title)
        {
            Title = title;
            Exchanges = new List<Speech>();
            Guid = System.Guid.NewGuid();
        }
    }
}