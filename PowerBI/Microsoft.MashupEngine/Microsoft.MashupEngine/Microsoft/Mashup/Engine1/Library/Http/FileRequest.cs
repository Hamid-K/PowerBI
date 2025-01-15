using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A50 RID: 2640
	internal sealed class FileRequest : Request
	{
		// Token: 0x060049BB RID: 18875 RVA: 0x000F5E98 File Offset: 0x000F4098
		public FileRequest(IEngineHost host, Uri uri, TextValue url, Value query, Value content, string webApiKey, Value headers, Value timeout, RetryPolicy retryPolicy)
			: base(host, "File", uri, url, query, content, webApiKey, headers, timeout, retryPolicy, null, null)
		{
			base.Method = "GET";
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x000F5ECC File Offset: 0x000F40CC
		public override string ProgressDataSource
		{
			get
			{
				return Path.GetFileName(base.Uri.LocalPath);
			}
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x000F5EDE File Offset: 0x000F40DE
		protected override Response CreateResponse(WebRequest webRequest, WebResponse webResponse, WebException webException = null, ResourceCredentialCollection credentials = null)
		{
			return new FileResponse(base.Host, (FileWebRequest)webRequest, (FileWebResponse)webResponse, new Action<Exception, long>(base.LogError));
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x000F5F03 File Offset: 0x000F4103
		protected override IResource CreateResource()
		{
			return Resource.New(base.ResourceKind, base.Uri.LocalPath);
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x000F5F1B File Offset: 0x000F411B
		protected override void ApplyCredentialsToRequest(WebRequest webRequest, ResourceCredentialCollection credentials)
		{
			if (credentials.Count == 1 && credentials[0] is WindowsCredential)
			{
				webRequest.Credentials = CredentialCache.DefaultCredentials;
			}
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x000F5F3F File Offset: 0x000F413F
		protected override bool TryCreateSecurityException(WebException exception, out ResourceSecurityException resourceSecurityException)
		{
			if (exception.Status == WebExceptionStatus.ProtocolError)
			{
				resourceSecurityException = DataSourceException.NewAccessAuthorizationError(base.Host, base.RequestResource, null, null, null);
				return true;
			}
			resourceSecurityException = null;
			return false;
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x000F5F66 File Offset: 0x000F4166
		public override void VerifyPermissionAndGetCredentials(out ResourceCredentialCollection credentials)
		{
			FileHelper.VerifyPermissionAndGetCredentials(base.Host, base.RequestResource, out credentials);
		}
	}
}
