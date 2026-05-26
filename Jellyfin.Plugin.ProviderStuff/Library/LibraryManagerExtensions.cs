using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;

namespace Jellyfin.Plugin.ProviderStuff.Library;

/// <summary>
/// Jellyfin 10.11 changed ILibraryManager.GetItemList(InternalItemsQuery) to return
/// IReadOnlyList instead of List. Direct calls compiled against 10.9 packages fail at runtime on 10.11+.
/// </summary>
internal static class LibraryManagerExtensions
{
    private static readonly MethodInfo GetItemListMethod = typeof(ILibraryManager).GetMethod(
        nameof(ILibraryManager.GetItemList),
        BindingFlags.Public | BindingFlags.Instance,
        binder: null,
        types: new[] { typeof(InternalItemsQuery) },
        modifiers: null)
        ?? throw new InvalidOperationException("ILibraryManager.GetItemList(InternalItemsQuery) was not found.");

    /// <summary>
    /// Returns library items for a query, compatible with Jellyfin 10.9 and 10.11+.
    /// </summary>
    /// <param name="libraryManager">Library manager instance.</param>
    /// <param name="query">Item query.</param>
    /// <returns>Matching items.</returns>
    public static IReadOnlyList<BaseItem> GetItemsList(this ILibraryManager libraryManager, InternalItemsQuery query)
    {
        ArgumentNullException.ThrowIfNull(libraryManager);
        ArgumentNullException.ThrowIfNull(query);

        var result = GetItemListMethod.Invoke(libraryManager, new object[] { query });
        return result switch
        {
            null => Array.Empty<BaseItem>(),
            List<BaseItem> list => list,
            IReadOnlyList<BaseItem> readOnly => readOnly,
            IEnumerable<BaseItem> enumerable => enumerable.ToList(),
            _ => throw new InvalidOperationException(
                $"Unexpected return type from GetItemList: {result.GetType().FullName}")
        };
    }
}
