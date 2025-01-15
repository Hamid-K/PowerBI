using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x02000013 RID: 19
	internal class OwinHttpListenerRequest
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00005DC8 File Offset: 0x00003FC8
		internal OwinHttpListenerRequest(HttpListenerRequest request, string basePath, string path, string query, CallEnvironment environment)
		{
			this._request = request;
			this._environment = environment;
			this._environment.RequestProtocol = OwinHttpListenerRequest.GetProtocol(request.ProtocolVersion);
			this._environment.RequestScheme = (request.IsSecureConnection ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
			this._environment.RequestMethod = request.HttpMethod;
			this._environment.RequestPathBase = basePath;
			this._environment.RequestPath = path;
			this._environment.RequestQueryString = query;
			this._environment.RequestId = request.RequestTraceIdentifier.ToString();
			this._environment.RequestHeaders = new RequestHeadersDictionary(request);
			if (this._request.IsSecureConnection)
			{
				this._environment.LoadClientCert = new Func<Task>(this.LoadClientCertAsync);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005EA9 File Offset: 0x000040A9
		private static string GetProtocol(Version version)
		{
			if (version.Major == 1)
			{
				if (version.Minor == 1)
				{
					return "HTTP/1.1";
				}
				if (version.Minor == 0)
				{
					return "HTTP/1.0";
				}
			}
			return "HTTP/" + version.ToString(2);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005EE4 File Offset: 0x000040E4
		internal bool TryGetClientCert(ref X509Certificate value, ref Exception errors)
		{
			if (!this._request.IsSecureConnection)
			{
				return false;
			}
			bool flag;
			try
			{
				value = this._request.GetClientCertificate();
				if (this._request.ClientCertificateError != 0)
				{
					errors = new Win32Exception(this._request.ClientCertificateError);
				}
				flag = value != null;
			}
			catch (HttpListenerException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005F4C File Offset: 0x0000414C
		private async Task LoadClientCertAsync()
		{
			try
			{
				if (this._environment.ClientCertNeedsInit)
				{
					X509Certificate2 x509Certificate = await this._request.GetClientCertificateAsync();
					X509Certificate cert = x509Certificate;
					this._environment.ClientCert = cert;
					this._environment.ClientCertErrors = ((this._request.ClientCertificateError == 0) ? null : new Win32Exception(this._request.ClientCertificateError));
				}
			}
			catch (Exception)
			{
				this._environment.ClientCert = null;
				this._environment.ClientCertErrors = null;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005F8F File Offset: 0x0000418F
		internal Stream GetRequestBody()
		{
			return new HttpListenerStreamWrapper(this._request.InputStream);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005FA1 File Offset: 0x000041A1
		internal string GetRemoteIpAddress()
		{
			return this._request.RemoteEndPoint.Address.ToString();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005FB8 File Offset: 0x000041B8
		internal string GetRemotePort()
		{
			return this._request.RemoteEndPoint.Port.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005FE2 File Offset: 0x000041E2
		internal string GetLocalIpAddress()
		{
			return this._request.LocalEndPoint.Address.ToString();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005FFC File Offset: 0x000041FC
		internal string GetLocalPort()
		{
			return this._request.LocalEndPoint.Port.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006026 File Offset: 0x00004226
		internal bool GetIsLocal()
		{
			return this._request.IsLocal;
		}

		// Token: 0x0400008F RID: 143
		private readonly CallEnvironment _environment;

		// Token: 0x04000090 RID: 144
		private readonly HttpListenerRequest _request;
	}
}
