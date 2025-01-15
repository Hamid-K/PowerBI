using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000045 RID: 69
	public sealed class ColumnCollection : NamedMetadataObjectCollection<Column, Table>, INotifyObjectLineageTagChange
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x00017984 File Offset: 0x00015B84
		internal ColumnCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Column, parent, comparer, false)
		{
			this.body = new ColumnCollection.ObjectCollectionBody(this, comparer);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001799D File Offset: 0x00015B9D
		public Column FindByLineageTag(string lineageTag)
		{
			return this.body.LineageMap.FindByTag(lineageTag);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000179B0 File Offset: 0x00015BB0
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x000179B8 File Offset: 0x00015BB8
		internal override MetadataObjectCollection<Column, Table>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ColumnCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000179C6 File Offset: 0x00015BC6
		internal override MetadataObjectCollection<Column, Table>.ObjectCollectionBody CreateBody()
		{
			return new ColumnCollection.ObjectCollectionBody(this, this.body.MapByName.Comparer);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000179E0 File Offset: 0x00015BE0
		internal override void CopyFrom(MetadataObjectCollection<Column, Table> other, CopyContext copyContext)
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

		// Token: 0x060002F6 RID: 758 RVA: 0x00017A2C File Offset: 0x00015C2C
		void INotifyObjectLineageTagChange.NotifyTagChanging(IMetadataObjectWithLineage obj, string newTag)
		{
			Utils.Verify(obj is Column);
			if (!base.Contains((Column)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy && !this.body.LineageMap.IsValidTagChange((Column)obj, newTag))
			{
				if (!this.copyInProgress)
				{
					throw new ArgumentException(TomSR.Exception_CollectionAlreadyContainsObjectWithTag(newTag));
				}
				this.rebuildLineageMapAfterCopy = true;
			}
			ObjectChangeTracker.RegisterCollectionChanging(this);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00017A9A File Offset: 0x00015C9A
		void INotifyObjectLineageTagChange.NotifyTagChanged(IMetadataObjectWithLineage obj, string oldTag)
		{
			Utils.Verify(obj is Column);
			if (!base.Contains((Column)obj))
			{
				return;
			}
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.UpdateTag((Column)obj, oldTag);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00017AD8 File Offset: 0x00015CD8
		internal override void ValidateCanAdd(Column item)
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

		// Token: 0x060002F9 RID: 761 RVA: 0x00017B29 File Offset: 0x00015D29
		internal override void OnItemAdded(Column item)
		{
			base.OnItemAdded(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Add(item);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00017B4B File Offset: 0x00015D4B
		internal override void OnItemRemoved(Column item)
		{
			base.OnItemRemoved(item);
			if (!this.rebuildLineageMapAfterCopy)
			{
				this.body.LineageMap.Remove(item);
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00017B70 File Offset: 0x00015D70
		private protected override void CompareWith(MetadataObjectCollection<Column, Table> other, CopyContext context, IList<Column> removedItems, IList<Column> addedItems, IList<KeyValuePair<Column, Column>> matchedItems)
		{
			ColumnCollection columnCollection = (ColumnCollection)other;
			bool flag = (context.Flags & CopyFlags.IncludeObjectIds) == CopyFlags.IncludeObjectIds;
			bool flag2 = (context.Flags & CopyFlags.Incremental) == CopyFlags.Incremental;
			Dictionary<string, Column> dictionary = (flag ? null : new Dictionary<string, Column>(StringComparer.Ordinal));
			if (!flag2)
			{
				IDictionary<string, Column> dictionary2 = ColumnCollection.BuildSourceColumnMapping(columnCollection);
				foreach (Column column in this)
				{
					Column column2 = ((flag && !column.Id.IsNull) ? columnCollection.FindById(column.Id) : columnCollection.Find(column.Name));
					Column column3;
					if (column2 == null || column2.Type != column.Type)
					{
						removedItems.Add(column);
						if (!flag && !string.IsNullOrEmpty(column.LineageTag))
						{
							dictionary.Add(column.LineageTag, column);
						}
					}
					else if (column.Type == ColumnType.CalculatedTableColumn && (!flag || column.Id.IsNull) && (!dictionary2.TryGetValue(column.SourceColumn, out column3) || column2 != column3))
					{
						removedItems.Add(column);
					}
				}
			}
			IDictionary<string, Column> dictionary3 = ColumnCollection.BuildSourceColumnMapping(this);
			HashSet<Column> columnsThatGotNewId = new HashSet<Column>();
			foreach (Column column4 in columnCollection)
			{
				Utils.Verify(!flag || !column4.Id.IsNull, "If we compare collections using Ids, then objects in 'other' collection must have Ids");
				Column column5 = ((flag && !column4.Id.IsNull) ? base.FindById(column4.Id) : null);
				if (column5 == null)
				{
					column5 = base.Find(column4.Name);
					if (column5 != null && flag && !column5.Id.IsNull)
					{
						column5 = null;
					}
				}
				bool flag3 = column5 != null && column5.Type == column4.Type && !removedItems.Contains(column5);
				if (column4.Type == ColumnType.CalculatedTableColumn)
				{
					if (flag3)
					{
						Column column6;
						if ((!flag || column5.Id.IsNull) && (!dictionary3.TryGetValue(column4.SourceColumn, out column6) || column5 != column6))
						{
							flag3 = false;
						}
					}
					else if (flag2 && dictionary3.TryGetValue(column4.SourceColumn, out column5))
					{
						flag3 = true;
					}
				}
				if (flag3)
				{
					matchedItems.Add(new KeyValuePair<Column, Column>(column5, column4));
					if (flag2 && flag && column5.Id.IsNull && !column4.Id.IsNull)
					{
						columnsThatGotNewId.Add(column5);
					}
				}
				else
				{
					if (column5 != null && !removedItems.Contains(column5))
					{
						removedItems.Add(column5);
					}
					addedItems.Add(column4);
				}
			}
			if (flag2)
			{
				Func<Column, bool> <>9__0;
				Func<Column, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Column c) => c.Id.IsNull && !removedItems.Contains(c) && !columnsThatGotNewId.Contains(c));
				}
				using (IEnumerator<Column> enumerator = this.Where(func).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Column column7 = enumerator.Current;
						removedItems.Add(column7);
					}
					return;
				}
			}
			if (!flag && dictionary.Count > 0 && addedItems.Count > 0)
			{
				int i = 0;
				while (i < addedItems.Count)
				{
					string lineageTag = addedItems[i].LineageTag;
					Column column8;
					if (string.IsNullOrEmpty(lineageTag) || !dictionary.TryGetValue(lineageTag, out column8) || column8.Type != addedItems[i].Type)
					{
						i++;
					}
					else
					{
						int num = removedItems.IndexOf(column8);
						matchedItems.Add(new KeyValuePair<Column, Column>(column8, addedItems[i]));
						removedItems.RemoveAt(num);
						addedItems.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00017FD0 File Offset: 0x000161D0
		private static IDictionary<string, Column> BuildSourceColumnMapping(IEnumerable<Column> columns)
		{
			Dictionary<string, Column> dictionary = new Dictionary<string, Column>();
			foreach (Column column in columns.Where((Column c) => c.Type == ColumnType.CalculatedTableColumn && !string.IsNullOrEmpty(c.SourceColumn)))
			{
				dictionary.Add(column.SourceColumn, column);
			}
			return dictionary;
		}

		// Token: 0x040000E5 RID: 229
		private ColumnCollection.ObjectCollectionBody body;

		// Token: 0x040000E6 RID: 230
		private bool rebuildLineageMapAfterCopy;

		// Token: 0x02000249 RID: 585
		internal new class ObjectCollectionBody : NamedMetadataObjectCollection<Column, Table>.ObjectCollectionBody
		{
			// Token: 0x06001F90 RID: 8080 RVA: 0x000D1013 File Offset: 0x000CF213
			public ObjectCollectionBody(ColumnCollection owner, IEqualityComparer<string> comparer)
				: base(owner, comparer)
			{
				this.LineageMap = new LineageMap<Column>(null);
			}

			// Token: 0x06001F91 RID: 8081 RVA: 0x000D1029 File Offset: 0x000CF229
			internal void CopyFrom(ColumnCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.LineageMap = new LineageMap<Column>(other.LineageMap.Map);
			}

			// Token: 0x06001F92 RID: 8082 RVA: 0x000D1049 File Offset: 0x000CF249
			internal override void CopyFrom(MetadataObjectCollection<Column, Table>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((ColumnCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x06001F93 RID: 8083 RVA: 0x000D1058 File Offset: 0x000CF258
			internal void RebuildLineageMap()
			{
				this.LineageMap.Map.Clear();
				foreach (Column column in base.List.Where((Column o) => !string.IsNullOrEmpty(o.LineageTag)))
				{
					this.LineageMap.Map[column.LineageTag] = column;
				}
			}

			// Token: 0x040007C8 RID: 1992
			internal LineageMap<Column> LineageMap;
		}
	}
}
