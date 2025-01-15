using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002079 RID: 8313
	[XmlRoot("NativeQueriesList")]
	public class NativeQueriesList : XmlRoot
	{
		// Token: 0x0600CB73 RID: 52083 RVA: 0x00288641 File Offset: 0x00286841
		public NativeQueriesList()
		{
			this.nativeQueries = new List<NativeQueryXml>();
		}

		// Token: 0x0600CB74 RID: 52084 RVA: 0x00288654 File Offset: 0x00286854
		public NativeQueriesList(List<NativeQueryXml> nativeQueries)
		{
			this.nativeQueries = nativeQueries;
		}

		// Token: 0x17003100 RID: 12544
		// (get) Token: 0x0600CB75 RID: 52085 RVA: 0x00288663 File Offset: 0x00286863
		[XmlArray("NativeQueries")]
		[XmlArrayItem("NativeQuery")]
		public List<NativeQueryXml> List
		{
			get
			{
				return this.nativeQueries;
			}
		}

		// Token: 0x04006745 RID: 26437
		private List<NativeQueryXml> nativeQueries;
	}
}
