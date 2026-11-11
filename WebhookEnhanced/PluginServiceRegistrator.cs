using Jellyfin.Data.Events.System;
using Jellyfin.Data.Events.Users;
using MediaBrowser.Common.Updates;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Events;
using MediaBrowser.Controller.Events.Authentication;
using MediaBrowser.Controller.Events.Session;
using MediaBrowser.Controller.Events.Updates;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Controller.Subtitles;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebhookEnhanced.Destinations;
using WebhookEnhanced.Destinations.Discord;
using WebhookEnhanced.Destinations.Generic;
using WebhookEnhanced.Destinations.GenericForm;
using WebhookEnhanced.Destinations.Gotify;
using WebhookEnhanced.Destinations.Mqtt;
using WebhookEnhanced.Destinations.Pushbullet;
using WebhookEnhanced.Destinations.Pushover;
using WebhookEnhanced.Destinations.Slack;
using WebhookEnhanced.Destinations.Smtp;
using WebhookEnhanced.Helpers;
using WebhookEnhanced.Notifiers;
using WebhookEnhanced.Notifiers.ItemAddedNotifier;
using WebhookEnhanced.Notifiers.ItemDeletedNotifier;
using WebhookEnhanced.Notifiers.UserDataSavedNotifier;

namespace WebhookEnhanced;

/// <summary>
/// Register webhook services.
/// </summary>
public class PluginServiceRegistrator : IPluginServiceRegistrator
{
    /// <inheritdoc />
    public void RegisterServices(IServiceCollection serviceCollection, IServerApplicationHost applicationHost)
    {
        HandlebarsFunctionHelpers.RegisterHelpers();

        serviceCollection.AddScoped<IWebhookClient<DiscordOption>, DiscordClient>();
        serviceCollection.AddScoped<IWebhookClient<GenericOption>, GenericClient>();
        serviceCollection.AddScoped<IWebhookClient<GenericFormOption>, GenericFormClient>();
        serviceCollection.AddScoped<IWebhookClient<GotifyOption>, GotifyClient>();
        serviceCollection.AddScoped<IWebhookClient<PushbulletOption>, PushbulletClient>();
        serviceCollection.AddScoped<IWebhookClient<PushoverOption>, PushoverClient>();
        serviceCollection.AddScoped<IWebhookClient<SlackOption>, SlackClient>();
        serviceCollection.AddScoped<IWebhookClient<SmtpOption>, SmtpClient>();
        serviceCollection.AddScoped<IWebhookClient<MqttOption>, MqttClient>();

        // Register sender.
        serviceCollection.AddScoped<IWebhookSender, WebhookSender>();

        // Register MqttClients
        serviceCollection.AddSingleton<IMqttClients, MqttClients>();

        /*-- Register event consumers. --*/
        // Library consumers.
        serviceCollection.AddScoped<IEventConsumer<SubtitleDownloadFailureEventArgs>, SubtitleDownloadFailureNotifier>();
        serviceCollection.AddSingleton<IItemAddedManager, ItemAddedManager>();
        serviceCollection.AddSingleton<IItemDeletedManager, ItemDeletedManager>();

        // Security consumers.
        serviceCollection.AddScoped<IEventConsumer<AuthenticationRequestEventArgs>, AuthenticationFailureNotifier>();
        serviceCollection.AddScoped<IEventConsumer<AuthenticationResultEventArgs>, AuthenticationSuccessNotifier>();

        // Session consumers.
        serviceCollection.AddScoped<IEventConsumer<PlaybackStartEventArgs>, PlaybackStartNotifier>();
        serviceCollection.AddScoped<IEventConsumer<PlaybackStopEventArgs>, PlaybackStopNotifier>();
        serviceCollection.AddScoped<IEventConsumer<PlaybackProgressEventArgs>, PlaybackProgressNotifier>();
        serviceCollection.AddScoped<IEventConsumer<SessionStartedEventArgs>, SessionStartNotifier>();

        // System consumers.
        serviceCollection.AddScoped<IEventConsumer<PendingRestartEventArgs>, PendingRestartNotifier>();
        serviceCollection.AddScoped<IEventConsumer<TaskCompletionEventArgs>, TaskCompletedNotifier>();

        // Update consumers.
        serviceCollection.AddScoped<IEventConsumer<PluginInstallationCancelledEventArgs>, PluginInstallationCancelledNotifier>();
        serviceCollection.AddScoped<IEventConsumer<InstallationFailedEventArgs>, PluginInstallationFailedNotifier>();
        serviceCollection.AddScoped<IEventConsumer<PluginInstalledEventArgs>, PluginInstalledNotifier>();
        serviceCollection.AddScoped<IEventConsumer<PluginInstallingEventArgs>, PluginInstallingNotifier>();
        serviceCollection.AddScoped<IEventConsumer<PluginUninstalledEventArgs>, PluginUninstalledNotifier>();
        serviceCollection.AddScoped<IEventConsumer<PluginUpdatedEventArgs>, PluginUpdatedNotifier>();

        // User consumers.
        serviceCollection.AddScoped<IEventConsumer<UserCreatedEventArgs>, UserCreatedNotifier>();
        serviceCollection.AddScoped<IEventConsumer<UserDeletedEventArgs>, UserDeletedNotifier>();
        serviceCollection.AddScoped<IEventConsumer<UserLockedOutEventArgs>, UserLockedOutNotifier>();
        serviceCollection.AddScoped<IEventConsumer<UserPasswordChangedEventArgs>, UserPasswordChangedNotifier>();
        serviceCollection.AddScoped<IEventConsumer<UserUpdatedEventArgs>, UserUpdatedNotifier>();

        serviceCollection.AddHostedService<WebhookServerEntryPoint>();
        serviceCollection.AddHostedService<ItemAddedNotifierEntryPoint>();
        serviceCollection.AddHostedService<ItemDeletedNotifierEntryPoint>();
        serviceCollection.AddHostedService<UserDataSavedNotifierEntryPoint>();
    }
}
