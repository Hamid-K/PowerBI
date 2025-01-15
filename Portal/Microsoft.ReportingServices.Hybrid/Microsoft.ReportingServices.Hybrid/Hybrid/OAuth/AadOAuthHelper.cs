using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000005 RID: 5
	internal static class AadOAuthHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static ServiceIdToken GetIdTokenFromResponseString(string responseString)
		{
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(responseString), "AAD: responseString is null");
			string[] array = responseString.Split(new char[] { '.' });
			if (array.Length == 3)
			{
				try
				{
					using (MemoryStream memoryStream = new MemoryStream(AadOAuthHelper.Base64UrlDecode(array[1])))
					{
						return (ServiceIdToken)new DataContractJsonSerializer(typeof(ServiceIdToken)).ReadObject(memoryStream);
					}
				}
				catch (SerializationException ex)
				{
					throw new InternalCatalogException(ex, "Failed to extract ServiceIdToken from Authentication Token");
				}
			}
			throw new UnauthorizedAccessException("There was an error while trying to sign in. Please sign in with a valid account and try again.");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public static UriBuilder GetUrlBuilder(string url)
		{
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(url), "AAD: url is null");
			return new UriBuilder(url);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002118 File Offset: 0x00000318
		public static ServiceToken GetTokenFromRequestValues(Uri tokenUrl, NameValueCollection values, IServiceTokenStore store)
		{
			RSTrace.CatalogTrace.Assert(tokenUrl != null && values != null, "AAD: tokenUrl or payload values are null");
			ServiceToken oauthTokenResponseFromResponseBytes;
			try
			{
				oauthTokenResponseFromResponseBytes = AadOAuthHelper.GetOAuthTokenResponseFromResponseBytes(store.UploadValuesPort(tokenUrl, "POST", values));
			}
			catch (SerializationException)
			{
				throw new AccessDeniedException("PBIUser", ErrorCode.rsAccessDenied);
			}
			catch (WebException ex)
			{
				if (!string.IsNullOrEmpty(ex.Response.ToString()))
				{
					RSTrace.UITracer.Trace("AAD returned '{0}'. You may need to reregister your application with PowerBI in the RS Config Tool.", new object[] { ex.Response.ToString() });
				}
				RSTrace.UITracer.Trace("WebException: {0}", new object[] { ex });
				throw new AccessDeniedException("PBIUser", ErrorCode.rsAccessDenied);
			}
			return oauthTokenResponseFromResponseBytes;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021E0 File Offset: 0x000003E0
		public static Uri GetReportManagerAADUrl()
		{
			if (HttpContext.Current != null && HttpContext.Current.Request.Url.Segments.Length > 2)
			{
				string[] segments = HttpContext.Current.Request.Url.Segments;
				return new Uri(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new object[]
				{
					HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority),
					segments[0].ToLowerInvariant(),
					segments[1].ToLowerInvariant(),
					AadOAuthHelper.LoginCompletePage
				}));
			}
			return null;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002274 File Offset: 0x00000474
		private static ServiceToken GetOAuthTokenResponseFromResponseBytes(byte[] responseBytes)
		{
			ServiceToken oauthTokenResponseFromResponseStream;
			using (MemoryStream memoryStream = new MemoryStream(responseBytes))
			{
				oauthTokenResponseFromResponseStream = AadOAuthHelper.GetOAuthTokenResponseFromResponseStream(memoryStream);
			}
			return oauthTokenResponseFromResponseStream;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022AC File Offset: 0x000004AC
		private static ServiceToken GetOAuthTokenResponseFromResponseStream(Stream responseStream)
		{
			return (ServiceToken)new DataContractJsonSerializer(typeof(ServiceToken)).ReadObject(responseStream);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022C8 File Offset: 0x000004C8
		private static byte[] Base64UrlDecode(string chars)
		{
			string text = chars.Replace('-', '+');
			text = text.Replace('_', '/');
			switch (text.Length % 4)
			{
			case 0:
				goto IL_0065;
			case 2:
				text += "==";
				goto IL_0065;
			case 3:
				text += "=";
				goto IL_0065;
			}
			throw new ArgumentException("Illegal base64 string", "chars");
			IL_0065:
			return Convert.FromBase64String(text);
		}

		// Token: 0x04000035 RID: 53
		private static string LoginCompletePage = "pages/logincomplete.aspx";
	}
}
