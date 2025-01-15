using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D96 RID: 3478
	internal class CdpaDynamicDimension : CdpaDimension
	{
		// Token: 0x06005EAB RID: 24235 RVA: 0x00147372 File Offset: 0x00145572
		public CdpaDynamicDimension(CdpaCube cube, QualifiedName qualifiedName, string caption)
		{
			this.cube = cube;
			this.qualifiedName = qualifiedName;
			this.caption = caption;
			this.attributes = new Dictionary<QualifiedName, CdpaDimensionAttribute>();
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x06005EAC RID: 24236 RVA: 0x0014739A File Offset: 0x0014559A
		public override CdpaCube Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x001473A2 File Offset: 0x001455A2
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x06005EAE RID: 24238 RVA: 0x001473AA File Offset: 0x001455AA
		public override string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x001473B2 File Offset: 0x001455B2
		public override IDictionary<QualifiedName, CdpaDimensionAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x040033FD RID: 13309
		private readonly CdpaCube cube;

		// Token: 0x040033FE RID: 13310
		private readonly QualifiedName qualifiedName;

		// Token: 0x040033FF RID: 13311
		private readonly string caption;

		// Token: 0x04003400 RID: 13312
		private readonly Dictionary<QualifiedName, CdpaDimensionAttribute> attributes;
	}
}
