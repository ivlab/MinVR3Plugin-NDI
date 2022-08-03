using UnityEngine;
using Klak.Ndi;

namespace IVLab.MinVR3.NDI
{
    /// <summary>
    /// Wrapper around Klak.Ndi's NdiSender class to make it possible for it to use a rendertexture that is
    /// not created and associated with a camera until runtime as the source for the video stream.  This is
    /// useful for streaming in the planetarium because the fisheye rendering script we are using creates a
    /// rendertexture dynamically.  We cannot assign it as the sourceTexture for a NdiSender in the editor
    /// because it doesn't exist yet.
    /// </summary>
    public class NdiSenderForCameraRenderTexture : MonoBehaviour
    {
        public Camera sourceCamera {
            get { return m_SourceCamera; }
            set { m_SourceCamera = value; }
        }

        public string ndiStreamName {
            get { return m_NdiStreamName; }
            set { m_NdiStreamName = value; }
        }

        public NdiResources ndiResources {
            get { return m_NdiResources; }
            set { m_NdiResources = value; }
        }

        private void TrySetTexture()
        {
            if ((m_SourceCamera != null) && (m_SourceCamera.targetTexture != null)) {
                m_NdiSender.sourceTexture = m_SourceCamera.targetTexture;
                m_Initialized = true;
            }
        }

        private void Start()
        {
            m_Initialized = false;
            m_NdiSender = gameObject.AddComponent<NdiSender>();
            m_NdiSender.captureMethod = CaptureMethod.Texture;
            m_NdiSender.keepAlpha = false;
            m_NdiSender.ndiName = m_NdiStreamName;
            m_NdiSender.SetResources(m_NdiResources);
            // can't guarantee that the render texture will be created at this point because the
            // source camera's Start() method may not have been called yet.
            TrySetTexture();
        }

        private void Update()
        {
            if (!m_Initialized) {
                TrySetTexture();
            }
        }

        [Tooltip("This camera's targetTexture will serve as the source for the NDI stream.")]
        [SerializeField] private Camera m_SourceCamera;

        [Tooltip("Name for the NDI Stream.  Only one stream with this name can exist on the local network.")]
        [SerializeField] private string m_NdiStreamName = "Unity Dome Stream";
       
        [Tooltip("Set to the NdiResources.asset that ships with Klak NDI")]
        [SerializeField] NdiResources m_NdiResources = null;

        private NdiSender m_NdiSender;
        private bool m_Initialized = false;
    }

}