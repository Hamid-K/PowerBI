using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002060 RID: 8288
	[XmlType("ConnectionStringAdornment")]
	public class ConnectionStringAdornmentCredentialData : CredentialData
	{
		// Token: 0x0600CACF RID: 51919 RVA: 0x00287642 File Offset: 0x00285842
		public ConnectionStringAdornmentCredentialData()
		{
		}

		// Token: 0x0600CAD0 RID: 51920 RVA: 0x00287D4C File Offset: 0x00285F4C
		public ConnectionStringAdornmentCredentialData(string connectionString)
		{
			this.connectionString = connectionString;
		}

		// Token: 0x170030CE RID: 12494
		// (get) Token: 0x0600CAD1 RID: 51921 RVA: 0x00002139 File Offset: 0x00000339
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Adornment;
			}
		}

		// Token: 0x170030CF RID: 12495
		// (get) Token: 0x0600CAD2 RID: 51922 RVA: 0x001422C0 File Offset: 0x001404C0
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.ConnectionString;
			}
		}

		// Token: 0x170030D0 RID: 12496
		// (get) Token: 0x0600CAD3 RID: 51923 RVA: 0x00287D5B File Offset: 0x00285F5B
		// (set) Token: 0x0600CAD4 RID: 51924 RVA: 0x00287D63 File Offset: 0x00285F63
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		// Token: 0x0600CAD5 RID: 51925 RVA: 0x00287D6C File Offset: 0x00285F6C
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new ConnectionStringAdornment(this.connectionString);
		}

		// Token: 0x0600CAD6 RID: 51926 RVA: 0x00287D7C File Offset: 0x00285F7C
		public override bool TryMergeWith(CredentialData credentialData)
		{
			ConnectionStringAdornmentCredentialData connectionStringAdornmentCredentialData = credentialData as ConnectionStringAdornmentCredentialData;
			if (connectionStringAdornmentCredentialData != null)
			{
				this.connectionString = connectionStringAdornmentCredentialData.connectionString;
				return true;
			}
			return false;
		}

		// Token: 0x0600CAD7 RID: 51927 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}

		// Token: 0x04006708 RID: 26376
		private string connectionString;
	}
}
