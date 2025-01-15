using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000132 RID: 306
	[DataContract]
	internal class NamedCachePropertyList
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00002061 File Offset: 0x00000261
		internal NamedCachePropertyList()
		{
		}

		// Token: 0x040006AB RID: 1707
		[DataMember]
		internal NamedCacheProperty[] propertiesRequired;
	}
}
