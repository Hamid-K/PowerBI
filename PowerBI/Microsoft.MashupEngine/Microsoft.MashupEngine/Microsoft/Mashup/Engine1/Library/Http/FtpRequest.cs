using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A52 RID: 2642
	internal sealed class FtpRequest : Request
	{
		// Token: 0x060049CD RID: 18893 RVA: 0x000F6000 File Offset: 0x000F4200
		public FtpRequest(IEngineHost host, Uri uri, TextValue url, Value query, Value content, string webApiKey, Value headers, Value timeout, RetryPolicy retryPolicy)
			: base(host, "Ftp", uri, url, query, content, webApiKey, headers, timeout, retryPolicy, null, null)
		{
			base.Method = "RETR";
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x000F6034 File Offset: 0x000F4234
		public override string ProgressDataSource
		{
			get
			{
				return base.Uri.Host;
			}
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x000F6041 File Offset: 0x000F4241
		protected override IResource CreateResource()
		{
			return Resource.New(base.ResourceKind, base.InitialUri.String);
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x000F605C File Offset: 0x000F425C
		protected override void ApplyCredentialsToRequest(WebRequest webRequest, ResourceCredentialCollection credentials)
		{
			if (credentials.Count == 0)
			{
				webRequest.Credentials = new NetworkCredential("anonymous", "");
				return;
			}
			if (credentials.Count == 1)
			{
				FtpLoginAuthCredential ftpLoginAuthCredential = credentials[0] as FtpLoginAuthCredential;
				if (ftpLoginAuthCredential != null)
				{
					webRequest.Credentials = new NetworkCredential(ftpLoginAuthCredential.Username, ftpLoginAuthCredential.Password);
				}
			}
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x000F60B8 File Offset: 0x000F42B8
		protected override bool TryCreateSecurityException(WebException exception, out ResourceSecurityException resourceSecurityException)
		{
			if (exception.Status == WebExceptionStatus.ProtocolError)
			{
				FtpWebResponse ftpWebResponse = exception.Response as FtpWebResponse;
				if (ftpWebResponse != null && (ftpWebResponse.StatusCode == FtpStatusCode.SendPasswordCommand || ftpWebResponse.StatusCode == FtpStatusCode.NeedLoginAccount || ftpWebResponse.StatusCode == FtpStatusCode.NotLoggedIn || ftpWebResponse.StatusCode == FtpStatusCode.AccountNeeded))
				{
					resourceSecurityException = DataSourceException.NewAccessAuthorizationError(base.Host, base.RequestResource, null, null, null);
					return true;
				}
			}
			resourceSecurityException = null;
			return false;
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x000F612D File Offset: 0x000F432D
		protected override Response CreateResponse(WebRequest webRequest, WebResponse webResponse, WebException webException = null, ResourceCredentialCollection credentials = null)
		{
			return new FtpResponse((FtpWebRequest)webRequest, (FtpWebResponse)webResponse, new Action<Exception, long>(base.LogError), base.Host);
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x000F6154 File Offset: 0x000F4354
		public override void VerifyPermissionAndGetCredentials(out ResourceCredentialCollection credentials)
		{
			credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(base.Host, base.RequestResource, null);
			if (credentials.Count == 0)
			{
				return;
			}
			if (credentials.Count == 1 && credentials[0] is FtpLoginAuthCredential)
			{
				return;
			}
			throw DataSourceException.NewInvalidCredentialsError(base.Host, base.RequestResource, null, null, null);
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x000F61AD File Offset: 0x000F43AD
		public override bool IsFailedStatusCode(Response response)
		{
			return response.StatusCode >= 300 || response.StatusCode < 100;
		}
	}
}
