using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D80 RID: 7552
	internal class CurrentEnvironmentService : ICurrentEnvironmentService
	{
		// Token: 0x17002E59 RID: 11865
		// (get) Token: 0x0600BBAA RID: 48042 RVA: 0x0025FA54 File Offset: 0x0025DC54
		// (set) Token: 0x0600BBAB RID: 48043 RVA: 0x0025FA5C File Offset: 0x0025DC5C
		public IRecordValue Environment
		{
			get
			{
				return this.environment;
			}
			set
			{
				this.environment = value;
			}
		}

		// Token: 0x04005F77 RID: 24439
		private IRecordValue environment;
	}
}
