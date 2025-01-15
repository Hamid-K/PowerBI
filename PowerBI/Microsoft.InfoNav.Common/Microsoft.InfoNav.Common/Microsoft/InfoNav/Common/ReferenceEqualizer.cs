using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200006C RID: 108
	internal sealed class ReferenceEqualizer<T> where T : class
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000A824 File Offset: 0x00008A24
		internal ReferenceEqualizer(IEqualityComparer<T> valueComparer = null)
		{
			valueComparer = valueComparer ?? EqualityComparer<T>.Default;
			this._map = new Dictionary<T, T>(valueComparer);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A844 File Offset: 0x00008A44
		internal bool TryAdd(T entry, out T existingEntry)
		{
			if (this._map.TryGetValue(entry, out existingEntry))
			{
				return false;
			}
			this._map.Add(entry, entry);
			existingEntry = entry;
			return true;
		}

		// Token: 0x040000DE RID: 222
		private readonly Dictionary<T, T> _map;
	}
}
