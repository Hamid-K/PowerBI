using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D92 RID: 3474
	internal class CdpaRowCountMeasure : CdpaMeasure
	{
		// Token: 0x06005E92 RID: 24210 RVA: 0x00147268 File Offset: 0x00145468
		public CdpaRowCountMeasure(CdpaCube cube, QualifiedName qualifiedName, string caption, IList<CdpaDimension> dimensions)
		{
			this.cube = cube;
			this.qualifiedName = qualifiedName;
			this.caption = caption;
			this.dimensions = dimensions;
		}

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x06005E93 RID: 24211 RVA: 0x0014728D File Offset: 0x0014548D
		public IList<CdpaDimension> Dimensions
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06005E94 RID: 24212 RVA: 0x00147295 File Offset: 0x00145495
		public override CdpaCube Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06005E95 RID: 24213 RVA: 0x0014729D File Offset: 0x0014549D
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06005E96 RID: 24214 RVA: 0x001472A5 File Offset: 0x001454A5
		public override string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x06005E97 RID: 24215 RVA: 0x001472AD File Offset: 0x001454AD
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Int64;
			}
		}

		// Token: 0x040033F1 RID: 13297
		private readonly CdpaCube cube;

		// Token: 0x040033F2 RID: 13298
		private readonly QualifiedName qualifiedName;

		// Token: 0x040033F3 RID: 13299
		private readonly string caption;

		// Token: 0x040033F4 RID: 13300
		private readonly IList<CdpaDimension> dimensions;
	}
}
