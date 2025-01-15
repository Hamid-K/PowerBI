using System;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000068 RID: 104
	internal interface IScopeHandler
	{
		// Token: 0x0600039E RID: 926
		DiagnosticScope CreateScope(ClientDiagnostics diagnostics, string name);

		// Token: 0x0600039F RID: 927
		void Start(string name, in DiagnosticScope scope);

		// Token: 0x060003A0 RID: 928
		void Dispose(string name, in DiagnosticScope scope);

		// Token: 0x060003A1 RID: 929
		void Fail(string name, in DiagnosticScope scope, Exception exception);
	}
}
