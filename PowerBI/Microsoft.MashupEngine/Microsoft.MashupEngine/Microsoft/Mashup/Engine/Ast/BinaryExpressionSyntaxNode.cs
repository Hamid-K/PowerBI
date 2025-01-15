using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B8D RID: 7053
	public abstract class BinaryExpressionSyntaxNode : RangeSyntaxNode, IBinaryExpression, IExpression, ISyntaxNode
	{
		// Token: 0x0600B0AA RID: 45226 RVA: 0x00243420 File Offset: 0x00241620
		protected BinaryExpressionSyntaxNode(IExpression left, IExpression right, TokenRange range)
			: base(range)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x0600B0AB RID: 45227 RVA: 0x00243438 File Offset: 0x00241638
		public static BinaryExpressionSyntaxNode New(BinaryOperator2 binaryOperator, IExpression left, IExpression right, TokenRange range)
		{
			switch (binaryOperator)
			{
			case BinaryOperator2.Add:
				return new AddBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Subtract:
				return new SubtractBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Multiply:
				return new MultiplyBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Divide:
				return new DivideBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.GreaterThan:
				return new GreaterThanBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.LessThan:
				return new LessThanBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.GreaterThanOrEquals:
				return new GreaterThanOrEqualsBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.LessThanOrEquals:
				return new LessThanOrEqualsBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Equals:
				return new EqualsBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.NotEquals:
				return new NotEqualsBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.And:
				return new AndBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Or:
				return new OrBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.MetadataAdd:
				return new MetadataAddBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Range:
				return new RangeBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Concatenate:
				return new ConcatenateBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.As:
				return new AsBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Is:
				return new IsBinaryExpressionSyntaxNode(left, right, range);
			case BinaryOperator2.Coalesce:
				return new CoalesceBinaryExpressionSyntaxNode(left, right, range);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002C17 RID: 11287
		// (get) Token: 0x0600B0AC RID: 45228 RVA: 0x00002105 File Offset: 0x00000305
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Binary;
			}
		}

		// Token: 0x17002C18 RID: 11288
		// (get) Token: 0x0600B0AD RID: 45229
		public abstract BinaryOperator2 Operator { get; }

		// Token: 0x17002C19 RID: 11289
		// (get) Token: 0x0600B0AE RID: 45230 RVA: 0x0024353F File Offset: 0x0024173F
		public IExpression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17002C1A RID: 11290
		// (get) Token: 0x0600B0AF RID: 45231 RVA: 0x00243547 File Offset: 0x00241747
		public IExpression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04005ACE RID: 23246
		private IExpression left;

		// Token: 0x04005ACF RID: 23247
		private IExpression right;
	}
}
