using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001CF RID: 463
	public sealed class OAuthCredentials : ExchangeCredentials
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x0003B760 File Offset: 0x0003A760
		public OAuthCredentials(string token)
			: this(token, false)
		{
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0003B76C File Offset: 0x0003A76C
		internal OAuthCredentials(string token, bool verbatim)
		{
			EwsUtilities.ValidateParam(token, "token");
			string text;
			if (verbatim)
			{
				text = token;
			}
			else
			{
				int num = token.IndexOf(' ');
				if (num == -1)
				{
					text = token;
				}
				else
				{
					string text2 = token.Substring(0, num);
					if (string.Compare(text2, "Bearer", 5) != 0)
					{
						throw new ArgumentException(Strings.InvalidAuthScheme);
					}
					text = token.Substring(num + 1);
				}
				if (!OAuthCredentials.validTokenPattern.IsMatch(text))
				{
					throw new ArgumentException(Strings.InvalidOAuthToken);
				}
			}
			this.token = "Bearer " + text;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0003B801 File Offset: 0x0003A801
		public OAuthCredentials(ICredentials credentials)
		{
			EwsUtilities.ValidateParam(credentials, "credentials");
			this.credentials = credentials;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0003B81B File Offset: 0x0003A81B
		internal override void PrepareWebRequest(IEwsHttpWebRequest request)
		{
			base.PrepareWebRequest(request);
			if (this.token != null)
			{
				request.Headers.Remove(24);
				request.Headers.Add(24, this.token);
				return;
			}
			request.Credentials = this.credentials;
		}

		// Token: 0x04000CD2 RID: 3282
		private const string BearerAuthenticationType = "Bearer";

		// Token: 0x04000CD3 RID: 3283
		private static readonly Regex validTokenPattern = new Regex("^[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]*$", 8);

		// Token: 0x04000CD4 RID: 3284
		private readonly string token;

		// Token: 0x04000CD5 RID: 3285
		private readonly ICredentials credentials;
	}
}
