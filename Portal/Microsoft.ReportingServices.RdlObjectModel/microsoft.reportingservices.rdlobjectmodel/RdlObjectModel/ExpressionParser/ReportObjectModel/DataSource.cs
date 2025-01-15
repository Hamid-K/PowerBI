using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
	// Token: 0x020002CB RID: 715
	internal abstract class DataSource
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001601 RID: 5633
		internal abstract string DataSourceReference { get; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001602 RID: 5634
		internal abstract string Type { get; }
	}
}
