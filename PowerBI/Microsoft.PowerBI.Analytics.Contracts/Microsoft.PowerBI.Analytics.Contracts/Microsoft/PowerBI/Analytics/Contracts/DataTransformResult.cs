using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200000A RID: 10
	public sealed class DataTransformResult
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000021D9 File Offset: 0x000003D9
		public DataTransformResult(IEnumerable<IDataRow> rows)
			: this(rows, null)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021E3 File Offset: 0x000003E3
		public DataTransformResult(IEnumerable<IDataRow> rows, IReadOnlyList<DataTransformMessage> messages)
		{
			this._rows = rows;
			this._messages = messages;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021F9 File Offset: 0x000003F9
		public IEnumerable<IDataRow> Rows
		{
			get
			{
				return this._rows;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002201 File Offset: 0x00000401
		public IReadOnlyList<DataTransformMessage> Messages
		{
			get
			{
				return this._messages;
			}
		}

		// Token: 0x04000036 RID: 54
		private readonly IEnumerable<IDataRow> _rows;

		// Token: 0x04000037 RID: 55
		private readonly IReadOnlyList<DataTransformMessage> _messages;
	}
}
