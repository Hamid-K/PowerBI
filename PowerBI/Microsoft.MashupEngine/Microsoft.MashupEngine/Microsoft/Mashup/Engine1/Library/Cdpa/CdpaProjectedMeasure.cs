using System;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D93 RID: 3475
	internal class CdpaProjectedMeasure : CdpaMeasure
	{
		// Token: 0x06005E98 RID: 24216 RVA: 0x001472B4 File Offset: 0x001454B4
		public CdpaProjectedMeasure(CdpaCube cube, QualifiedName qualifiedName, string caption, TypeValue type, CubeExpression projection)
		{
			this.cube = cube;
			this.qualifiedName = qualifiedName;
			this.caption = caption;
			this.type = type;
			this.projection = projection;
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x06005E99 RID: 24217 RVA: 0x001472E1 File Offset: 0x001454E1
		public override CdpaCube Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x06005E9A RID: 24218 RVA: 0x001472E9 File Offset: 0x001454E9
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x001472F1 File Offset: 0x001454F1
		public override string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x06005E9C RID: 24220 RVA: 0x001472F9 File Offset: 0x001454F9
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x06005E9D RID: 24221 RVA: 0x00147301 File Offset: 0x00145501
		public CubeExpression Projection
		{
			get
			{
				return this.projection;
			}
		}

		// Token: 0x040033F5 RID: 13301
		private readonly CdpaCube cube;

		// Token: 0x040033F6 RID: 13302
		private readonly QualifiedName qualifiedName;

		// Token: 0x040033F7 RID: 13303
		private readonly string caption;

		// Token: 0x040033F8 RID: 13304
		private readonly TypeValue type;

		// Token: 0x040033F9 RID: 13305
		private readonly CubeExpression projection;
	}
}
