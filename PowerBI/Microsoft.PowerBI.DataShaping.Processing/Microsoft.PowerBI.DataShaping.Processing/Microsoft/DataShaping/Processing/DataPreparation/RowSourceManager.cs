using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000088 RID: 136
	internal sealed class RowSourceManager : IRowSourceManager
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000B7F8 File Offset: 0x000099F8
		internal RowSourceManager(IReadOnlyList<IRowSource> rowSources, IReadOnlyList<ResultTableLookupInfo> resultTableInfos, IReadOnlyList<bool[]> localCacheFlags, Microsoft.DataShaping.ServiceContracts.ITracer tracer, IDataTransformManager transformManager)
		{
			this._rowSources = rowSources;
			this._resultTableInfos = resultTableInfos;
			this._activeResultSetIndex = -1;
			this._isSourceSetup = new bool[rowSources.Count];
			this._tracer = tracer;
			this._transformManager = transformManager;
			this._transformResultSets = new Dictionary<int, IResultSet>();
			this._resultSetIndexToTypes = new Dictionary<int, IReadOnlyList<Type>>(resultTableInfos.Count);
			this._cachingManager = new ResultSetCachingManager(resultTableInfos, localCacheFlags);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000B86C File Offset: 0x00009A6C
		public bool TrySetupResultSet(int resultSetIndex)
		{
			return resultSetIndex < this._resultTableInfos.Count && (this.TrySetupFromCache(resultSetIndex, true) || (this.TrySetupFromInput(resultSetIndex) && (!this._cachingManager.TryCache(this._activeResultSet, resultSetIndex) || this.TrySetupFromCache(resultSetIndex, false))));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000B8BE File Offset: 0x00009ABE
		public bool TryRestoreResultSet(int resultSetIndex)
		{
			return resultSetIndex < this._resultTableInfos.Count && (this._activeResultSetIndex == resultSetIndex || this.TrySetupFromCache(resultSetIndex, false) || this.TryRestoreFromInput(resultSetIndex));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000B8F3 File Offset: 0x00009AF3
		public IDataRow ReadRow()
		{
			Contract.RetailAssert(this._activeResultSetIndex != -1, "Cannot read row before calling SetupResultSet");
			return this._activeResultSet.ReadRow();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000B918 File Offset: 0x00009B18
		public IReadOnlyList<Type> GetActiveResultSetColumnTypes()
		{
			Contract.RetailAssert(this._activeResultSetIndex != -1, "Cannot obtain result set schema information before calling SetupResultSet");
			IReadOnlyList<Type> readOnlyList;
			if (!this._resultSetIndexToTypes.TryGetValue(this._activeResultSetIndex, out readOnlyList))
			{
				if (this._activeResultSet.Schema == null && this._activeResultSet.RowCount == 0L)
				{
					readOnlyList = new Type[0];
				}
				else
				{
					readOnlyList = this._activeResultSet.Schema.GetColumnTypes();
				}
				this._resultSetIndexToTypes.Add(this._activeResultSetIndex, readOnlyList);
			}
			return readOnlyList;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000B998 File Offset: 0x00009B98
		public bool WasSetup(int resultSetIndex)
		{
			ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[resultSetIndex];
			if (resultTableLookupInfo.IsDataSetTable)
			{
				IRowSource rowSource = this.GetRowSource(resultTableLookupInfo);
				return this._isSourceSetup[resultTableLookupInfo.DataSetIndex] && rowSource.CurrentResultSetIndex >= resultTableLookupInfo.LocalTableIndex;
			}
			return this._transformResultSets.ContainsKey(resultTableLookupInfo.DataTransformIndex);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		public bool HasRows(int resultSetIndex)
		{
			ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[resultSetIndex];
			if (resultTableLookupInfo.IsDataSetTable)
			{
				return this.GetRowSource(resultTableLookupInfo).GetRowCount(resultTableLookupInfo.LocalTableIndex) > 0L;
			}
			IResultSet resultSet;
			return this._transformResultSets.TryGetValue(resultTableLookupInfo.DataTransformIndex, out resultSet) && resultSet.RowCount > 0L;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000BA52 File Offset: 0x00009C52
		public int ActiveResultSetIndex
		{
			get
			{
				return this._activeResultSetIndex;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000BA5A File Offset: 0x00009C5A
		internal IResultSet ActiveResultSet
		{
			get
			{
				return this._activeResultSet;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000BA62 File Offset: 0x00009C62
		public long TotalCachedRowsCount
		{
			get
			{
				return this._cachingManager.TotalCachedRowsCount;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000BA6F File Offset: 0x00009C6F
		public IReadOnlyRowCache ActiveRowCache
		{
			get
			{
				return this._cachingManager.GetAsRowCache(this._activeResultSetIndex);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000BA82 File Offset: 0x00009C82
		public IReadOnlyList<Message> Messages
		{
			get
			{
				if (this._transformManager != null)
				{
					return this._transformManager.GetMessages();
				}
				return null;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000BA9C File Offset: 0x00009C9C
		private bool TrySetupFromCache(int resultSetIndex, bool shouldStopCaching)
		{
			InMemoryResultSet inMemoryResultSet;
			if (this._cachingManager.TryGet(resultSetIndex, out inMemoryResultSet))
			{
				if (shouldStopCaching)
				{
					this._cachingManager.StopResultSetCaching(resultSetIndex);
				}
				this.SetActiveResultSet(inMemoryResultSet, resultSetIndex);
				return true;
			}
			return false;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		private bool TrySetupFromInput(int resultSetIndex)
		{
			ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[resultSetIndex];
			if (resultTableLookupInfo.IsDataSetTable)
			{
				IRowSource rowSource = this.GetRowSource(resultTableLookupInfo);
				return this.TrySetupSource(rowSource, resultSetIndex, resultTableLookupInfo.DataSetIndex, resultTableLookupInfo.LocalTableIndex);
			}
			return this.TrySetupFromTransform(resultTableLookupInfo, resultSetIndex);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000BB1C File Offset: 0x00009D1C
		private bool TrySetupFromTransform(ResultTableLookupInfo tableInfo, int resultSetIndex)
		{
			IResultSet resultSet;
			if (!this._transformResultSets.TryGetValue(tableInfo.DataTransformIndex, out resultSet))
			{
				int inputResultSetIndex = this._transformManager.GetInputResultSetIndex(tableInfo);
				if (!this.TrySetupResultSet(inputResultSetIndex))
				{
					return false;
				}
				resultSet = this._transformManager.GetResultSetAsync(this._activeResultSet, tableInfo).WaitAndUnwrapResult<IResultSet>();
				this._transformResultSets.Add(tableInfo.DataTransformIndex, resultSet);
			}
			this.SetActiveResultSet(resultSet, resultSetIndex);
			return true;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000BB89 File Offset: 0x00009D89
		private bool TrySetupSource(IRowSource rowSource, int resultSetIndex, int sourceIndex, int localResultSetIndex)
		{
			if (this.TrySetupSourceAtCurrentIndex(rowSource, resultSetIndex, sourceIndex, localResultSetIndex))
			{
				return true;
			}
			if (this.TryMoveToResultSet(rowSource, resultSetIndex, sourceIndex, localResultSetIndex))
			{
				this.SetActiveResultSet(rowSource, resultSetIndex, sourceIndex);
				return true;
			}
			this._activeResultSet = null;
			return false;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000BBBC File Offset: 0x00009DBC
		private bool TryMoveToResultSet(IRowSource rowSource, int resultSetIndex, int sourceIndex, int localResultSetIndex)
		{
			this._cachingManager.StopSourceCaching(sourceIndex, rowSource.CurrentResultSetIndex);
			bool flag = !this._isSourceSetup[sourceIndex];
			if (rowSource.CurrentResultSetIndex + 1 < localResultSetIndex || flag)
			{
				if (!flag)
				{
					if (rowSource.ReadRow() != null)
					{
						Contract.RetailFail("Cannot move to next result set without reading all previous rows.");
					}
					if (!rowSource.NextResultSet())
					{
						return false;
					}
				}
				this._cachingManager.CacheAndExhaustUntil(rowSource, resultSetIndex);
				this._tracer.SanitizedTrace(TraceLevel.Info, "RowSourceManager: CachedAndExhausted sourceIdx:{0} from idx:{1} to idx:{2}", sourceIndex, rowSource.CurrentResultSetIndex, localResultSetIndex);
				return true;
			}
			Contract.RetailAssert(rowSource.ReadRow() == null, "Cannot move to next result set without reading all previous rows.");
			return rowSource.NextResultSet();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000BC67 File Offset: 0x00009E67
		private bool TrySetupSourceAtCurrentIndex(IRowSource rowSource, int resultSetIndex, int sourceIndex, int localResultSetIndex)
		{
			if (rowSource.CurrentResultSetIndex != localResultSetIndex)
			{
				return false;
			}
			this.SetActiveResultSet(rowSource, resultSetIndex, sourceIndex);
			return true;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000BC80 File Offset: 0x00009E80
		private bool TryRestoreFromInput(int resultSetIndex)
		{
			ResultTableLookupInfo resultTableLookupInfo = this._resultTableInfos[resultSetIndex];
			if (resultTableLookupInfo.IsDataSetTable)
			{
				IRowSource rowSource = this.GetRowSource(resultTableLookupInfo);
				return this.TrySetupSourceAtCurrentIndex(rowSource, resultSetIndex, resultTableLookupInfo.DataSetIndex, resultTableLookupInfo.LocalTableIndex);
			}
			return this.TryRestoreFromTransform(resultTableLookupInfo, resultSetIndex);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		private bool TryRestoreFromTransform(ResultTableLookupInfo tableInfo, int resultSetIndex)
		{
			IResultSet resultSet;
			if (!this._transformResultSets.TryGetValue(tableInfo.DataTransformIndex, out resultSet))
			{
				return false;
			}
			this.SetActiveResultSet(resultSet, resultSetIndex);
			return true;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000BCF5 File Offset: 0x00009EF5
		private void SetActiveResultSet(IResultSet resultSet, int resultSetIndex)
		{
			this._activeResultSet = resultSet;
			this._activeResultSetIndex = resultSetIndex;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000BD05 File Offset: 0x00009F05
		private void SetActiveResultSet(IResultSet resultSet, int resultSetIndex, int sourceIndex)
		{
			this.SetActiveResultSet(resultSet, resultSetIndex);
			this._isSourceSetup[sourceIndex] = true;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000BD18 File Offset: 0x00009F18
		private IRowSource GetRowSource(ResultTableLookupInfo tableInfo)
		{
			return this._rowSources[tableInfo.DataSetIndex];
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000BD2B File Offset: 0x00009F2B
		internal IDataTransformManager TransformManager
		{
			get
			{
				return this._transformManager;
			}
		}

		// Token: 0x040001EE RID: 494
		private const int DefaultResultSetIndex = -1;

		// Token: 0x040001EF RID: 495
		private readonly Microsoft.DataShaping.ServiceContracts.ITracer _tracer;

		// Token: 0x040001F0 RID: 496
		private readonly IReadOnlyList<IRowSource> _rowSources;

		// Token: 0x040001F1 RID: 497
		private readonly IReadOnlyList<ResultTableLookupInfo> _resultTableInfos;

		// Token: 0x040001F2 RID: 498
		private readonly ResultSetCachingManager _cachingManager;

		// Token: 0x040001F3 RID: 499
		private readonly IDataTransformManager _transformManager;

		// Token: 0x040001F4 RID: 500
		private readonly bool[] _isSourceSetup;

		// Token: 0x040001F5 RID: 501
		private readonly Dictionary<int, IResultSet> _transformResultSets;

		// Token: 0x040001F6 RID: 502
		private readonly Dictionary<int, IReadOnlyList<Type>> _resultSetIndexToTypes;

		// Token: 0x040001F7 RID: 503
		private IResultSet _activeResultSet;

		// Token: 0x040001F8 RID: 504
		private int _activeResultSetIndex;
	}
}
