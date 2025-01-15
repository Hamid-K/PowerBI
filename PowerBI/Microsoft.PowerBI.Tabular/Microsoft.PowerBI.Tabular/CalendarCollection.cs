using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200003E RID: 62
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class CalendarCollection : NamedMetadataObjectCollection<Calendar, Table>, INotifyObjectLineageTagChange
	{
		// Token: 0x060001ED RID: 493 RVA: 0x0000E6C9 File Offset: 0x0000C8C9
		internal CalendarCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Calendar, parent, comparer, false)
		{
			this.body = new CalendarCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000E6E3 File Offset: 0x0000C8E3
		public Calendar FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000E6F6 File Offset: 0x0000C8F6
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000E6FE File Offset: 0x0000C8FE
		internal override MetadataObjectCollection<Calendar, Table>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (CalendarCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000E70C File Offset: 0x0000C90C
		internal override MetadataObjectCollection<Calendar, Table>.ObjectCollectionBody CreateBody()
		{
			return new CalendarCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E724 File Offset: 0x0000C924
		internal override void CopyFrom(MetadataObjectCollection<Calendar, Table> other, CopyContext copyContext)
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

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E770 File Offset: 0x0000C970
		private protected override void CompareWith(MetadataObjectCollection<Calendar, Table> other, CopyContext context, IList<Calendar> removedItems, IList<Calendar> addedItems, IList<KeyValuePair<Calendar, Calendar>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustLineageObjectCollectionsComparison<Calendar>(context, removedItems, addedItems, matchedItems);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E78C File Offset: 0x0000C98C
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is Calendar);
			if (!base.Contains((Calendar)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((Calendar)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E7FA File Offset: 0x0000C9FA
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is Calendar);
			if (!base.Contains((Calendar)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((Calendar)obj, oldTag);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000E838 File Offset: 0x0000CA38
		internal override void ValidateCanAdd(Calendar item)
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

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E889 File Offset: 0x0000CA89
		internal override void OnItemAdded(Calendar item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000E8AB File Offset: 0x0000CAAB
		internal override void OnItemRemoved(Calendar item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x040000DC RID: 220
		private CalendarCollection.ObjectCollectionBody body;

		// Token: 0x040000DD RID: 221
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x0200023F RID: 575
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<Calendar, Table>.ObjectCollectionBody
		{
			// Token: 0x06001F4A RID: 8010 RVA: 0x000CEC17 File Offset: 0x000CCE17
			public ObjectCollectionBody(CalendarCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<Calendar>(null);
			}

			// Token: 0x06001F4B RID: 8011 RVA: 0x000CEC2D File Offset: 0x000CCE2D
			internal void CopyFrom(CalendarCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<Calendar>(other.LineageMap.Map);
			}

			// Token: 0x06001F4C RID: 8012 RVA: 0x000CEC4D File Offset: 0x000CCE4D
			internal override void CopyFrom(MetadataObjectCollection<Calendar, Table>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((CalendarCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x06001F4D RID: 8013 RVA: 0x000CEC5C File Offset: 0x000CCE5C
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (Calendar calendar in base.List.Where((Calendar o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[calendar.LineageTag] = calendar;
				}
			}

			// Token: 0x0400077D RID: 1917
			internal LineageMap<Calendar> LineageMap;
		}
	}
}
