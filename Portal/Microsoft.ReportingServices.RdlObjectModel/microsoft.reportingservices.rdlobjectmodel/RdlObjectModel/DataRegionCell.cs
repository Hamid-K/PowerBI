using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B7 RID: 183
	public abstract class DataRegionCell : ReportObject
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x0001B9AA File Offset: 0x00019BAA
		public DataRegionCell()
		{
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001B9B2 File Offset: 0x00019BB2
		internal DataRegionCell(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
