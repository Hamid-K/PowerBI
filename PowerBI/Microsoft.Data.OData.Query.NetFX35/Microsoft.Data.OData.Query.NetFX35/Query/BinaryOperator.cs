using System;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000008 RID: 8
	internal sealed class BinaryOperator
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000025F8 File Offset: 0x000007F8
		private BinaryOperator(string text, short precedence, bool needParenEvenWhenTheSame)
		{
			this.text = text;
			this.precedence = precedence;
			this.needParenEvenWhenTheSame = needParenEvenWhenTheSame;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002615 File Offset: 0x00000815
		public bool NeedParenEvenWhenTheSame
		{
			get
			{
				return this.needParenEvenWhenTheSame;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000261D File Offset: 0x0000081D
		public short Precedence
		{
			get
			{
				return this.precedence;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002625 File Offset: 0x00000825
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002630 File Offset: 0x00000830
		public static BinaryOperator GetOperator(BinaryOperatorKind operatorKind)
		{
			switch (operatorKind)
			{
			case BinaryOperatorKind.Or:
				return BinaryOperator.Or;
			case BinaryOperatorKind.And:
				return BinaryOperator.And;
			case BinaryOperatorKind.Equal:
				return BinaryOperator.Equal;
			case BinaryOperatorKind.NotEqual:
				return BinaryOperator.NotEqual;
			case BinaryOperatorKind.GreaterThan:
				return BinaryOperator.GreaterThan;
			case BinaryOperatorKind.GreaterThanOrEqual:
				return BinaryOperator.GreaterThanOrEqual;
			case BinaryOperatorKind.LessThan:
				return BinaryOperator.LessThan;
			case BinaryOperatorKind.LessThanOrEqual:
				return BinaryOperator.LessThanOrEqual;
			case BinaryOperatorKind.Add:
				return BinaryOperator.Add;
			case BinaryOperatorKind.Subtract:
				return BinaryOperator.Subtract;
			case BinaryOperatorKind.Multiply:
				return BinaryOperator.Multiply;
			case BinaryOperatorKind.Divide:
				return BinaryOperator.Divide;
			case BinaryOperatorKind.Modulo:
				return BinaryOperator.Modulo;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.BinaryOperator_GetOperator_UnreachableCodePath));
			}
		}

		// Token: 0x04000018 RID: 24
		private static readonly BinaryOperator Add = new BinaryOperator("add", 4, false);

		// Token: 0x04000019 RID: 25
		private static readonly BinaryOperator And = new BinaryOperator("and", 1, false);

		// Token: 0x0400001A RID: 26
		private static readonly BinaryOperator Divide = new BinaryOperator("div", 5, true);

		// Token: 0x0400001B RID: 27
		private static readonly BinaryOperator Equal = new BinaryOperator("eq", 2, true);

		// Token: 0x0400001C RID: 28
		private static readonly BinaryOperator GreaterThanOrEqual = new BinaryOperator("ge", 3, true);

		// Token: 0x0400001D RID: 29
		private static readonly BinaryOperator GreaterThan = new BinaryOperator("gt", 3, true);

		// Token: 0x0400001E RID: 30
		private static readonly BinaryOperator LessThanOrEqual = new BinaryOperator("le", 3, true);

		// Token: 0x0400001F RID: 31
		private static readonly BinaryOperator LessThan = new BinaryOperator("lt", 3, true);

		// Token: 0x04000020 RID: 32
		private static readonly BinaryOperator Modulo = new BinaryOperator("mod", 5, true);

		// Token: 0x04000021 RID: 33
		private static readonly BinaryOperator Multiply = new BinaryOperator("mul", 5, false);

		// Token: 0x04000022 RID: 34
		private static readonly BinaryOperator NotEqual = new BinaryOperator("ne", 2, true);

		// Token: 0x04000023 RID: 35
		private static readonly BinaryOperator Or = new BinaryOperator("or", 0, false);

		// Token: 0x04000024 RID: 36
		private static readonly BinaryOperator Subtract = new BinaryOperator("sub", 4, true);

		// Token: 0x04000025 RID: 37
		private readonly string text;

		// Token: 0x04000026 RID: 38
		private readonly short precedence;

		// Token: 0x04000027 RID: 39
		private readonly bool needParenEvenWhenTheSame;
	}
}
