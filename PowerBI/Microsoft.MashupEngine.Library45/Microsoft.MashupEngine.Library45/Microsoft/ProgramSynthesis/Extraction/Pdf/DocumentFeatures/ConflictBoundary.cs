using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C9E RID: 3230
	[NullableContext(1)]
	[Nullable(0)]
	internal class ConflictBoundary
	{
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060052FF RID: 21247 RVA: 0x00105D1D File Offset: 0x00103F1D
		public Conflict Conflict { get; }

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06005300 RID: 21248 RVA: 0x00105D25 File Offset: 0x00103F25
		public bool Top { get; }

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x00105D30 File Offset: 0x00103F30
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> Range
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.Conflict.Bounds.Horizontal;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x00105D50 File Offset: 0x00103F50
		public AxisAligned<bool> IsExclusive
		{
			get
			{
				return this.Conflict.IsExclusive;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x00105D60 File Offset: 0x00103F60
		public int Y
		{
			get
			{
				if (!this.Top)
				{
					return this.Conflict.Bounds.Bottom;
				}
				return this.Conflict.Bounds.Top;
			}
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x00105D9C File Offset: 0x00103F9C
		public ConflictBoundary(Conflict conflict, bool top)
		{
			this.Conflict = conflict;
			this.Top = top;
		}
	}
}
