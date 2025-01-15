using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000777 RID: 1911
	internal struct VariantResult
	{
		// Token: 0x06006A24 RID: 27172 RVA: 0x001AA14D File Offset: 0x001A834D
		internal VariantResult(bool errorOccurred, object v)
		{
			this.ErrorOccurred = errorOccurred;
			this.Value = v;
			this.FieldStatus = DataFieldStatus.None;
			this.ExceptionMessage = null;
		}

		// Token: 0x040035C6 RID: 13766
		internal bool ErrorOccurred;

		// Token: 0x040035C7 RID: 13767
		internal DataFieldStatus FieldStatus;

		// Token: 0x040035C8 RID: 13768
		internal string ExceptionMessage;

		// Token: 0x040035C9 RID: 13769
		internal object Value;
	}
}
