using System;
using System.Runtime.Serialization;
using System.Threading;

namespace Microsoft.DataShaping.Processing.QueryExecutionCommon
{
	// Token: 0x0200006E RID: 110
	[DataContract]
	internal sealed class QueryExecutionStatistics
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000786D File Offset: 0x00005A6D
		internal void RegisterOpenConnection(bool isFromPool)
		{
			Interlocked.Increment(ref this._connectionCount);
			if (isFromPool)
			{
				Interlocked.Increment(ref this._connectionsFromPoolCount);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000788A File Offset: 0x00005A8A
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00007892 File Offset: 0x00005A92
		[DataMember(Name = "ConnCount", EmitDefaultValue = false, Order = 10)]
		public int ConnectionCount
		{
			get
			{
				return this._connectionCount;
			}
			private set
			{
				this._connectionCount = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000789B File Offset: 0x00005A9B
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000078A3 File Offset: 0x00005AA3
		[DataMember(Name = "ConnPoolCount", EmitDefaultValue = false, Order = 20)]
		public int ConnectionsFromPoolCount
		{
			get
			{
				return this._connectionsFromPoolCount;
			}
			private set
			{
				this._connectionsFromPoolCount = value;
			}
		}

		// Token: 0x04000193 RID: 403
		private int _connectionCount;

		// Token: 0x04000194 RID: 404
		private int _connectionsFromPoolCount;
	}
}
