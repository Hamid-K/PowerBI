using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000031 RID: 49
	internal sealed class DataSet
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000054B5 File Offset: 0x000036B5
		internal DataSet(string id, string dataSourceId, string query, IList<ResultTable> resultTables, IList<ItemSourceLocation> querySourceMap)
		{
			this._id = id;
			this._dataSourceId = dataSourceId;
			this._query = query;
			this._resultTables = resultTables.AsReadOnlyCollection<ResultTable>();
			this._querySourceMap = querySourceMap.AsReadOnlyCollection<ItemSourceLocation>();
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000054EC File Offset: 0x000036EC
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000054F4 File Offset: 0x000036F4
		internal string DataSourceId
		{
			get
			{
				return this._dataSourceId;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000054FC File Offset: 0x000036FC
		internal string Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005504 File Offset: 0x00003704
		internal IReadOnlyList<ResultTable> ResultTables
		{
			get
			{
				return this._resultTables;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000550C File Offset: 0x0000370C
		internal IReadOnlyList<ItemSourceLocation> QuerySourceMap
		{
			get
			{
				return this._querySourceMap;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005514 File Offset: 0x00003714
		internal bool[] ResultTableIndexesToCache
		{
			get
			{
				if (this._resultSetIndexesToCache == null)
				{
					this._resultSetIndexesToCache = new bool[this._resultTables.Count];
					for (int i = 0; i < this._resultTables.Count; i++)
					{
						this._resultSetIndexesToCache[i] = this._resultTables[i].IsReusable;
					}
				}
				return this._resultSetIndexesToCache;
			}
		}

		// Token: 0x040000D8 RID: 216
		private readonly string _id;

		// Token: 0x040000D9 RID: 217
		private readonly string _dataSourceId;

		// Token: 0x040000DA RID: 218
		private readonly string _query;

		// Token: 0x040000DB RID: 219
		private readonly ReadOnlyCollection<ResultTable> _resultTables;

		// Token: 0x040000DC RID: 220
		private readonly ReadOnlyCollection<ItemSourceLocation> _querySourceMap;

		// Token: 0x040000DD RID: 221
		private bool[] _resultSetIndexesToCache;
	}
}
