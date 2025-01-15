using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x02000216 RID: 534
	internal class SingletonThrottlingManager : IThrottlingProvider
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x000495D8 File Offset: 0x000477D8
		public IEnumerable<IThrottlingProvider> ThrottlingProviders { get; }

		// Token: 0x06001633 RID: 5683 RVA: 0x000495E0 File Offset: 0x000477E0
		private SingletonThrottlingManager()
		{
			this.ThrottlingProviders = new List<IThrottlingProvider>
			{
				new RetryAfterProvider(),
				new HttpStatusProvider(),
				new UiRequiredProvider()
			};
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00049614 File Offset: 0x00047814
		public static SingletonThrottlingManager GetInstance()
		{
			return SingletonThrottlingManager.lazyPrivateCtor.Value;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00049620 File Offset: 0x00047820
		public void RecordException(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams, MsalServiceException ex)
		{
			if (!(ex is MsalThrottledServiceException))
			{
				foreach (IThrottlingProvider throttlingProvider in this.ThrottlingProviders)
				{
					throttlingProvider.RecordException(requestParams, bodyParams, ex);
				}
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00049678 File Offset: 0x00047878
		public void TryThrottle(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams)
		{
			foreach (IThrottlingProvider throttlingProvider in this.ThrottlingProviders)
			{
				throttlingProvider.TryThrottle(requestParams, bodyParams);
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000496C4 File Offset: 0x000478C4
		public void ResetCache()
		{
			foreach (IThrottlingProvider throttlingProvider in this.ThrottlingProviders)
			{
				throttlingProvider.ResetCache();
			}
		}

		// Token: 0x0400096B RID: 2411
		private static readonly Lazy<SingletonThrottlingManager> lazyPrivateCtor = new Lazy<SingletonThrottlingManager>(() => new SingletonThrottlingManager());
	}
}
