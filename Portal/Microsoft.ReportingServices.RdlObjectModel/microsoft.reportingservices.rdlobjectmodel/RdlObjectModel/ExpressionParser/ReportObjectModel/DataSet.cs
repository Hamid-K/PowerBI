using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
	// Token: 0x020002C9 RID: 713
	internal abstract class DataSet
	{
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060015FC RID: 5628
		internal abstract string CommandText { get; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060015FD RID: 5629
		internal abstract string RewrittenCommandText { get; }
	}
}
