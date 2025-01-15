using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C9C RID: 3228
	[NullableContext(1)]
	[Nullable(0)]
	internal class Conflict : IBounded<PixelUnit>
	{
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060052F9 RID: 21241 RVA: 0x00105CA7 File Offset: 0x00103EA7
		public AxisAligned<bool> IsExclusive { get; }

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060052FA RID: 21242 RVA: 0x00105CAF File Offset: 0x00103EAF
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x00105CB7 File Offset: 0x00103EB7
		public Conflict([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
		{
			this.Bounds = bounds;
			this.IsExclusive = new AxisAligned<bool>(true);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00105CD4 File Offset: 0x00103ED4
		public Conflict([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, Axis exclusiveAxis)
		{
			this.Bounds = bounds;
			this.IsExclusive = new AxisAligned<bool>((Axis axis) => axis == exclusiveAxis);
		}
	}
}
