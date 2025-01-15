using System;
using Microsoft.ReportingServices.OnDemandProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004DC RID: 1244
	public interface IRIFReportScope : IInstancePath
	{
		// Token: 0x06003EB4 RID: 16052
		void AddInScopeEventSource(IInScopeEventSource eventSource);

		// Token: 0x06003EB5 RID: 16053
		void AddInScopeTextBox(TextBox textbox);

		// Token: 0x06003EB6 RID: 16054
		void ResetTextBoxImpls(OnDemandProcessingContext context);

		// Token: 0x06003EB7 RID: 16055
		bool VariableInScope(int sequenceIndex);

		// Token: 0x06003EB8 RID: 16056
		bool TextboxInScope(int sequenceIndex);

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06003EB9 RID: 16057
		// (set) Token: 0x06003EBA RID: 16058
		bool NeedToCacheDataRows { get; set; }
	}
}
