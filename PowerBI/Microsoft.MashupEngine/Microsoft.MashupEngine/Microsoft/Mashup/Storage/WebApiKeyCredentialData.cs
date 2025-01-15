using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200205B RID: 8283
	[XmlType("WebApiKey")]
	public class WebApiKeyCredentialData : CredentialData
	{
		// Token: 0x0600CAA0 RID: 51872 RVA: 0x00287642 File Offset: 0x00285842
		public WebApiKeyCredentialData()
		{
		}

		// Token: 0x0600CAA1 RID: 51873 RVA: 0x00287946 File Offset: 0x00285B46
		public WebApiKeyCredentialData(string apiKeyValue)
		{
			this.apiKeyValue = apiKeyValue;
		}

		// Token: 0x170030BF RID: 12479
		// (get) Token: 0x0600CAA2 RID: 51874 RVA: 0x0000240C File Offset: 0x0000060C
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.WebAPI;
			}
		}

		// Token: 0x170030C0 RID: 12480
		// (get) Token: 0x0600CAA3 RID: 51875 RVA: 0x00287955 File Offset: 0x00285B55
		// (set) Token: 0x0600CAA4 RID: 51876 RVA: 0x0028795D File Offset: 0x00285B5D
		public string ApiKeyValue
		{
			get
			{
				return this.apiKeyValue;
			}
			set
			{
				this.apiKeyValue = value;
			}
		}

		// Token: 0x170030C1 RID: 12481
		// (get) Token: 0x0600CAA5 RID: 51877 RVA: 0x00002105 File Offset: 0x00000305
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Credential;
			}
		}

		// Token: 0x0600CAA6 RID: 51878 RVA: 0x00287966 File Offset: 0x00285B66
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new WebApiKeyCredential(this.ApiKeyValue);
		}

		// Token: 0x0600CAA7 RID: 51879 RVA: 0x00287974 File Offset: 0x00285B74
		public override bool TryMergeWith(CredentialData credentialData)
		{
			WebApiKeyCredentialData webApiKeyCredentialData = credentialData as WebApiKeyCredentialData;
			if (webApiKeyCredentialData != null)
			{
				if (webApiKeyCredentialData.ApiKeyValue != null)
				{
					this.ApiKeyValue = webApiKeyCredentialData.ApiKeyValue;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CAA8 RID: 51880 RVA: 0x002879A2 File Offset: 0x00285BA2
		public override void Validate()
		{
			if (string.IsNullOrEmpty(this.ApiKeyValue))
			{
				throw StorageExceptions.CredentialValidationException(Strings.API_Key_Not_Specified, null);
			}
		}

		// Token: 0x04006703 RID: 26371
		private string apiKeyValue;
	}
}
