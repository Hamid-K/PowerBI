using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B7 RID: 183
	[ImmutableObject(true)]
	public sealed class PosTag
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00006E1C File Offset: 0x0000501C
		public PosTag(PosTagKind kind, double confidence)
		{
			this._kind = kind;
			this._confidence = confidence;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00006E32 File Offset: 0x00005032
		public PosTagKind TagKind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00006E3A File Offset: 0x0000503A
		public double Confidence
		{
			get
			{
				return this._confidence;
			}
		}

		// Token: 0x040003DF RID: 991
		public static readonly ReadOnlyCollection<PosTag> NonePosTags = new ReadOnlyCollection<PosTag>(new PosTag[]
		{
			new PosTag(PosTagKind.None, 1.0)
		});

		// Token: 0x040003E0 RID: 992
		private readonly PosTagKind _kind;

		// Token: 0x040003E1 RID: 993
		private readonly double _confidence;
	}
}
