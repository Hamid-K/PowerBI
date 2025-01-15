using System;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002AD RID: 685
	[Serializable]
	internal sealed class FunctionRelationalLike : FunctionBinary
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x00031606 File Offset: 0x0002F806
		public FunctionRelationalLike(IInternalExpression lhs, IInternalExpression rhs)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0003161C File Offset: 0x0002F81C
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0003161F File Offset: 0x0002F81F
		public override string BinaryOperator()
		{
			return " Like ";
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00031628 File Offset: 0x0002F828
		public override object Evaluate()
		{
			string text = base.Lhs.EvaluateString();
			string text2 = base.Rhs.EvaluateString();
			text2 = text2.Replace("*", "(\\w){0,}");
			text2 = text2.Replace("?", "(\\w){1}");
			text2 = text2.Replace("[!", "[^");
			text2 = text2.Replace("#", "[0-9]");
			text2 = "^" + text2 + "\\z";
			return Regex.Match(text, text2).Success;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000316B1 File Offset: 0x0002F8B1
		public override int PriorityCode
		{
			get
			{
				return 9;
			}
		}
	}
}
