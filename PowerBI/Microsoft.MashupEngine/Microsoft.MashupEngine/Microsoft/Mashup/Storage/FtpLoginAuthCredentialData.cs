using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002064 RID: 8292
	[XmlType("FtpLoginAuth")]
	public class FtpLoginAuthCredentialData : UsernamePasswordCredentialData
	{
		// Token: 0x0600CAFC RID: 51964 RVA: 0x002876B2 File Offset: 0x002858B2
		public FtpLoginAuthCredentialData()
		{
		}

		// Token: 0x0600CAFD RID: 51965 RVA: 0x002876BA File Offset: 0x002858BA
		public FtpLoginAuthCredentialData(string username, string password)
			: base(username, password)
		{
		}

		// Token: 0x170030DC RID: 12508
		// (get) Token: 0x0600CAFE RID: 51966 RVA: 0x00075E2C File Offset: 0x0007402C
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.FtpLoginAuth;
			}
		}

		// Token: 0x0600CAFF RID: 51967 RVA: 0x0028806D File Offset: 0x0028626D
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new FtpLoginAuthCredential(base.Username, base.Password);
		}

		// Token: 0x0600CB00 RID: 51968 RVA: 0x00288080 File Offset: 0x00286280
		public override bool TryMergeWith(CredentialData credentialData)
		{
			FtpLoginAuthCredentialData ftpLoginAuthCredentialData = credentialData as FtpLoginAuthCredentialData;
			if (ftpLoginAuthCredentialData != null)
			{
				if (ftpLoginAuthCredentialData.Username != null)
				{
					base.Username = ftpLoginAuthCredentialData.Username;
				}
				if (ftpLoginAuthCredentialData.Password != null)
				{
					base.Password = ftpLoginAuthCredentialData.Password;
				}
				return true;
			}
			return false;
		}
	}
}
