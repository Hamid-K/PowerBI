using System;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200014E RID: 334
	public sealed class CsdlReaderSettings
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00016232 File Offset: 0x00014432
		public CsdlReaderSettings()
		{
			this.IgnoreUnexpectedAttributesAndElements = false;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00016241 File Offset: 0x00014441
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00016249 File Offset: 0x00014449
		public Func<Uri, XmlReader> GetReferencedModelReaderFunc { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00016252 File Offset: 0x00014452
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x0001625A File Offset: 0x0001445A
		public bool IgnoreUnexpectedAttributesAndElements { get; set; }
	}
}
