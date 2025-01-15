using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000296 RID: 662
	[Serializable]
	internal sealed class FunctionConcatenate : FunctionBinary
	{
		// Token: 0x060014B1 RID: 5297 RVA: 0x000307D3 File Offset: 0x0002E9D3
		public FunctionConcatenate(IInternalExpression lhs, IInternalExpression rhs, TokenTypes tokenType)
		{
			base.Lhs = lhs;
			base.Rhs = rhs;
			this._TokenType = tokenType;
			this._TypeCode = global::System.TypeCode.String;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000307F8 File Offset: 0x0002E9F8
		public override TypeCode TypeCode()
		{
			return this._TypeCode;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00030800 File Offset: 0x0002EA00
		public override string BinaryOperator()
		{
			if (this._TokenType == TokenTypes.CONCATENATE)
			{
				return " & ";
			}
			return " + ";
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00030818 File Offset: 0x0002EA18
		public override object Evaluate()
		{
			string text = base.Lhs.EvaluateString();
			string text2 = base.Rhs.EvaluateString();
			return text + text2;
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00030842 File Offset: 0x0002EA42
		public override int PriorityCode
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00030845 File Offset: 0x0002EA45
		public override void Validate(ExpressionValidationContext context)
		{
			base.ArrayCheck();
		}

		// Token: 0x040006B5 RID: 1717
		private readonly TypeCode _TypeCode;

		// Token: 0x040006B6 RID: 1718
		private readonly TokenTypes _TokenType;
	}
}
