using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200066F RID: 1647
	internal class TraceContainerInformation
	{
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000B9F3A File Offset: 0x000B813A
		// (set) Token: 0x06003727 RID: 14119 RVA: 0x000B9F42 File Offset: 0x000B8142
		internal TraceContainer DefinitionTraceContainer { get; set; }

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000B9F4B File Offset: 0x000B814B
		// (set) Token: 0x06003729 RID: 14121 RVA: 0x000B9F53 File Offset: 0x000B8153
		internal Dictionary<long, TraceContainer> CorrelatorToInstances { get; set; }

		// Token: 0x0600372A RID: 14122 RVA: 0x000B9F5C File Offset: 0x000B815C
		internal TraceContainerInformation(TraceContainer definitionTraceContainer)
		{
			this.DefinitionTraceContainer = definitionTraceContainer;
			definitionTraceContainer.TraceContainerInformation = this;
			if (definitionTraceContainer.LongRunning)
			{
				this.CorrelatorToInstances = new Dictionary<long, TraceContainer>();
			}
		}
	}
}
