using System;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000148 RID: 328
	public sealed class CsdlReaderSettings
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x00015D85 File Offset: 0x00013F85
		public CsdlReaderSettings()
		{
			this.IgnoreUnexpectedAttributesAndElements = false;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00015D94 File Offset: 0x00013F94
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00015D9C File Offset: 0x00013F9C
		public Func<Uri, XmlReader> GetReferencedModelReaderFunc { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00015DA5 File Offset: 0x00013FA5
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00015DAD File Offset: 0x00013FAD
		public bool IgnoreUnexpectedAttributesAndElements { get; set; }
	}
}
