using System;
using System.Xml.Serialization;
using Microsoft.BIServer.Configuration.Exceptions;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200002B RID: 43
	public sealed class URL
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00006527 File Offset: 0x00004727
		public URL(string urlString, string accountName, string accountSecurityIdentifier, string accountSecurityDescriptor)
		{
			if (urlString == null)
			{
				throw new ConfigException.InvalidUrlReservation("UrlString is null.");
			}
			this.EffectiveUrl = URL.GetEffectiveUrl(urlString);
			this.UrlString = urlString;
			this.AccountName = accountName;
			this.AccountSid = accountSecurityIdentifier;
			this.AccountSecurityDescriptor = accountSecurityDescriptor;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00002083 File Offset: 0x00000283
		private URL()
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006566 File Offset: 0x00004766
		public static URL Create(string urlString)
		{
			return new URL(urlString, null, null, null);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006571 File Offset: 0x00004771
		public static URL Create(string urlString, AccountCredentials accountCredentials)
		{
			if (accountCredentials != null)
			{
				return new URL(urlString, accountCredentials.DomainUser, accountCredentials.GetSecurityIdentifier().ToString(), accountCredentials.GetSecurityDescriptor());
			}
			return URL.Create(urlString);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000659A File Offset: 0x0000479A
		public static URL Create(string urlString, string accountName, string accountSecurityIdentifier)
		{
			return new URL(urlString, accountName, accountSecurityIdentifier, null);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000065A5 File Offset: 0x000047A5
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000065AD File Offset: 0x000047AD
		public string UrlString { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000065B6 File Offset: 0x000047B6
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000065BE File Offset: 0x000047BE
		public string AccountSid { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000065C7 File Offset: 0x000047C7
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000065CF File Offset: 0x000047CF
		public string AccountName { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000065D8 File Offset: 0x000047D8
		// (set) Token: 0x06000187 RID: 391 RVA: 0x000065E0 File Offset: 0x000047E0
		[XmlIgnore]
		public string AccountSecurityDescriptor { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000065E9 File Offset: 0x000047E9
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000065F1 File Offset: 0x000047F1
		[XmlIgnore]
		public Uri EffectiveUrl { get; private set; }

		// Token: 0x0600018A RID: 394 RVA: 0x000065FC File Offset: 0x000047FC
		private static Uri GetEffectiveUrl(string urlString)
		{
			Uri uri;
			if (!Uri.TryCreate(URL.ReplaceWildcard(urlString), UriKind.Absolute, out uri))
			{
				throw new ConfigException.InvalidUrlReservation(string.Format("The format of UrlString is invalid: {0}", urlString));
			}
			return uri;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000662B File Offset: 0x0000482B
		private static string ReplaceWildcard(string urlString)
		{
			return urlString.Replace("*", "localhost").Replace("+", "localhost");
		}

		// Token: 0x0400010E RID: 270
		private const string Localhost = "localhost";
	}
}
