using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D95 RID: 3477
	internal abstract class CdpaDimension
	{
		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x06005EA6 RID: 24230
		public abstract CdpaCube Cube { get; }

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06005EA7 RID: 24231
		public abstract QualifiedName QualifiedName { get; }

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06005EA8 RID: 24232
		public abstract string Caption { get; }

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x06005EA9 RID: 24233
		public abstract IDictionary<QualifiedName, CdpaDimensionAttribute> Attributes { get; }
	}
}
