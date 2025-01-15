using System;
using System.IO;
using Microsoft.MachineLearning.Internal.Lexer;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B8 RID: 440
	internal sealed class NodePrinter : PreVisitor
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x00034024 File Offset: 0x00032224
		private NodePrinter(IndentingTextWriter wrt, bool showTypes, bool showValues)
		{
			this._showTypes = showTypes;
			this._showValues = showValues;
			this._wrt = wrt;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00034044 File Offset: 0x00032244
		public static void Print(Node node, TextWriter writer, bool showTypes = false, bool showValues = false)
		{
			IndentingTextWriter indentingTextWriter = IndentingTextWriter.Wrap(writer, "  ");
			NodePrinter nodePrinter = new NodePrinter(indentingTextWriter, showTypes, showValues);
			node.Accept(nodePrinter);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0003406D File Offset: 0x0003226D
		private bool NeedParensLeft(Precedence precLeft, Precedence precOp)
		{
			return precLeft < precOp || (precLeft <= precOp && precOp == Precedence.Power);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00034080 File Offset: 0x00032280
		private bool NeedParensRight(Precedence precOp, Precedence precRight)
		{
			return precOp != Precedence.Postfix && (precOp > precRight || (precOp >= precRight && precOp != Precedence.Power));
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000340A0 File Offset: 0x000322A0
		private Precedence GetPrec(Node node)
		{
			switch (node.Kind)
			{
			case NodeKind.BinaryOp:
				return this.GetPrec(node.AsBinaryOp.Op);
			case NodeKind.UnaryOp:
				return Precedence.PrefixUnary;
			case NodeKind.Compare:
				return Precedence.Compare;
			case NodeKind.Call:
			case NodeKind.With:
				return Precedence.Primary;
			case NodeKind.Ident:
			case NodeKind.BoolLit:
			case NodeKind.NumLit:
			case NodeKind.StrLit:
				return Precedence.Atomic;
			}
			return Precedence.None;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0003410C File Offset: 0x0003230C
		private Precedence GetPrec(BinaryOp op)
		{
			switch (op)
			{
			case BinaryOp.Or:
				return Precedence.Or;
			case BinaryOp.And:
				return Precedence.And;
			case BinaryOp.Add:
			case BinaryOp.Sub:
				return Precedence.Add;
			case BinaryOp.Mul:
			case BinaryOp.Div:
			case BinaryOp.Mod:
				return Precedence.Mul;
			case BinaryOp.Power:
				return Precedence.Power;
			case BinaryOp.Error:
				return Precedence.None;
			default:
				return Precedence.None;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00034158 File Offset: 0x00032358
		private string GetString(BinaryOp op)
		{
			switch (op)
			{
			case BinaryOp.Or:
				return " or ";
			case BinaryOp.And:
				return " and ";
			case BinaryOp.Add:
				return " + ";
			case BinaryOp.Sub:
				return " - ";
			case BinaryOp.Mul:
				return " * ";
			case BinaryOp.Div:
				return " / ";
			case BinaryOp.Mod:
				return " % ";
			case BinaryOp.Power:
				return " ^ ";
			case BinaryOp.Error:
				return " <err> ";
			default:
				return " <bad> ";
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000341D0 File Offset: 0x000323D0
		private string GetString(UnaryOp op)
		{
			switch (op)
			{
			case UnaryOp.Not:
				return "not ";
			case UnaryOp.Minus:
				return "-";
			default:
				return "<bad> ";
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00034200 File Offset: 0x00032400
		private string GetString(TokKind tidCompare)
		{
			switch (tidCompare)
			{
			case 34:
				return " != ";
			case 35:
				return " = ";
			case 36:
				return " == ";
			case 38:
				return " < ";
			case 40:
				return " <= ";
			case 41:
				return " <> ";
			case 43:
				return " > ";
			case 45:
				return " >= ";
			}
			return " <bad> ";
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0003427F File Offset: 0x0003247F
		private bool TryShowValue(ExprNode node)
		{
			if (!this._showValues)
			{
				return false;
			}
			if (node.ExprValue == null)
			{
				return false;
			}
			this.ShowValueCore(node);
			this.ShowType(node);
			return true;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000342A4 File Offset: 0x000324A4
		private void ShowValueCore(ExprNode node)
		{
			object exprValue = node.ExprValue;
			switch (node.ExprType.Kind)
			{
			case ExprTypeKind.BL:
				this.Show((DvBool)exprValue);
				return;
			case ExprTypeKind.I4:
				this.Show((DvInt4)exprValue);
				return;
			case ExprTypeKind.I8:
				this.Show((DvInt8)exprValue);
				return;
			case ExprTypeKind.R4:
				this.Show((float)exprValue);
				return;
			case ExprTypeKind.R8:
				this.Show((double)exprValue);
				return;
			case ExprTypeKind.TX:
				this.Show((DvText)exprValue);
				return;
			default:
				return;
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00034332 File Offset: 0x00032532
		private void Show(DvInt4 x)
		{
			if (x.IsNA)
			{
				this._wrt.Write("NA");
				return;
			}
			this._wrt.Write(x);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0003435F File Offset: 0x0003255F
		private void Show(DvInt8 x)
		{
			if (x.IsNA)
			{
				this._wrt.Write("NA");
				return;
			}
			this._wrt.Write(x);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0003438C File Offset: 0x0003258C
		private void Show(float x)
		{
			if (TypeUtils.IsNA(x))
			{
				this._wrt.Write("NA");
				return;
			}
			this._wrt.Write("{0:R}", x);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000343BD File Offset: 0x000325BD
		private void Show(double x)
		{
			if (TypeUtils.IsNA(x))
			{
				this._wrt.Write("NA");
				return;
			}
			this._wrt.Write("{0:R}", x);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000343F0 File Offset: 0x000325F0
		private void Show(DvBool x)
		{
			bool valueOrDefault = ((bool?)x).GetValueOrDefault();
			bool? flag;
			if (flag != null)
			{
				switch (valueOrDefault)
				{
				case false:
					this._wrt.Write("false");
					return;
				case true:
					this._wrt.Write("true");
					return;
				}
			}
			this._wrt.Write("NA");
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00034458 File Offset: 0x00032658
		private void Show(DvText str)
		{
			if (str.IsNA)
			{
				this._wrt.Write("NA");
				return;
			}
			int num = str.Length;
			if (num > 100)
			{
				num = 97;
			}
			this._wrt.Write('"');
			for (int i = 0; i < num; i++)
			{
				char c = str[i];
				if (c < ' ' || c == '"')
				{
					this._wrt.Write(' ');
				}
				else
				{
					this._wrt.Write(c);
				}
			}
			if (num < str.Length)
			{
				this._wrt.Write("...");
			}
			this._wrt.Write('"');
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00034500 File Offset: 0x00032700
		private void ShowType(ExprNode node)
		{
			if (!this._showTypes)
			{
				return;
			}
			if (node.IsNone)
			{
				return;
			}
			this._wrt.Write(':');
			this._wrt.Write(node.ExprType.ToString());
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0003454C File Offset: 0x0003274C
		private void ShowType(ParamNode node)
		{
			if (!this._showTypes)
			{
				return;
			}
			if (node.ExprType.Kind == ExprTypeKind.None)
			{
				return;
			}
			this._wrt.Write(':');
			this._wrt.Write(node.ExprType.ToString());
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00034599 File Offset: 0x00032799
		public override void Visit(BoolLitNode node)
		{
			this._wrt.Write(node.Value ? "true" : "false");
			this.ShowType(node);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000345C1 File Offset: 0x000327C1
		public override void Visit(StrLitNode node)
		{
			this.Show(node.Value);
			this.ShowType(node);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000345D6 File Offset: 0x000327D6
		public override void Visit(NumLitNode node)
		{
			this._wrt.Write(node.Value.ToString());
			this.ShowType(node);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000345F5 File Offset: 0x000327F5
		public override void Visit(NameNode node)
		{
			this._wrt.Write(node.Value);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00034608 File Offset: 0x00032808
		public override void Visit(IdentNode node)
		{
			if (this.TryShowValue(node))
			{
				return;
			}
			this._wrt.Write(node.Value);
			this.ShowType(node);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0003462C File Offset: 0x0003282C
		public override void Visit(ParamNode node)
		{
			this._wrt.Write(node.Name);
			this.ShowType(node);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00034648 File Offset: 0x00032848
		public override void Visit(LambdaNode node)
		{
			if (node.Vars.Length == 1)
			{
				node.Vars[0].Accept(this);
			}
			else
			{
				this._wrt.Write('(');
				string text = "";
				foreach (ParamNode paramNode in node.Vars)
				{
					this._wrt.Write(text);
					paramNode.Accept(this);
					text = "";
				}
				this._wrt.Write(")");
			}
			this._wrt.Write(" => ");
			node.Expr.Accept(this);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000346E4 File Offset: 0x000328E4
		public override void Visit(UnaryOpNode node)
		{
			if (this.TryShowValue(node))
			{
				return;
			}
			Precedence prec = this.GetPrec(node.Arg);
			this._wrt.Write(this.GetString(node.Op));
			if (prec < Precedence.PrefixUnary)
			{
				this._wrt.Write('(');
			}
			node.Arg.Accept(this);
			if (prec < Precedence.PrefixUnary)
			{
				this._wrt.Write(')');
			}
			this.ShowType(node);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00034758 File Offset: 0x00032958
		public override void Visit(BinaryOpNode node)
		{
			if (this.TryShowValue(node))
			{
				return;
			}
			Precedence prec = this.GetPrec(node.Op);
			Precedence prec2 = this.GetPrec(node.Left);
			Precedence prec3 = this.GetPrec(node.Right);
			bool flag = this.NeedParensLeft(prec2, prec);
			bool flag2 = this.NeedParensRight(prec, prec3);
			if (flag)
			{
				this._wrt.Write('(');
			}
			node.Left.Accept(this);
			if (flag)
			{
				this._wrt.Write(')');
			}
			this._wrt.Write(this.GetString(node.Op));
			if (flag2)
			{
				this._wrt.Write('(');
			}
			node.Right.Accept(this);
			if (flag2)
			{
				this._wrt.Write(')');
			}
			this.ShowType(node);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00034824 File Offset: 0x00032A24
		public override void Visit(ConditionalNode node)
		{
			if (this.TryShowValue(node))
			{
				return;
			}
			Precedence prec = this.GetPrec(node.Cond);
			this.GetPrec(node.Left);
			this.GetPrec(node.Right);
			bool flag = this.NeedParensLeft(prec, Precedence.Conditional);
			if (flag)
			{
				this._wrt.Write('(');
			}
			node.Cond.Accept(this);
			if (flag)
			{
				this._wrt.Write(')');
			}
			this._wrt.Write(" ? ");
			node.Left.Accept(this);
			this._wrt.Write(" : ");
			node.Right.Accept(this);
			this.ShowType(node);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000348D8 File Offset: 0x00032AD8
		public override void Visit(CompareNode node)
		{
			if (this.TryShowValue(node))
			{
				return;
			}
			TokKind tidLax = node.TidLax;
			TokKind tidStrict = node.TidStrict;
			string @string = this.GetString(tidLax);
			string string2 = this.GetString(tidStrict);
			string empty = string.Empty;
			string text = string.Empty;
			int num = 0;
			for (;;)
			{
				this._wrt.Write(text);
				Node node2 = node.Operands.Items[num];
				Precedence prec = this.GetPrec(node2);
				if (prec <= Precedence.Compare)
				{
					this._wrt.Write('(');
				}
				node2.Accept(this);
				if (prec <= Precedence.Compare)
				{
					this._wrt.Write(')');
				}
				if (++num >= node.Operands.Items.Length)
				{
					break;
				}
				TokKind kind = node.Operands.Delimiters[num - 1].Kind;
				text = ((kind == tidLax) ? @string : string2);
			}
			this.ShowType(node);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000349B4 File Offset: 0x00032BB4
		public override void Visit(CallNode node)
		{
			if (this.TryShowValue(node))
			{
				return;
			}
			if (node.NameSpace != null)
			{
				node.NameSpace.Accept(this);
				this._wrt.Write('.');
			}
			node.Head.Accept(this);
			this._wrt.Write('(');
			node.Args.Accept(this);
			this._wrt.Write(')');
			this.ShowType(node);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00034A28 File Offset: 0x00032C28
		public override void Visit(ListNode node)
		{
			int num = node.Items.Length;
			if (num == 0)
			{
				return;
			}
			if (node.Delimiters == null)
			{
				foreach (Node node2 in node.Items)
				{
					node2.Accept(this);
				}
				return;
			}
			if (num <= 6)
			{
				node.Items[0].Accept(this);
				for (int j = 1; j < num; j++)
				{
					this._wrt.Write(", ");
					node.Items[j].Accept(this);
				}
				return;
			}
			for (int k = 0; k < 5; k++)
			{
				node.Items[k].Accept(this);
				this._wrt.Write(", ");
			}
			this._wrt.Write("..., ");
			node.Items[num - 1].Accept(this);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00034AFC File Offset: 0x00032CFC
		public override void Visit(WithNode node)
		{
			if (this.TryShowValue(node))
			{
				return;
			}
			this._wrt.Write("with(");
			node.Local.Accept(this);
			this._wrt.Write("; ");
			node.Body.Accept(this);
			this._wrt.Write(")");
			this.ShowType(node);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00034B62 File Offset: 0x00032D62
		public override void Visit(WithLocalNode node)
		{
			this._wrt.Write(node.Name);
			this._wrt.Write(" = ");
			node.Value.Accept(this);
		}

		// Token: 0x04000514 RID: 1300
		private readonly bool _showTypes;

		// Token: 0x04000515 RID: 1301
		private readonly bool _showValues;

		// Token: 0x04000516 RID: 1302
		private readonly IndentingTextWriter _wrt;
	}
}
