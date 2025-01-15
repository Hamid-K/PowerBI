using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000012 RID: 18
	internal sealed class WindowsUserIdentityConnectionImpersonator : IConnectionUserImpersonator
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002E1C File Offset: 0x0000101C
		public WindowsUserIdentityConnectionImpersonator(WindowsIdentity identity)
		{
			this._identity = identity;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E2C File Offset: 0x0000102C
		public void ExecuteInContext(Action action)
		{
			using (this._identity.Impersonate())
			{
				action();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E68 File Offset: 0x00001068
		public T ExecuteInContext<T>(Func<T> func)
		{
			T t;
			using (this._identity.Impersonate())
			{
				t = func();
			}
			return t;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EA8 File Offset: 0x000010A8
		public async Task ExecuteInContextAsync(Func<Task> func)
		{
			using (this._identity.Impersonate())
			{
				await func();
			}
			WindowsImpersonationContext windowsImpersonationContext = null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002EF4 File Offset: 0x000010F4
		public async Task<T> ExecuteInContextAsync<T>(Func<Task<T>> func)
		{
			T t;
			using (this._identity.Impersonate())
			{
				t = await func();
			}
			return t;
		}

		// Token: 0x0400004D RID: 77
		private readonly WindowsIdentity _identity;
	}
}
