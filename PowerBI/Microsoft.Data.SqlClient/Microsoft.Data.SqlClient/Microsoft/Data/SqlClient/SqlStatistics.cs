using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200009E RID: 158
	internal sealed class SqlStatistics
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x000263D5 File Offset: 0x000245D5
		internal static SqlStatistics StartTimer(SqlStatistics statistics)
		{
			if (statistics != null && !statistics.RequestExecutionTimer())
			{
				statistics = null;
			}
			return statistics;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000263E6 File Offset: 0x000245E6
		internal static void StopTimer(SqlStatistics statistics)
		{
			if (statistics != null)
			{
				statistics.ReleaseAndUpdateExecutionTimer();
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000263F1 File Offset: 0x000245F1
		internal static ValueSqlStatisticsScope TimedScope(SqlStatistics statistics)
		{
			return new ValueSqlStatisticsScope(statistics);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000027D1 File Offset: 0x000009D1
		internal SqlStatistics()
		{
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000263F9 File Offset: 0x000245F9
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00026401 File Offset: 0x00024601
		internal bool WaitForDoneAfterRow
		{
			get
			{
				return this._waitForDoneAfterRow;
			}
			set
			{
				this._waitForDoneAfterRow = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002640A File Offset: 0x0002460A
		internal bool WaitForReply
		{
			get
			{
				return this._waitForReply;
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00026412 File Offset: 0x00024612
		internal void ContinueOnNewConnection()
		{
			this._startExecutionTimestamp = 0L;
			this._startFetchTimestamp = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00026434 File Offset: 0x00024634
		internal IDictionary GetDictionary()
		{
			return new SqlStatistics.StatisticsDictionary(18)
			{
				{ "BuffersReceived", this._buffersReceived },
				{ "BuffersSent", this._buffersSent },
				{ "BytesReceived", this._bytesReceived },
				{ "BytesSent", this._bytesSent },
				{ "CursorOpens", this._cursorOpens },
				{ "IduCount", this._iduCount },
				{ "IduRows", this._iduRows },
				{ "PreparedExecs", this._preparedExecs },
				{ "Prepares", this._prepares },
				{ "SelectCount", this._selectCount },
				{ "SelectRows", this._selectRows },
				{ "ServerRoundtrips", this._serverRoundtrips },
				{ "SumResultSets", this._sumResultSets },
				{ "Transactions", this._transactions },
				{ "UnpreparedExecs", this._unpreparedExecs },
				{
					"ConnectionTime",
					ADP.TimerToMilliseconds(this._connectionTime)
				},
				{
					"ExecutionTime",
					ADP.TimerToMilliseconds(this._executionTime)
				},
				{
					"NetworkServerTime",
					ADP.TimerToMilliseconds(this._networkServerTime)
				}
			};
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000265E5 File Offset: 0x000247E5
		internal bool RequestExecutionTimer()
		{
			if (this._startExecutionTimestamp == 0L)
			{
				this._startExecutionTimestamp = ADP.TimerCurrent();
				return true;
			}
			return false;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000265FD File Offset: 0x000247FD
		internal void RequestNetworkServerTimer()
		{
			if (this._startNetworkServerTimestamp == 0L)
			{
				this._startNetworkServerTimestamp = ADP.TimerCurrent();
			}
			this._waitForReply = true;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00026619 File Offset: 0x00024819
		internal void ReleaseAndUpdateExecutionTimer()
		{
			if (this._startExecutionTimestamp > 0L)
			{
				this._executionTime += ADP.TimerCurrent() - this._startExecutionTimestamp;
				this._startExecutionTimestamp = 0L;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00026646 File Offset: 0x00024846
		internal void ReleaseAndUpdateNetworkServerTimer()
		{
			if (this._waitForReply && this._startNetworkServerTimestamp > 0L)
			{
				this._networkServerTime += ADP.TimerCurrent() - this._startNetworkServerTimestamp;
				this._startNetworkServerTimestamp = 0L;
			}
			this._waitForReply = false;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00026684 File Offset: 0x00024884
		internal void Reset()
		{
			this._buffersReceived = 0L;
			this._buffersSent = 0L;
			this._bytesReceived = 0L;
			this._bytesSent = 0L;
			this._connectionTime = 0L;
			this._cursorOpens = 0L;
			this._executionTime = 0L;
			this._iduCount = 0L;
			this._iduRows = 0L;
			this._networkServerTime = 0L;
			this._preparedExecs = 0L;
			this._prepares = 0L;
			this._selectCount = 0L;
			this._selectRows = 0L;
			this._serverRoundtrips = 0L;
			this._sumResultSets = 0L;
			this._transactions = 0L;
			this._unpreparedExecs = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
			this._startExecutionTimestamp = 0L;
			this._startNetworkServerTimestamp = 0L;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002673F File Offset: 0x0002493F
		internal void SafeAdd(ref long value, long summand)
		{
			if (9223372036854775807L - value > summand)
			{
				value += summand;
				return;
			}
			value = long.MaxValue;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00026762 File Offset: 0x00024962
		internal long SafeIncrement(ref long value)
		{
			if (value < 9223372036854775807L)
			{
				value += 1L;
			}
			return value;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002677C File Offset: 0x0002497C
		internal void UpdateStatistics()
		{
			if (this._closeTimestamp >= this._openTimestamp && 9223372036854775807L > this._closeTimestamp - this._openTimestamp)
			{
				this._connectionTime = this._closeTimestamp - this._openTimestamp;
				return;
			}
			this._connectionTime = long.MaxValue;
		}

		// Token: 0x0400033F RID: 831
		internal long _closeTimestamp;

		// Token: 0x04000340 RID: 832
		internal long _openTimestamp;

		// Token: 0x04000341 RID: 833
		internal long _startExecutionTimestamp;

		// Token: 0x04000342 RID: 834
		internal long _startFetchTimestamp;

		// Token: 0x04000343 RID: 835
		internal long _startNetworkServerTimestamp;

		// Token: 0x04000344 RID: 836
		internal long _buffersReceived;

		// Token: 0x04000345 RID: 837
		internal long _buffersSent;

		// Token: 0x04000346 RID: 838
		internal long _bytesReceived;

		// Token: 0x04000347 RID: 839
		internal long _bytesSent;

		// Token: 0x04000348 RID: 840
		internal long _connectionTime;

		// Token: 0x04000349 RID: 841
		internal long _cursorOpens;

		// Token: 0x0400034A RID: 842
		internal long _executionTime;

		// Token: 0x0400034B RID: 843
		internal long _iduCount;

		// Token: 0x0400034C RID: 844
		internal long _iduRows;

		// Token: 0x0400034D RID: 845
		internal long _networkServerTime;

		// Token: 0x0400034E RID: 846
		internal long _preparedExecs;

		// Token: 0x0400034F RID: 847
		internal long _prepares;

		// Token: 0x04000350 RID: 848
		internal long _selectCount;

		// Token: 0x04000351 RID: 849
		internal long _selectRows;

		// Token: 0x04000352 RID: 850
		internal long _serverRoundtrips;

		// Token: 0x04000353 RID: 851
		internal long _sumResultSets;

		// Token: 0x04000354 RID: 852
		internal long _transactions;

		// Token: 0x04000355 RID: 853
		internal long _unpreparedExecs;

		// Token: 0x04000356 RID: 854
		private bool _waitForDoneAfterRow;

		// Token: 0x04000357 RID: 855
		private bool _waitForReply;

		// Token: 0x020001D8 RID: 472
		private sealed class StatisticsDictionary : Dictionary<object, object>, IDictionary, ICollection, IEnumerable
		{
			// Token: 0x06001DCF RID: 7631 RVA: 0x0007AF93 File Offset: 0x00079193
			public StatisticsDictionary(int capacity)
				: base(capacity)
			{
			}

			// Token: 0x17000A30 RID: 2608
			// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0007AF9C File Offset: 0x0007919C
			ICollection IDictionary.Keys
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection collection;
					if ((collection = this._keys) == null)
					{
						collection = (this._keys = new SqlStatistics.StatisticsDictionary.Collection(this, base.Keys));
					}
					return collection;
				}
			}

			// Token: 0x17000A31 RID: 2609
			// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0007AFC8 File Offset: 0x000791C8
			ICollection IDictionary.Values
			{
				get
				{
					SqlStatistics.StatisticsDictionary.Collection collection;
					if ((collection = this._values) == null)
					{
						collection = (this._values = new SqlStatistics.StatisticsDictionary.Collection(this, base.Values));
					}
					return collection;
				}
			}

			// Token: 0x06001DD2 RID: 7634 RVA: 0x0007AFF4 File Offset: 0x000791F4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IDictionary)this).GetEnumerator();
			}

			// Token: 0x06001DD3 RID: 7635 RVA: 0x0007AFFC File Offset: 0x000791FC
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}

			// Token: 0x06001DD4 RID: 7636 RVA: 0x0007B074 File Offset: 0x00079274
			private void CopyKeys(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Key, arrayIndex++);
				}
			}

			// Token: 0x06001DD5 RID: 7637 RVA: 0x0007B0D8 File Offset: 0x000792D8
			private void CopyValues(Array array, int arrayIndex)
			{
				this.ValidateCopyToArguments(array, arrayIndex);
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array.SetValue(keyValuePair.Value, arrayIndex++);
				}
			}

			// Token: 0x06001DD6 RID: 7638 RVA: 0x0007B13C File Offset: 0x0007933C
			private void ValidateCopyToArguments(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(Strings.Arg_RankMultiDimNotSupported);
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", Strings.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - arrayIndex < base.Count)
				{
					throw new ArgumentException(Strings.Arg_ArrayPlusOffTooSmall);
				}
			}

			// Token: 0x04001436 RID: 5174
			private SqlStatistics.StatisticsDictionary.Collection _keys;

			// Token: 0x04001437 RID: 5175
			private SqlStatistics.StatisticsDictionary.Collection _values;

			// Token: 0x02000294 RID: 660
			private sealed class Collection : ICollection, IEnumerable
			{
				// Token: 0x06001F5E RID: 8030 RVA: 0x0007FD1C File Offset: 0x0007DF1C
				public Collection(SqlStatistics.StatisticsDictionary dictionary, ICollection collection)
				{
					this._dictionary = dictionary;
					this._collection = collection;
				}

				// Token: 0x17000A52 RID: 2642
				// (get) Token: 0x06001F5F RID: 8031 RVA: 0x0007FD32 File Offset: 0x0007DF32
				int ICollection.Count
				{
					get
					{
						return this._collection.Count;
					}
				}

				// Token: 0x17000A53 RID: 2643
				// (get) Token: 0x06001F60 RID: 8032 RVA: 0x0007FD3F File Offset: 0x0007DF3F
				bool ICollection.IsSynchronized
				{
					get
					{
						return this._collection.IsSynchronized;
					}
				}

				// Token: 0x17000A54 RID: 2644
				// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0007FD4C File Offset: 0x0007DF4C
				object ICollection.SyncRoot
				{
					get
					{
						return this._collection.SyncRoot;
					}
				}

				// Token: 0x06001F62 RID: 8034 RVA: 0x0007FD59 File Offset: 0x0007DF59
				void ICollection.CopyTo(Array array, int arrayIndex)
				{
					if (this._collection is Dictionary<object, object>.KeyCollection)
					{
						this._dictionary.CopyKeys(array, arrayIndex);
						return;
					}
					this._dictionary.CopyValues(array, arrayIndex);
				}

				// Token: 0x06001F63 RID: 8035 RVA: 0x0007FD83 File Offset: 0x0007DF83
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this._collection.GetEnumerator();
				}

				// Token: 0x040017BB RID: 6075
				private readonly SqlStatistics.StatisticsDictionary _dictionary;

				// Token: 0x040017BC RID: 6076
				private readonly ICollection _collection;
			}
		}
	}
}
