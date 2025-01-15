using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200099A RID: 2458
	internal abstract class MdxExpression
	{
		// Token: 0x06004665 RID: 18021
		public abstract void Write(MdxExpressionWriter writer);

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsComplex
		{
			get
			{
				return false;
			}
		}
	}
}
