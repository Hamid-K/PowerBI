using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DE9 RID: 7657
	public sealed class DocumentEvaluationConfig
	{
		// Token: 0x0600BD99 RID: 48537 RVA: 0x00266D1F File Offset: 0x00264F1F
		public DocumentEvaluationConfig()
		{
			this.debug = true;
			this.enableFirewall = true;
			this.requiredModules = EmptyArray<IModule>.Instance;
		}

		// Token: 0x0600BD9A RID: 48538 RVA: 0x00266D40 File Offset: 0x00264F40
		public DocumentEvaluationConfig Clone()
		{
			return new DocumentEvaluationConfig
			{
				debug = this.debug,
				enableFirewall = this.enableFirewall,
				requiredModules = (IModule[])this.requiredModules.Clone()
			};
		}

		// Token: 0x040060C2 RID: 24770
		public bool debug;

		// Token: 0x040060C3 RID: 24771
		public bool enableFirewall;

		// Token: 0x040060C4 RID: 24772
		public IModule[] requiredModules;
	}
}
