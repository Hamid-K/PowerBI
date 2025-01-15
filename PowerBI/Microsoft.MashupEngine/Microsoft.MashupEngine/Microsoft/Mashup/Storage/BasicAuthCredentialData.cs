using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002055 RID: 8277
	[XmlType("BasicAuth")]
	public class BasicAuthCredentialData : UsernamePasswordCredentialData
	{
		// Token: 0x0600CA81 RID: 51841 RVA: 0x002876B2 File Offset: 0x002858B2
		public BasicAuthCredentialData()
		{
		}

		// Token: 0x0600CA82 RID: 51842 RVA: 0x002876BA File Offset: 0x002858BA
		public BasicAuthCredentialData(string username, string password)
			: base(username, password)
		{
		}

		// Token: 0x170030B6 RID: 12470
		// (get) Token: 0x0600CA83 RID: 51843 RVA: 0x000023C4 File Offset: 0x000005C4
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.BasicAuth;
			}
		}

		// Token: 0x0600CA84 RID: 51844 RVA: 0x0028771A File Offset: 0x0028591A
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new BasicAuthCredential(base.Username, base.Password);
		}

		// Token: 0x0600CA85 RID: 51845 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}

		// Token: 0x0600CA86 RID: 51846 RVA: 0x00287730 File Offset: 0x00285930
		public override bool TryMergeWith(CredentialData credentialData)
		{
			BasicAuthCredentialData basicAuthCredentialData = credentialData as BasicAuthCredentialData;
			if (basicAuthCredentialData != null)
			{
				if (basicAuthCredentialData.Username != null)
				{
					base.Username = basicAuthCredentialData.Username;
				}
				if (basicAuthCredentialData.Password != null)
				{
					base.Password = basicAuthCredentialData.Password;
				}
				return true;
			}
			return false;
		}
	}
}
