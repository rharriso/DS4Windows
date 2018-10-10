using System.ComponentModel;

namespace DS4Windows.DS4Control
{
    class RecordOutputSettings : INotifyPropertyChanged
    {
        private string _directory = Properties.Settings.Default.recordingOutputDirectory;
        private int _frequency = Properties.Settings.Default.recordingFrequency;
        private int _recordingLength = Properties.Settings.Default.recordingLength;
        private bool _isRecording;

        public string Directory
        {
            get => _directory;
            set
            {
                _directory = value;
                Properties.Settings.Default.recordingOutputDirectory = value;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("Directory");
            }
        }

        public int Frequency
        {
            get => _frequency;
            set
            {
                _frequency = value;
                Properties.Settings.Default.recordingFrequency = value;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("Frequency");
            }
        }

        
        public int RecordingLength
        {
            get => _recordingLength;
            set
            {
                _recordingLength= value;
                Properties.Settings.Default.recordingLength = value;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("RecordingLength");
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
