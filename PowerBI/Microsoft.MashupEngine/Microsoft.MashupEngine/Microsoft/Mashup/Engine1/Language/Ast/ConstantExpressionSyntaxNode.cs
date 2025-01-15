using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018AF RID: 6319
	internal sealed class ConstantExpressionSyntaxNode : RangeSyntaxNode, IConstantExpression, IExpression, ISyntaxNode, IConstantValue, IConstantExpression2
	{
		// Token: 0x0600A0F1 RID: 41201 RVA: 0x00215DCC File Offset: 0x00213FCC
		public ConstantExpressionSyntaxNode(Value value)
			: this(value, TokenRange.Null)
		{
		}

		// Token: 0x0600A0F2 RID: 41202 RVA: 0x00215DDA File Offset: 0x00213FDA
		public ConstantExpressionSyntaxNode(Value value, TokenRange range)
			: base(range)
		{
			this.value = value;
		}

		// Token: 0x0600A0F3 RID: 41203 RVA: 0x00215DEC File Offset: 0x00213FEC
		public static ConstantExpressionSyntaxNode New(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind != ValueKind.Null)
			{
				if (kind == ValueKind.Logical)
				{
					if (value.MetaValue.IsEmpty)
					{
						return ConstantExpressionSyntaxNode.New(value.AsBoolean);
					}
				}
			}
			else if (value.MetaValue.IsEmpty)
			{
				return ConstantExpressionSyntaxNode.Null;
			}
			return new ConstantExpressionSyntaxNode(value);
		}

		// Token: 0x0600A0F4 RID: 41204 RVA: 0x00215E3B File Offset: 0x0021403B
		public static ConstantExpressionSyntaxNode New(Identifier identifier)
		{
			if (identifier != null)
			{
				return new ConstantExpressionSyntaxNode(TextValue.New(identifier));
			}
			return ConstantExpressionSyntaxNode.Null;
		}

		// Token: 0x0600A0F5 RID: 41205 RVA: 0x00215E5C File Offset: 0x0021405C
		public static ConstantExpressionSyntaxNode New(string value)
		{
			if (value != null)
			{
				return new ConstantExpressionSyntaxNode(TextValue.New(value));
			}
			return ConstantExpressionSyntaxNode.Null;
		}

		// Token: 0x0600A0F6 RID: 41206 RVA: 0x00215E72 File Offset: 0x00214072
		public static ConstantExpressionSyntaxNode New(bool value)
		{
			if (!value)
			{
				return ConstantExpressionSyntaxNode.False;
			}
			return ConstantExpressionSyntaxNode.True;
		}

		// Token: 0x17002934 RID: 10548
		// (get) Token: 0x0600A0F7 RID: 41207 RVA: 0x00002139 File Offset: 0x00000339
		public ExpressionKind Kind
		{
			get
			{
				return ExpressionKind.Constant;
			}
		}

		// Token: 0x17002935 RID: 10549
		// (get) Token: 0x0600A0F8 RID: 41208 RVA: 0x00215E82 File Offset: 0x00214082
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17002936 RID: 10550
		// (get) Token: 0x0600A0F9 RID: 41209 RVA: 0x00215E8A File Offset: 0x0021408A
		IValue IConstantExpression2.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x04005466 RID: 21606
		public static readonly ConstantExpressionSyntaxNode Null = new ConstantExpressionSyntaxNode(Value.Null);

		// Token: 0x04005467 RID: 21607
		public static readonly ConstantExpressionSyntaxNode False = new ConstantExpressionSyntaxNode(LogicalValue.False);

		// Token: 0x04005468 RID: 21608
		public static readonly ConstantExpressionSyntaxNode True = new ConstantExpressionSyntaxNode(LogicalValue.True);

		// Token: 0x04005469 RID: 21609
		public static readonly ConstantExpressionSyntaxNode EmptyRecord = new ConstantExpressionSyntaxNode(RecordValue.Empty);

		// Token: 0x0400546A RID: 21610
		private readonly Value value;
	}
}
