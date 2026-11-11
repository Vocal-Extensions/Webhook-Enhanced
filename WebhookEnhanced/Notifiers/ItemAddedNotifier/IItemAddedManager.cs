using MediaBrowser.Controller.Entities;

namespace WebhookEnhanced.Notifiers.ItemAddedNotifier;

/// <summary>
/// Item added manager interface.
/// </summary>
public interface IItemAddedManager
{
    /// <summary>
    /// Process the current queue.
    /// </summary>
    /// <param name="item">The added item.</param>
    public void AddItem(BaseItem item);

    /// <summary>
    /// Add item to process queue.
    /// </summary>
    /// <param name="item">The updated item.</param>
    public void HandleItemUpdated(BaseItem item);
}
