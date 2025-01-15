using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012E9 RID: 4841
	public sealed class EnvironmentModule : Module
	{
		// Token: 0x06008037 RID: 32823 RVA: 0x001B5467 File Offset: 0x001B3667
		public EnvironmentModule(RecordValue environment)
		{
			this.exports = environment;
		}

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x06008038 RID: 32824 RVA: 0x001B5476 File Offset: 0x001B3676
		public override Keys ExportKeys
		{
			get
			{
				return this.exports.Keys;
			}
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x001B5483 File Offset: 0x001B3683
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return this.exports;
		}

		// Token: 0x040045CE RID: 17870
		private RecordValue exports;
	}
}
