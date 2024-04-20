using MelonLoader;
using Newtonsoft.Json;
using System.Reflection;
using UnityEngine;

namespace part_tweaker_streets
{
    public class PartTweaker : MelonMod
    {
        private bool menuOpen = false;
        private DeformationController deformationController;
        private GameObject bmxBody;

        // Get the assembly where the MelonInfo attribute is defined
        private readonly Assembly melonAssembly = Assembly.GetExecutingAssembly();

        public override void OnInitializeMelon()
        {
            // Get the MelonInfo attribute
            var melonInfoAttribute = (MelonInfoAttribute)melonAssembly.GetCustomAttribute(typeof(MelonInfoAttribute));

            if (melonInfoAttribute == null)
            {
                Logger.Log("No MelonInfo attribute found!", Logger.LogLevel.Error);
                return;
            }

            Logger.Log($"{melonInfoAttribute.Name} v{melonInfoAttribute.Version} by {melonInfoAttribute.Author} - Initializing...", Logger.LogLevel.Info);
            
            // Initialize DeformationController
            deformationController = new DeformationController();
        }

        public override void OnUpdate()
        {
            if (!bmxBody)
            {
                return;
            }

            // Toggle menu visibility
            if (Input.GetKeyDown(KeyCode.X))
            {
                menuOpen = !menuOpen;

                if (!menuOpen)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    return;
                }

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            // Apply part deformations
            deformationController.UpdatePartDeformations();
        }

        public override void OnGUI()
        {
            // Display menu GUI if open
            if (!menuOpen)
            {
                return;
            }

            deformationController.DrawMenuGUI();
        }

        // Get the bmx body game object when a scene is loaded
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "Bridging Physics PIPE Style")
            {
                bmxBody = GameObject.Find("BMXS Game World PIPE Sytle/Player Components/BMX Components (PIPE STYLE) 2/BMX Body");
            }
        }
    }
}
