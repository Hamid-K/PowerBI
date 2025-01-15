using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000051 RID: 81
	public interface IReportContext
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DF RID: 223
		string ReportName { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E0 RID: 224
		string ReportPath { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E1 RID: 225
		bool IsLinkedReport { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E2 RID: 226
		string LinkedReportTargetName { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E3 RID: 227
		string LinkedReportTargetPath { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E4 RID: 228
		bool IsSubreport { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E5 RID: 229
		string ParentReportName { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E6 RID: 230
		string ParentReportPath { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E7 RID: 231
		IDictionary<string, IParameter> QueryParameters { get; }
	}
}
