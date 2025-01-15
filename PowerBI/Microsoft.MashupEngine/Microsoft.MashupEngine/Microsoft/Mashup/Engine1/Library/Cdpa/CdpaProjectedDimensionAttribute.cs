using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D9F RID: 3487
	internal class CdpaProjectedDimensionAttribute : CdpaVirtualDimensionAttribute
	{
		// Token: 0x06005EF9 RID: 24313 RVA: 0x00147D77 File Offset: 0x00145F77
		public CdpaProjectedDimensionAttribute(CdpaDimension dimension, string name, string caption, TypeValue type, CubeExpression projection)
			: base(dimension, name, caption)
		{
			this.type = type;
			this.projection = projection;
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x06005EFA RID: 24314 RVA: 0x00147D92 File Offset: 0x00145F92
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x06005EFB RID: 24315 RVA: 0x00147D9A File Offset: 0x00145F9A
		public CubeExpression Projection
		{
			get
			{
				return this.projection;
			}
		}

		// Token: 0x0400341E RID: 13342
		private readonly TypeValue type;

		// Token: 0x0400341F RID: 13343
		private readonly CubeExpression projection;
	}
}
