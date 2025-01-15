using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x02000298 RID: 664
	public static class ConfidentialClientApplicationExtensions
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x00052D14 File Offset: 0x00050F14
		public static async Task<bool> StopLongRunningProcessInWebApiAsync(this ILongRunningWebApi clientApp, string longRunningProcessSessionKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await ((ConfidentialClientApplication)clientApp).StopLongRunningProcessInWebApiAsync(longRunningProcessSessionKey, cancellationToken).ConfigureAwait(false);
		}
	}
}
