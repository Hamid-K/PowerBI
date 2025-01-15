using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A5 RID: 1701
	[Serializable]
	internal sealed class DataSourceList : ArrayList
	{
		// Token: 0x06005C84 RID: 23684 RVA: 0x00179A00 File Offset: 0x00177C00
		internal DataSourceList()
		{
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x00179A08 File Offset: 0x00177C08
		internal DataSourceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700207F RID: 8319
		internal DataSource this[int index]
		{
			get
			{
				return (DataSource)base[index];
			}
		}
	}
}
