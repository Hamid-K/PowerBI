using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003CB RID: 971
	internal sealed class CollationTypeConverter : ExclusiveStringListConverter
	{
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0007E372 File Offset: 0x0007C572
		internal override string[] Values
		{
			get
			{
				return Constants.CollationTypes;
			}
		}
	}
}
