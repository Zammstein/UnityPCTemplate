using System;

[Serializable]
public class AudioOptionModel : SaveModel {
    public const string ID = "AudioOptionModel";

    public AudioOptionModel() {
        this.version = 1;
    }

    public override void UpgradeModel(SaveModel oldVersion) {
        
    }
}
