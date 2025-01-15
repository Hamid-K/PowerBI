using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000076 RID: 118
	internal interface IQueryConstraint : IEquatable<IQueryConstraint>
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005DE RID: 1502
		int PaddedCount { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005DF RID: 1503
		int RawCount { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005E0 RID: 1504
		bool IsWindow { get; }
	}
}
