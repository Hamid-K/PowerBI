using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000087 RID: 135
	internal sealed class ResultSetCachingManager
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000B52C File Offset: 0x0000972C
		internal ResultSetCachingManager(IReadOnlyList<ResultTableLookupInfo> resultTableInfos, IReadOnlyList<bool[]> localShouldCacheFlags)
		{
			this._resultTableInfos = resultTableInfos;
			this._localShouldCacheFlags = localShouldCacheFlags;
			this._cachedResultSets = new Dictionary<int, InMemoryResultSet>();
			this._globalShouldCacheFlags = new bool[this._resultTableInfos.Count];
			this._localToGlobalResultSetMapping = new int[this._localShouldCacheFlags.Count][];
			this.PopulateMappings();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000B58C File Offset: 0x0000978C
		private void PopulateMappings()
		{
			for (int i = 0; i < this._resultTableInfos.Count; i++)
			{
				ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[i];
				if (resultTableLookupInfo.IsDataSetTable)
				{
					int dataSetIndex = resultTableLookupInfo.DataSetIndex;
					int localTableIndex = resultTableLookupInfo.LocalTableIndex;
					this._globalShouldCacheFlags[i] = this._localShouldCacheFlags[dataSetIndex][localTableIndex];
					if (this._localToGlobalResultSetMapping[dataSetIndex] == null)
					{
						this._localToGlobalResultSetMapping[dataSetIndex] = new int[this._localShouldCacheFlags[dataSetIndex].Length];
					}
					this._localToGlobalResultSetMapping[dataSetIndex][localTableIndex] = i;
				}
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000B618 File Offset: 0x00009818
		internal bool TryCache(IResultSet resultSet, int resultSetIndex)
		{
			if (!this._globalShouldCacheFlags[resultSetIndex])
			{
				return false;
			}
			this.StartResultSetCaching(resultSet, resultSetIndex);
			return true;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B630 File Offset: 0x00009830
		internal void StopSourceCaching(int sourceIndex, int localResultSetIndex)
		{
			int num = this._localToGlobalResultSetMapping[sourceIndex][localResultSetIndex];
			this.StopResultSetCaching(num);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B650 File Offset: 0x00009850
		internal void StopResultSetCaching(int resultSetIndex)
		{
			InMemoryResultSet inMemoryResultSet;
			if (this._cachedResultSets.TryGetValue(resultSetIndex, out inMemoryResultSet))
			{
				inMemoryResultSet.StopCachingAndResetRowIndex();
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B673 File Offset: 0x00009873
		internal bool TryGet(int resultSetIndex, out InMemoryResultSet resultSet)
		{
			return this._cachedResultSets.TryGetValue(resultSetIndex, out resultSet);
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000B684 File Offset: 0x00009884
		internal long TotalCachedRowsCount
		{
			get
			{
				long num = 0L;
				foreach (KeyValuePair<int, InMemoryResultSet> keyValuePair in this._cachedResultSets)
				{
					num += keyValuePair.Value.RowCount;
				}
				return num;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000B6E4 File Offset: 0x000098E4
		internal IReadOnlyRowCache GetAsRowCache(int resultSetIndex)
		{
			InMemoryResultSet inMemoryResultSet;
			if (!this.TryGet(resultSetIndex, out inMemoryResultSet))
			{
				return null;
			}
			return inMemoryResultSet.ToReadOnlyRowCache();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000B704 File Offset: 0x00009904
		internal void CacheAndExhaustUntil(IRowSource source, int resultSetIndex)
		{
			ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[resultSetIndex];
			int dataSetIndex = resultTableLookupInfo.DataSetIndex;
			int localTableIndex = resultTableLookupInfo.LocalTableIndex;
			bool[] array = this._localShouldCacheFlags[dataSetIndex];
			while (source.CurrentResultSetIndex < localTableIndex)
			{
				Contract.RetailAssert(array[source.CurrentResultSetIndex], "Can only step over result set table {0} to result set {1} (data set {2} table {3}) if the former is cached (IsReusable).", source.CurrentResultSetIndex, resultSetIndex, resultTableLookupInfo.DataSetIndex, localTableIndex);
				InMemoryResultSet inMemoryResultSet = this.StartResultSetCaching(source, dataSetIndex, source.CurrentResultSetIndex);
				while (inMemoryResultSet.ReadRow() != null)
				{
				}
				inMemoryResultSet.StopCachingAndResetRowIndex();
				source.NextResultSet();
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000B7A0 File Offset: 0x000099A0
		private InMemoryResultSet StartResultSetCaching(IRowSource rowSource, int sourceIndex, int localResultSetIndex)
		{
			Contract.RetailAssert(localResultSetIndex == rowSource.CurrentResultSetIndex, "Wrong result set index to cache.");
			int num = this._localToGlobalResultSetMapping[sourceIndex][localResultSetIndex];
			return this.StartResultSetCaching(rowSource, num);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000B7D4 File Offset: 0x000099D4
		private InMemoryResultSet StartResultSetCaching(IResultSet resultSet, int resultSetIndex)
		{
			InMemoryResultSet inMemoryResultSet = new InMemoryResultSet(resultSet);
			this._cachedResultSets.Add(resultSetIndex, inMemoryResultSet);
			return inMemoryResultSet;
		}

		// Token: 0x040001E9 RID: 489
		private readonly Dictionary<int, InMemoryResultSet> _cachedResultSets;

		// Token: 0x040001EA RID: 490
		private readonly IReadOnlyList<ResultTableLookupInfo> _resultTableInfos;

		// Token: 0x040001EB RID: 491
		private readonly IReadOnlyList<bool[]> _localShouldCacheFlags;

		// Token: 0x040001EC RID: 492
		private readonly bool[] _globalShouldCacheFlags;

		// Token: 0x040001ED RID: 493
		private readonly int[][] _localToGlobalResultSetMapping;
	}
}
