using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200099D RID: 2461
	internal sealed class SetMdxExpression : MdxExpression
	{
		// Token: 0x06004672 RID: 18034 RVA: 0x000EC6C8 File Offset: 0x000EA8C8
		public SetMdxExpression(params MdxExpression[] members)
		{
			this.members = members;
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsComplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06004674 RID: 18036 RVA: 0x000EC6D7 File Offset: 0x000EA8D7
		public MdxExpression[] Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x000EC6E0 File Offset: 0x000EA8E0
		public SetMdxExpression Flatten()
		{
			List<MdxExpression> list = new List<MdxExpression>();
			foreach (MdxExpression mdxExpression in this.members)
			{
				if (mdxExpression is SetMdxExpression)
				{
					foreach (MdxExpression mdxExpression2 in ((SetMdxExpression)mdxExpression).Flatten().Members)
					{
						list.Add(mdxExpression2);
					}
				}
				else
				{
					list.Add(mdxExpression);
				}
			}
			return new SetMdxExpression(list.ToArray());
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x000EC75C File Offset: 0x000EA95C
		public override void Write(MdxExpressionWriter writer)
		{
			writer.WriteExpressionList(this.Members, "{", ",", "}");
		}

		// Token: 0x0400253C RID: 9532
		private readonly MdxExpression[] members;
	}
}
