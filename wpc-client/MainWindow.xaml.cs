using Microsoft.Bot.Schema;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Dialog;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpc_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool MicActive { get; set; }
        public bool FirstInteraction { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MicActive = false;

            InitConnector();
        }

        #region Speech Events
        public void InitConnector() {

            DialogServiceConfig config = BotFrameworkConfig.FromSubscription("f9c8910cd51c4c6a88ee5e55e68c5b89", "westeurope");
            config.Language = "en-us";
            //config.SetProperty(PropertyId.SpeechServiceConnection_SynthLanguage, "en-us");
            //config.SetProperty(PropertyId. Speech_SegmentationSilenceTimeoutMs, "5000");
            //config.SetProperty(PropertyId.Conversation_From_Id, "test 30");

          
            AudioConfig audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        
            connector = new DialogServiceConnector(config, audioConfig);

            connector.ActivityReceived += Connector_ActivityReceived;
            //this.player.PlaybackStopped += Player_PlaybackStopped;
            connector.SessionStopped += Connector_SessionStopped;
            connector.SessionStarted += Connector_SessionStarted;
            connector.TurnStatusReceived += Connector_TurnStatusReceived;
            connector.Canceled += Connector_Canceled;
            connector.SpeechEndDetected += Connector_SpeechEndDetected;
            connector.SpeechStartDetected += Connector_SpeechStartDetected;
         
            
            connector.ConnectAsync().GetAwaiter().GetResult();

        }

        private void Connector_SpeechStartDetected(object? sender, RecognitionEventArgs e)
        {
            Dispatcher.BeginInvoke(
               new ThreadStart(() =>
               {
                   Notifiche.Text += $"\n{DateTime.Now} SpeechStartDetected";
                 
               }
             ));
        }

        private void Connector_SpeechEndDetected(object? sender, RecognitionEventArgs e)
        {
            Dispatcher.BeginInvoke(
              new ThreadStart(() =>
              {
                  Notifiche.Text += $"\n{DateTime.Now} SpeechEndDetected";
                 
              }
            ));
        }

        private void Connector_Canceled(object? sender, Microsoft.CognitiveServices.Speech.SpeechRecognitionCanceledEventArgs e)
        {
            Dispatcher.BeginInvoke(
               new ThreadStart(() =>
               {
                   Notifiche.Text += $"\n{DateTime.Now} cancelled";
                 
               }
             ));
        }

        private void Connector_TurnStatusReceived(object? sender, TurnStatusReceivedEventArgs e)
        {
            Dispatcher.BeginInvoke(
               new ThreadStart(() =>
               {
                   Notifiche.Text += $"\n{DateTime.Now} TurnStatusReceived";
                  
               }
             ));
        }

        private void Connector_SessionStarted(object? sender, Microsoft.CognitiveServices.Speech.SessionEventArgs e)
        {
            Dispatcher.BeginInvoke(
                     new ThreadStart(() =>
                     {
                         Notifiche.Text += $"\n{DateTime.Now} session started";
                     
                     }
                   ));
        }

        private void Connector_SessionStopped(object? sender, Microsoft.CognitiveServices.Speech.SessionEventArgs e)
        {
            Dispatcher.BeginInvoke(
                      new ThreadStart(() =>
                      {
                          Notifiche.Text += $"\n{DateTime.Now} session stopped";
                        
                      }
                    ));
        }

        public ListenState ListeningState = ListenState.NotListening;
        private DirectSoundOut player = new DirectSoundOut();
        private Queue<WavQueueEntry> playbackStreams = new Queue<WavQueueEntry>();
        private DialogServiceConnector connector = null;

        private void Connector_ActivityReceived(object sender, ActivityReceivedEventArgs e)
        {           

            var json = e.Activity;
            var activity = JsonConvert.DeserializeObject<Activity>(json);

            if (activity.Type == ActivityTypes.Message)
            {
                Dispatcher.BeginInvoke(
                     new ThreadStart(() =>
                     {
                         Notifiche.Text += $"\n{DateTime.Now} Message: {activity.Text} - {activity.Locale}";
                        
                     }
                   ));
            }        
            else if(activity.Type == ActivityTypes.ConversationUpdate)
            {
                Dispatcher.BeginInvoke(
                     new ThreadStart(() =>
                     {
                         Notifiche.Text += $"\n{DateTime.Now} Conversationupdate";
                        
                     }
                   ));
            }

        }

        private WavQueueEntry PeeKFromAudioQueue()
        {
            WavQueueEntry entry = null;
            lock (playbackStreams)
            {
                if (playbackStreams.Count > 0)
                {
                    entry = playbackStreams.Peek();
                }
                else
                {
                    int n = 0;
                }

            }

            return entry;
        }

        private bool PlayFromAudioQueue(WavQueueEntry entry)
        {
            
            if (entry != null)
            {
                System.Diagnostics.Debug.WriteLine($"START playing {entry.Id}");
               
                lock (player)
                {
                    player.Init(entry.Reader);
                    player.Play();
                }
                return true;
            }
            else
            {
                int n = 0;
            }

            return false;
        }

        private void Player_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            lock (this.playbackStreams)
            {
                if (this.playbackStreams.Count == 0)
                {
                    return;
                }
                //Thread.Sleep(500);
                //var entry = this.playbackStreams.Dequeue();
                //entry.Stream.Close();
            }
          
        }


        private async Task OpenMic()
        {
            StopAnyTTSPlayback();

            if (ListeningState == ListenState.NotListening)
            {
                await StartListening();
            }
            else
            {
                // Todo: canceling listening not supported
            }
        }

        private void StopAnyTTSPlayback()
        {
            lock (playbackStreams)
            {
                playbackStreams.Clear();
            }

            if (player.PlaybackState == PlaybackState.Playing)
            {
                player.Stop();
            }
        }

        private async Task StartListening()
        {
            if (ListeningState == ListenState.NotListening)
            {               

                try
                {
                    ListeningState = ListenState.Initiated;

                    await connector.ListenOnceAsync();

                    ListeningState = ListenState.NotListening;

                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
#pragma warning restore CA1031 // Do not catch general exception types
            }
        }
        #endregion

        #region UI Events
        private async void micBtn_Click(object sender, RoutedEventArgs e)
        {
            micBtn.Background = new SolidColorBrush(Colors.Red);

            await OpenMic();

            micBtn.Background = new SolidColorBrush(Colors.White);
        } 
              
        private void reconnBtn_Click(object sender, RoutedEventArgs e)
        {           

            Dispatcher.BeginInvoke(
                        new ThreadStart(() =>
                        {
                            Notifiche.Text = $"";
                           
                        }
                     ));

            StopAnyTTSPlayback();

            connector.Dispose();

            InitConnector();
        }

        #endregion

    }
}
