using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200099C RID: 2460
	internal sealed class TupleMdxExpression : MdxExpression
	{
		// Token: 0x0600466E RID: 18030 RVA: 0x000EC694 File Offset: 0x000EA894
		public TupleMdxExpression(params MdxExpression[] entries)
		{
			this.entries = entries;
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x0600466F RID: 18031 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsComplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06004670 RID: 18032 RVA: 0x000EC6A3 File Offset: 0x000EA8A3
		public MdxExpression[] Entries
		{
			get
			{
				return this.entries;
			}
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000EC6AB File Offset: 0x000EA8AB
		public override void Write(MdxExpressionWriter writer)
		{
			writer.WriteExpressionList(this.Entries, "(", ",", ")");
		}

		// Token: 0x0400253B RID: 9531
		private readonly MdxExpression[] entries;
	}
}
