using System;

namespace Microsoft.Mashup.Engine.Interface.DataSourceReference
{
	// Token: 0x02000147 RID: 327
	public interface IFormulaCreationResult
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005B1 RID: 1457
		bool Success { get; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005B2 RID: 1458
		string Formula { get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005B3 RID: 1459
		DataSourceReferenceReaderFailureReason FailureReason { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005B4 RID: 1460
		string ErrorMessage { get; }
	}
}
