using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000D4 RID: 212
	internal sealed class Row
	{
		// Token: 0x06000EEE RID: 3822 RVA: 0x0002F72A File Offset: 0x0002D92A
		internal Row(int rowCount)
		{
			this._dataFields = new object[rowCount];
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0002F73E File Offset: 0x0002D93E
		internal object[] DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x17000801 RID: 2049
		internal object this[int index]
		{
			get
			{
				return this._dataFields[index];
			}
		}

		// Token: 0x04000660 RID: 1632
		private readonly object[] _dataFields;
	}
}
