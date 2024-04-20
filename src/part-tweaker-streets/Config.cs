using Newtonsoft.Json;

namespace part_tweaker_streets
{
    public class Config
    {

        public float PegScale { get; set; }
        public float BarScale { get; set; }
        public float CrankScale { get; set; }
        public float SeatPostScale { get; set; }
        public float SeatPostHeight { get; set; }
        public float SeatHeight { get; set; }

        public void Save(string filePath)
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, json);

            Logger.Log("Config saved successfully.", Logger.LogLevel.Info);
        }

        public static Config Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Logger.Log("No config found, creating one...", Logger.LogLevel.Warning);
                
                return new Config()
                {
                    PegScale = 1f,
                    SeatPostScale = 1f,
                    BarScale = 1f,
                    CrankScale = 1f,
                    SeatPostHeight = 0f
                };
            }

            string json = File.ReadAllText(filePath);

            Logger.Log("Config loaded successfully.", Logger.LogLevel.Info);
            return JsonConvert.DeserializeObject<Config>(json);
        }
    }
}
