using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200206F RID: 8303
	[XmlRoot("CredentialsList")]
	public class CredentialsList : XmlRoot
	{
		// Token: 0x0600CB3A RID: 52026 RVA: 0x002883D5 File Offset: 0x002865D5
		public CredentialsList()
		{
			this.credentials = new List<Credential>();
		}

		// Token: 0x0600CB3B RID: 52027 RVA: 0x002883E8 File Offset: 0x002865E8
		public CredentialsList(List<Credential> credentials)
		{
			this.credentials = credentials;
		}

		// Token: 0x170030F0 RID: 12528
		// (get) Token: 0x0600CB3C RID: 52028 RVA: 0x002883F7 File Offset: 0x002865F7
		[XmlArray("Credentials")]
		[XmlArrayItem("Credential")]
		public List<Credential> List
		{
			get
			{
				return this.credentials;
			}
		}

		// Token: 0x0400672E RID: 26414
		private List<Credential> credentials;
	}
}
