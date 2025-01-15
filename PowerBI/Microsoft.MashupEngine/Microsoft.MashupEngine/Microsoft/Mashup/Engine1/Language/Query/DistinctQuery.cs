using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001842 RID: 6210
	internal class DistinctQuery : Query
	{
		// Token: 0x06009D53 RID: 40275 RVA: 0x002081DC File Offset: 0x002063DC
		public static Query New(TableDistinct distinctCriteria, Query innerQuery, bool floating = false)
		{
			if (!floating && distinctCriteria.Distincts.Length == 0)
			{
				return innerQuery.Take(RowCount.One);
			}
			return new DistinctQuery(distinctCriteria, floating, innerQuery);
		}

		// Token: 0x06009D54 RID: 40276 RVA: 0x002081FE File Offset: 0x002063FE
		private DistinctQuery(TableDistinct distinctCriteria, bool floating, Query innerQuery)
		{
			this.distinctCriteria = distinctCriteria;
			this.floating = floating;
			this.innerQuery = innerQuery;
		}

		// Token: 0x1700288C RID: 10380
		// (get) Token: 0x06009D55 RID: 40277 RVA: 0x00002461 File Offset: 0x00000661
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.Distinct;
			}
		}

		// Token: 0x1700288D RID: 10381
		// (get) Token: 0x06009D56 RID: 40278 RVA: 0x0020821B File Offset: 0x0020641B
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x1700288E RID: 10382
		// (get) Token: 0x06009D57 RID: 40279 RVA: 0x00208223 File Offset: 0x00206423
		public TableDistinct DistinctCriteria
		{
			get
			{
				return this.distinctCriteria;
			}
		}

		// Token: 0x1700288F RID: 10383
		// (get) Token: 0x06009D58 RID: 40280 RVA: 0x0020822B File Offset: 0x0020642B
		public bool Floating
		{
			get
			{
				return this.floating;
			}
		}

		// Token: 0x17002890 RID: 10384
		// (get) Token: 0x06009D59 RID: 40281 RVA: 0x00208233 File Offset: 0x00206433
		public override Keys Columns
		{
			get
			{
				return this.innerQuery.Columns;
			}
		}

		// Token: 0x06009D5A RID: 40282 RVA: 0x00208240 File Offset: 0x00206440
		public override TypeValue GetColumnType(int column)
		{
			return this.innerQuery.GetColumnType(column);
		}

		// Token: 0x17002891 RID: 10385
		// (get) Token: 0x06009D5B RID: 40283 RVA: 0x00208250 File Offset: 0x00206450
		public override IList<TableKey> TableKeys
		{
			get
			{
				int[] array;
				if (this.innerQuery.TableKeys.Count == 0 && this.distinctCriteria.TryGetColumns(QueryTableValue.NewRowType(this), out array))
				{
					return new TableKey[]
					{
						new TableKey(array, true)
					};
				}
				return this.innerQuery.TableKeys;
			}
		}

		// Token: 0x17002892 RID: 10386
		// (get) Token: 0x06009D5C RID: 40284 RVA: 0x002082A0 File Offset: 0x002064A0
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.innerQuery.ComputedColumns;
			}
		}

		// Token: 0x17002893 RID: 10387
		// (get) Token: 0x06009D5D RID: 40285 RVA: 0x002082AD File Offset: 0x002064AD
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.innerQuery.SortOrder;
			}
		}

		// Token: 0x06009D5E RID: 40286 RVA: 0x002082BA File Offset: 0x002064BA
		public override TableValue GetPartitionTable(int[] columns)
		{
			return this.innerQuery.GetPartitionTable(columns);
		}

		// Token: 0x06009D5F RID: 40287 RVA: 0x002082C8 File Offset: 0x002064C8
		public override Query Unordered()
		{
			return this.innerQuery.Unordered().Distinct(this.distinctCriteria);
		}

		// Token: 0x17002894 RID: 10388
		// (get) Token: 0x06009D60 RID: 40288 RVA: 0x002082E0 File Offset: 0x002064E0
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009D61 RID: 40289 RVA: 0x002082F0 File Offset: 0x002064F0
		public override IEnumerable<IValueReference> GetRows()
		{
			Func<Value, Value> func;
			IEqualityComparer<Value> equalityComparer = this.distinctCriteria.ToComparer(QueryTableValue.NewRowType(this), out func);
			return new DistinctQuery.DistinctEnumerable(this.innerQuery.GetRows(), equalityComparer, func);
		}

		// Token: 0x040052AF RID: 21167
		private Query innerQuery;

		// Token: 0x040052B0 RID: 21168
		private TableDistinct distinctCriteria;

		// Token: 0x040052B1 RID: 21169
		private bool floating;

		// Token: 0x02001843 RID: 6211
		private class DistinctEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009D62 RID: 40290 RVA: 0x00208323 File Offset: 0x00206523
			public DistinctEnumerable(IEnumerable<IValueReference> rows, IEqualityComparer<Value> comparer, Func<Value, Value> keyTransformer)
			{
				this.rows = rows;
				this.comparer = comparer;
				this.keyTransformer = keyTransformer;
			}

			// Token: 0x06009D63 RID: 40291 RVA: 0x00208340 File Offset: 0x00206540
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009D64 RID: 40292 RVA: 0x00208348 File Offset: 0x00206548
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new DistinctQuery.DistinctEnumerable.DistinctEnumerator(this.rows.GetEnumerator(), this.comparer, this.keyTransformer);
			}

			// Token: 0x040052B2 RID: 21170
			private readonly IEnumerable<IValueReference> rows;

			// Token: 0x040052B3 RID: 21171
			private readonly IEqualityComparer<Value> comparer;

			// Token: 0x040052B4 RID: 21172
			private readonly Func<Value, Value> keyTransformer;

			// Token: 0x02001844 RID: 6212
			private class DistinctEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009D65 RID: 40293 RVA: 0x00208366 File Offset: 0x00206566
				public DistinctEnumerator(IEnumerator<IValueReference> enumerator, IEqualityComparer<Value> comparer, Func<Value, Value> keyTransformer)
				{
					this.enumerator = enumerator;
					this.hashSet = new HashSet<Value>(comparer);
					this.keyTransformer = keyTransformer;
				}

				// Token: 0x17002895 RID: 10389
				// (get) Token: 0x06009D66 RID: 40294 RVA: 0x00208388 File Offset: 0x00206588
				public IValueReference Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x17002896 RID: 10390
				// (get) Token: 0x06009D67 RID: 40295 RVA: 0x00208395 File Offset: 0x00206595
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009D68 RID: 40296 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x06009D69 RID: 40297 RVA: 0x0020839D File Offset: 0x0020659D
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x06009D6A RID: 40298 RVA: 0x002083AA File Offset: 0x002065AA
				public bool MoveNext()
				{
					while (this.enumerator.MoveNext())
					{
						if (this.hashSet.Add(this.keyTransformer(this.enumerator.Current.Value)))
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x040052B5 RID: 21173
				private readonly IEnumerator<IValueReference> enumerator;

				// Token: 0x040052B6 RID: 21174
				private readonly HashSet<Value> hashSet;

				// Token: 0x040052B7 RID: 21175
				private readonly Func<Value, Value> keyTransformer;
			}
		}
	}
}
