using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000844 RID: 2116
	public class TraceEntryArrayPool
	{
		// Token: 0x0600432A RID: 17194 RVA: 0x000E13F8 File Offset: 0x000DF5F8
		private TraceEntryArrayPool()
		{
			for (int i = 0; i < this._minPoolSize; i++)
			{
				this._pool.Push(new LogDataArrayItem());
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x000E1449 File Offset: 0x000DF649
		public static TraceEntryArrayPool Instance
		{
			get
			{
				return TraceEntryArrayPool._instance;
			}
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000E1450 File Offset: 0x000DF650
		public static LogDataArrayItem Get()
		{
			TraceEntryArrayPool instance = TraceEntryArrayPool._instance;
			LogDataArrayItem logDataArrayItem2;
			lock (instance)
			{
				LogDataArrayItem logDataArrayItem;
				if (TraceEntryArrayPool.Instance._pool.Count != 0)
				{
					logDataArrayItem = TraceEntryArrayPool.Instance._pool.Pop();
				}
				else
				{
					logDataArrayItem = new LogDataArrayItem();
				}
				logDataArrayItem2 = logDataArrayItem;
			}
			return logDataArrayItem2;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000E14B8 File Offset: 0x000DF6B8
		public static void Put(LogDataArrayItem item)
		{
			TraceEntryArrayPool instance = TraceEntryArrayPool._instance;
			lock (instance)
			{
				if (TraceEntryArrayPool.Instance._pool.Count < TraceEntryArrayPool.Instance._maxPoolSize)
				{
					item.Reset();
					TraceEntryArrayPool.Instance._pool.Push(item);
				}
			}
		}

		// Token: 0x04002F61 RID: 12129
		public const int MAX_BUFFER_SIZE = 10000;

		// Token: 0x04002F62 RID: 12130
		private int _minPoolSize = 3;

		// Token: 0x04002F63 RID: 12131
		private int _maxPoolSize = 1000;

		// Token: 0x04002F64 RID: 12132
		private Stack<LogDataArrayItem> _pool = new Stack<LogDataArrayItem>();

		// Token: 0x04002F65 RID: 12133
		private static readonly TraceEntryArrayPool _instance = new TraceEntryArrayPool();
	}
}
