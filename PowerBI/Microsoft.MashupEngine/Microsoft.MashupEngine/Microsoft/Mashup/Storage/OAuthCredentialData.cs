using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002056 RID: 8278
	[XmlType("OAuth")]
	public class OAuthCredentialData : CredentialData
	{
		// Token: 0x0600CA87 RID: 51847 RVA: 0x00287642 File Offset: 0x00285842
		public OAuthCredentialData()
		{
		}

		// Token: 0x0600CA88 RID: 51848 RVA: 0x00287772 File Offset: 0x00285972
		public OAuthCredentialData(string accessToken, string expires, string refreshToken, Dictionary<string, string> properties)
		{
			this.accessToken = accessToken;
			this.expires = expires;
			this.refreshToken = refreshToken;
			if (properties != null)
			{
				this.properties = new SerializableDictionary<string, string>(properties);
			}
		}

		// Token: 0x170030B7 RID: 12471
		// (get) Token: 0x0600CA89 RID: 51849 RVA: 0x00142610 File Offset: 0x00140810
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.OAuth;
			}
		}

		// Token: 0x170030B8 RID: 12472
		// (get) Token: 0x0600CA8A RID: 51850 RVA: 0x002877A0 File Offset: 0x002859A0
		// (set) Token: 0x0600CA8B RID: 51851 RVA: 0x002877A8 File Offset: 0x002859A8
		public string AccessToken
		{
			get
			{
				return this.accessToken;
			}
			set
			{
				this.accessToken = value;
			}
		}

		// Token: 0x170030B9 RID: 12473
		// (get) Token: 0x0600CA8C RID: 51852 RVA: 0x002877B1 File Offset: 0x002859B1
		// (set) Token: 0x0600CA8D RID: 51853 RVA: 0x002877B9 File Offset: 0x002859B9
		public string Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		// Token: 0x170030BA RID: 12474
		// (get) Token: 0x0600CA8E RID: 51854 RVA: 0x002877C2 File Offset: 0x002859C2
		// (set) Token: 0x0600CA8F RID: 51855 RVA: 0x002877CA File Offset: 0x002859CA
		public string RefreshToken
		{
			get
			{
				return this.refreshToken;
			}
			set
			{
				this.refreshToken = value;
			}
		}

		// Token: 0x170030BB RID: 12475
		// (get) Token: 0x0600CA90 RID: 51856 RVA: 0x002877D3 File Offset: 0x002859D3
		// (set) Token: 0x0600CA91 RID: 51857 RVA: 0x002877DB File Offset: 0x002859DB
		public SerializableDictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x170030BC RID: 12476
		// (get) Token: 0x0600CA92 RID: 51858 RVA: 0x002877E4 File Offset: 0x002859E4
		// (set) Token: 0x0600CA93 RID: 51859 RVA: 0x002877F6 File Offset: 0x002859F6
		public string AccountId
		{
			get
			{
				return this.GetProperty("accountId", string.Empty);
			}
			set
			{
				this.SetProperty("accountId", value);
			}
		}

		// Token: 0x170030BD RID: 12477
		// (get) Token: 0x0600CA94 RID: 51860 RVA: 0x00287804 File Offset: 0x00285A04
		// (set) Token: 0x0600CA95 RID: 51861 RVA: 0x00287816 File Offset: 0x00285A16
		public string AccountName
		{
			get
			{
				return this.GetProperty("accountName", string.Empty);
			}
			set
			{
				this.SetProperty("accountName", value);
			}
		}

		// Token: 0x0600CA96 RID: 51862 RVA: 0x00287824 File Offset: 0x00285A24
		public override void Validate()
		{
			if (string.IsNullOrEmpty(this.accessToken))
			{
				throw StorageExceptions.CredentialValidationException(Strings.User_Not_Signed_In, null);
			}
		}

		// Token: 0x170030BE RID: 12478
		// (get) Token: 0x0600CA97 RID: 51863 RVA: 0x00002105 File Offset: 0x00000305
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Credential;
			}
		}

		// Token: 0x0600CA98 RID: 51864 RVA: 0x0028783F File Offset: 0x00285A3F
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new OAuthCredential(this.AccessToken, this.Expires, this.RefreshToken, this.Properties);
		}

		// Token: 0x0600CA99 RID: 51865 RVA: 0x00287860 File Offset: 0x00285A60
		public override bool TryMergeWith(CredentialData credentialData)
		{
			OAuthCredentialData oauthCredentialData = credentialData as OAuthCredentialData;
			if (oauthCredentialData != null)
			{
				if (oauthCredentialData.AccessToken != null)
				{
					this.AccessToken = oauthCredentialData.AccessToken;
				}
				if (oauthCredentialData.Expires != null)
				{
					this.Expires = oauthCredentialData.Expires;
				}
				if (oauthCredentialData.RefreshToken != null)
				{
					this.RefreshToken = oauthCredentialData.RefreshToken;
				}
				if (oauthCredentialData.Properties != null && oauthCredentialData.Properties.Count > 0)
				{
					this.Properties = oauthCredentialData.Properties;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CA9A RID: 51866 RVA: 0x002878D8 File Offset: 0x00285AD8
		private string GetProperty(string propertyName, string defaultValue)
		{
			string text;
			if (this.properties == null || !this.properties.TryGetValue(propertyName, out text))
			{
				text = defaultValue;
			}
			return text;
		}

		// Token: 0x0600CA9B RID: 51867 RVA: 0x00287900 File Offset: 0x00285B00
		private void SetProperty(string propertyName, string value)
		{
			if (this.properties == null && value != null)
			{
				this.properties = new SerializableDictionary<string, string>();
				this.properties[propertyName] = value;
				return;
			}
			if (this.properties != null && value == null)
			{
				this.properties.Remove(propertyName);
			}
		}

		// Token: 0x040066FF RID: 26367
		private string accessToken;

		// Token: 0x04006700 RID: 26368
		private string expires;

		// Token: 0x04006701 RID: 26369
		private string refreshToken;

		// Token: 0x04006702 RID: 26370
		private SerializableDictionary<string, string> properties;
	}
}
