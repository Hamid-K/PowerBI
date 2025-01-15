using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000010 RID: 16
	public sealed class ExecutionMetricsDataReader : IDataReader, IDisposable
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002AAE File Offset: 0x00000CAE
		public ExecutionMetricsDataReader(IDataReader underlyingReader, ExecutionMetricsColumnNames columnNames, int? maxEventCount, ExecutionMetricsCache cache)
		{
			this._underlyingReader = underlyingReader;
			this._columnNames = columnNames;
			this._maxEventCount = maxEventCount;
			this._cache = cache;
			this._hasResultSet = true;
			this._hasPendingNextResult = false;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002AE1 File Offset: 0x00000CE1
		public int ColumnCount
		{
			get
			{
				return this._underlyingReader.ColumnCount;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002AEE File Offset: 0x00000CEE
		public bool IsOpen
		{
			get
			{
				return this._underlyingReader.IsOpen;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AFB File Offset: 0x00000CFB
		public void ConsumeMetrics()
		{
			this._consumeMetricsCalled = true;
			if (this.ConsumeAndBufferExecutionMetrics())
			{
				this.NextResult();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B13 File Offset: 0x00000D13
		public void Close()
		{
			this._underlyingReader.Close();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B20 File Offset: 0x00000D20
		public void Dispose()
		{
			this._underlyingReader.Dispose();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B2D File Offset: 0x00000D2D
		public string GetColumnName(int index)
		{
			return this._underlyingReader.GetColumnName(index);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B3B File Offset: 0x00000D3B
		public Type GetColumnType(int index)
		{
			return this._underlyingReader.GetColumnType(index);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B49 File Offset: 0x00000D49
		public int GetOrdinal(string name)
		{
			return this._underlyingReader.GetOrdinal(name);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B57 File Offset: 0x00000D57
		public object GetValue(int index)
		{
			return this._underlyingReader.GetValue(index);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B65 File Offset: 0x00000D65
		public void GetValues(object[] values)
		{
			this._underlyingReader.GetValues(values);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B73 File Offset: 0x00000D73
		public bool MoveNext()
		{
			return !this._hasPendingNextResult && this._underlyingReader.MoveNext();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B8A File Offset: 0x00000D8A
		public bool NextResult()
		{
			while (this.InternalNextResult() && this.ConsumeAndBufferExecutionMetrics())
			{
			}
			return this._hasResultSet;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BA2 File Offset: 0x00000DA2
		private bool InternalNextResult()
		{
			if (this._hasPendingNextResult)
			{
				return true;
			}
			this._hasPendingNextResult = false;
			this._hasResultSet = this._hasResultSet && this._underlyingReader.NextResult();
			return this._hasResultSet;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private bool ConsumeAndBufferExecutionMetrics()
		{
			int[] array;
			if (!this.TryBuildSchemaMapping(this._columnNames, out array))
			{
				return false;
			}
			bool flag = false;
			List<ExecutionEventData> list = new List<ExecutionEventData>();
			try
			{
				while (this._underlyingReader.MoveNext())
				{
					if (this._maxEventCount != null)
					{
						int count = list.Count;
						int? maxEventCount = this._maxEventCount;
						if ((count == maxEventCount.GetValueOrDefault()) & (maxEventCount != null))
						{
							flag = true;
							continue;
						}
					}
					string text = this.ReadIdValue(ExecutionMetricsSlot.Id, array);
					string text2 = this.ReadIdValue(ExecutionMetricsSlot.ParentId, array);
					string text3 = this.ReadValue<string>(ExecutionMetricsSlot.Name, array);
					string text4 = this.ReadValue<string>(ExecutionMetricsSlot.Component, array);
					DateTime value = this.ReadDateTimeAsUTC(ExecutionMetricsSlot.Start, array).Value;
					DateTime? dateTime = this.ReadDateTimeAsUTC(ExecutionMetricsSlot.End, array);
					string text5 = this.ReadValue<string>(ExecutionMetricsSlot.Metrics, array);
					list.Add(new ExecutionEventData(text, text2, text3, text4, value, dateTime, text5));
				}
			}
			finally
			{
				this._cache.Enqueue(new ExecutionMetricsCachedResultSet(list, flag));
			}
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CD4 File Offset: 0x00000ED4
		private string ReadIdValue(ExecutionMetricsSlot slot, int[] schemaMapping)
		{
			return ExecutionMetricsUtils.ToIdString(this.ReadValue(slot, schemaMapping));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private DateTime? ReadDateTimeAsUTC(ExecutionMetricsSlot slot, int[] schemaMapping)
		{
			DateTime? dateTime = this.ReadValue<DateTime?>(slot, schemaMapping);
			if (dateTime == null)
			{
				return dateTime;
			}
			return new DateTime?(new DateTime(dateTime.Value.Ticks, DateTimeKind.Utc));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D1F File Offset: 0x00000F1F
		private T ReadValue<T>(ExecutionMetricsSlot slot, int[] schemaMapping)
		{
			return (T)((object)this.ReadValue(slot, schemaMapping));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D2E File Offset: 0x00000F2E
		private object ReadValue(ExecutionMetricsSlot slot, int[] schemaMapping)
		{
			return this._underlyingReader.GetValue(schemaMapping[(int)slot]);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D40 File Offset: 0x00000F40
		private bool TryBuildSchemaMapping(ExecutionMetricsColumnNames columnNames, out int[] ordinalMapping)
		{
			ordinalMapping = null;
			int columnCount = this._underlyingReader.ColumnCount;
			if (columnCount != columnNames.Count)
			{
				return false;
			}
			for (int i = 0; i < columnCount; i++)
			{
				string columnName = this._underlyingReader.GetColumnName(i);
				ExecutionMetricsSlot executionMetricsSlot;
				if (!columnNames.TryGetSlot(columnName, out executionMetricsSlot))
				{
					return false;
				}
				if (ordinalMapping == null)
				{
					ordinalMapping = new int[columnNames.Count];
				}
				ordinalMapping[(int)executionMetricsSlot] = i;
			}
			return true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public void DiscardRowsAndConsumeMetrics()
		{
			if (!this._hasResultSet)
			{
				return;
			}
			while (this.MoveNext())
			{
			}
			if (this.InternalNextResult() && !this.ConsumeAndBufferExecutionMetrics())
			{
				this._hasPendingNextResult = true;
			}
		}

		// Token: 0x04000070 RID: 112
		private readonly IDataReader _underlyingReader;

		// Token: 0x04000071 RID: 113
		private readonly ExecutionMetricsColumnNames _columnNames;

		// Token: 0x04000072 RID: 114
		private readonly int? _maxEventCount;

		// Token: 0x04000073 RID: 115
		private bool _consumeMetricsCalled;

		// Token: 0x04000074 RID: 116
		private bool _hasResultSet;

		// Token: 0x04000075 RID: 117
		private bool _hasPendingNextResult;

		// Token: 0x04000076 RID: 118
		private ExecutionMetricsCache _cache;
	}
}
