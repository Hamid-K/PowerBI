using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001AB RID: 427
	internal sealed class NumLitNode : ExprNode
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x00032AD4 File Offset: 0x00030CD4
		public NumLitNode(NumLitToken tok)
			: base(tok)
		{
			switch (tok.Kind)
			{
			case 4:
			{
				IntLitToken intLitToken = tok.As<IntLitToken>();
				ulong value = intLitToken.Value;
				bool flag = (intLitToken.IntKind & 2) != 0;
				bool flag2 = (intLitToken.IntKind & 1) != 0;
				if (!flag && (value <= 2147483647UL || (value <= (ulong)(-1) && intLitToken.IsHex && !flag2)))
				{
					base.SetValue((int)value);
					return;
				}
				if (value <= 9223372036854775807UL || (intLitToken.IsHex && !flag2))
				{
					base.SetValue((long)value);
					return;
				}
				base.SetType(ExprType.Error);
				return;
			}
			case 5:
				base.SetValue(tok.As<FltLitToken>().Value);
				return;
			case 6:
			{
				DblLitToken dblLitToken = tok.As<DblLitToken>();
				if (dblLitToken.HasSuffix)
				{
					base.SetValue(dblLitToken.Value);
					return;
				}
				base.SetValue((float)dblLitToken.Value);
				return;
			}
			default:
				base.SetType(ExprType.Error);
				return;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00032BD7 File Offset: 0x00030DD7
		public NumLitToken Value
		{
			get
			{
				return this.Token.As<NumLitToken>();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00032BE4 File Offset: 0x00030DE4
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.NumLit;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00032BE8 File Offset: 0x00030DE8
		public override NumLitNode AsNumLit
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00032BEB File Offset: 0x00030DEB
		public override NumLitNode TestNumLit
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00032BEE File Offset: 0x00030DEE
		public override void Accept(NodeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
