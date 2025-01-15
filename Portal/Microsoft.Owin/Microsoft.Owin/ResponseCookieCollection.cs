using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Owin
{
	// Token: 0x0200001B RID: 27
	public class ResponseCookieCollection
	{
		// Token: 0x06000157 RID: 343 RVA: 0x000038FA File Offset: 0x00001AFA
		public ResponseCookieCollection(IHeaderDictionary headers)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			this.Headers = headers;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003917 File Offset: 0x00001B17
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000391F File Offset: 0x00001B1F
		private IHeaderDictionary Headers { get; set; }

		// Token: 0x0600015A RID: 346 RVA: 0x00003928 File Offset: 0x00001B28
		public void Append(string key, string value)
		{
			this.Headers.AppendValues("Set-Cookie", new string[] { Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value) + "; path=/" });
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000396C File Offset: 0x00001B6C
		public void Append(string key, string value, CookieOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			bool domainHasValue = !string.IsNullOrEmpty(options.Domain);
			bool pathHasValue = !string.IsNullOrEmpty(options.Path);
			bool expiresHasValue = options.Expires != null;
			bool sameSiteHasValue = options.SameSite != null;
			string setCookieValue = string.Concat(new string[]
			{
				Uri.EscapeDataString(key),
				"=",
				Uri.EscapeDataString(value ?? string.Empty),
				(!domainHasValue) ? null : "; domain=",
				(!domainHasValue) ? null : options.Domain,
				(!pathHasValue) ? null : "; path=",
				(!pathHasValue) ? null : options.Path,
				(!expiresHasValue) ? null : "; expires=",
				(!expiresHasValue) ? null : options.Expires.Value.ToString("ddd, dd-MMM-yyyy HH:mm:ss \\G\\M\\T", CultureInfo.InvariantCulture),
				(!options.Secure) ? null : "; secure",
				(!options.HttpOnly) ? null : "; HttpOnly",
				(!sameSiteHasValue) ? null : "; SameSite=",
				(!sameSiteHasValue) ? null : ResponseCookieCollection.GetStringRepresentationOfSameSite(options.SameSite.Value)
			});
			this.Headers.AppendValues("Set-Cookie", new string[] { setCookieValue });
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003ADC File Offset: 0x00001CDC
		public void Delete(string key)
		{
			Func<string, bool> predicate = (string value) => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase);
			string[] deleteCookies = new string[] { Uri.EscapeDataString(key) + "=; expires=Thu, 01-Jan-1970 00:00:00 GMT" };
			IList<string> existingValues = this.Headers.GetValues("Set-Cookie");
			if (existingValues == null || existingValues.Count == 0)
			{
				this.Headers.SetValues("Set-Cookie", deleteCookies);
				return;
			}
			this.Headers.SetValues("Set-Cookie", existingValues.Where((string value) => !predicate(value)).Concat(deleteCookies).ToArray<string>());
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003B84 File Offset: 0x00001D84
		public void Delete(string key, CookieOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			bool domainHasValue = !string.IsNullOrEmpty(options.Domain);
			bool pathHasValue = !string.IsNullOrEmpty(options.Path);
			Func<string, bool> rejectPredicate;
			if (domainHasValue)
			{
				rejectPredicate = (string value) => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase) && value.IndexOf("domain=" + options.Domain, StringComparison.OrdinalIgnoreCase) != -1;
			}
			else if (pathHasValue)
			{
				rejectPredicate = (string value) => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase) && value.IndexOf("path=" + options.Path, StringComparison.OrdinalIgnoreCase) != -1;
			}
			else
			{
				rejectPredicate = (string value) => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase);
			}
			IList<string> existingValues = this.Headers.GetValues("Set-Cookie");
			if (existingValues != null)
			{
				this.Headers.SetValues("Set-Cookie", existingValues.Where((string value) => !rejectPredicate(value)).ToArray<string>());
			}
			this.Append(key, string.Empty, new CookieOptions
			{
				Path = options.Path,
				Domain = options.Domain,
				HttpOnly = options.HttpOnly,
				SameSite = options.SameSite,
				Secure = options.Secure,
				Expires = new DateTime?(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
			});
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003CE4 File Offset: 0x00001EE4
		private static string GetStringRepresentationOfSameSite(SameSiteMode siteMode)
		{
			switch (siteMode)
			{
			case SameSiteMode.None:
				return "None";
			case SameSiteMode.Lax:
				return "Lax";
			case SameSiteMode.Strict:
				return "Strict";
			default:
				throw new ArgumentOutOfRangeException("siteMode", string.Format(CultureInfo.InvariantCulture, "Unexpected SameSiteMode value: {0}", new object[] { siteMode }));
			}
		}
	}
}
