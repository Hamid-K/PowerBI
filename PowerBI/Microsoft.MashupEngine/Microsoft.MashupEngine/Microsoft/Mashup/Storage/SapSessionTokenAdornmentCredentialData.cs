using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002066 RID: 8294
	[XmlType("SapSessionTokenAdornment")]
	public class SapSessionTokenAdornmentCredentialData : CredentialData
	{
		// Token: 0x0600CB09 RID: 51977 RVA: 0x0028815E File Offset: 0x0028635E
		public SapSessionTokenAdornmentCredentialData()
			: this(null)
		{
		}

		// Token: 0x0600CB0A RID: 51978 RVA: 0x00288167 File Offset: 0x00286367
		public SapSessionTokenAdornmentCredentialData(string sessionToken)
		{
			this.references = new List<string>();
			this.sessionToken = sessionToken;
		}

		// Token: 0x170030DF RID: 12511
		// (get) Token: 0x0600CB0B RID: 51979 RVA: 0x00002139 File Offset: 0x00000339
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Adornment;
			}
		}

		// Token: 0x170030E0 RID: 12512
		// (get) Token: 0x0600CB0C RID: 51980 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.SapSessionToken;
			}
		}

		// Token: 0x170030E1 RID: 12513
		// (get) Token: 0x0600CB0D RID: 51981 RVA: 0x00288181 File Offset: 0x00286381
		// (set) Token: 0x0600CB0E RID: 51982 RVA: 0x00288189 File Offset: 0x00286389
		public string SessionToken
		{
			get
			{
				return this.sessionToken;
			}
			set
			{
				this.sessionToken = value;
			}
		}

		// Token: 0x170030E2 RID: 12514
		// (get) Token: 0x0600CB0F RID: 51983 RVA: 0x00288192 File Offset: 0x00286392
		// (set) Token: 0x0600CB10 RID: 51984 RVA: 0x0028819A File Offset: 0x0028639A
		public DateTime SessionTokenAcquiredDateTime
		{
			get
			{
				return this.tokenAcquiredDateTime;
			}
			set
			{
				this.tokenAcquiredDateTime = value;
			}
		}

		// Token: 0x170030E3 RID: 12515
		// (get) Token: 0x0600CB11 RID: 51985 RVA: 0x002881A3 File Offset: 0x002863A3
		// (set) Token: 0x0600CB12 RID: 51986 RVA: 0x002881AB File Offset: 0x002863AB
		public string LogonUrl
		{
			get
			{
				return this.logonUrl;
			}
			set
			{
				this.logonUrl = value;
			}
		}

		// Token: 0x170030E4 RID: 12516
		// (get) Token: 0x0600CB13 RID: 51987 RVA: 0x002881B4 File Offset: 0x002863B4
		// (set) Token: 0x0600CB14 RID: 51988 RVA: 0x002881BC File Offset: 0x002863BC
		public List<string> References
		{
			get
			{
				return this.references;
			}
			set
			{
				this.references = value;
			}
		}

		// Token: 0x0600CB15 RID: 51989 RVA: 0x002881C8 File Offset: 0x002863C8
		public override bool TryMergeWith(CredentialData credentialData)
		{
			SapSessionTokenAdornmentCredentialData sapSessionTokenAdornmentCredentialData = credentialData as SapSessionTokenAdornmentCredentialData;
			if (sapSessionTokenAdornmentCredentialData != null)
			{
				if (sapSessionTokenAdornmentCredentialData.SessionToken != null)
				{
					this.sessionToken = sapSessionTokenAdornmentCredentialData.SessionToken;
					this.SessionTokenAcquiredDateTime = sapSessionTokenAdornmentCredentialData.SessionTokenAcquiredDateTime;
					this.logonUrl = sapSessionTokenAdornmentCredentialData.logonUrl;
					this.references = sapSessionTokenAdornmentCredentialData.references;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CB16 RID: 51990 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}

		// Token: 0x0600CB17 RID: 51991 RVA: 0x0028821A File Offset: 0x0028641A
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new EncryptedConnectionAdornment(false);
		}

		// Token: 0x04006711 RID: 26385
		private string logonUrl;

		// Token: 0x04006712 RID: 26386
		private string sessionToken;

		// Token: 0x04006713 RID: 26387
		private DateTime tokenAcquiredDateTime;

		// Token: 0x04006714 RID: 26388
		private List<string> references;
	}
}
