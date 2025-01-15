using System;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004BD RID: 1213
	internal sealed class SapBwMdxVariableExpression
	{
		// Token: 0x060027D5 RID: 10197 RVA: 0x00075650 File Offset: 0x00073850
		public SapBwMdxVariableExpression(IdentifierMdxExpression name, SapBwMdxVariableExpressionSign sign, MdxExpression value)
		{
			this.name = name;
			this.sign = sign;
			this.value = value;
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0007566D File Offset: 0x0007386D
		public void Write(MdxExpressionWriter writer)
		{
			this.name.Write(writer);
			writer.Write(SapBwMdxVariableExpression.ToString(this.sign));
			this.value.Write(writer);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00075698 File Offset: 0x00073898
		private static string ToString(SapBwMdxVariableExpressionSign sign)
		{
			if (sign == SapBwMdxVariableExpressionSign.Including)
			{
				return "INCLUDING";
			}
			if (sign == SapBwMdxVariableExpressionSign.Excluding)
			{
				return "EXCLUDING";
			}
			throw new InvalidOperationException(sign.ToString());
		}

		// Token: 0x040010D7 RID: 4311
		private readonly IdentifierMdxExpression name;

		// Token: 0x040010D8 RID: 4312
		private readonly SapBwMdxVariableExpressionSign sign;

		// Token: 0x040010D9 RID: 4313
		private readonly MdxExpression value;
	}
}
