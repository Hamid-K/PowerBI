using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002052 RID: 8274
	[XmlInclude(typeof(SqlAuthCredentialData))]
	[XmlInclude(typeof(BasicAuthCredentialData))]
	[XmlInclude(typeof(WebApiKeyCredentialData))]
	[XmlInclude(typeof(SharedKeyAuthCredentialData))]
	[XmlInclude(typeof(FeedKeyCredentialData))]
	[XmlInclude(typeof(ExchangeCredentialData))]
	[XmlInclude(typeof(FtpLoginAuthCredentialData))]
	[XmlInclude(typeof(WindowsAuthCredentialData))]
	[XmlInclude(typeof(DataMarketAuthCredentialData))]
	[XmlInclude(typeof(SharePointAuthCredentialData))]
	[XmlInclude(typeof(AadOAuth2CredentialData))]
	[XmlInclude(typeof(ParameterizedAuthCredentialData))]
	[XmlInclude(typeof(EncryptionAdornmentCredentialData))]
	[XmlInclude(typeof(SapSessionTokenAdornmentCredentialData))]
	[XmlInclude(typeof(SapBasicAuthCredentialData))]
	[XmlInclude(typeof(ConnectionStringPropertiesAdornmentCredentialData))]
	[XmlInclude(typeof(ApplicationCredentialPropertiesAdornmentCredentialData))]
	[XmlInclude(typeof(ConnectionStringAdornmentCredentialData))]
	[XmlInclude(typeof(WindowsAuthDeploymentAdornmentCredentialData))]
	public abstract class CredentialData
	{
		// Token: 0x170030B0 RID: 12464
		// (get) Token: 0x0600CA6A RID: 51818
		public abstract CredentialDataKind Kind { get; }

		// Token: 0x170030B1 RID: 12465
		// (get) Token: 0x0600CA6B RID: 51819
		public abstract CredentialDataType Type { get; }

		// Token: 0x0600CA6C RID: 51820
		public abstract IResourceCredential ToResourceCredential(IdentityContext context = null);

		// Token: 0x0600CA6D RID: 51821
		public abstract bool TryMergeWith(CredentialData credentialData);

		// Token: 0x0600CA6E RID: 51822
		public abstract void Validate();

		// Token: 0x0600CA6F RID: 51823 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void InitializeWithDefaults()
		{
		}

		// Token: 0x0600CA70 RID: 51824 RVA: 0x002873C2 File Offset: 0x002855C2
		public virtual IResourceCredential[] ToResourceCredentialArray(IdentityContext context = null)
		{
			return new IResourceCredential[] { this.ToResourceCredential(context) };
		}

		// Token: 0x0600CA71 RID: 51825 RVA: 0x002873D4 File Offset: 0x002855D4
		public static CredentialData From(IResourceCredential credential)
		{
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			SqlAuthCredential sqlAuthCredential = credential as SqlAuthCredential;
			if (sqlAuthCredential != null)
			{
				return new SqlAuthCredentialData(sqlAuthCredential.Username, sqlAuthCredential.Password);
			}
			BasicAuthCredential basicAuthCredential = credential as BasicAuthCredential;
			if (basicAuthCredential != null)
			{
				return new BasicAuthCredentialData(basicAuthCredential.Username, basicAuthCredential.Password);
			}
			WebApiKeyCredential webApiKeyCredential = credential as WebApiKeyCredential;
			if (webApiKeyCredential != null)
			{
				return new WebApiKeyCredentialData(webApiKeyCredential.ApiKeyValue);
			}
			SharedKeyAuthCredential sharedKeyAuthCredential = credential as SharedKeyAuthCredential;
			if (sharedKeyAuthCredential != null)
			{
				return new SharedKeyAuthCredentialData(sharedKeyAuthCredential.SharedKey);
			}
			FeedKeyCredential feedKeyCredential = credential as FeedKeyCredential;
			if (feedKeyCredential != null)
			{
				return new FeedKeyCredentialData(feedKeyCredential.FeedKey);
			}
			FtpLoginAuthCredential ftpLoginAuthCredential = credential as FtpLoginAuthCredential;
			if (ftpLoginAuthCredential != null)
			{
				return new FtpLoginAuthCredentialData(ftpLoginAuthCredential.Username, ftpLoginAuthCredential.Password);
			}
			WindowsCredential windowsCredential = credential as WindowsCredential;
			if (windowsCredential != null)
			{
				if (windowsCredential.Token != null)
				{
					return new WindowsAuthCredentialData
					{
						IdentitySource = "Thread"
					};
				}
				if (!windowsCredential.OverrideCurrentUser)
				{
					return new WindowsAuthCredentialData
					{
						IdentitySource = "Process"
					};
				}
				if (windowsCredential.Password == null)
				{
					return new WindowsAuthCredentialData
					{
						IdentitySource = "Service",
						Username = windowsCredential.Username
					};
				}
				return new WindowsAuthCredentialData
				{
					IdentitySource = "Explicit",
					Username = windowsCredential.Username,
					Password = windowsCredential.Password
				};
			}
			else
			{
				OAuthCredential oauthCredential = credential as OAuthCredential;
				if (oauthCredential != null)
				{
					return new OAuthCredentialData(oauthCredential.AccessToken, oauthCredential.Expires, oauthCredential.RefreshToken, oauthCredential.Properties);
				}
				ParameterizedCredential parameterizedCredential = credential as ParameterizedCredential;
				if (parameterizedCredential != null)
				{
					SerializableDictionary<string, string> serializableDictionary = new SerializableDictionary<string, string>(parameterizedCredential.Values);
					return new ParameterizedAuthCredentialData(parameterizedCredential.Name, serializableDictionary);
				}
				EncryptedConnectionAdornment encryptedConnectionAdornment = credential as EncryptedConnectionAdornment;
				if (encryptedConnectionAdornment != null)
				{
					return new EncryptionAdornmentCredentialData(encryptedConnectionAdornment.RequireEncryption);
				}
				ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment = credential as ConnectionStringPropertiesAdornment;
				if (connectionStringPropertiesAdornment != null)
				{
					return new ConnectionStringPropertiesAdornmentCredentialData(connectionStringPropertiesAdornment.Properties);
				}
				ApplicationCredentialPropertiesAdornment applicationCredentialPropertiesAdornment = credential as ApplicationCredentialPropertiesAdornment;
				if (applicationCredentialPropertiesAdornment != null)
				{
					return new ApplicationCredentialPropertiesAdornmentCredentialData(applicationCredentialPropertiesAdornment.Properties);
				}
				SapBasicAuthCredential sapBasicAuthCredential = credential as SapBasicAuthCredential;
				if (sapBasicAuthCredential != null)
				{
					return new SapBasicAuthCredentialData(sapBasicAuthCredential.Username, sapBasicAuthCredential.Password, sapBasicAuthCredential.Authentication);
				}
				ConnectionStringAdornment connectionStringAdornment = credential as ConnectionStringAdornment;
				if (connectionStringAdornment != null)
				{
					return new ConnectionStringAdornmentCredentialData(connectionStringAdornment.ConnectionString);
				}
				UserAnnotationAdornmentCredential userAnnotationAdornmentCredential = credential as UserAnnotationAdornmentCredential;
				if (userAnnotationAdornmentCredential != null)
				{
					return userAnnotationAdornmentCredential.Annotation;
				}
				string text = "Unknown credential type '";
				Type type = credential.GetType();
				throw new ArgumentException(text + ((type != null) ? type.ToString() : null) + "'.", "credential");
			}
		}
	}
}
