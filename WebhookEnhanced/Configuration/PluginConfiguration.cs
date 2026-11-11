using System;
using MediaBrowser.Model.Plugins;
using WebhookEnhanced.Destinations.Discord;
using WebhookEnhanced.Destinations.Generic;
using WebhookEnhanced.Destinations.GenericForm;
using WebhookEnhanced.Destinations.Gotify;
using WebhookEnhanced.Destinations.Mqtt;
using WebhookEnhanced.Destinations.Pushbullet;
using WebhookEnhanced.Destinations.Pushover;
using WebhookEnhanced.Destinations.Slack;
using WebhookEnhanced.Destinations.Smtp;
using WebhookEnhanced.Tester;

namespace WebhookEnhanced.Configuration;

/// <summary>
/// Webhook plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        ItemNotificationDelay = 5;
        ServerUrl = string.Empty;
        DiscordOptions = Array.Empty<DiscordOption>();
        GenericOptions = Array.Empty<GenericOption>();
        GenericFormOptions = Array.Empty<GenericFormOption>();
        GotifyOptions = Array.Empty<GotifyOption>();
        PushbulletOptions = Array.Empty<PushbulletOption>();
        PushoverOptions = Array.Empty<PushoverOption>();
        SlackOptions = Array.Empty<SlackOption>();
        SmtpOptions = Array.Empty<SmtpOption>();
        MqttOptions = Array.Empty<MqttOption>();
        TestOptions = Array.Empty<TestOption>();
    }

    /// <summary>
    /// Gets or sets the delay in seconds before firing item added/deleted webhooks.
    /// </summary>
    public int ItemNotificationDelay { get; set; }

    /// <summary>
    /// Gets or sets the jellyfin server url.
    /// </summary>
    public string ServerUrl { get; set; }

    /// <summary>
    /// Gets or sets the discord options.
    /// </summary>
    public DiscordOption[] DiscordOptions { get; set; }

    /// <summary>
    /// Gets or sets the generic options.
    /// </summary>
    public GenericOption[] GenericOptions { get; set; }

    /// <summary>
    /// Gets or sets the generic form options.
    /// </summary>
    public GenericFormOption[] GenericFormOptions { get; set; }

    /// <summary>
    /// Gets or sets the gotify options.
    /// </summary>
    public GotifyOption[] GotifyOptions { get; set; }

    /// <summary>
    /// Gets or sets the pushbullet options.
    /// </summary>
    public PushbulletOption[] PushbulletOptions { get; set; }

    /// <summary>
    /// Gets or sets the pushover options.
    /// </summary>
    public PushoverOption[] PushoverOptions { get; set; }

    /// <summary>
    /// Gets or sets the slack options.
    /// </summary>
    public SlackOption[] SlackOptions { get; set; }

    /// <summary>
    /// Gets or sets the smtp options.
    /// </summary>
    public SmtpOption[] SmtpOptions { get; set; }

    /// <summary>
    /// Gets or sets the mqtt options.
    /// </summary>
    public MqttOption[] MqttOptions { get; set; }

    /// <summary>
    /// Gets or sets the test options.
    /// </summary>
    public TestOption[] TestOptions { get; set; }
}
