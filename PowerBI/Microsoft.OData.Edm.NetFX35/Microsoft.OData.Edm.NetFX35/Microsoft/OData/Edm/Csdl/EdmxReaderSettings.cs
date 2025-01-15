using System;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000016 RID: 22
	public sealed class EdmxReaderSettings
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002C32 File Offset: 0x00000E32
		public EdmxReaderSettings()
		{
			this.IgnoreUnexpectedAttributesAndElements = false;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002C41 File Offset: 0x00000E41
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002C49 File Offset: 0x00000E49
		public Func<Uri, XmlReader> GetReferencedModelReaderFunc { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002C52 File Offset: 0x00000E52
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002C5A File Offset: 0x00000E5A
		public bool IgnoreUnexpectedAttributesAndElements { get; set; }
	}
}
