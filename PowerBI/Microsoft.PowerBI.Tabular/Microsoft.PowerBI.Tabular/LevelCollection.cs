using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000070 RID: 112
	public sealed class LevelCollection : NamedMetadataObjectCollection<Level, Hierarchy>, INotifyObjectLineageTagChange
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x0002F3E7 File Offset: 0x0002D5E7
		internal LevelCollection(Hierarchy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Level, parent, comparer, false)
		{
			this.body = new LevelCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002F401 File Offset: 0x0002D601
		public Level FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0002F414 File Offset: 0x0002D614
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0002F41C File Offset: 0x0002D61C
		internal override MetadataObjectCollection<Level, Hierarchy>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (LevelCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002F42A File Offset: 0x0002D62A
		internal override MetadataObjectCollection<Level, Hierarchy>.ObjectCollectionBody CreateBody()
		{
			return new LevelCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002F444 File Offset: 0x0002D644
		internal override void CopyFrom(MetadataObjectCollection<Level, Hierarchy> other, CopyContext copyContext)
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

		// Token: 0x0600061E RID: 1566 RVA: 0x0002F490 File Offset: 0x0002D690
		private protected override void CompareWith(MetadataObjectCollection<Level, Hierarchy> other, CopyContext context, IList<Level> removedItems, IList<Level> addedItems, IList<KeyValuePair<Level, Level>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustLineageObjectCollectionsComparison<Level>(context, removedItems, addedItems, matchedItems);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is Level);
			if (!base.Contains((Level)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((Level)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0002F51A File Offset: 0x0002D71A
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is Level);
			if (!base.Contains((Level)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((Level)obj, oldTag);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002F558 File Offset: 0x0002D758
		internal override void ValidateCanAdd(Level item)
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

		// Token: 0x06000622 RID: 1570 RVA: 0x0002F5A9 File Offset: 0x0002D7A9
		internal override void OnItemAdded(Level item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002F5CB File Offset: 0x0002D7CB
		internal override void OnItemRemoved(Level item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x04000110 RID: 272
		private LevelCollection.ObjectCollectionBody body;

		// Token: 0x04000111 RID: 273
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x0200027A RID: 634
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<Level, Hierarchy>.ObjectCollectionBody
		{
			// Token: 0x060020BD RID: 8381 RVA: 0x000D6433 File Offset: 0x000D4633
			public ObjectCollectionBody(LevelCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<Level>(null);
			}

			// Token: 0x060020BE RID: 8382 RVA: 0x000D6449 File Offset: 0x000D4649
			internal void CopyFrom(LevelCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<Level>(other.LineageMap.Map);
			}

			// Token: 0x060020BF RID: 8383 RVA: 0x000D6469 File Offset: 0x000D4669
			internal override void CopyFrom(MetadataObjectCollection<Level, Hierarchy>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((LevelCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x060020C0 RID: 8384 RVA: 0x000D6478 File Offset: 0x000D4678
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (Level level in base.List.Where((Level o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[level.LineageTag] = level;
				}
			}

			// Token: 0x04000895 RID: 2197
			internal LineageMap<Level> LineageMap;
		}
	}
}
