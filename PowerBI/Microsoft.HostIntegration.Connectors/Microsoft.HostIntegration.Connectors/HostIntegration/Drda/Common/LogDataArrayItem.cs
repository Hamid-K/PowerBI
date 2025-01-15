using System;
using System.Threading;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000843 RID: 2115
	public class LogDataArrayItem
	{
		// Token: 0x06004324 RID: 17188 RVA: 0x000E1394 File Offset: 0x000DF594
		public LogDataArrayItem()
		{
			this._data = new LogData[10000];
			this._id = Interlocked.Increment(ref LogDataArrayItem._globalId);
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000E13BC File Offset: 0x000DF5BC
		public void Reset()
		{
			this._id = Interlocked.Increment(ref LogDataArrayItem._globalId);
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x000E13CE File Offset: 0x000DF5CE
		public LogData[] LogDataArray
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000E13D6 File Offset: 0x000DF5D6
		public override bool Equals(object obj)
		{
			return obj is LogDataArrayItem && ((LogDataArrayItem)obj)._id == this._id;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x0003FF3A File Offset: 0x0003E13A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002F5E RID: 12126
		private static long _globalId;

		// Token: 0x04002F5F RID: 12127
		private LogData[] _data;

		// Token: 0x04002F60 RID: 12128
		private long _id;
	}
}
