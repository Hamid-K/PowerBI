using System;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Outliers
{
	// Token: 0x02000023 RID: 35
	[ImmutableObject(true)]
	internal sealed class OutlierDataRow : TrainDataRow
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003A34 File Offset: 0x00001C34
		internal OutlierDataRow(IDataRow sourceDataRow, double xValue, double yValue)
			: base(sourceDataRow)
		{
			this.XValue = xValue;
			this.YValue = yValue;
		}

		// Token: 0x04000097 RID: 151
		internal readonly double XValue;

		// Token: 0x04000098 RID: 152
		internal readonly double YValue;
	}
}
