using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F8 RID: 504
	internal struct LineageMap<TObjectWithTag> where TObjectWithTag : MetadataObject, IMetadataObjectWithLineage
	{
		// Token: 0x06001CBB RID: 7355 RVA: 0x000C62B2 File Offset: 0x000C44B2
		public LineageMap(IDictionary<string, TObjectWithTag> map)
		{
			this.map = ((map != null) ? new Dictionary<string, TObjectWithTag>(map, StringComparer.Ordinal) : new Dictionary<string, TObjectWithTag>(StringComparer.Ordinal));
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x000C62D4 File Offset: 0x000C44D4
		public IDictionary<string, TObjectWithTag> Map
		{
			get
			{
				return this.map;
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000C62DC File Offset: 0x000C44DC
		public TObjectWithTag FindByTag(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentNullException("tag");
			}
			TObjectWithTag tobjectWithTag;
			if (!this.map.TryGetValue(tag, out tobjectWithTag))
			{
				return default(TObjectWithTag);
			}
			return tobjectWithTag;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000C6317 File Offset: 0x000C4517
		public bool CanAdd(TObjectWithTag @object)
		{
			return string.IsNullOrEmpty(@object.LineageTag) || !this.map.ContainsKey(@object.LineageTag);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000C6346 File Offset: 0x000C4546
		public void Add(TObjectWithTag @object)
		{
			if (!string.IsNullOrEmpty(@object.LineageTag))
			{
				this.map.Add(@object.LineageTag, @object);
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000C6371 File Offset: 0x000C4571
		public void Remove(TObjectWithTag @object)
		{
			if (!string.IsNullOrEmpty(@object.LineageTag))
			{
				this.map.Remove(@object.LineageTag);
			}
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000C639C File Offset: 0x000C459C
		public bool IsValidTagChange(TObjectWithTag @object, string newTag)
		{
			TObjectWithTag tobjectWithTag;
			return string.IsNullOrEmpty(newTag) || !this.map.TryGetValue(newTag, out tobjectWithTag) || @object == tobjectWithTag;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000C63D3 File Offset: 0x000C45D3
		public void UpdateTag(TObjectWithTag @object, string oldTag)
		{
			if (!string.IsNullOrEmpty(oldTag))
			{
				this.map.Remove(oldTag);
			}
			if (!string.IsNullOrEmpty(@object.LineageTag))
			{
				this.map.Add(@object.LineageTag, @object);
			}
		}

		// Token: 0x04000687 RID: 1671
		private Dictionary<string, TObjectWithTag> map;
	}
}
