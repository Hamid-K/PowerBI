using System;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003ED RID: 1005
	internal class StandaloneId : UniqueId
	{
		// Token: 0x0600234C RID: 9036 RVA: 0x0006C6AD File Offset: 0x0006A8AD
		public StandaloneId()
			: base(Guid.NewGuid())
		{
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0006C6BA File Offset: 0x0006A8BA
		public StandaloneId(string id)
			: base(id)
		{
		}
	}
}
