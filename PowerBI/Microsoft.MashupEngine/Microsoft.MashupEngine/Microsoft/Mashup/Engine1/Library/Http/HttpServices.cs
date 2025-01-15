using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A66 RID: 2662
	internal static class HttpServices
	{
		// Token: 0x06004A4F RID: 19023 RVA: 0x000F7AFB File Offset: 0x000F5CFB
		public static void ApplyCredentials(MashupHttpWebRequest httpRequest, string resourceKind, Uri requestUri, ResourceCredentialCollection credentials, IEngineHost hostEnvironment, string oAuthResource)
		{
			if (!HttpResourceCredentialDispatcher.ApplyCredentialsToRequest(httpRequest, credentials, hostEnvironment, oAuthResource))
			{
				throw DataSourceException.NewInvalidCredentialsError(hostEnvironment, Resource.New(resourceKind, Uri.UnescapeDataString(requestUri.GetLeftPart(UriPartial.Path))), null, null, null);
			}
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x000F7B28 File Offset: 0x000F5D28
		public static bool IsHttpsOrLoopbackUri(string resourcePath)
		{
			Uri uri;
			if (Uri.TryCreate(resourcePath, UriKind.Absolute, out uri))
			{
				if (HttpServices.IsHttpsUri(uri) || uri.IsLoopback)
				{
					return true;
				}
				try
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(uri.Host);
					if (hostEntry != null)
					{
						return HttpServices.IsLoopback(hostEntry);
					}
				}
				catch (SocketException)
				{
				}
				catch (FormatException)
				{
				}
				catch (ArgumentException)
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x000F7BA4 File Offset: 0x000F5DA4
		public static ValueException NewDataSourceError<T>(IEngineHost engineHost, T message, IResource resource, TextValue url) where T : IUserMessage
		{
			return DataSourceException.NewDataSourceError<T>(engineHost, message, resource, "Url", url, TypeValue.Text, null);
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x000F7BBC File Offset: 0x000F5DBC
		private static bool IsLoopback(IPHostEntry hostEntry)
		{
			IPAddress[] addressList = hostEntry.AddressList;
			for (int i = 0; i < addressList.Length; i++)
			{
				if (!HttpServices.IsLoopback(addressList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x000F7BEB File Offset: 0x000F5DEB
		private static bool IsLoopback(IPAddress ipAddress)
		{
			return (ipAddress.AddressFamily == AddressFamily.InterNetworkV6 || ipAddress.AddressFamily == AddressFamily.InterNetwork) && IPAddress.IsLoopback(ipAddress);
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x000396A7 File Offset: 0x000378A7
		public static bool IsHttpsUri(Uri uri)
		{
			return Uri.UriSchemeHttps.Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x000F7C08 File Offset: 0x000F5E08
		public static void ThrowIfAuthorizationError(IEngineHost engineHost, WebException exception, IResource resource)
		{
			if (exception.Status == WebExceptionStatus.ProtocolError)
			{
				MashupHttpWebResponse mashupHttpWebResponse = exception.Response.Wrap() as MashupHttpWebResponse;
				ResourceSecurityException ex;
				if (mashupHttpWebResponse != null && HttpServices.TryGetResourceSecurityException(engineHost, (int)mashupHttpWebResponse.StatusCode, resource, mashupHttpWebResponse.Headers, out ex))
				{
					throw ex;
				}
			}
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x000F7C4B File Offset: 0x000F5E4B
		public static bool TryGetResourceSecurityException(IEngineHost engineHost, int statusCode, IResource resource, out ResourceSecurityException exception)
		{
			return HttpServices.TryGetResourceSecurityException(engineHost, statusCode, resource, null, out exception);
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x000F7C58 File Offset: 0x000F5E58
		public static bool TryGetResourceSecurityException(IEngineHost engineHost, int statusCode, IResource resource, WebHeaderCollection responseHeaderCollection, out ResourceSecurityException exception)
		{
			exception = null;
			if (statusCode == 401 || HttpServices.IsSharePoint403(statusCode, resource, responseHeaderCollection))
			{
				exception = DataSourceException.NewAccessAuthorizationError(engineHost, resource, null, null, null);
			}
			else if (statusCode == 403)
			{
				exception = DataSourceException.NewAccessForbiddenError(engineHost, resource, null, null, null);
			}
			return exception != null;
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x000F7CA5 File Offset: 0x000F5EA5
		private static bool IsSharePoint403(int statusCode, IResource resource, WebHeaderCollection responseHeaders)
		{
			return statusCode == 403 && (resource.Kind == "SharePoint" || (responseHeaders != null && responseHeaders["MicrosoftSharePointTeamServices"] != null));
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x000F7CD4 File Offset: 0x000F5ED4
		public static void VerifyPermissionAndGetCredentials(IEngineHost host, IResource resource, out ResourceCredentialCollection credentials)
		{
			HttpServices.VerifyPermissionAndGetCredentials(host, resource, false, out credentials);
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x000F7CE0 File Offset: 0x000F5EE0
		public static void VerifyPermissionAndGetCredentials(IEngineHost host, IResource resource, bool requireWebKey, out ResourceCredentialCollection credentials)
		{
			credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(host, resource, null);
			if (requireWebKey)
			{
				if (credentials.SingleOrDefault((IResourceCredential o) => o is WebApiKeyCredential) != null)
				{
					return;
				}
				IResourceCredential resourceCredential = credentials.SingleOrDefault((IResourceCredential o) => o is FeedKeyCredential);
				if (resourceCredential != null)
				{
					resourceCredential = new WebApiKeyCredential(((FeedKeyCredential)resourceCredential).Password);
					credentials = new ResourceCredentialCollection(resource, new IResourceCredential[] { resourceCredential }.Concat(credentials.Where((IResourceCredential o) => !(o is FeedKeyCredential))).ToList<IResourceCredential>());
					return;
				}
				string text = Strings.HttpCredentialsWebApiKeyRequiresApiKeyName;
				throw DataSourceException.NewInvalidCredentialsError(host, resource, text, text, null);
			}
			else
			{
				if (credentials.Count == 0)
				{
					return;
				}
				IResourceCredential resourceCredential = credentials.RemoveAdornments();
				if (resourceCredential == null || resourceCredential is WindowsCredential || resourceCredential is BasicAuthCredential || resourceCredential is OAuthCredential || resourceCredential is SharedKeyAuthCredential)
				{
					return;
				}
				if (resourceCredential is WebApiKeyCredential)
				{
					string text2 = Strings.HttpCredentialsWebApiKeyOnlyUsedWithApiKeyName;
					throw DataSourceException.NewInvalidCredentialsError(host, resource, text2, text2, null);
				}
				throw DataSourceException.NewInvalidCredentialsError(host, resource, null, null, null);
			}
		}

		// Token: 0x04002786 RID: 10118
		public const string UrlKey = "Url";

		// Token: 0x04002787 RID: 10119
		private const string SharePointOnlineHeader = "MicrosoftSharePointTeamServices";
	}
}
