using System;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x02000038 RID: 56
	public interface IDiagnosticContext<TMessage> : IDiagnosticContext where TMessage : DiagnosticMessage
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E4 RID: 228
		bool HasError { get; }

		// Token: 0x060000E5 RID: 229
		void Register(TMessage message);
	}
}
