using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200021B RID: 539
	internal class ConsolidatedIndex
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x0005130F File Offset: 0x0004F50F
		public ConsolidatedIndex(string table, IndexAttribute index)
		{
			this._table = table;
			this._index = index;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00051330 File Offset: 0x0004F530
		public ConsolidatedIndex(string table, string column, IndexAttribute index)
			: this(table, index)
		{
			this._columns[index.Order] = column;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0005134C File Offset: 0x0004F54C
		public static IEnumerable<ConsolidatedIndex> BuildIndexes(string tableName, IEnumerable<Tuple<string, EdmProperty>> columns)
		{
			List<ConsolidatedIndex> list = new List<ConsolidatedIndex>();
			foreach (Tuple<string, EdmProperty> tuple in columns)
			{
				using (IEnumerator<IndexAttribute> enumerator2 = (from a in tuple.Item2.Annotations
					where a.Name == "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index"
					select a.Value).OfType<IndexAnnotation>().SelectMany((IndexAnnotation a) => a.Indexes).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						IndexAttribute index = enumerator2.Current;
						ConsolidatedIndex consolidatedIndex = ((index.Name == null) ? null : list.FirstOrDefault((ConsolidatedIndex i) => i.Index.Name == index.Name));
						if (consolidatedIndex == null)
						{
							list.Add(new ConsolidatedIndex(tableName, tuple.Item1, index));
						}
						else
						{
							consolidatedIndex.Add(tuple.Item1, index);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x000514C4 File Offset: 0x0004F6C4
		public IndexAttribute Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x000514CC File Offset: 0x0004F6CC
		public IEnumerable<string> Columns
		{
			get
			{
				return from c in this._columns
					orderby c.Key
					select c.Value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00051527 File Offset: 0x0004F727
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00051530 File Offset: 0x0004F730
		public void Add(string columnName, IndexAttribute index)
		{
			if (this._columns.ContainsKey(index.Order))
			{
				throw new InvalidOperationException(Strings.OrderConflictWhenConsolidating(index.Name, this._table, index.Order, this._columns[index.Order], columnName));
			}
			this._columns[index.Order] = columnName;
			CompatibilityResult compatibilityResult = this._index.IsCompatibleWith(index, true);
			if (!compatibilityResult)
			{
				throw new InvalidOperationException(Strings.ConflictWhenConsolidating(index.Name, this._table, compatibilityResult.ErrorMessage));
			}
			this._index = this._index.MergeWith(index, true);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000515DC File Offset: 0x0004F7DC
		public CreateIndexOperation CreateCreateIndexOperation()
		{
			string[] array = this.Columns.ToArray<string>();
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(null)
			{
				Name = (this._index.Name ?? IndexOperation.BuildDefaultName(array)),
				Table = this._table
			};
			foreach (string text in array)
			{
				createIndexOperation.Columns.Add(text);
			}
			if (this._index.IsClusteredConfigured)
			{
				createIndexOperation.IsClustered = this._index.IsClustered;
			}
			if (this._index.IsUniqueConfigured)
			{
				createIndexOperation.IsUnique = this._index.IsUnique;
			}
			return createIndexOperation;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00051682 File Offset: 0x0004F882
		public DropIndexOperation CreateDropIndexOperation()
		{
			return (DropIndexOperation)this.CreateCreateIndexOperation().Inverse;
		}

		// Token: 0x04000AEF RID: 2799
		private readonly string _table;

		// Token: 0x04000AF0 RID: 2800
		private IndexAttribute _index;

		// Token: 0x04000AF1 RID: 2801
		private readonly IDictionary<int, string> _columns = new Dictionary<int, string>();
	}
}
