using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200205D RID: 8285
	[XmlType("EncryptionAdornment")]
	public class EncryptionAdornmentCredentialData : CredentialData
	{
		// Token: 0x0600CAB4 RID: 51892 RVA: 0x00287642 File Offset: 0x00285842
		public EncryptionAdornmentCredentialData()
		{
		}

		// Token: 0x0600CAB5 RID: 51893 RVA: 0x00287BA9 File Offset: 0x00285DA9
		public EncryptionAdornmentCredentialData(bool requireEncryption)
		{
			this.requireEncryption = requireEncryption;
		}

		// Token: 0x170030C5 RID: 12485
		// (get) Token: 0x0600CAB6 RID: 51894 RVA: 0x00002139 File Offset: 0x00000339
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Adornment;
			}
		}

		// Token: 0x170030C6 RID: 12486
		// (get) Token: 0x0600CAB7 RID: 51895 RVA: 0x0014213C File Offset: 0x0014033C
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.Encryption;
			}
		}

		// Token: 0x170030C7 RID: 12487
		// (get) Token: 0x0600CAB8 RID: 51896 RVA: 0x00287BB8 File Offset: 0x00285DB8
		// (set) Token: 0x0600CAB9 RID: 51897 RVA: 0x00287BC0 File Offset: 0x00285DC0
		public bool RequireEncryption
		{
			get
			{
				return this.requireEncryption;
			}
			set
			{
				this.requireEncryption = value;
			}
		}

		// Token: 0x0600CABA RID: 51898 RVA: 0x00287BC9 File Offset: 0x00285DC9
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new EncryptedConnectionAdornment(this.requireEncryption);
		}

		// Token: 0x0600CABB RID: 51899 RVA: 0x00287BD8 File Offset: 0x00285DD8
		public override bool TryMergeWith(CredentialData credentialData)
		{
			EncryptionAdornmentCredentialData encryptionAdornmentCredentialData = credentialData as EncryptionAdornmentCredentialData;
			if (encryptionAdornmentCredentialData != null)
			{
				this.requireEncryption = encryptionAdornmentCredentialData.requireEncryption;
				return true;
			}
			return false;
		}

		// Token: 0x0600CABC RID: 51900 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}

		// Token: 0x04006705 RID: 26373
		private bool requireEncryption;
	}
}
