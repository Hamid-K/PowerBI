using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000066 RID: 102
	public sealed class HierarchyCollection : NamedMetadataObjectCollection<Hierarchy, Table>, INotifyObjectLineageTagChange
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x0002AA69 File Offset: 0x00028C69
		internal HierarchyCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Hierarchy, parent, comparer, false)
		{
			this.body = new HierarchyCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002AA83 File Offset: 0x00028C83
		public Hierarchy FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0002AA96 File Offset: 0x00028C96
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0002AA9E File Offset: 0x00028C9E
		internal override MetadataObjectCollection<Hierarchy, Table>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (HierarchyCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002AAAC File Offset: 0x00028CAC
		internal override MetadataObjectCollection<Hierarchy, Table>.ObjectCollectionBody CreateBody()
		{
			return new HierarchyCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0002AAC4 File Offset: 0x00028CC4
		internal override void CopyFrom(MetadataObjectCollection<Hierarchy, Table> other, CopyContext copyContext)
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

		// Token: 0x0600058A RID: 1418 RVA: 0x0002AB10 File Offset: 0x00028D10
		private protected override void CompareWith(MetadataObjectCollection<Hierarchy, Table> other, CopyContext context, IList<Hierarchy> removedItems, IList<Hierarchy> addedItems, IList<KeyValuePair<Hierarchy, Hierarchy>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustLineageObjectCollectionsComparison<Hierarchy>(context, removedItems, addedItems, matchedItems);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0002AB2C File Offset: 0x00028D2C
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is Hierarchy);
			if (!base.Contains((Hierarchy)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((Hierarchy)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002AB9A File Offset: 0x00028D9A
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is Hierarchy);
			if (!base.Contains((Hierarchy)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((Hierarchy)obj, oldTag);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002ABD8 File Offset: 0x00028DD8
		internal override void ValidateCanAdd(Hierarchy item)
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

		// Token: 0x0600058E RID: 1422 RVA: 0x0002AC29 File Offset: 0x00028E29
		internal override void OnItemAdded(Hierarchy item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002AC4B File Offset: 0x00028E4B
		internal override void OnItemRemoved(Hierarchy item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x04000107 RID: 263
		private HierarchyCollection.ObjectCollectionBody body;

		// Token: 0x04000108 RID: 264
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x02000271 RID: 625
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<Hierarchy, Table>.ObjectCollectionBody
		{
			// Token: 0x06002087 RID: 8327 RVA: 0x000D5447 File Offset: 0x000D3647
			public ObjectCollectionBody(HierarchyCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<Hierarchy>(null);
			}

			// Token: 0x06002088 RID: 8328 RVA: 0x000D545D File Offset: 0x000D365D
			internal void CopyFrom(HierarchyCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<Hierarchy>(other.LineageMap.Map);
			}

			// Token: 0x06002089 RID: 8329 RVA: 0x000D547D File Offset: 0x000D367D
			internal override void CopyFrom(MetadataObjectCollection<Hierarchy, Table>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((HierarchyCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x0600208A RID: 8330 RVA: 0x000D548C File Offset: 0x000D368C
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (Hierarchy hierarchy in base.List.Where((Hierarchy o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[hierarchy.LineageTag] = hierarchy;
				}
			}

			// Token: 0x0400086C RID: 2156
			internal LineageMap<Hierarchy> LineageMap;
		}
	}
}
