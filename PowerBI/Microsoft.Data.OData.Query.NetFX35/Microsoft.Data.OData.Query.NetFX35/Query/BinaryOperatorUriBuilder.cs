using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200000C RID: 12
	internal sealed class BinaryOperatorUriBuilder
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002829 File Offset: 0x00000A29
		public BinaryOperatorUriBuilder(ODataUriBuilder builder)
		{
			this.builder = builder;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002838 File Offset: 0x00000A38
		public void Write(BinaryOperatorQueryToken binary)
		{
			this.Write(false, binary);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002844 File Offset: 0x00000A44
		private static bool NeedParenthesesLeft(BinaryOperator currentOperator, BinaryOperatorQueryToken leftSubtree)
		{
			BinaryOperator @operator = BinaryOperator.GetOperator(leftSubtree.OperatorKind);
			return @operator.Precedence < currentOperator.Precedence;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000286C File Offset: 0x00000A6C
		private static bool NeedParenthesesRight(BinaryOperator currentOperator, BinaryOperatorQueryToken rightSubtree)
		{
			BinaryOperator @operator = BinaryOperator.GetOperator(rightSubtree.OperatorKind);
			return currentOperator.Precedence >= @operator.Precedence && (currentOperator.Precedence > @operator.Precedence || currentOperator.NeedParenEvenWhenTheSame);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028AC File Offset: 0x00000AAC
		private void Write(bool needParenthesis, BinaryOperatorQueryToken binary)
		{
			if (needParenthesis)
			{
				this.builder.Append("(");
			}
			BinaryOperator @operator = BinaryOperator.GetOperator(binary.OperatorKind);
			BinaryOperatorQueryToken binaryOperatorQueryToken = binary.Left as BinaryOperatorQueryToken;
			if (binaryOperatorQueryToken != null)
			{
				this.Write(BinaryOperatorUriBuilder.NeedParenthesesLeft(@operator, binaryOperatorQueryToken), binaryOperatorQueryToken);
			}
			else
			{
				this.builder.WriteQuery(binary.Left);
			}
			this.builder.Append("%20");
			this.builder.Append(@operator.Text);
			this.builder.Append("%20");
			BinaryOperatorQueryToken binaryOperatorQueryToken2 = binary.Right as BinaryOperatorQueryToken;
			if (binaryOperatorQueryToken2 != null)
			{
				this.Write(BinaryOperatorUriBuilder.NeedParenthesesRight(@operator, binaryOperatorQueryToken2), binaryOperatorQueryToken2);
			}
			else
			{
				this.builder.WriteQuery(binary.Right);
			}
			if (needParenthesis)
			{
				this.builder.Append(")");
			}
		}

		// Token: 0x0400003A RID: 58
		private readonly ODataUriBuilder builder;
	}
}
