using RawPushBGTask.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;

namespace RawPushBGTask
{
    public sealed class PushBGTask : IBackgroundTask
    {
        static String BGTaskName = "pushtask";
        EmployeeCheckInBG checkIn = new EmployeeCheckInBG();

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            #region RAW_BGTASK_ENABLED
            RawNotification notification = (RawNotification)taskInstance.TriggerDetails;
            string content = notification.Content;

            //var toastDescriptor = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            //var txtNodes = toastDescriptor.GetElementsByTagName("text");

            //txtNodes[0].AppendChild(toastDescriptor.CreateTextNode("Raw Notification"));
            //txtNodes[1].AppendChild(toastDescriptor.CreateTextNode(content));

            //var toast = new ToastNotification(toastDescriptor);
            //var toatNotifier = ToastNotificationManager.CreateToastNotifier();
            //toatNotifier.Show(toast);

            checkIn.storeEmployeeCheckIn(content, checkIn.checkNumber());
            Debugger.Break();
            #endregion
        }

        public async static void Register()
        {
            #region REGISTER_BG_TASK
            if (!IsTaskRegistered())
            {
                var result = await BackgroundExecutionManager.RequestAccessAsync();
                var builder = new BackgroundTaskBuilder();

                builder.Name = BGTaskName;
                builder.TaskEntryPoint = typeof(PushBGTask).FullName;
                builder.SetTrigger(new PushNotificationTrigger());
                BackgroundTaskRegistration task = builder.Register();
            }
            Debugger.Break();
            #endregion
        }

        public static bool IsTaskRegistered()
        {
            var taskRegistered = false;

            var entry = BackgroundTaskRegistration.AllTasks.FirstOrDefault(kvp => kvp.Value.Name == BGTaskName);
            
            if (entry.Value != null)
            {
                taskRegistered = true;
            }

            return taskRegistered;
        }

        public static void DisableTask()
        {
            var entry = BackgroundTaskRegistration.AllTasks.FirstOrDefault(kvp => kvp.Value.Name == BGTaskName);

            if (entry.Value != null)
            {
                entry.Value.Unregister(true);
            }
        }
    }
}
