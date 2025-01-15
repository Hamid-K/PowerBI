using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000032 RID: 50
	[DataContract]
	public class CredentialDetails
	{
		// Token: 0x06000109 RID: 265 RVA: 0x0000307C File Offset: 0x0000127C
		public CredentialDetails()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003084 File Offset: 0x00001284
		public CredentialDetails(CredentialDetails original)
		{
			if (original == null)
			{
				throw new ArgumentNullException("original");
			}
			this.Credentials = original.Credentials;
			this.CredentialType = original.CredentialType;
			this.EncryptedConnection = original.EncryptedConnection;
			this.PrivacyLevel = original.PrivacyLevel;
			this.EncryptionAlgorithm = original.EncryptionAlgorithm;
			this.UpdateByOAuthToken = original.UpdateByOAuthToken;
			this.SkipGetOAuthToken = original.SkipGetOAuthToken;
			this.SkipTestConnection = original.SkipTestConnection;
			this.UseConfidentialClientForOAuth = original.UseConfidentialClientForOAuth;
			this.UseCustomOAuthApp = original.UseCustomOAuthApp;
			this.RuntimeCredentialProperties = original.RuntimeCredentialProperties;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003129 File Offset: 0x00001329
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00003131 File Offset: 0x00001331
		[RequiredIfNotAnonymous]
		[DataMember(Name = "credentials", Order = 0, EmitDefaultValue = false)]
		public string Credentials { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000313A File Offset: 0x0000133A
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00003142 File Offset: 0x00001342
		[Required]
		[DataMember(Name = "credentialType", Order = 10)]
		public CredentialType? CredentialType { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000314B File Offset: 0x0000134B
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00003153 File Offset: 0x00001353
		[Required]
		[DataMember(Name = "encryptedConnection", Order = 20)]
		public EncryptedConnection? EncryptedConnection { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00003164 File Offset: 0x00001364
		[Required]
		[DataMember(Name = "privacyLevel", Order = 30)]
		public PrivacyLevel? PrivacyLevel { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000316D File Offset: 0x0000136D
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00003175 File Offset: 0x00001375
		[Required]
		[DataMember(Name = "encryptionAlgorithm", Order = 40)]
		public string EncryptionAlgorithm { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000317E File Offset: 0x0000137E
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00003186 File Offset: 0x00001386
		[DataMember(Name = "SkipGetOAuthToken", Order = 50)]
		public bool SkipGetOAuthToken { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000318F File Offset: 0x0000138F
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00003197 File Offset: 0x00001397
		[DataMember(Name = "useConfidentialClientForOAuth", Order = 60, EmitDefaultValue = false)]
		public bool UseConfidentialClientForOAuth { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000031A0 File Offset: 0x000013A0
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000031A8 File Offset: 0x000013A8
		[DataMember(Name = "updateByOAuthToken", Order = 70, EmitDefaultValue = false)]
		public bool UpdateByOAuthToken { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000031B1 File Offset: 0x000013B1
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000031B9 File Offset: 0x000013B9
		[DataMember(Name = "useCustomOAuthApp", Order = 80, EmitDefaultValue = false)]
		public bool UseCustomOAuthApp { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000031C2 File Offset: 0x000013C2
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000031CA File Offset: 0x000013CA
		[DataMember(Name = "skipTestConnection", Order = 90, EmitDefaultValue = false)]
		public bool SkipTestConnection { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000031D3 File Offset: 0x000013D3
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000031DB File Offset: 0x000013DB
		[DataMember(Name = "runtimeCredentialProperties", Order = 100, EmitDefaultValue = false)]
		public string RuntimeCredentialProperties { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000031E4 File Offset: 0x000013E4
		public bool IsCredentialEncrypted
		{
			get
			{
				return this.EncryptionAlgorithm != null && !string.Equals("NONE", this.EncryptionAlgorithm, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003204 File Offset: 0x00001404
		public CredentialDetails WithSymmetricKeyCredentials(string symmetricKeyCredentials)
		{
			return new CredentialDetails
			{
				Credentials = symmetricKeyCredentials,
				EncryptionAlgorithm = "AES",
				CredentialType = this.CredentialType,
				EncryptedConnection = this.EncryptedConnection,
				PrivacyLevel = this.PrivacyLevel
			};
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003241 File Offset: 0x00001441
		public CredentialDetails WithSymmetricKeyCredentials(CredentialDetails credentialDetails, string symmetricKeyCredentials)
		{
			return new CredentialDetails(credentialDetails)
			{
				Credentials = symmetricKeyCredentials,
				EncryptionAlgorithm = "AES"
			};
		}
	}
}
