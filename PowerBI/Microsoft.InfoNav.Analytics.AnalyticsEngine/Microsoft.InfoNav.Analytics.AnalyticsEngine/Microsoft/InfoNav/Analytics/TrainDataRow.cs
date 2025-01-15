using System;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200001F RID: 31
	[ImmutableObject(true)]
	internal abstract class TrainDataRow
	{
		// Token: 0x0600006D RID: 109 RVA: 0x0000367C File Offset: 0x0000187C
		protected TrainDataRow(IDataRow sourceDataRow)
		{
			this._sourceDataRow = sourceDataRow;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000368B File Offset: 0x0000188B
		internal IDataRow SourceDataRow
		{
			get
			{
				return this._sourceDataRow;
			}
		}

		// Token: 0x04000090 RID: 144
		private readonly IDataRow _sourceDataRow;
	}
}
