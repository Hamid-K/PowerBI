using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200005F RID: 95
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Internal")]
	public sealed class FunctionCollection : NamedMetadataObjectCollection<Function, Model>, INotifyObjectLineageTagChange
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x00026A77 File Offset: 0x00024C77
		internal FunctionCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Function, parent, comparer, false)
		{
			this.body = new FunctionCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00026A91 File Offset: 0x00024C91
		public Function FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00026AA4 File Offset: 0x00024CA4
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00026AAC File Offset: 0x00024CAC
		internal override MetadataObjectCollection<Function, Model>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (FunctionCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00026ABA File Offset: 0x00024CBA
		internal override MetadataObjectCollection<Function, Model>.ObjectCollectionBody CreateBody()
		{
			return new FunctionCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00026AD4 File Offset: 0x00024CD4
		internal override void CopyFrom(MetadataObjectCollection<Function, Model> other, CopyContext copyContext)
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

		// Token: 0x0600050B RID: 1291 RVA: 0x00026B20 File Offset: 0x00024D20
		private protected override void CompareWith(MetadataObjectCollection<Function, Model> other, CopyContext context, IList<Function> removedItems, IList<Function> addedItems, IList<KeyValuePair<Function, Function>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustLineageObjectCollectionsComparison<Function>(context, removedItems, addedItems, matchedItems);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00026B3C File Offset: 0x00024D3C
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is Function);
			if (!base.Contains((Function)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((Function)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00026BAA File Offset: 0x00024DAA
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is Function);
			if (!base.Contains((Function)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((Function)obj, oldTag);
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00026BE8 File Offset: 0x00024DE8
		internal override void ValidateCanAdd(Function item)
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

		// Token: 0x0600050F RID: 1295 RVA: 0x00026C39 File Offset: 0x00024E39
		internal override void OnItemAdded(Function item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00026C5B File Offset: 0x00024E5B
		internal override void OnItemRemoved(Function item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x040000FE RID: 254
		private FunctionCollection.ObjectCollectionBody body;

		// Token: 0x040000FF RID: 255
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x02000269 RID: 617
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<Function, Model>.ObjectCollectionBody
		{
			// Token: 0x06002050 RID: 8272 RVA: 0x000D44EB File Offset: 0x000D26EB
			public ObjectCollectionBody(FunctionCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<Function>(null);
			}

			// Token: 0x06002051 RID: 8273 RVA: 0x000D4501 File Offset: 0x000D2701
			internal void CopyFrom(FunctionCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<Function>(other.LineageMap.Map);
			}

			// Token: 0x06002052 RID: 8274 RVA: 0x000D4521 File Offset: 0x000D2721
			internal override void CopyFrom(MetadataObjectCollection<Function, Model>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((FunctionCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x06002053 RID: 8275 RVA: 0x000D4530 File Offset: 0x000D2730
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (Function function in base.List.Where((Function o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[function.LineageTag] = function;
				}
			}

			// Token: 0x04000847 RID: 2119
			internal LineageMap<Function> LineageMap;
		}
	}
}
