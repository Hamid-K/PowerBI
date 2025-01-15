using System;
using System.Net;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200133F RID: 4927
	internal class ImpersonationCredential : ICredentials
	{
		// Token: 0x060081E3 RID: 33251 RVA: 0x001B9191 File Offset: 0x001B7391
		public ImpersonationCredential(Func<IDisposable> impersonationWrapper)
		{
			this.impersonationWrapper = impersonationWrapper;
		}

		// Token: 0x060081E4 RID: 33252 RVA: 0x000091AE File Offset: 0x000073AE
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060081E5 RID: 33253 RVA: 0x001B91A0 File Offset: 0x001B73A0
		public IDisposable Impersonate()
		{
			return this.impersonationWrapper();
		}

		// Token: 0x040046A2 RID: 18082
		private Func<IDisposable> impersonationWrapper;
	}
}
