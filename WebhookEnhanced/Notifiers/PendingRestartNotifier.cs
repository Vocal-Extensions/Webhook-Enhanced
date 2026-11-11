using System.Threading.Tasks;
using Jellyfin.Data.Events.System;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Events;
using WebhookEnhanced.Destinations;
using WebhookEnhanced.Helpers;

namespace WebhookEnhanced.Notifiers;

/// <summary>
/// Pending restart notifier.
/// </summary>
public class PendingRestartNotifier : IEventConsumer<PendingRestartEventArgs>
{
    private readonly IServerApplicationHost _applicationHost;
    private readonly IWebhookSender _webhookSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="PendingRestartNotifier"/> class.
    /// </summary>
    /// <param name="applicationHost">Instance of the <see cref="IServerApplicationHost"/> interface.</param>
    /// <param name="webhookSender">Instance of the <see cref="IWebhookSender"/> interface.</param>
    public PendingRestartNotifier(
        IServerApplicationHost applicationHost,
        IWebhookSender webhookSender)
    {
        _applicationHost = applicationHost;
        _webhookSender = webhookSender;
    }

    /// <inheritdoc />
    public async Task OnEvent(PendingRestartEventArgs eventArgs)
    {
        var dataObject = DataObjectHelpers
            .GetBaseDataObject(_applicationHost, NotificationType.PendingRestart);

        await _webhookSender.SendNotification(NotificationType.PendingRestart, dataObject)
            .ConfigureAwait(false);
    }
}
