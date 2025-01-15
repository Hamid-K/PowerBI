using System;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000AA RID: 170
	internal sealed class IndexRange
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0000DA8F File Offset: 0x0000BC8F
		internal IndexRange()
		{
			this._start = -1;
			this._end = -1;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000DAA5 File Offset: 0x0000BCA5
		internal void ExtendRange(int value)
		{
			if (this._start < 0)
			{
				this._start = value;
			}
			else
			{
				Contract.RetailAssert(value == this._end + 1, "Must increase end index by one.");
			}
			this._end = value;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
		public int Start
		{
			get
			{
				Contract.RetailAssert(this._start > -1, "Range has not been started");
				return this._start;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		public int End
		{
			get
			{
				Contract.RetailAssert(this._end > -1, "Range has not been started");
				return this._end;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000DB0B File Offset: 0x0000BD0B
		public bool IsInRange(int index)
		{
			return this.Start <= index && this.End >= index;
		}

		// Token: 0x04000243 RID: 579
		private const int InitialValue = -1;

		// Token: 0x04000244 RID: 580
		private int _start;

		// Token: 0x04000245 RID: 581
		private int _end;
	}
}
