using System;
using UnityEngine;

[Serializable]
public class GraphicsOptionModel : SaveModel {
    public const string ID = "GraphicsOptionModel";

    private int qualityLevel;
    private int textureResolution;
    private int shadowQuality;
    private int antiAliasing;
    private bool anisotropicFiltering;
    private bool fullscreenEnabled;
    private bool vSyncEnabled;
    private bool reduceInputLag;

    public GraphicsOptionModel() {
        this.version = 1;

        qualityLevel = 1;
        textureResolution = 0;
        shadowQuality = 0;
        antiAliasing = 0;
        anisotropicFiltering = true;
        fullscreenEnabled = true;
        vSyncEnabled = true;
        reduceInputLag = false;
    }

    #region Getters
    public bool GetVSync() {
        return this.vSyncEnabled;
    }

    public int GetAntiAliasing() {
        return this.antiAliasing;
    }

    public int GetQualityLevel() {
        return this.qualityLevel;
    }

    public int GetTextureResolution() {
        return this.textureResolution;
    }

    public bool GetAnisotropicFiltering() {
        return this.anisotropicFiltering;
    }

    public bool GetFullscreenMode() {
        return this.fullscreenEnabled;
    }

    public int GetShadowQuality() {
        return this.shadowQuality;
    }

    public bool GetReduceInputLag() {
        return this.reduceInputLag;
    }
    #endregion

    #region Setters
    public void SetVSync(bool value) {
        this.vSyncEnabled = value;
    }

    public void SetAntiAliasing(int value) {
        this.antiAliasing = value;
    }

    public void SetQualityLevel(int value) {
        this.qualityLevel = value;
    }

    public void SetTextureResolution(int value) {
        this.textureResolution = value;
    }

    public void SetAnisotropicFiltering(bool value) {
        this.anisotropicFiltering = value;
    }

    public void SetFullscreenMode(bool value) {
        this.fullscreenEnabled = value;
    }

    public void SetShadowQuality(int value) {
        this.shadowQuality = value;
    }

    public void SetReduceInputLag(bool value) {
        this.reduceInputLag = value;
    }
    #endregion

    public void ResetGraphicsSettings() {
        qualityLevel = 1;
        textureResolution = 0;
        shadowQuality = 0;
        antiAliasing = 0;
        anisotropicFiltering = true;
        fullscreenEnabled = true;
        vSyncEnabled = true;
        reduceInputLag = false;
    }

    public override void UpgradeModel(SaveModel oldVersion) {
        vSyncEnabled = ((GraphicsOptionModel)oldVersion).GetVSync();
        antiAliasing = ((GraphicsOptionModel)oldVersion).GetAntiAliasing();
        qualityLevel = ((GraphicsOptionModel)oldVersion).GetQualityLevel();
        textureResolution = ((GraphicsOptionModel)oldVersion).GetTextureResolution();
        anisotropicFiltering = ((GraphicsOptionModel)oldVersion).GetAnisotropicFiltering();
        fullscreenEnabled = ((GraphicsOptionModel)oldVersion).GetFullscreenMode();
        shadowQuality = ((GraphicsOptionModel)oldVersion).GetShadowQuality();
        reduceInputLag = ((GraphicsOptionModel)oldVersion).GetReduceInputLag();
    }
}
