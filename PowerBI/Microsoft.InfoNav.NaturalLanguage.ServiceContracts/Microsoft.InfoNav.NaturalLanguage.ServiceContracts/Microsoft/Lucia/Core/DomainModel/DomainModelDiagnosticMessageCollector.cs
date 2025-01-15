using System;
using System.Collections.Generic;
using Microsoft.Lucia.Diagnostics;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000189 RID: 393
	public sealed class DomainModelDiagnosticMessageCollector : IDomainModelDiagnosticContext, IDiagnosticContext<DomainModelDiagnosticMessage>, IDiagnosticContext
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x0000E74C File Offset: 0x0000C94C
		public DomainModelDiagnosticMessageCollector([Nullable] ITracingProvider tracingProvider = null)
		{
			this.Tracing = tracingProvider ?? TracingProviderFactory.Empty;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0000E764 File Offset: 0x0000C964
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0000E76C File Offset: 0x0000C96C
		public bool HasError { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000E775 File Offset: 0x0000C975
		public ITracingProvider Tracing { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000E780 File Offset: 0x0000C980
		public IReadOnlyList<DomainModelDiagnosticMessage> Messages
		{
			get
			{
				IReadOnlyList<DomainModelDiagnosticMessage> messages = this._messages;
				return messages ?? DomainModelDiagnosticMessage.EmptyList();
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000E79E File Offset: 0x0000C99E
		public void Register(DomainModelDiagnosticMessage message)
		{
			if (message.Severity == DiagnosticSeverity.Error)
			{
				this.HasError = true;
			}
			if (this._messages == null)
			{
				this._messages = new List<DomainModelDiagnosticMessage>();
			}
			this._messages.Add(message);
		}

		// Token: 0x040006F9 RID: 1785
		private List<DomainModelDiagnosticMessage> _messages;
	}
}
