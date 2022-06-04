using CorePush.Google;
using DriverHire.Entity.NotificationModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static DriverHire.Entity.NotificationModels.GoogleNotification;

namespace DriverHire.Services.Infrastructure
{
    public interface IPushNotificationGenerator
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
    public class PushNotificationGenerator : IPushNotificationGenerator
    {
        private readonly IConfiguration _configuration;

        public PushNotificationGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {

                /* FCM Sender (Android Device) */
                FcmSettings settings = new FcmSettings()
                {
                    SenderId = _configuration["Notification:senderId"],
                    ServerKey = _configuration["Notification:serverKey"]
                };
                HttpClient httpClient = new HttpClient();

                string authorizationKey = string.Format("key={0}", settings.ServerKey);
                string deviceToken = notificationModel.DeviceId;

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                DataPayload dataPayload = new DataPayload()
                {
                    Title = notificationModel.Title,
                    Body = notificationModel.Body
                };
                GoogleNotification notification = new GoogleNotification
                {
                    Data = dataPayload,
                    Notification = dataPayload
                };

                var fcm = new FcmSender(settings, httpClient);
                var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                if (fcmSendResponse.IsSuccess())
                {
                    response.IsSuccess = true;
                    response.Message = "Notification sent successfully";
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = fcmSendResponse.Results[0].Error;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }
}

