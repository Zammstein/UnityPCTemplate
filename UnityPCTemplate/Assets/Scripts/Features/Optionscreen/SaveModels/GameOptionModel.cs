using Features.SaveLoad;
using System;

namespace Features.Optionscreen.SaveModels {

    [Serializable]
    public class GameOptionModel : SaveModel {
        public const string ID = "GameOptionModel";

        private float mouseSensitivity;
        private float controllerSensitivity;

        private bool mouseYinverted;
        private bool controllerYinverted;

        private bool holdToCrouch;
        private bool holdToZoom;

        public GameOptionModel() {
            this.version = 1;

            mouseSensitivity = 5f;
            controllerSensitivity = 5f;
            mouseYinverted = false;
            controllerYinverted = false;
            holdToCrouch = false;
            holdToZoom = false;
        }

        #region Getters
        public float GetMouseSensitivity() {
            return this.mouseSensitivity;
        }

        public float GetControllerSensitivity() {
            return this.controllerSensitivity;
        }

        public bool GetMouseYInverted() {
            return this.mouseYinverted;
        }

        public bool GetControllerYInverted() {
            return this.controllerYinverted;
        }

        public bool GetHoldToCrouch() {
            return this.holdToCrouch;
        }

        public bool GetHoldToZoom() {
            return this.holdToZoom;
        }
        #endregion

        #region Setters
        public void SetMouseSensitivity(float value) {
            this.mouseSensitivity = value;
        }

        public void SetControllerSensitivity(float value) {
            this.controllerSensitivity = value;
        }

        public void SetMouseYInverted(bool value) {
            this.mouseYinverted = value;
        }

        public void SetControllerYInverted(bool value) {
            this.controllerYinverted = value;
        }

        public void SetHoldToCrouch(bool value) {
            this.holdToCrouch = value;
        }

        public void SetHoldToZoom(bool value) {
            this.holdToZoom = value;
        }
        #endregion

        public void ResetGameSettings() {
            mouseSensitivity = 5f;
            controllerSensitivity = 5f;
            mouseYinverted = false;
            controllerYinverted = false;
            holdToCrouch = false;
            holdToZoom = false;
        }

        public override void UpgradeModel(SaveModel oldVersion) {
            mouseSensitivity = ((GameOptionModel)oldVersion).GetMouseSensitivity(); ;
            controllerSensitivity = ((GameOptionModel)oldVersion).GetControllerSensitivity(); ;
            mouseYinverted = ((GameOptionModel)oldVersion).GetMouseYInverted(); ;
            controllerYinverted = ((GameOptionModel)oldVersion).GetControllerYInverted(); ;
            holdToCrouch = ((GameOptionModel)oldVersion).GetHoldToCrouch(); ;
            holdToZoom = ((GameOptionModel)oldVersion).GetHoldToZoom(); ;
        }
    }
}
