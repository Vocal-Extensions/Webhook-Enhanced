using System.Threading.Tasks;
using Jellyfin.Data.Events.Users;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Events;
using WebhookEnhanced.Destinations;
using WebhookEnhanced.Helpers;

namespace WebhookEnhanced.Notifiers;

/// <summary>
/// User locked out notifier.
/// </summary>
public class UserLockedOutNotifier : IEventConsumer<UserLockedOutEventArgs>
{
    private readonly IServerApplicationHost _applicationHost;
    private readonly IWebhookSender _webhookSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserLockedOutNotifier"/> class.
    /// </summary>
    /// <param name="applicationHost">Instance of the <see cref="IServerApplicationHost"/> interface.</param>
    /// <param name="webhookSender">Instance of the <see cref="IWebhookSender"/> interface.</param>
    public UserLockedOutNotifier(
        IServerApplicationHost applicationHost,
        IWebhookSender webhookSender)
    {
        _applicationHost = applicationHost;
        _webhookSender = webhookSender;
    }

    /// <inheritdoc />
    public async Task OnEvent(UserLockedOutEventArgs eventArgs)
    {
        var dataObject = DataObjectHelpers
            .GetBaseDataObject(_applicationHost, NotificationType.UserLockedOut)
            .AddUserData(eventArgs.Argument);

        await _webhookSender.SendNotification(NotificationType.UserLockedOut, dataObject)
            .ConfigureAwait(false);
    }
}
