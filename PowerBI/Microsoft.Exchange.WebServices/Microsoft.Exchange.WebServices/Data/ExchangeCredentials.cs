using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001CD RID: 461
	public abstract class ExchangeCredentials
	{
		// Token: 0x0600151B RID: 5403 RVA: 0x0003B6C5 File Offset: 0x0003A6C5
		public static implicit operator ExchangeCredentials(NetworkCredential credentials)
		{
			return new WebCredentials(credentials);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0003B6CD File Offset: 0x0003A6CD
		public static implicit operator ExchangeCredentials(CredentialCache credentials)
		{
			return new WebCredentials(credentials);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0003B6D8 File Offset: 0x0003A6D8
		internal static string GetUriWithoutSuffix(Uri url)
		{
			string absoluteUri = url.AbsoluteUri;
			int num = absoluteUri.IndexOf("/wssecurity", 5);
			if (num != -1)
			{
				return absoluteUri.Substring(0, num);
			}
			return absoluteUri;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0003B707 File Offset: 0x0003A707
		internal virtual void PreAuthenticate()
		{
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0003B709 File Offset: 0x0003A709
		internal virtual void PrepareWebRequest(IEwsHttpWebRequest request)
		{
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0003B70B File Offset: 0x0003A70B
		internal virtual void EmitExtraSoapHeaderNamespaceAliases(XmlWriter writer)
		{
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0003B70D File Offset: 0x0003A70D
		internal virtual void SerializeExtraSoapHeaders(XmlWriter writer, string webMethodName)
		{
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0003B70F File Offset: 0x0003A70F
		internal virtual void SerializeWSSecurityHeaders(XmlWriter writer)
		{
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0003B711 File Offset: 0x0003A711
		internal virtual Uri AdjustUrl(Uri url)
		{
			return new Uri(ExchangeCredentials.GetUriWithoutSuffix(url));
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0003B71E File Offset: 0x0003A71E
		internal virtual bool NeedSignature
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0003B721 File Offset: 0x0003A721
		internal virtual void Sign(MemoryStream memoryStream)
		{
			throw new InvalidOperationException();
		}
	}
}
