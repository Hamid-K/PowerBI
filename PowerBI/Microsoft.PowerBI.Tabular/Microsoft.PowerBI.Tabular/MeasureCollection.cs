using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000078 RID: 120
	public sealed class MeasureCollection : NamedMetadataObjectCollection<Measure, Table>, INotifyObjectLineageTagChange
	{
		// Token: 0x060006B2 RID: 1714 RVA: 0x00034F33 File Offset: 0x00033133
		internal MeasureCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Measure, parent, comparer, false)
		{
			this.body = new MeasureCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00034F4C File Offset: 0x0003314C
		public Measure FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00034F5F File Offset: 0x0003315F
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00034F67 File Offset: 0x00033167
		internal override MetadataObjectCollection<Measure, Table>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (MeasureCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00034F75 File Offset: 0x00033175
		internal override MetadataObjectCollection<Measure, Table>.ObjectCollectionBody CreateBody()
		{
			return new MeasureCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00034F90 File Offset: 0x00033190
		internal override void CopyFrom(MetadataObjectCollection<Measure, Table> other, CopyContext copyContext)
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

		// Token: 0x060006B8 RID: 1720 RVA: 0x00034FDC File Offset: 0x000331DC
		private protected override void CompareWith(MetadataObjectCollection<Measure, Table> other, CopyContext context, IList<Measure> removedItems, IList<Measure> addedItems, IList<KeyValuePair<Measure, Measure>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustLineageObjectCollectionsComparison<Measure>(context, removedItems, addedItems, matchedItems);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00034FF8 File Offset: 0x000331F8
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is Measure);
			if (!base.Contains((Measure)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((Measure)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00035066 File Offset: 0x00033266
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is Measure);
			if (!base.Contains((Measure)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((Measure)obj, oldTag);
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000350A4 File Offset: 0x000332A4
		internal override void ValidateCanAdd(Measure item)
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

		// Token: 0x060006BC RID: 1724 RVA: 0x000350F5 File Offset: 0x000332F5
		internal override void OnItemAdded(Measure item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00035117 File Offset: 0x00033317
		internal override void OnItemRemoved(Measure item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x04000119 RID: 281
		private MeasureCollection.ObjectCollectionBody body;

		// Token: 0x0400011A RID: 282
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x02000285 RID: 645
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<Measure, Table>.ObjectCollectionBody
		{
			// Token: 0x06002103 RID: 8451 RVA: 0x000D79B7 File Offset: 0x000D5BB7
			public ObjectCollectionBody(MeasureCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<Measure>(null);
			}

			// Token: 0x06002104 RID: 8452 RVA: 0x000D79CD File Offset: 0x000D5BCD
			internal void CopyFrom(MeasureCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<Measure>(other.LineageMap.Map);
			}

			// Token: 0x06002105 RID: 8453 RVA: 0x000D79ED File Offset: 0x000D5BED
			internal override void CopyFrom(MetadataObjectCollection<Measure, Table>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((MeasureCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x06002106 RID: 8454 RVA: 0x000D79FC File Offset: 0x000D5BFC
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (Measure measure in base.List.Where((Measure o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[measure.LineageTag] = measure;
				}
			}

			// Token: 0x040008CB RID: 2251
			internal LineageMap<Measure> LineageMap;
		}
	}
}
