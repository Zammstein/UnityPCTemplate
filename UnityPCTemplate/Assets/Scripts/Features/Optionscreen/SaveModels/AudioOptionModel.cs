using Features.SaveLoad;
using System;

namespace Features.Optionscreen.SaveModels {

    [Serializable]
    public class AudioOptionModel : SaveModel {
        public const string ID = "AudioOptionModel";

        private float masterVolume;
        private float musicVolume;
        private float soundEffectVolume;
        private bool soundEnabled;

        public AudioOptionModel() {
            this.version = 1;

            masterVolume = 0.5f;
            musicVolume = 0.5f;
            soundEffectVolume = 0.5f;
            soundEnabled = true;
        }

        #region Getters
        public float GetMasterVolume() {
            return this.masterVolume;
        }

        public float GetMusicVolume() {
            return this.musicVolume;
        }

        public float GetSoundEffectVolume() {
            return this.soundEffectVolume;
        }

        public bool GetSoundEnabled() {
            return this.soundEnabled;
        }
        #endregion

        #region Setters
        public void SetMasterVolume(float value) {
            this.masterVolume = value;
        }

        public void SetMusicVolume(float value) {
            this.musicVolume = value;
        }

        public void SetSoundEffectVolume(float value) {
            this.soundEffectVolume = value;
        }

        public void SetSoundEnabled(bool value) {
            this.soundEnabled = value;
        }
        #endregion

        public void ResetAudioSettings() {
            masterVolume = 0.5f;
            musicVolume = 0.5f;
            soundEffectVolume = 0.5f;
            soundEnabled = true;
        }

        public override void UpgradeModel(SaveModel oldVersion) {
            masterVolume = ((AudioOptionModel)oldVersion).GetMasterVolume();
            musicVolume = ((AudioOptionModel)oldVersion).GetMusicVolume();
            soundEffectVolume = ((AudioOptionModel)oldVersion).GetSoundEffectVolume();
            soundEnabled = ((AudioOptionModel)oldVersion).GetSoundEnabled();
        }
    }
}
