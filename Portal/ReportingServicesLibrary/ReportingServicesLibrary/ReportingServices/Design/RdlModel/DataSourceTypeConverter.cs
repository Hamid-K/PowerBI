using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C9 RID: 969
	internal sealed class DataSourceTypeConverter : ExclusiveStringListConverter
	{
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0007E2E4 File Offset: 0x0007C4E4
		internal override string[] Values
		{
			get
			{
				return Constants.DataSourceTypes;
			}
		}
	}
}
