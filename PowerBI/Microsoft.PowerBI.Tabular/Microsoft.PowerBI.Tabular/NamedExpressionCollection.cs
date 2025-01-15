using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000088 RID: 136
	[CompatibilityRequirement("1400")]
	public sealed class NamedExpressionCollection : NamedMetadataObjectCollection<NamedExpression, Model>, INotifyObjectLineageTagChange
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00046E27 File Offset: 0x00045027
		internal NamedExpressionCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Expression, parent, comparer, false)
		{
			this.body = new NamedExpressionCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00046E41 File Offset: 0x00045041
		public NamedExpression FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00046E54 File Offset: 0x00045054
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00046E5C File Offset: 0x0004505C
		internal override MetadataObjectCollection<NamedExpression, Model>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (NamedExpressionCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00046E6A File Offset: 0x0004506A
		internal override MetadataObjectCollection<NamedExpression, Model>.ObjectCollectionBody CreateBody()
		{
			return new NamedExpressionCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00046E84 File Offset: 0x00045084
		internal override void CopyFrom(MetadataObjectCollection<NamedExpression, Model> other, CopyContext copyContext)
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

		// Token: 0x06000835 RID: 2101 RVA: 0x00046ED0 File Offset: 0x000450D0
		private protected override void CompareWith(MetadataObjectCollection<NamedExpression, Model> other, CopyContext context, IList<NamedExpression> removedItems, IList<NamedExpression> addedItems, IList<KeyValuePair<NamedExpression, NamedExpression>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustLineageObjectCollectionsComparison<NamedExpression>(context, removedItems, addedItems, matchedItems);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00046EEC File Offset: 0x000450EC
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is NamedExpression);
			if (!base.Contains((NamedExpression)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((NamedExpression)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00046F5A File Offset: 0x0004515A
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is NamedExpression);
			if (!base.Contains((NamedExpression)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((NamedExpression)obj, oldTag);
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00046F98 File Offset: 0x00045198
		internal override void ValidateCanAdd(NamedExpression item)
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

		// Token: 0x06000839 RID: 2105 RVA: 0x00046FE9 File Offset: 0x000451E9
		internal override void OnItemAdded(NamedExpression item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0004700B File Offset: 0x0004520B
		internal override void OnItemRemoved(NamedExpression item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x04000144 RID: 324
		private NamedExpressionCollection.ObjectCollectionBody body;

		// Token: 0x04000145 RID: 325
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x02000298 RID: 664
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<NamedExpression, Model>.ObjectCollectionBody
		{
			// Token: 0x060021A8 RID: 8616 RVA: 0x000DA52F File Offset: 0x000D872F
			public ObjectCollectionBody(NamedExpressionCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<NamedExpression>(null);
			}

			// Token: 0x060021A9 RID: 8617 RVA: 0x000DA545 File Offset: 0x000D8745
			internal void CopyFrom(NamedExpressionCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<NamedExpression>(other.LineageMap.Map);
			}

			// Token: 0x060021AA RID: 8618 RVA: 0x000DA565 File Offset: 0x000D8765
			internal override void CopyFrom(MetadataObjectCollection<NamedExpression, Model>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((NamedExpressionCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x060021AB RID: 8619 RVA: 0x000DA574 File Offset: 0x000D8774
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (NamedExpression namedExpression in base.List.Where((NamedExpression o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[namedExpression.LineageTag] = namedExpression;
				}
			}

			// Token: 0x04000959 RID: 2393
			internal LineageMap<NamedExpression> LineageMap;
		}
	}
}
