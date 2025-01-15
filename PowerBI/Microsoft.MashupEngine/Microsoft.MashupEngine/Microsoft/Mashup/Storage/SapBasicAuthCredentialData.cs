using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002065 RID: 8293
	[XmlType("SapBasicAuth")]
	public class SapBasicAuthCredentialData : UsernamePasswordCredentialData
	{
		// Token: 0x0600CB01 RID: 51969 RVA: 0x002876B2 File Offset: 0x002858B2
		public SapBasicAuthCredentialData()
		{
		}

		// Token: 0x0600CB02 RID: 51970 RVA: 0x002880C2 File Offset: 0x002862C2
		public SapBasicAuthCredentialData(string username, string password, string authentication)
			: base(username, password)
		{
			this.authentication = authentication;
		}

		// Token: 0x170030DD RID: 12509
		// (get) Token: 0x0600CB03 RID: 51971 RVA: 0x0014025A File Offset: 0x0013E45A
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.SapBasicAuth;
			}
		}

		// Token: 0x170030DE RID: 12510
		// (get) Token: 0x0600CB04 RID: 51972 RVA: 0x002880D3 File Offset: 0x002862D3
		// (set) Token: 0x0600CB05 RID: 51973 RVA: 0x002880DB File Offset: 0x002862DB
		public string Authentication
		{
			get
			{
				return this.authentication;
			}
			set
			{
				this.authentication = value;
			}
		}

		// Token: 0x0600CB06 RID: 51974 RVA: 0x002880E4 File Offset: 0x002862E4
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new SapBasicAuthCredential(base.Username, base.Password, this.authentication);
		}

		// Token: 0x0600CB07 RID: 51975 RVA: 0x00288100 File Offset: 0x00286300
		public override bool TryMergeWith(CredentialData credentialData)
		{
			SapBasicAuthCredentialData sapBasicAuthCredentialData = credentialData as SapBasicAuthCredentialData;
			if (sapBasicAuthCredentialData != null)
			{
				if (sapBasicAuthCredentialData.Username != null)
				{
					base.Username = sapBasicAuthCredentialData.Username;
				}
				if (sapBasicAuthCredentialData.Password != null)
				{
					base.Password = sapBasicAuthCredentialData.Password;
				}
				if (sapBasicAuthCredentialData.authentication != null)
				{
					this.authentication = sapBasicAuthCredentialData.authentication;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CB08 RID: 51976 RVA: 0x00288156 File Offset: 0x00286356
		public override void Validate()
		{
			base.Validate();
		}

		// Token: 0x04006710 RID: 26384
		private string authentication;
	}
}
