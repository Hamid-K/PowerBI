using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002054 RID: 8276
	[XmlType("SqlAuth")]
	public class SqlAuthCredentialData : UsernamePasswordCredentialData
	{
		// Token: 0x0600CA7C RID: 51836 RVA: 0x002876B2 File Offset: 0x002858B2
		public SqlAuthCredentialData()
		{
		}

		// Token: 0x0600CA7D RID: 51837 RVA: 0x002876BA File Offset: 0x002858BA
		public SqlAuthCredentialData(string username, string password)
			: base(username, password)
		{
		}

		// Token: 0x170030B5 RID: 12469
		// (get) Token: 0x0600CA7E RID: 51838 RVA: 0x00002139 File Offset: 0x00000339
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.SqlAuth;
			}
		}

		// Token: 0x0600CA7F RID: 51839 RVA: 0x002876C4 File Offset: 0x002858C4
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new SqlAuthCredential(base.Username, base.Password);
		}

		// Token: 0x0600CA80 RID: 51840 RVA: 0x002876D8 File Offset: 0x002858D8
		public override bool TryMergeWith(CredentialData credentialData)
		{
			SqlAuthCredentialData sqlAuthCredentialData = credentialData as SqlAuthCredentialData;
			if (sqlAuthCredentialData != null)
			{
				if (sqlAuthCredentialData.Username != null)
				{
					base.Username = sqlAuthCredentialData.Username;
				}
				if (sqlAuthCredentialData.Password != null)
				{
					base.Password = sqlAuthCredentialData.Password;
				}
				return true;
			}
			return false;
		}
	}
}
