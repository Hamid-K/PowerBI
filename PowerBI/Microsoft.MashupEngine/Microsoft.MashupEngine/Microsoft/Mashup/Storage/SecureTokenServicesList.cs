using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200208D RID: 8333
	[XmlRoot("SecureTokenServicesList")]
	public class SecureTokenServicesList : XmlRoot
	{
		// Token: 0x0600CBF6 RID: 52214 RVA: 0x0028977F File Offset: 0x0028797F
		public SecureTokenServicesList()
		{
			this.secureTokenServices = new List<SecureTokenServiceXml>();
		}

		// Token: 0x0600CBF7 RID: 52215 RVA: 0x00289792 File Offset: 0x00287992
		public SecureTokenServicesList(List<SecureTokenServiceXml> secureTokenServices)
		{
			this.secureTokenServices = secureTokenServices;
		}

		// Token: 0x1700311B RID: 12571
		// (get) Token: 0x0600CBF8 RID: 52216 RVA: 0x002897A1 File Offset: 0x002879A1
		[XmlArray("SecureTokenServices")]
		[XmlArrayItem("SecureTokenService")]
		public List<SecureTokenServiceXml> List
		{
			get
			{
				return this.secureTokenServices;
			}
		}

		// Token: 0x0400676B RID: 26475
		private List<SecureTokenServiceXml> secureTokenServices;
	}
}
