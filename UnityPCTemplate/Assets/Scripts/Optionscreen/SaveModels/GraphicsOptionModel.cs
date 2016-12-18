using System;

[Serializable]
public class GraphicsOptionModel : SaveModel {
    public const string ID = "GraphicsOptionModel";

    public GraphicsOptionModel() {
        this.version = 1;
    }

    public override void UpgradeModel(SaveModel oldVersion) {
        
    }
}
