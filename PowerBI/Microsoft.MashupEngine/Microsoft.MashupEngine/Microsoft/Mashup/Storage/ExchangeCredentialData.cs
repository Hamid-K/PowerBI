using System;
using System.Net.Mail;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002062 RID: 8290
	[XmlType("Exchange")]
	public class ExchangeCredentialData : CredentialData
	{
		// Token: 0x0600CAE1 RID: 51937 RVA: 0x00287642 File Offset: 0x00285842
		public ExchangeCredentialData()
		{
		}

		// Token: 0x0600CAE2 RID: 51938 RVA: 0x00287E19 File Offset: 0x00286019
		public ExchangeCredentialData(string emailAddress, string username, string password)
		{
			this.emailAddress = emailAddress;
			this.username = username;
			this.password = password;
		}

		// Token: 0x0600CAE3 RID: 51939 RVA: 0x00287E36 File Offset: 0x00286036
		public ExchangeCredentialData(string emailAddress, string username, string password, string ewsUrl, string ewsSupportedSchema)
		{
			this.emailAddress = emailAddress;
			this.username = username;
			this.password = password;
			this.ewsUrl = ewsUrl;
			this.ewsSupportedSchema = ewsSupportedSchema;
		}

		// Token: 0x170030D4 RID: 12500
		// (get) Token: 0x0600CAE4 RID: 51940 RVA: 0x00002105 File Offset: 0x00000305
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Credential;
			}
		}

		// Token: 0x170030D5 RID: 12501
		// (get) Token: 0x0600CAE5 RID: 51941 RVA: 0x00002475 File Offset: 0x00000675
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.ExchangeAuth;
			}
		}

		// Token: 0x170030D6 RID: 12502
		// (get) Token: 0x0600CAE6 RID: 51942 RVA: 0x00287E63 File Offset: 0x00286063
		// (set) Token: 0x0600CAE7 RID: 51943 RVA: 0x00287E6B File Offset: 0x0028606B
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x170030D7 RID: 12503
		// (get) Token: 0x0600CAE8 RID: 51944 RVA: 0x00287E74 File Offset: 0x00286074
		// (set) Token: 0x0600CAE9 RID: 51945 RVA: 0x00287E7C File Offset: 0x0028607C
		public string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x170030D8 RID: 12504
		// (get) Token: 0x0600CAEA RID: 51946 RVA: 0x00287E85 File Offset: 0x00286085
		// (set) Token: 0x0600CAEB RID: 51947 RVA: 0x00287E8D File Offset: 0x0028608D
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170030D9 RID: 12505
		// (get) Token: 0x0600CAEC RID: 51948 RVA: 0x00287E96 File Offset: 0x00286096
		// (set) Token: 0x0600CAED RID: 51949 RVA: 0x00287E9E File Offset: 0x0028609E
		public string EwsUrl
		{
			get
			{
				return this.ewsUrl;
			}
			set
			{
				this.ewsUrl = value;
			}
		}

		// Token: 0x170030DA RID: 12506
		// (get) Token: 0x0600CAEE RID: 51950 RVA: 0x00287EA7 File Offset: 0x002860A7
		// (set) Token: 0x0600CAEF RID: 51951 RVA: 0x00287EAF File Offset: 0x002860AF
		public string EwsSupportedSchema
		{
			get
			{
				return this.ewsSupportedSchema;
			}
			set
			{
				this.ewsSupportedSchema = value;
			}
		}

		// Token: 0x0600CAF0 RID: 51952 RVA: 0x00287EB8 File Offset: 0x002860B8
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			if (this.username == null)
			{
				return new WindowsCredential();
			}
			return new BasicAuthCredential(this.username, this.password);
		}

		// Token: 0x0600CAF1 RID: 51953 RVA: 0x00287ED9 File Offset: 0x002860D9
		public override IResourceCredential[] ToResourceCredentialArray(IdentityContext context = null)
		{
			return new IResourceCredential[]
			{
				this.ToResourceCredential(null),
				new ExchangeCredentialAdornment(this.ewsUrl, this.ewsSupportedSchema, this.emailAddress)
			};
		}

		// Token: 0x0600CAF2 RID: 51954 RVA: 0x00287F08 File Offset: 0x00286108
		public override bool TryMergeWith(CredentialData credentialData)
		{
			ExchangeCredentialData exchangeCredentialData = credentialData as ExchangeCredentialData;
			if (exchangeCredentialData != null)
			{
				if (exchangeCredentialData.emailAddress != null)
				{
					this.emailAddress = exchangeCredentialData.emailAddress;
				}
				if (exchangeCredentialData.username != null)
				{
					this.username = exchangeCredentialData.username;
				}
				if (exchangeCredentialData.password != null)
				{
					this.password = exchangeCredentialData.password;
				}
				this.ewsUrl = exchangeCredentialData.ewsUrl;
				this.ewsSupportedSchema = exchangeCredentialData.ewsSupportedSchema;
				return true;
			}
			return false;
		}

		// Token: 0x0600CAF3 RID: 51955 RVA: 0x00287F76 File Offset: 0x00286176
		public override void Validate()
		{
			if (string.IsNullOrEmpty(this.emailAddress))
			{
				throw StorageExceptions.CredentialValidationException(Strings.EmailAddress_Not_Specified, null);
			}
			if (!ExchangeCredentialData.IsValidEmailAddress(this.emailAddress))
			{
				throw StorageExceptions.CredentialValidationException(Strings.EmailAddress_Not_Valid, null);
			}
		}

		// Token: 0x0600CAF4 RID: 51956 RVA: 0x00287FAC File Offset: 0x002861AC
		private static bool IsValidEmailAddress(string emailAddress)
		{
			bool flag;
			try
			{
				new MailAddress(emailAddress);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400670A RID: 26378
		private string emailAddress;

		// Token: 0x0400670B RID: 26379
		private string username;

		// Token: 0x0400670C RID: 26380
		private string password;

		// Token: 0x0400670D RID: 26381
		private string ewsUrl;

		// Token: 0x0400670E RID: 26382
		private string ewsSupportedSchema;
	}
}
