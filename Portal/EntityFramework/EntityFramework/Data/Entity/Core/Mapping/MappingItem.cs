using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000548 RID: 1352
	public abstract class MappingItem
	{
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000E0705 File Offset: 0x000DE905
		internal bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000E070D File Offset: 0x000DE90D
		internal IList<MetadataProperty> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000E0715 File Offset: 0x000DE915
		internal virtual void SetReadOnly()
		{
			this._annotations.TrimExcess();
			this._readOnly = true;
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000E0729 File Offset: 0x000DE929
		internal void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyItem);
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000E073E File Offset: 0x000DE93E
		internal static void SetReadOnly(MappingItem item)
		{
			if (item != null)
			{
				item.SetReadOnly();
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000E074C File Offset: 0x000DE94C
		internal static void SetReadOnly(IEnumerable<MappingItem> items)
		{
			if (items == null)
			{
				return;
			}
			foreach (MappingItem mappingItem in items)
			{
				MappingItem.SetReadOnly(mappingItem);
			}
		}

		// Token: 0x04001761 RID: 5985
		private bool _readOnly;

		// Token: 0x04001762 RID: 5986
		private readonly List<MetadataProperty> _annotations = new List<MetadataProperty>();
	}
}
