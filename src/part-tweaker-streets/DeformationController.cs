using MelonLoader;
using Newtonsoft.Json;
using UnityEngine;

namespace part_tweaker_streets
{
    public class DeformationController
    {
        private float PegScale;
        private float SeatPostScale;
        private float BarScale;
        private float CrankScale;
        private float SeatPostHeight;
        private float SeatHeight;

        private Config config;
        private readonly string configFilePath = Path.Combine(Application.persistentDataPath, "part_tweaker.json");

        public DeformationController()
        {
            config = Config.Load(configFilePath);

            PegScale = config.PegScale;
            BarScale = config.BarScale;
            CrankScale = config.CrankScale;
            SeatHeight = config.SeatHeight;
            SeatPostScale = config.SeatPostScale;
            SeatPostHeight = config.SeatPostHeight;
        }

        public void UpdatePartDeformations()
        {
            RescalePegLength();
            RescaleSeatPost();
            RescaleBars();
            RescaleCranks();
            SetPostHeight();
        }

        // Menu GUI layout
        public void DrawMenuGUI()
        {
            GUI.backgroundColor = Color.gray;
            GUI.color = Color.white;
            GUI.Box(new Rect(10f, 10f, 170f, 280f), " ");

            GUI.backgroundColor = Color.magenta;
            GUI.color = Color.white;
            GUI.Label(new Rect(20f, 20f, 150f, 20f), "Peg Length");
            GUI.color = Color.magenta;
            PegScale = GUI.HorizontalSlider(new Rect(20f, 40f, 150f, 20f), PegScale, 0f, 4f);

            GUI.color = Color.white;
            GUI.Label(new Rect(20f, 60f, 150f, 20f), "Post Length");
            GUI.color = Color.magenta;
            SeatPostScale = GUI.HorizontalSlider(new Rect(20f, 80f, 150f, 20f), SeatPostScale, 0f, 4f);

            GUI.color = Color.white;
            GUI.Label(new Rect(20f, 100f, 150f, 20f), "Post Height");
            GUI.color = Color.magenta;
            SeatPostHeight = GUI.HorizontalSlider(new Rect(20f, 120f, 150f, 20f), SeatPostHeight, 0f, 2f);

            GUI.color = Color.white;
            GUI.Label(new Rect(20f, 140f, 150f, 20f), "Bar Scale");
            GUI.color = Color.magenta;
            BarScale = GUI.HorizontalSlider(new Rect(20f, 160f, 150f, 20f), BarScale, 0f, 2f);

            GUI.color = Color.white;
            GUI.Label(new Rect(20f, 180f, 150f, 20f), "Crank Length");
            GUI.color = Color.magenta;
            CrankScale = GUI.HorizontalSlider(new Rect(20f, 200f, 150f, 20f), CrankScale, 0f, 2f);

            GUI.color = Color.white;
            bool Save = GUI.Button(new Rect(20f, 220f, 150f, 20f), "Save");
            if (Save)
            {
                config.Save(configFilePath);
            }

            GUI.color = Color.white;
            bool Reset = GUI.Button(new Rect(20f, 260f, 150f, 20f), "Reset");
            if (Reset)
            {
                ResetValues();
                UpdatePartDeformations();
            }
        }

        // Methods for applying part deformations
        private void RescalePegLength()
        {
            GameObject.Find("BMX_Peg_BackRight_EquipSlot").transform.localScale = new Vector3(PegScale, 1f, 1f);
            GameObject.Find("BMX_Peg_BackLeft_EquipSlot").transform.localScale = new Vector3(PegScale, 1f, 1f);
            GameObject.Find("BMX_Peg_FrontRight_EquipSlot").transform.localScale = new Vector3(PegScale, 1f, 1f);
            GameObject.Find("BMX_Peg_FrontLeft_EquipSlot").transform.localScale = new Vector3(PegScale, 1f, 1f);

            config.PegScale = PegScale;
        }

        private void RescaleSeatPost()
        {
            GameObject.Find("Seat Post Equip Root").transform.GetChild(1).transform.localScale = new Vector3(1f, SeatPostScale, 1f);

            config.SeatPostScale = SeatPostScale;
        }

        private void SetPostHeight()
        {
            Vector3 SeatPostPosition = GameObject.Find("Seat Post Equip Root").transform.GetChild(1).transform.localPosition;
            Vector3 SeatPosition = GameObject.Find("BMX_Seat_EquipSlot").transform.localPosition;
            GameObject.Find("Seat Post Equip Root").transform.GetChild(1).transform.localPosition = new Vector3(SeatPostPosition.x, SeatPostHeight, SeatPostPosition.z);

            // Apparently 0.03f does the job for most post deformations
            SeatHeight = SeatPostHeight + 0.03f;
            GameObject.Find("BMX_Seat_EquipSlot").transform.localPosition = new Vector3(SeatPosition.x, SeatHeight, SeatPosition.z);

            config.SeatPostHeight = SeatPostHeight;
            config.SeatHeight = SeatHeight;
        }

        private void RescaleBars()
        {
            GameObject.Find("BMX_Bars_EquipSlot").transform.localScale = new Vector3(BarScale, BarScale, BarScale);

            config.BarScale = BarScale;
        }

        private void RescaleCranks()
        {
            GameObject.Find("BMX_Cranks_Left_EquipSlot").transform.localScale = new Vector3(1f, 1f, CrankScale);
            GameObject.Find("BMX_Cranks_Right_EquipSlot").transform.localScale = new Vector3(1f, 1f, CrankScale);

            config.CrankScale = CrankScale;
        }

        // Method to reset values to their defaults
        private void ResetValues()
        {
            PegScale = 1f;
            SeatPostScale = 1f;
            BarScale = 1f;
            CrankScale = 1f;
            SeatPostHeight = 0f;
            SeatHeight = SeatPostHeight - 0.03f;
        }
    }
}
