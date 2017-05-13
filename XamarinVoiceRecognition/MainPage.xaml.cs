using System;
using System.Threading.Tasks;
using Plugin.BingSpeech;
using Xamarin.Forms;

namespace XamarinVoiceRecognition
{
    public partial class MainPage : ContentPage
    {
        private const string FILENAME = "recording.wav";
        private const string BING_SPEECH_API_KEY = "YOUR_API_KEY";

        public MainPage()
        {
            InitializeComponent();

            BingSpeech.Current.BingSpeechService
               .InitializeService(BING_SPEECH_API_KEY);
        }

        private async void recordButton_Clicked(object sender, EventArgs e)
        {
            if (!activityIndicator.IsVisible)
            {
                ChangeState();

                BingSpeech.Current.MicrophoneService
                    .StartRecording(FILENAME);

                return;
            }

            BingSpeech.Current.MicrophoneService
                .StopRecording();

            label.Text = await BingSpeech.Current.BingSpeechService
                .GetTextResult(FILENAME);

            ChangeState();
        }

        private void ChangeState()
        {
            activityIndicator.IsVisible = !activityIndicator.IsVisible;
            activityIndicator.IsRunning = !activityIndicator.IsRunning;

            recordButton.Text = activityIndicator.IsVisible ?
                 "Stop Recording" :
                 "Start Recording";
        }
    }
}
