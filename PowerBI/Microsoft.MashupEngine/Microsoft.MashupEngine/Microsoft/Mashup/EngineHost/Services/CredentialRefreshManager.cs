using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services.OAuth;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019A3 RID: 6563
	public class CredentialRefreshManager
	{
		// Token: 0x0600A663 RID: 42595 RVA: 0x00226D0C File Offset: 0x00224F0C
		public ResourceCredentialCollection RefreshIfNeeded(ResourceCredentialCollection credentials, bool forceRefresh, Func<ResourceCredentialCollection> refreshMethod)
		{
			bool flag = forceRefresh;
			Dictionary<IResource, DateTime> dictionary = this.forcedRefreshes;
			lock (dictionary)
			{
				if (forceRefresh)
				{
					DateTime utcNow = DateTime.UtcNow;
					DateTime dateTime;
					if (this.forcedRefreshes.TryGetValue(credentials.Resource, out dateTime) && utcNow < dateTime)
					{
						flag = false;
					}
					else
					{
						this.forcedRefreshes[credentials.Resource] = utcNow + CredentialRefreshManager.forcedRefreshWindow;
						this.CleanForcedRefreshCache(utcNow);
					}
				}
			}
			OAuthCredential oauthCredential = credentials.FirstOrDefault<IResourceCredential>() as OAuthCredential;
			if (!flag && oauthCredential != null)
			{
				TimeSpan timeSpan;
				flag = oauthCredential.ToTokenCredential().TryGetExpiresIn(out timeSpan) && timeSpan <= Constants.AccessTokenSoonToExpireInterval;
			}
			if (!flag)
			{
				return credentials;
			}
			return refreshMethod();
		}

		// Token: 0x0600A664 RID: 42596 RVA: 0x00226DD8 File Offset: 0x00224FD8
		private void CleanForcedRefreshCache(DateTime now)
		{
			List<IResource> list = new List<IResource>();
			foreach (KeyValuePair<IResource, DateTime> keyValuePair in this.forcedRefreshes)
			{
				if (DateTime.Compare(keyValuePair.Value, now) < 0)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (IResource resource in list)
			{
				this.forcedRefreshes.Remove(resource);
			}
		}

		// Token: 0x04005698 RID: 22168
		private static readonly TimeSpan forcedRefreshWindow = new TimeSpan(0, 1, 0);

		// Token: 0x04005699 RID: 22169
		private readonly Dictionary<IResource, DateTime> forcedRefreshes = new Dictionary<IResource, DateTime>(ResourceComparer.Instance);
	}
}
