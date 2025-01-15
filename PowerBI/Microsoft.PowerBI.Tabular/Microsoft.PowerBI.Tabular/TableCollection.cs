using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C1 RID: 193
	public sealed class TableCollection : NamedMetadataObjectCollection<Table, Model>, INotifyObjectLineageTagChange
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x00066F80 File Offset: 0x00065180
		internal TableCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Table, parent, comparer, false)
		{
			this.body = new TableCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00066F99 File Offset: 0x00065199
		public Table FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00066FAC File Offset: 0x000651AC
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00066FB4 File Offset: 0x000651B4
		internal override MetadataObjectCollection<Table, Model>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (TableCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00066FC2 File Offset: 0x000651C2
		internal override MetadataObjectCollection<Table, Model>.ObjectCollectionBody CreateBody()
		{
			return new TableCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00066FDC File Offset: 0x000651DC
		internal override void CopyFrom(MetadataObjectCollection<Table, Model> other, CopyContext copyContext)
		{
			try
			{
				this.rebuildLineageMapAfterCopy = false;
				base.CopyFrom(other, copyContext);
			}
			finally
			{
				if (this.rebuildLineageMapAfterCopy)
				{
					this.body.RebuildLineageMap();
				}
				this.rebuildLineageMapAfterCopy = false;
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00067028 File Offset: 0x00065228
		private protected override void CompareWith(MetadataObjectCollection<Table, Model> other, CopyContext context, IList<Table> removedItems, IList<Table> addedItems, IList<KeyValuePair<Table, Table>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustLineageObjectCollectionsComparison<Table>(context, removedItems, addedItems, matchedItems);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00067044 File Offset: 0x00065244
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is Table);
			if (!base.Contains((Table)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((Table)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000670B2 File Offset: 0x000652B2
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is Table);
			if (!base.Contains((Table)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((Table)obj, oldTag);
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000670F0 File Offset: 0x000652F0
		internal override void ValidateCanAdd(Table item)
		{
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.CanAdd(item))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(item.LineageTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			base.ValidateCanAdd(item);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00067141 File Offset: 0x00065341
		internal override void OnItemAdded(Table item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00067163 File Offset: 0x00065363
		internal override void OnItemRemoved(Table item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x04000182 RID: 386
		private TableCollection.ObjectCollectionBody body;

		// Token: 0x04000183 RID: 387
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x020002D8 RID: 728
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<Table, Model>.ObjectCollectionBody
		{
			// Token: 0x0600234B RID: 9035 RVA: 0x000E139F File Offset: 0x000DF59F
			public ObjectCollectionBody(TableCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<Table>(null);
			}

			// Token: 0x0600234C RID: 9036 RVA: 0x000E13B5 File Offset: 0x000DF5B5
			internal void CopyFrom(TableCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<Table>(other.LineageMap.Map);
			}

			// Token: 0x0600234D RID: 9037 RVA: 0x000E13D5 File Offset: 0x000DF5D5
			internal override void CopyFrom(MetadataObjectCollection<Table, Model>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((TableCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x0600234E RID: 9038 RVA: 0x000E13E4 File Offset: 0x000DF5E4
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (Table table in base.List.Where((Table o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[table.LineageTag] = table;
				}
			}

			// Token: 0x04000A7B RID: 2683
			internal LineageMap<Table> LineageMap;
		}
	}
}
