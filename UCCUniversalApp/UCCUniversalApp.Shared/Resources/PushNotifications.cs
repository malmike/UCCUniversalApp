using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCUniversalApp.ViewModels;
using Windows.Foundation;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;

namespace UCCUniversalApp.Resources
{
    public class PushNotifications
    {

        public PushNotificationChannel channel { get; private set; }
        private SharedInformation sharedInformation = SharedInformation.getInstance();
        private AppSettings appSettings = new AppSettings();
        private UpdatePushURI updatePushURI = new UpdatePushURI();
        private OnlineURI webURI = new OnlineURI();

        public async void Initilialize()
        {
            channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            channel.PushNotificationReceived += channel_PushNotificationReceived;
            Debug.WriteLine(channel.Uri.ToString());
            
            if (!appSettings.verifyRegistrationSettings())
            {
                sharedInformation.pushURI = channel.Uri.ToString();
                sharedInformation.storePushURI(channel.Uri.ToString());

            }
            else if(appSettings.verifyRegistrationSettings() & appSettings.retrievePushURISettings() == null)
            {
                sharedInformation.pushURI = channel.Uri.ToString();
                sharedInformation.storePushURI(channel.Uri.ToString());
                updatePushURI.ChangePushURI(webURI.updatePushURI, channel.Uri.ToString(), appSettings.retrieveDeviceSettings(), sharedInformation.empData.Serial);

            }
            else if (appSettings.verifyRegistrationSettings() & appSettings.retrievePushURISettings() != channel.Uri.ToString())
            {
                sharedInformation.pushURI = channel.Uri.ToString();
                sharedInformation.storePushURI(channel.Uri.ToString());
                updatePushURI.ChangePushURI(webURI.updatePushURI, channel.Uri.ToString(), appSettings.retrieveDeviceSettings(), sharedInformation.empData.Serial);

            }

            
        }

        private void channel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            switch (args.NotificationType)
            {
                case PushNotificationType.Raw:
                    deliverRawNotification(sender, args.RawNotification);
                    break;
                case PushNotificationType.Badge:
                    deliverBadgeNotification(sender, args.BadgeNotification);
                    break;
                case PushNotificationType.Tile:
                    deliverTileNotification(sender, args.TileNotification);
                    break;
                case PushNotificationType.Toast:
                    deliverToastNotification(sender, args);
                    break;
            }
        }

        public event TypedEventHandler<PushNotificationChannel, PushNotificationReceivedEventArgs> deliverToastNotification;
        public event TypedEventHandler<PushNotificationChannel, TileNotification> deliverTileNotification;
        public event TypedEventHandler<PushNotificationChannel, RawNotification> deliverRawNotification;
        public event TypedEventHandler<PushNotificationChannel, BadgeNotification> deliverBadgeNotification;

    }
}
