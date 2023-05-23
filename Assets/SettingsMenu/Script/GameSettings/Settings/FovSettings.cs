using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameSettings
{
    [RequireComponent(typeof(Slider))]
    public class FovSettings : Settings, ISettings
    {
       
        private VideoSettingsController _videoSettingsController;
        private Slider uiItem;
        
        [Range(0,1)]
        [SerializeField] private float defaultVal = 0;
        [SerializeField] private TMP_Text label;
        private CinemachineVirtualCamera virtualCamera;
        private void OnEnable()
        {
            _videoSettingsController = FindObjectOfType<VideoSettingsController>();
            _videoSettingsController.ApplyAction += ApplyAction;
            _videoSettingsController.RestoreAction += RestoreAction;
        }

        private void OnDisable()
        {
            _videoSettingsController.ApplyAction -= ApplyAction;
            _videoSettingsController.RestoreAction -= RestoreAction;
        }

        public override void Awake()
        {
            uiItem = GetComponent<Slider>();
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            defaultValue = defaultVal;
           
            base.Awake();
            
            uiItem.Init(currentValue.ToFloat());
            label.text = FloatToText(defaultValue.ToFloat());
           
            uiItem.onValueChanged.AddListener((value) =>
            {
                currentValue = value;
                if(isLive) Apply();
                label.text = FloatToText(value);
            });
            
            Apply();
            
        }
        
        private void RestoreAction()
        {
            uiItem.value = defaultValue.ToInt(); // on change currentValue will be changed
            base.Save();
            if(!isLive) Apply(); // if Live then already applied this
        }
        private void ApplyAction()
        {
            base.Save();
            if(!isLive) Apply();  // if Live then already applied this
        }

        public void Apply()
        {
           Debug.Log($" virtualCamera.m_Lens.FieldOfView { virtualCamera.m_Lens.FieldOfView }");
                virtualCamera.m_Lens.FieldOfView = 60f + Mathf.Clamp01(currentValue.ToFloat()) * 60f; 
                Debug.Log($" virtualCamera.m_Lens.FieldOfView { virtualCamera.m_Lens.FieldOfView }");
            // float : 0 - 1, 60-120
            
        }

        
    }
    
  
    
}