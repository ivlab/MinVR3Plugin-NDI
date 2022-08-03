using UnityEditor;
using UnityEngine;
using System;
using Klak.Ndi;
using ES;
using IVLab.MinVR3.Digistar;

// disable warnings about unused functions because these editor menu functions can look to the compiler
// as though they are never called
#pragma warning disable IDE0051

namespace IVLab.MinVR3.NDI
{

    public class Menu_GameObject_MinVR_NDI : MonoBehaviour
    {
        [MenuItem("GameObject/MinVR/VRConfig/VRConfig_BellPlanetarium (via Klak NDI, E&S Fisheye)", false, MenuHelpers.vrConfigSec2Priority)]
        public static void CreateVRConfigKlakNDIDome(MenuCommand command)
        {
            MenuHelpers.CreateVREngineIfNeeded();
            MenuHelpers.CreateRoomSpaceOriginIfNeeded();

            GameObject inputDevicesChild = null;
            GameObject displayDevicesChild = null;
            GameObject eventAliasesChild = null;
            GameObject vrConfigObj = MenuHelpers.CreateVRConfigTemplate(command, "Bell Planetarium (Klak  NDI, E&S Fisheye)", ref inputDevicesChild, ref displayDevicesChild, ref eventAliasesChild);

            // the way these objects work together is a bit tricky, so going for very descripting names :)
            string n1 = "CubeMap Camera (Renders the Scene to Each Face of a CubeMap)";
            string n2 = "Fisheye Camera (Renders Fisheye Projection from CubeMap to RenderTexture)";
            string n3 = "Display Camera (Copies Fisheye Camera's RenderTexture to the Screen)";
            string n4 = "Klak NDI Stream (Streams Fisheye Camera's RenderTexture using Klak's NDI)";

            var cubeMapObj = MenuHelpers.CreateAndPlaceGameObject(n1, displayDevicesChild,
                typeof(Camera), typeof(AudioListener));
            var cubeMapCam = cubeMapObj.GetComponent<Camera>();
            cubeMapCam.transform.rotation = Quaternion.Euler(new Vector3(-75, 0, 0));
            cubeMapCam.tag = "MainCamera";

            var fishEyeObj = MenuHelpers.CreateAndPlaceGameObject(n2, displayDevicesChild,
                typeof(Camera), typeof(FisheyeRenderer));
            var fishEyeCam = fishEyeObj.GetComponent<Camera>();
            var fishEyeRend = fishEyeObj.GetComponent<FisheyeRenderer>();
            fishEyeRend.sceneCamera = cubeMapCam;
            fishEyeRend.autoSelectCamera = false;

            var dispObj = MenuHelpers.CreateAndPlaceGameObject(n3, displayDevicesChild,
                typeof(Camera), typeof(RenderTextureFromOtherCamera));
            var dispCam = dispObj.GetComponent<Camera>();
            dispCam.cullingMask = 0;
            dispCam.clearFlags = CameraClearFlags.SolidColor;
            var rendOther = dispObj.GetComponent<RenderTextureFromOtherCamera>();
            rendOther.sourceCamera = fishEyeCam;

            var ndiObj = MenuHelpers.CreateAndPlaceGameObject(n4, displayDevicesChild,
                typeof(NdiSenderForCameraRenderTexture));
            var ndiSend = ndiObj.GetComponent<NdiSenderForCameraRenderTexture>();
            ndiSend.sourceCamera = fishEyeCam;
            ndiSend.ndiStreamName = "Unity Dome Stream";
            string[] ndiResourcesAssets = AssetDatabase.FindAssets("NdiResources t:NdiResources", new string[] { "Packages" });
            if (ndiResourcesAssets.Length > 0) {
                ndiSend.ndiResources = AssetDatabase.LoadAssetAtPath<NdiResources>(
                    AssetDatabase.GUIDToAssetPath(ndiResourcesAssets[0]));
            }
            Selection.activeGameObject = vrConfigObj;
        }

    } // end class

} // end namespace

#pragma warning restore IDE0051
