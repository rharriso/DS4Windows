using System.ComponentModel;

namespace DS4Windows.DS4Control
{
    class RecordOutputSettings : INotifyPropertyChanged
    {
        string directory = Properties.Settings.Default.recordingOutputDirectory;
        int frequency = Properties.Settings.Default.recordingFrequency;
        private bool _isRecording;

        public string Directory
        {
            get => directory;
            set
            {
                directory = value;
                Properties.Settings.Default.recordingOutputDirectory = value;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("Directory");
            }
        }

        public int Frequency
        {
            get => frequency;
            set
            {
                frequency = value;
                Properties.Settings.Default.recordingFrequency = value;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("Frequency");
            }
        }

        public bool IsRecording
        {
            get => _isRecording;
            set {
                _isRecording = value;
                RaiseNotifyPropertyChanged("IsRecording");
            }
        }

        // allow property updates to be broadcasted
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseNotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


        // only allow the single shared instance            
        public static RecordOutputSettings _instance = new RecordOutputSettings();
        public static RecordOutputSettings Instance { get => _instance; }
        private RecordOutputSettings() { }
    }
}
