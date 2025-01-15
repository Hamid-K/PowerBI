using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000240 RID: 576
	internal interface IReportLink : ICloneable
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001345 RID: 4933
		Report OwnerReport { get; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001346 RID: 4934
		IReportLink ParentElement { get; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001347 RID: 4935
		string FullyQualifiedName { get; }
	}
}
