using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B34 RID: 6964
	internal sealed class SessionService : ISessionService
	{
		// Token: 0x0600AE55 RID: 44629 RVA: 0x0023B1D0 File Offset: 0x002393D0
		public SessionService(string session)
		{
			this.session = session;
		}

		// Token: 0x17002BC7 RID: 11207
		// (get) Token: 0x0600AE56 RID: 44630 RVA: 0x0023B1DF File Offset: 0x002393DF
		public string Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x040059E6 RID: 23014
		private readonly string session;
	}
}
