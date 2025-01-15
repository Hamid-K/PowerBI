using System;
using System.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B8 RID: 6328
	internal class PowerQueryStringLiteral : FormulaStringLiteral
	{
		// Token: 0x0600CE43 RID: 52803 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public PowerQueryStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600CE44 RID: 52804 RVA: 0x002BFEC0 File Offset: 0x002BE0C0
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryStringLiteral(base.Value);
		}

		// Token: 0x0600CE45 RID: 52805 RVA: 0x002BFED0 File Offset: 0x002BE0D0
		public static string EscapeString(string str)
		{
			StringBuilder stringBuilder = new StringBuilder(str.Length + 2);
			stringBuilder.Append('"');
			bool flag = false;
			int i = 0;
			while (i < str.Length)
			{
				char c = str[i];
				switch (c)
				{
				case '\t':
					stringBuilder.Append("#(tab)");
					break;
				case '\n':
					stringBuilder.Append("#(lf)");
					break;
				case '\v':
				case '\f':
					goto IL_009E;
				case '\r':
					stringBuilder.Append("#(cr)");
					break;
				default:
					if (c != '"')
					{
						if (c != '(')
						{
							goto IL_009E;
						}
						if (flag)
						{
							stringBuilder.Append("(#)");
							goto IL_009E;
						}
						goto IL_009E;
					}
					else
					{
						stringBuilder.Append("\"\"");
					}
					break;
				}
				IL_00AE:
				i++;
				continue;
				IL_009E:
				flag = c == '#';
				stringBuilder.Append(c);
				goto IL_00AE;
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x0600CE46 RID: 52806 RVA: 0x002BFFAA File Offset: 0x002BE1AA
		protected override string ToCodeString()
		{
			return PowerQueryStringLiteral.EscapeString(base.Value) ?? "";
		}
	}
}
