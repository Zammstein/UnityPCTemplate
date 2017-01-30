using System;

namespace Features.SaveLoad {

    [Serializable]
    public abstract class SaveModel {
        protected int version;

        public int GetVersion() { return version; }
        public virtual void UpgradeModel(SaveModel oldVersion) { }
    }
}