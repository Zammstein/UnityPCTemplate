using System;

[Serializable]
public class ControlOptionModel : SaveModel {
    public const string ID = "ControlOptionModel";

    public ControlOptionModel() {
        this.version = 1;
    }

    public override void UpgradeModel(SaveModel oldVersion) {
        
    }
}
