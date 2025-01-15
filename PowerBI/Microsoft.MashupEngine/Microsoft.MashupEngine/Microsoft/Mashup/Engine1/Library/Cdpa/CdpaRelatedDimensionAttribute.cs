using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D9E RID: 3486
	internal class CdpaRelatedDimensionAttribute : CdpaVirtualDimensionAttribute
	{
		// Token: 0x06005EF6 RID: 24310 RVA: 0x00147D49 File Offset: 0x00145F49
		public CdpaRelatedDimensionAttribute(CdpaDimension dimension, string name, string caption, TypeValue type)
			: base(dimension, name, caption)
		{
			this.type = type;
			this.relatedAttributes = new HashSet<CdpaDimensionAttribute>();
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x06005EF7 RID: 24311 RVA: 0x00147D67 File Offset: 0x00145F67
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x06005EF8 RID: 24312 RVA: 0x00147D6F File Offset: 0x00145F6F
		public HashSet<CdpaDimensionAttribute> RelatedAttributes
		{
			get
			{
				return this.relatedAttributes;
			}
		}

		// Token: 0x0400341C RID: 13340
		private readonly TypeValue type;

		// Token: 0x0400341D RID: 13341
		private readonly HashSet<CdpaDimensionAttribute> relatedAttributes;
	}
}
