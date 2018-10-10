using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DS4Windows.DS4Control
{
    class ControlStateLogger
    {
        private static List<DS4State> states = new List<DS4State>();
        private static MemoryStream memStream = new MemoryStream();

        public static void Log(DS4State s)
        {
            states.Add(new DS4State(s));

            if (RecordOutputSettings.Instance.RecordingLength <= states.Count)
            {
                // log
                var time = states[0].ReportTimeStamp
                    .ToFileTimeUtc()
                    .ToString();
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
    }
}
