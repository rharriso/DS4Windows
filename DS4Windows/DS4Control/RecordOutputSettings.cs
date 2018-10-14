using System;
using System.ComponentModel;

namespace DS4Windows.DS4Control
{
    class RecordOutputSettings : INotifyPropertyChanged
    {
        private string _directory = Properties.Settings.Default.recordingOutputDirectory;
        private int _captureFrequency = Properties.Settings.Default.captureFrequency;
        private int _frequency = Properties.Settings.Default.recordingFrequency;
        private int _recordingLength = Properties.Settings.Default.recordingLength;
        private bool _isRecording;

        // recording frequencies in frames per second
        private static int[] RECORDING_FREQUENCIES = { 5, 10, 20, 30 };
        private static int[] IMG_CAPTURE_FREQUENCIES = { 10, 20, 30, 60 };

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
        }

        public int FrequencyIndex {
            get => Array.IndexOf(RECORDING_FREQUENCIES, _frequency);
            set
            {
                var index = Math.Min(Math.Max(value, 0), RECORDING_FREQUENCIES.Length - 1);
                _frequency = RECORDING_FREQUENCIES[index];
                Properties.Settings.Default.recordingFrequency = _frequency;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("Frequency");
            }
        }

        public int CaptureFrequency
        {
            get => _captureFrequency;
        }

        public int CaptureFrequencyIndex {
            get => Array.IndexOf(IMG_CAPTURE_FREQUENCIES, _captureFrequency);
            set
            {
                var index = Math.Min(Math.Max(value, 0), IMG_CAPTURE_FREQUENCIES.Length - 1);
                _captureFrequency = IMG_CAPTURE_FREQUENCIES[index];
                Properties.Settings.Default.captureFrequency = _captureFrequency;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("CaptureFrequency");
            }
        }

        public int RecordingLength
        {
            get => _recordingLength;
            set
            {
                _recordingLength = value;
                Properties.Settings.Default.recordingLength = value;
                Properties.Settings.Default.Save();
                RaiseNotifyPropertyChanged("RecordingLength");
            }
        }
        public bool IsRecording
        {
            get => _isRecording;
            set
            {
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
