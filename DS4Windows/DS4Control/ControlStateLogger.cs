using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace DS4Windows.DS4Control
{
    class ControlStateLogger
    {
        private static List<DS4State> states = new List<DS4State>();
        private static MemoryStream memStream = new MemoryStream();

        public static void LogState(DS4State s)
        {
            var stateCopy = new DS4State(s);
            var time = stateCopy.ReportTimeStamp
                .ToFileTimeUtc()
                .ToString();
            states.Add(stateCopy);

            // capture screenshot

            if (RecordOutputSettings.Instance.RecordingLength <= states.Count)
            {
                // log
                var path = RecordOutputSettings.Instance.Directory + '\\' + time + ".json";
                var serializer = new DataContractJsonSerializer(typeof(List<DS4State>));
                memStream.Position = 0;

                using (var file = File.Create(path))
                {
                    serializer.WriteObject(file, states);
                }

                // empty the states list
                states = new List<DS4State>();
            }
        }

        public static void SaveScreenshot(DS4State state)
        {
            var time = state.ReportTimeStamp
                .ToFileTimeUtc()
                .ToString();
            var imagePath = RecordOutputSettings.Instance.Directory + '\\' + time + ".png";

            var bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                bitmap.Save(imagePath, ImageFormat.Png);
            }

        }
    }
}
