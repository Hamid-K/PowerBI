using System;

namespace NLog.MessageTemplates
{
	// Token: 0x02000084 RID: 132
	internal struct LiteralHole
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x000189AE File Offset: 0x00016BAE
		public LiteralHole(Literal literal, Hole hole)
		{
			this.Literal = literal;
			this.Hole = hole;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x000189BE File Offset: 0x00016BBE
		public bool MaybePositionalTemplate
		{
			get
			{
				return this.Literal.Skip != 0 && this.Hole.Index != -1 && this.Hole.CaptureType == CaptureType.Normal;
			}
		}

		// Token: 0x04000231 RID: 561
		public readonly Literal Literal;

		// Token: 0x04000232 RID: 562
		public readonly Hole Hole;
	}
}
