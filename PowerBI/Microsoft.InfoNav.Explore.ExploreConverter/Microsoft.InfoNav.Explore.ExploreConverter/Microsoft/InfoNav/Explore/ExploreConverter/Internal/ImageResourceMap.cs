using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200005C RID: 92
	internal sealed class ImageResourceMap
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000B2C4 File Offset: 0x000094C4
		internal ImageResourceMap()
		{
			this._map = new Dictionary<string, ImageResource>();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B2D8 File Offset: 0x000094D8
		public void Insert(string key, byte[] bytes)
		{
			if (this._map.ContainsKey(key))
			{
				this._map.Remove(key);
			}
			ImageResource imageResource = new ImageResource(bytes);
			this._map.Add(key, imageResource);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B314 File Offset: 0x00009514
		public byte[] Find(string key)
		{
			ImageResource imageResource;
			Contract.Check(this._map.TryGetValue(key, out imageResource), StringUtil.FormatInvariant("Expected to find item with name: {0}", key));
			return imageResource.Bytes;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B345 File Offset: 0x00009545
		public List<PVResourceEntry> ConvertToDocumentImageResourceMap()
		{
			return (from kvp in this._map.ToList<KeyValuePair<string, ImageResource>>()
				select new PVResourceEntry
				{
					ResourceId = kvp.Key,
					ImageBytes = kvp.Value.EncodedBytes
				}).ToList<PVResourceEntry>();
		}

		// Token: 0x04000153 RID: 339
		private Dictionary<string, ImageResource> _map;
	}
}
