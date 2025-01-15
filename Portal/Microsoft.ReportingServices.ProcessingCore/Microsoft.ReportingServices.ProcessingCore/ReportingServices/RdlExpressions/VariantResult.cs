using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000572 RID: 1394
	internal struct VariantResult
	{
		// Token: 0x060050A5 RID: 20645 RVA: 0x00152E1C File Offset: 0x0015101C
		internal VariantResult(bool errorOccurred, object v)
		{
			this.ErrorOccurred = errorOccurred;
			this.Value = v;
			this.FieldStatus = DataFieldStatus.None;
			this.ExceptionMessage = null;
			this.TypeCode = TypeCode.Empty;
		}

		// Token: 0x040028A8 RID: 10408
		internal bool ErrorOccurred;

		// Token: 0x040028A9 RID: 10409
		internal DataFieldStatus FieldStatus;

		// Token: 0x040028AA RID: 10410
		internal string ExceptionMessage;

		// Token: 0x040028AB RID: 10411
		internal object Value;

		// Token: 0x040028AC RID: 10412
		internal TypeCode TypeCode;
	}
}
