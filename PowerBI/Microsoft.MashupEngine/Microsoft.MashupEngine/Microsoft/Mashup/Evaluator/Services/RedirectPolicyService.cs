using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D86 RID: 7558
	public sealed class RedirectPolicyService : IRedirectPolicyService
	{
		// Token: 0x0600BBBA RID: 48058 RVA: 0x0025FB96 File Offset: 0x0025DD96
		public RedirectPolicyService(bool legacy)
		{
			this.legacy = legacy;
		}

		// Token: 0x17002E5E RID: 11870
		// (get) Token: 0x0600BBBB RID: 48059 RVA: 0x0025FBA5 File Offset: 0x0025DDA5
		public bool Legacy
		{
			get
			{
				return this.legacy;
			}
		}

		// Token: 0x04005F7F RID: 24447
		private readonly bool legacy;
	}
}
