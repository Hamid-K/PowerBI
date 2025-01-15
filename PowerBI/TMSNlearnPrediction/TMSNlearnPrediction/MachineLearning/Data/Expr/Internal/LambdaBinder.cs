using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Internal.Lexer;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x0200018C RID: 396
	internal sealed class LambdaBinder : NodeVisitor
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0002A61C File Offset: 0x0002881C
		private LambdaBinder(Action<string> printError)
		{
			this._printError = printError;
			this._rgwith = new List<WithNode>();
			this._providers = MetadataUtils.Prepend<IFunctionProvider>(from info in ComponentCatalog.GetAllDerivedClasses(typeof(IFunctionProvider), typeof(SignatureFunctionProvider), null)
				select info.CreateInstance<IFunctionProvider>(), new IFunctionProvider[] { BuiltinFunctions.Instance }).ToArray<IFunctionProvider>();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002A6A0 File Offset: 0x000288A0
		public static void Run(ref List<Error> errors, LambdaNode node, Action<string> printError)
		{
			LambdaBinder lambdaBinder = new LambdaBinder(printError);
			lambdaBinder._errors = errors;
			node.Accept(lambdaBinder);
			ExprNode expr = node.Expr;
			switch (expr.ExprType.Kind)
			{
			case ExprTypeKind.BL:
				node.ResultType = BoolType.Instance;
				break;
			case ExprTypeKind.I4:
				node.ResultType = NumberType.I4;
				break;
			case ExprTypeKind.I8:
				node.ResultType = NumberType.I8;
				break;
			case ExprTypeKind.R4:
				node.ResultType = NumberType.R4;
				break;
			case ExprTypeKind.R8:
				node.ResultType = NumberType.R8;
				break;
			case ExprTypeKind.TX:
				node.ResultType = TextType.Instance;
				break;
			default:
				if (!lambdaBinder.HasErrors)
				{
					lambdaBinder.PostError(expr, "Invalid result type");
				}
				break;
			}
			errors = lambdaBinder._errors;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0002A762 File Offset: 0x00028962
		private bool HasErrors
		{
			get
			{
				return Utils.Size<Error>(this._errors) > 0;
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002A772 File Offset: 0x00028972
		private void PostError(Node node, string msg)
		{
			Utils.Add<Error>(ref this._errors, new Error(node.Token, msg));
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002A78B File Offset: 0x0002898B
		private void PostError(Node node, string msg, params object[] args)
		{
			Utils.Add<Error>(ref this._errors, new Error(node.Token, string.Format(msg, args)));
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0002A7AA File Offset: 0x000289AA
		public override void Visit(BoolLitNode node)
		{
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002A7AC File Offset: 0x000289AC
		public override void Visit(StrLitNode node)
		{
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002A7AE File Offset: 0x000289AE
		public override void Visit(NumLitNode node)
		{
			if (node.IsError)
			{
				this.PostError(node, "Overflow");
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002A7C4 File Offset: 0x000289C4
		public override void Visit(NameNode node)
		{
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002A7C8 File Offset: 0x000289C8
		public override void Visit(IdentNode node)
		{
			if (node.IsMissing)
			{
				node.SetType(ExprType.Error);
				return;
			}
			string value = node.Value;
			int num = this._rgwith.Count;
			while (--num >= 0)
			{
				WithNode withNode = this._rgwith[num];
				if (value == withNode.Local.Name)
				{
					node.Referent = withNode.Local;
					node.SetValue(withNode.Local.Value);
					withNode.Local.UseCount++;
					return;
				}
			}
			ParamNode paramNode;
			if (this._lambda != null && (paramNode = this._lambda.FindParam(node.Value)) != null)
			{
				node.Referent = paramNode;
				node.SetType(paramNode.ExprType);
				return;
			}
			this.PostError(node, "Unresolved identifier '{0}'", new object[] { node.Value });
			node.SetType(ExprType.Error);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002A8B3 File Offset: 0x00028AB3
		public override void Visit(ParamNode node)
		{
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002A8B5 File Offset: 0x00028AB5
		public override bool PreVisit(LambdaNode node)
		{
			this._lambda = node;
			node.Expr.Accept(this);
			this._lambda = null;
			return false;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002A8D2 File Offset: 0x00028AD2
		public override void PostVisit(LambdaNode node)
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002A8D4 File Offset: 0x00028AD4
		private string GetStr(ExprTypeKind kind)
		{
			switch (kind)
			{
			case ExprTypeKind.BL:
				return "boolean";
			case ExprTypeKind.I4:
			case ExprTypeKind.I8:
				return "integer";
			case ExprTypeKind.R4:
			case ExprTypeKind.R8:
				return "numeric";
			case ExprTypeKind.TX:
				return "text";
			default:
				return null;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002A91E File Offset: 0x00028B1E
		private void BadNum(ExprNode arg)
		{
			if (!arg.IsError)
			{
				this.PostError(arg, "Invalid numeric operand");
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002A934 File Offset: 0x00028B34
		private void BadNum(ExprNode node, ExprNode arg)
		{
			this.BadNum(arg);
			node.SetType(ExprType.Error);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002A948 File Offset: 0x00028B48
		private void BadText(ExprNode arg)
		{
			if (!arg.IsError)
			{
				this.PostError(arg, "Invalid text operand");
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002A960 File Offset: 0x00028B60
		private void BadArg(ExprNode arg, ExprTypeKind kind)
		{
			if (!arg.IsError)
			{
				string str = this.GetStr(kind);
				if (str != null)
				{
					this.PostError(arg, "Invalid {0} operand", new object[] { str });
					return;
				}
				this.PostError(arg, "Invalid operand");
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		public override void PostVisit(UnaryOpNode node)
		{
			ExprNode arg = node.Arg;
			switch (node.Op)
			{
			case UnaryOp.Not:
			{
				DvBool? boolOp = this.GetBoolOp(node.Arg);
				if (boolOp != null)
				{
					node.SetValue(!boolOp.Value);
					return;
				}
				node.SetValue(boolOp);
				return;
			}
			case UnaryOp.Minus:
				switch (arg.ExprType.Kind)
				{
				case ExprTypeKind.I4:
					node.SetValue(-(DvInt4?)arg.ExprValue);
					return;
				case ExprTypeKind.I8:
					node.SetValue(-(DvInt8?)arg.ExprValue);
					return;
				case ExprTypeKind.R4:
				{
					float? num = (float?)arg.ExprValue;
					node.SetValue((num != null) ? new float?(-num.GetValueOrDefault()) : null);
					return;
				}
				case ExprTypeKind.R8:
				{
					double? num2 = (double?)arg.ExprValue;
					node.SetValue((num2 != null) ? new double?(-num2.GetValueOrDefault()) : null);
					return;
				}
				default:
					this.BadNum(node, arg);
					return;
				}
				break;
			default:
				this.PostError(node, "Unknown unary operator");
				node.SetType(ExprType.Error);
				return;
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002AB30 File Offset: 0x00028D30
		private DvBool? GetBoolOp(ExprNode arg)
		{
			if (arg.IsBool)
			{
				return (DvBool?)arg.ExprValue;
			}
			this.BadArg(arg, ExprTypeKind.BL);
			return null;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002AB64 File Offset: 0x00028D64
		private DvText? GetTextOp(ExprNode arg)
		{
			if (arg.IsTX)
			{
				return (DvText?)arg.ExprValue;
			}
			this.BadArg(arg, ExprTypeKind.TX);
			return null;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002AB98 File Offset: 0x00028D98
		public override void PostVisit(BinaryOpNode node)
		{
			switch (node.Op)
			{
			case BinaryOp.Coalesce:
				if (node.Left.IsNumber)
				{
					this.ApplyNumericBinOp(node);
					return;
				}
				if (node.Left.IsBool)
				{
					this.ApplyBoolBinOp(node);
					return;
				}
				if (node.Left.IsTX)
				{
					this.ApplyTextBinOp(node);
					return;
				}
				if (node.Right.IsNumber)
				{
					this.ApplyNumericBinOp(node);
					return;
				}
				if (node.Right.IsBool)
				{
					this.ApplyBoolBinOp(node);
					return;
				}
				if (node.Right.IsTX)
				{
					this.ApplyTextBinOp(node);
					return;
				}
				this.ApplyNumericBinOp(node);
				return;
			case BinaryOp.Or:
			case BinaryOp.And:
				this.ApplyBoolBinOp(node);
				return;
			case BinaryOp.Add:
			case BinaryOp.Sub:
			case BinaryOp.Mul:
			case BinaryOp.Div:
			case BinaryOp.Mod:
			case BinaryOp.Power:
				this.ApplyNumericBinOp(node);
				return;
			case BinaryOp.Error:
				node.SetType(ExprType.Error);
				return;
			default:
				this.PostError(node, "Unknown binary operator");
				node.SetType(ExprType.Error);
				return;
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002AC98 File Offset: 0x00028E98
		private void ApplyBoolBinOp(BinaryOpNode node)
		{
			node.SetType(ExprType.BL);
			DvBool? boolOp = this.GetBoolOp(node.Left);
			DvBool? boolOp2 = this.GetBoolOp(node.Right);
			switch (node.Op)
			{
			case BinaryOp.Coalesce:
				if (boolOp != null)
				{
					if (!boolOp.Value.IsNA)
					{
						node.SetValue(boolOp);
						return;
					}
					if (boolOp2 != null)
					{
						node.SetValue(boolOp2);
						return;
					}
					node.ReduceToRight = true;
					return;
				}
				else if (boolOp2 != null && boolOp2.Value.IsNA)
				{
					node.ReduceToLeft = true;
				}
				break;
			case BinaryOp.Or:
				if (boolOp != null && boolOp2 != null)
				{
					node.SetValue(boolOp.Value | boolOp2.Value);
					return;
				}
				if ((boolOp != null && boolOp.Value.IsTrue) || (boolOp2 != null && boolOp2.Value.IsTrue))
				{
					node.SetValue(DvBool.True);
					return;
				}
				if (boolOp != null && boolOp.Value.IsFalse)
				{
					node.ReduceToRight = true;
					return;
				}
				if (boolOp2 != null && boolOp2.Value.IsFalse)
				{
					node.ReduceToLeft = true;
					return;
				}
				break;
			case BinaryOp.And:
				if (boolOp != null && boolOp2 != null)
				{
					node.SetValue(boolOp.Value & boolOp2.Value);
					return;
				}
				if ((boolOp != null && boolOp.Value.IsFalse) || (boolOp2 != null && boolOp2.Value.IsFalse))
				{
					node.SetValue(DvBool.False);
					return;
				}
				if (boolOp != null && boolOp.Value.IsTrue)
				{
					node.ReduceToRight = true;
					return;
				}
				if (boolOp2 != null && boolOp2.Value.IsTrue)
				{
					node.ReduceToLeft = true;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002AEB0 File Offset: 0x000290B0
		private void ApplyTextBinOp(BinaryOpNode node)
		{
			node.SetType(ExprType.TX);
			DvText? textOp = this.GetTextOp(node.Left);
			DvText? textOp2 = this.GetTextOp(node.Right);
			BinaryOp op = node.Op;
			if (op != BinaryOp.Coalesce)
			{
				return;
			}
			if (textOp == null)
			{
				if (textOp2 != null && textOp2.Value.IsNA)
				{
					node.ReduceToLeft = true;
				}
				return;
			}
			if (!textOp.Value.IsNA)
			{
				node.SetValue(textOp);
				return;
			}
			if (textOp2 != null)
			{
				node.SetValue(textOp2);
				return;
			}
			node.ReduceToRight = true;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002AF4C File Offset: 0x0002914C
		private void ReconcileNumericTypes(ExprNode a, ExprNode b, out ExprTypeKind kind)
		{
			if (!LambdaBinder.CanPromote(false, a.ExprType.Kind, b.ExprType.Kind, out kind))
			{
				if (a.IsNumber)
				{
					kind = a.ExprType.Kind;
					return;
				}
				if (b.IsNumber)
				{
					kind = b.ExprType.Kind;
					return;
				}
				kind = ExprTypeKind.R4;
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002AFA8 File Offset: 0x000291A8
		private void ApplyNumericBinOp(BinaryOpNode node)
		{
			ExprNode left = node.Left;
			ExprNode right = node.Right;
			ExprTypeKind exprTypeKind;
			this.ReconcileNumericTypes(left, right, out exprTypeKind);
			switch (exprTypeKind)
			{
			case ExprTypeKind.I4:
			{
				node.SetType(ExprType.I4);
				DvInt4? dvInt;
				bool flag = left.TryGet(out dvInt);
				DvInt4? dvInt2;
				bool flag2 = right.TryGet(out dvInt2);
				if (!flag)
				{
					this.BadNum(left);
					return;
				}
				if (!flag2)
				{
					this.BadNum(right);
					return;
				}
				this.ReduceBinOp(node, dvInt, dvInt2);
				return;
			}
			case ExprTypeKind.I8:
			{
				node.SetType(ExprType.I8);
				DvInt8? dvInt3;
				bool flag3 = left.TryGet(out dvInt3);
				DvInt8? dvInt4;
				bool flag4 = right.TryGet(out dvInt4);
				if (!flag3)
				{
					this.BadNum(left);
					return;
				}
				if (!flag4)
				{
					this.BadNum(right);
					return;
				}
				this.ReduceBinOp(node, dvInt3, dvInt4);
				return;
			}
			case ExprTypeKind.R8:
			{
				node.SetType(ExprType.R8);
				double? num;
				bool flag5 = left.TryGet(out num);
				double? num2;
				bool flag6 = right.TryGet(out num2);
				if (!flag5)
				{
					this.BadNum(left);
					return;
				}
				if (!flag6)
				{
					this.BadNum(right);
					return;
				}
				this.ReduceBinOp(node, num, num2);
				return;
			}
			}
			node.SetType(ExprType.R4);
			float? num3;
			bool flag7 = left.TryGet(out num3);
			float? num4;
			bool flag8 = right.TryGet(out num4);
			if (!flag7)
			{
				this.BadNum(left);
				return;
			}
			if (!flag8)
			{
				this.BadNum(right);
				return;
			}
			this.ReduceBinOp(node, num3, num4);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002B0F8 File Offset: 0x000292F8
		private void ReduceBinOp(BinaryOpNode node, DvInt4? a, DvInt4? b)
		{
			if (a != null && b != null)
			{
				node.SetValue(this.BinOp(node, a.Value, b.Value));
				return;
			}
			if (a != null)
			{
				DvInt4 value = a.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (!value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					node.ReduceToRight = true;
					return;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					if (value.RawValue == 0)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					break;
				case BinaryOp.Mul:
					if (value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					if (value.RawValue == 1)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				default:
					return;
				}
			}
			else if (b != null)
			{
				DvInt4 value2 = b.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (value2.IsNA)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (value2.IsNA)
					{
						node.SetValue(value2);
						return;
					}
					if (value2.RawValue == 0)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (value2.IsNA)
					{
						node.SetValue(value2);
					}
					break;
				case BinaryOp.Mul:
					if (value2.IsNA)
					{
						node.SetValue(value2);
						return;
					}
					if (value2.RawValue == 1)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002B298 File Offset: 0x00029498
		private void ReduceBinOp(BinaryOpNode node, DvInt8? a, DvInt8? b)
		{
			if (a != null && b != null)
			{
				node.SetValue(this.BinOp(node, a.Value, b.Value));
				return;
			}
			if (a != null)
			{
				DvInt8 value = a.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (!value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					node.ReduceToRight = true;
					return;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					if (value.RawValue == 0L)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					break;
				case BinaryOp.Mul:
					if (value.IsNA)
					{
						node.SetValue(value);
						return;
					}
					if (value.RawValue == 1L)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				default:
					return;
				}
			}
			else if (b != null)
			{
				DvInt8 value2 = b.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (value2.IsNA)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (value2.IsNA)
					{
						node.SetValue(value2);
						return;
					}
					if (value2.RawValue == 0L)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (value2.IsNA)
					{
						node.SetValue(value2);
					}
					break;
				case BinaryOp.Mul:
					if (value2.IsNA)
					{
						node.SetValue(value2);
						return;
					}
					if (value2.RawValue == 1L)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002B43C File Offset: 0x0002963C
		private void ReduceBinOp(BinaryOpNode node, float? a, float? b)
		{
			if (a != null && b != null)
			{
				node.SetValue(this.BinOp(node, a.Value, b.Value));
				return;
			}
			if (a != null)
			{
				float value = a.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (!TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					node.ReduceToRight = true;
					return;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					if (value == 0f)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					break;
				case BinaryOp.Mul:
					if (TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					if (value == 1f)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				default:
					return;
				}
			}
			else if (b != null)
			{
				float value2 = b.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (TypeUtils.IsNA(value2))
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (TypeUtils.IsNA(value2))
					{
						node.SetValue(value2);
						return;
					}
					if (value2 == 0f)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (TypeUtils.IsNA(value2))
					{
						node.SetValue(value2);
					}
					break;
				case BinaryOp.Mul:
					if (TypeUtils.IsNA(value2))
					{
						node.SetValue(value2);
						return;
					}
					if (value2 == 1f)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002B5CC File Offset: 0x000297CC
		private void ReduceBinOp(BinaryOpNode node, double? a, double? b)
		{
			if (a != null && b != null)
			{
				node.SetValue(this.BinOp(node, a.Value, b.Value));
				return;
			}
			if (a != null)
			{
				double value = a.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (!TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					node.ReduceToRight = true;
					return;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					if (value == 0.0)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					break;
				case BinaryOp.Mul:
					if (TypeUtils.IsNA(value))
					{
						node.SetValue(value);
						return;
					}
					if (value == 1.0)
					{
						node.ReduceToRight = true;
						return;
					}
					break;
				default:
					return;
				}
			}
			else if (b != null)
			{
				double value2 = b.Value;
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					if (TypeUtils.IsNA(value2))
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Or:
				case BinaryOp.And:
					break;
				case BinaryOp.Add:
					if (TypeUtils.IsNA(value2))
					{
						node.SetValue(value2);
						return;
					}
					if (value2 == 0.0)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				case BinaryOp.Sub:
				case BinaryOp.Div:
				case BinaryOp.Mod:
					if (TypeUtils.IsNA(value2))
					{
						node.SetValue(value2);
					}
					break;
				case BinaryOp.Mul:
					if (TypeUtils.IsNA(value2))
					{
						node.SetValue(value2);
						return;
					}
					if (value2 == 1.0)
					{
						node.ReduceToLeft = true;
						return;
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002B76C File Offset: 0x0002996C
		private DvInt4 BinOp(BinaryOpNode node, DvInt4 v1, DvInt4 v2)
		{
			switch (node.Op)
			{
			case BinaryOp.Coalesce:
				if (v1.IsNA)
				{
					return v2;
				}
				return v1;
			case BinaryOp.Add:
				return v1 + v2;
			case BinaryOp.Sub:
				return v1 - v2;
			case BinaryOp.Mul:
				return v1 * v2;
			case BinaryOp.Div:
				return v1 / v2;
			case BinaryOp.Mod:
				return v1 % v2;
			case BinaryOp.Power:
				return DvInt4.Pow(v1, v2);
			}
			return DvInt4.NA;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002B7F0 File Offset: 0x000299F0
		private DvInt8 BinOp(BinaryOpNode node, DvInt8 v1, DvInt8 v2)
		{
			switch (node.Op)
			{
			case BinaryOp.Coalesce:
				if (v1.IsNA)
				{
					return v2;
				}
				return v1;
			case BinaryOp.Add:
				return v1 + v2;
			case BinaryOp.Sub:
				return v1 - v2;
			case BinaryOp.Mul:
				return v1 * v2;
			case BinaryOp.Div:
				return v1 / v2;
			case BinaryOp.Mod:
				return v1 % v2;
			case BinaryOp.Power:
				return DvInt8.Pow(v1, v2);
			}
			return DvInt8.NA;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002B874 File Offset: 0x00029A74
		private float BinOp(BinaryOpNode node, float v1, float v2)
		{
			switch (node.Op)
			{
			case BinaryOp.Coalesce:
				if (TypeUtils.IsNA(v1))
				{
					return v2;
				}
				return v1;
			case BinaryOp.Add:
				return v1 + v2;
			case BinaryOp.Sub:
				return v1 - v2;
			case BinaryOp.Mul:
				return v1 * v2;
			case BinaryOp.Div:
				return v1 / v2;
			case BinaryOp.Mod:
				return v1 % v2;
			case BinaryOp.Power:
				return BuiltinFunctions.Pow(v1, v2);
			}
			return float.NaN;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		private double BinOp(BinaryOpNode node, double v1, double v2)
		{
			switch (node.Op)
			{
			case BinaryOp.Coalesce:
				if (TypeUtils.IsNA(v1))
				{
					return v2;
				}
				return v1;
			case BinaryOp.Add:
				return v1 + v2;
			case BinaryOp.Sub:
				return v1 - v2;
			case BinaryOp.Mul:
				return v1 * v2;
			case BinaryOp.Div:
				return v1 / v2;
			case BinaryOp.Mod:
				return v1 % v2;
			case BinaryOp.Power:
				return Math.Pow(v1, v2);
			}
			return double.NaN;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002B958 File Offset: 0x00029B58
		public override void PostVisit(ConditionalNode node)
		{
			DvBool? boolOp = this.GetBoolOp(node.Cond);
			ExprNode left = node.Left;
			ExprNode right = node.Right;
			ExprTypeKind exprTypeKind;
			if (!LambdaBinder.CanPromote(false, left.ExprType.Kind, right.ExprType.Kind, out exprTypeKind))
			{
				if (left.IsNumber)
				{
					exprTypeKind = left.ExprType.Kind;
				}
				else if (right.IsNumber)
				{
					exprTypeKind = right.ExprType.Kind;
				}
				else if (!left.IsError && !left.IsNone)
				{
					exprTypeKind = left.ExprType.Kind;
				}
				else if (!right.IsError && !right.IsNone)
				{
					exprTypeKind = right.ExprType.Kind;
				}
				else
				{
					exprTypeKind = ExprTypeKind.None;
				}
			}
			switch (exprTypeKind)
			{
			case ExprTypeKind.BL:
			{
				node.SetType(ExprType.BL);
				DvBool? boolOp2 = this.GetBoolOp(node.Left);
				DvBool? boolOp3 = this.GetBoolOp(node.Right);
				if (boolOp != null)
				{
					if (boolOp.Value.IsTrue)
					{
						node.SetValue(boolOp2);
						return;
					}
					if (boolOp.Value.IsFalse)
					{
						node.SetValue(boolOp3);
						return;
					}
					node.SetValue(DvBool.NA);
					return;
				}
				break;
			}
			case ExprTypeKind.I4:
			{
				node.SetType(ExprType.I4);
				DvInt4? dvInt;
				bool flag = left.TryGet(out dvInt);
				DvInt4? dvInt2;
				bool flag2 = right.TryGet(out dvInt2);
				if (!flag)
				{
					this.BadNum(left);
				}
				if (!flag2)
				{
					this.BadNum(right);
				}
				if (boolOp != null)
				{
					if (boolOp.Value.IsTrue)
					{
						node.SetValue(dvInt);
						return;
					}
					if (boolOp.Value.IsFalse)
					{
						node.SetValue(dvInt2);
						return;
					}
					node.SetValue(DvInt4.NA);
					return;
				}
				break;
			}
			case ExprTypeKind.I8:
			{
				node.SetType(ExprType.I8);
				DvInt8? dvInt3;
				bool flag3 = left.TryGet(out dvInt3);
				DvInt8? dvInt4;
				bool flag4 = right.TryGet(out dvInt4);
				if (!flag3)
				{
					this.BadNum(left);
				}
				if (!flag4)
				{
					this.BadNum(right);
				}
				if (boolOp != null)
				{
					if (boolOp.Value.IsTrue)
					{
						node.SetValue(dvInt3);
						return;
					}
					if (boolOp.Value.IsFalse)
					{
						node.SetValue(dvInt4);
						return;
					}
					node.SetValue(DvInt8.NA);
					return;
				}
				break;
			}
			case ExprTypeKind.R4:
			{
				node.SetType(ExprType.R4);
				float? num;
				bool flag5 = left.TryGet(out num);
				float? num2;
				bool flag6 = right.TryGet(out num2);
				if (!flag5)
				{
					this.BadNum(left);
				}
				if (!flag6)
				{
					this.BadNum(right);
				}
				if (boolOp != null)
				{
					if (boolOp.Value.IsTrue)
					{
						node.SetValue(num);
						return;
					}
					if (boolOp.Value.IsFalse)
					{
						node.SetValue(num2);
						return;
					}
					node.SetValue(float.NaN);
					return;
				}
				break;
			}
			case ExprTypeKind.R8:
			{
				node.SetType(ExprType.R8);
				double? num3;
				bool flag7 = left.TryGet(out num3);
				double? num4;
				bool flag8 = right.TryGet(out num4);
				if (!flag7)
				{
					this.BadNum(left);
				}
				if (!flag8)
				{
					this.BadNum(right);
				}
				if (boolOp != null)
				{
					if (boolOp.Value.IsTrue)
					{
						node.SetValue(num3);
						return;
					}
					if (boolOp.Value.IsFalse)
					{
						node.SetValue(num4);
						return;
					}
					node.SetValue(double.NaN);
					return;
				}
				break;
			}
			case ExprTypeKind.TX:
			{
				node.SetType(ExprType.TX);
				DvText? dvText;
				bool flag9 = left.TryGet(out dvText);
				DvText? dvText2;
				bool flag10 = right.TryGet(out dvText2);
				if (!flag9)
				{
					this.BadText(left);
				}
				if (!flag10)
				{
					this.BadText(right);
				}
				if (boolOp != null)
				{
					if (boolOp.Value.IsTrue)
					{
						node.SetValue(dvText);
						return;
					}
					if (boolOp.Value.IsFalse)
					{
						node.SetValue(dvText2);
						return;
					}
					node.SetValue(DvText.NA);
				}
				break;
			}
			default:
				this.PostError(node, "Invalid conditional expression");
				node.SetType(ExprType.Error);
				return;
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002BD4C File Offset: 0x00029F4C
		public override void PostVisit(CompareNode node)
		{
			TokKind tidLax = node.TidLax;
			TokKind tidStrict = node.TidStrict;
			ExprTypeKind exprTypeKind = ExprTypeKind.None;
			bool flag = false;
			Node[] items = node.Operands.Items;
			for (int i = 0; i < items.Length; i++)
			{
				ExprNode exprNode = items[i].AsExpr;
				if (!this.ValidateType(exprNode, ref exprTypeKind))
				{
					this.BadArg(exprNode, exprTypeKind);
					flag = true;
				}
			}
			node.ArgTypeKind = exprTypeKind;
			node.SetType(ExprType.BL);
			if (flag)
			{
				return;
			}
			int num = items.Length;
			int num2 = num;
			for (int j = 0; j < num2; j++)
			{
				ExprNode exprNode = items[j].AsExpr;
				exprNode.Convert(exprTypeKind);
				if (j < num && exprNode.ExprValue == null)
				{
					num = j;
				}
			}
			int num3 = (int)exprTypeKind;
			if (num3 >= LambdaBinder._fnEqual.Length || num3 < 0)
			{
				this.PostError(node, "Internal error in CompareNode");
				return;
			}
			LambdaBinder.Cmp cmp;
			LambdaBinder.Cmp cmp2;
			switch (node.Op)
			{
			case CompareOp.Equal:
				cmp = LambdaBinder._fnEqual[num3];
				cmp2 = cmp;
				break;
			case CompareOp.NotEqual:
				cmp = LambdaBinder._fnNotEqual[num3];
				cmp2 = cmp;
				break;
			case CompareOp.IncrChain:
				cmp = LambdaBinder._fnLessEqual[num3];
				cmp2 = LambdaBinder._fnLess[num3];
				break;
			case CompareOp.DecrChain:
				cmp = LambdaBinder._fnGreaterEqual[num3];
				cmp2 = LambdaBinder._fnGreater[num3];
				break;
			default:
				return;
			}
			if (cmp == null)
			{
				this.PostError(node, "Bad operands for comparison");
				return;
			}
			object obj;
			if (num < 2 && (obj = items[1 - num].AsExpr.ExprValue) != null && cmp(obj, obj).IsNA)
			{
				node.SetValue(DvBool.NA);
				return;
			}
			if (num >= 2)
			{
				if (node.Op != CompareOp.NotEqual)
				{
					bool flag2 = false;
					ExprNode exprNode = items[0].AsExpr;
					object obj2 = exprNode.ExprValue;
					for (int k = 1; k < num; k++)
					{
						TokKind kind = node.Operands.Delimiters[k - 1].Kind;
						if (kind == tidStrict)
						{
							flag2 = true;
						}
						exprNode = items[k].AsExpr;
						obj = exprNode.ExprValue;
						DvBool dvBool = (flag2 ? cmp2(obj2, obj) : cmp(obj2, obj));
						if (!dvBool.IsTrue)
						{
							node.SetValue(dvBool);
							return;
						}
						obj2 = obj;
						flag2 = false;
					}
				}
				else
				{
					for (int l = 1; l < num; l++)
					{
						ExprNode exprNode = items[l].AsExpr;
						obj = exprNode.ExprValue;
						for (int m = 0; m < l; m++)
						{
							ExprNode asExpr = items[m].AsExpr;
							object exprValue = asExpr.ExprValue;
							DvBool dvBool2 = cmp2(exprValue, obj);
							if (!dvBool2.IsTrue)
							{
								node.SetValue(dvBool2);
								return;
							}
						}
					}
				}
				if (num == num2)
				{
					node.SetValue(DvBool.True);
				}
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002C01C File Offset: 0x0002A21C
		public override void PostVisit(CallNode node)
		{
			ExprTypeKind[] array = node.Args.Items.Select((Node item) => item.AsExpr.ExprType.Kind).ToArray<ExprTypeKind>();
			int num = array.Length;
			bool flag = false;
			List<LambdaBinder.Candidate> list = new List<LambdaBinder.Candidate>();
			foreach (IFunctionProvider functionProvider in this._providers)
			{
				if (node.NameSpace == null || !(functionProvider.NameSpace != node.NameSpace.Value))
				{
					MethodInfo[] array2 = functionProvider.Lookup(node.Head.Value);
					if (Utils.Size<MethodInfo>(array2) != 0)
					{
						foreach (MethodInfo methodInfo in array2)
						{
							LambdaBinder.Candidate candidate;
							if (LambdaBinder.Candidate.TryGetCandidate(node, functionProvider, methodInfo, this._printError, out candidate))
							{
								bool flag2 = candidate.MatchesArity(num);
								if (flag)
								{
									if (!flag2)
									{
										goto IL_00DF;
									}
								}
								else if (flag2)
								{
									list.Clear();
									flag = true;
								}
								list.Add(candidate);
							}
							IL_00DF:;
						}
					}
				}
			}
			if (list.Count == 0)
			{
				this.PostError(node.Head, "Unknown function");
				node.SetType(ExprType.Error);
				return;
			}
			if (flag)
			{
				int num2 = 0;
				int num3 = int.MaxValue;
				int num4 = -1;
				for (int k = 0; k < list.Count; k++)
				{
					LambdaBinder.Candidate candidate2 = list[k];
					int num5;
					if (candidate2.IsApplicable(array, out num5))
					{
						list[num2++] = candidate2;
					}
					else if (num5 < num3)
					{
						num3 = num5;
						num4 = k;
					}
				}
				if (0 < num2 && num2 < list.Count)
				{
					list.RemoveRange(num2, list.Count - num2);
				}
				LambdaBinder.Candidate candidate3;
				if (num2 > 1)
				{
					candidate3 = this.GetBestOverload(node, list);
				}
				else if (num2 == 1)
				{
					candidate3 = list[0];
				}
				else
				{
					candidate3 = list[num4];
					this.PostError(node, "The best overload of '{0}' has some invalid arguments", new object[] { node.Head.Value });
				}
				object[] array4 = new object[node.Args.Items.Length];
				bool flag3 = true;
				int num6 = candidate3.Kinds.Length - 1;
				for (int l = 0; l < node.Args.Items.Length; l++)
				{
					array4[l] = this.Convert(node.Args.Items[l].AsExpr, candidate3.Kinds[Math.Min(l, num6)]);
					flag3 &= array4[l] != null;
				}
				object obj;
				if (candidate3.IsIdentity)
				{
					obj = array4[0];
				}
				else if (!flag3)
				{
					obj = candidate3.Provider.ResolveToConstant(node.Head.Value, candidate3.Method, array4);
					if (obj != null && obj.GetType() != candidate3.Method.ReturnType)
					{
						this._printError(string.Format("Error in ExprTransform: Function '{0}' in namespace '{1}' produced wrong constant value type '{2}' vs '{3}'", new object[]
						{
							node.Head.Value,
							candidate3.Provider.NameSpace,
							obj.GetType(),
							candidate3.Method.ReturnType
						}));
						obj = null;
					}
				}
				else
				{
					if (candidate3.IsVariable)
					{
						int num7 = candidate3.Kinds.Length - 1;
						int num8 = array4.Length - num7;
						Type type = candidate3.Method.GetParameters()[num6].ParameterType;
						type = type.GetElementType();
						Array array5 = Array.CreateInstance(type, num8);
						for (int m = 0; m < num8; m++)
						{
							array5.SetValue(array4[num7 + m], m);
						}
						Array.Resize<object>(ref array4, num7 + 1);
						array4[num7] = array5;
					}
					obj = candidate3.Method.Invoke(null, array4);
				}
				node.SetType(new ExprType(candidate3.ReturnKind), obj);
				node.SetMethod(candidate3.Method);
				return;
			}
			int[] array6 = (from x in list.Select((LambdaBinder.Candidate c) => c.Arity).Distinct<int>()
				orderby x
				select x).ToArray<int>();
			if (array6.Length == 1)
			{
				if (array6[0] == 1)
				{
					this.PostError(node, "Expected one argument to function '{1}'", new object[]
					{
						array6[0],
						node.Head.Value
					});
				}
				else
				{
					this.PostError(node, "Expected {0} arguments to function '{1}'", new object[]
					{
						array6[0],
						node.Head.Value
					});
				}
			}
			else if (array6.Length == 2)
			{
				this.PostError(node, "Expected {0} or {1} arguments to function '{2}'", new object[]
				{
					array6[0],
					array6[1],
					node.Head.Value
				});
			}
			else
			{
				this.PostError(node, "No overload of function '{0}' takes {1} arguments", new object[]
				{
					node.Head.Value,
					num
				});
			}
			ExprTypeKind[] array7 = list.Select((LambdaBinder.Candidate c) => c.ReturnKind).Distinct<ExprTypeKind>().ToArray<ExprTypeKind>();
			if (array7.Length == 1)
			{
				node.SetType(new ExprType(array7[0]));
				return;
			}
			node.SetType(ExprType.Error);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002C59C File Offset: 0x0002A79C
		private static bool CanConvert(ExprTypeKind src, ExprTypeKind dst)
		{
			if (src == ExprTypeKind.Error)
			{
				return true;
			}
			if (src == dst)
			{
				return true;
			}
			if (src == ExprTypeKind.I4)
			{
				return dst == ExprTypeKind.I8 || dst == ExprTypeKind.R4 || dst == ExprTypeKind.R8;
			}
			if (src == ExprTypeKind.I8)
			{
				return dst == ExprTypeKind.R8;
			}
			return src == ExprTypeKind.R4 && dst == ExprTypeKind.R8;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002C5D0 File Offset: 0x0002A7D0
		private object Convert(ExprNode expr, ExprTypeKind kind)
		{
			switch (kind)
			{
			case ExprTypeKind.BL:
			{
				DvBool? dvBool;
				if (!expr.TryGet(out dvBool))
				{
					this.BadArg(expr, ExprTypeKind.BL);
				}
				return dvBool;
			}
			case ExprTypeKind.I4:
			{
				DvInt4? dvInt;
				if (!expr.TryGet(out dvInt))
				{
					this.BadArg(expr, ExprTypeKind.I4);
				}
				return dvInt;
			}
			case ExprTypeKind.I8:
			{
				DvInt8? dvInt2;
				if (!expr.TryGet(out dvInt2))
				{
					this.BadArg(expr, ExprTypeKind.I8);
				}
				return dvInt2;
			}
			case ExprTypeKind.R4:
			{
				float? num;
				if (!expr.TryGet(out num))
				{
					this.BadArg(expr, ExprTypeKind.R4);
				}
				return num;
			}
			case ExprTypeKind.R8:
			{
				double? num2;
				if (!expr.TryGet(out num2))
				{
					this.BadArg(expr, ExprTypeKind.R8);
				}
				return num2;
			}
			case ExprTypeKind.TX:
			{
				DvText? dvText;
				if (!expr.TryGet(out dvText))
				{
					this.BadArg(expr, ExprTypeKind.TX);
				}
				return dvText;
			}
			default:
				this.PostError(expr, "Internal error in Convert");
				return null;
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002C6AC File Offset: 0x0002A8AC
		private LambdaBinder.Candidate GetBestOverload(CallNode node, List<LambdaBinder.Candidate> candidates)
		{
			LambdaBinder.Candidate candidate = null;
			LambdaBinder.Candidate candidate2 = null;
			int i = 0;
			while (i < candidates.Count)
			{
				LambdaBinder.Candidate candidate3 = candidates[i];
				int num = -1;
				int num2 = 0;
				for (;;)
				{
					if (num2 != i)
					{
						if (num2 >= candidates.Count)
						{
							goto Block_2;
						}
						int num3 = candidate3.CompareSignatures(candidates[num2]);
						if (num3 > 0)
						{
							break;
						}
						if (num3 == 0)
						{
							num = num2;
						}
					}
					num2++;
				}
				IL_0061:
				i++;
				continue;
				Block_2:
				if (num < 0)
				{
					return candidate3;
				}
				if (candidate == null)
				{
					candidate = candidate3;
					candidate2 = candidates[num];
					goto IL_0061;
				}
				goto IL_0061;
			}
			if (candidate != null)
			{
				if (candidate.Provider.NameSpace.CompareTo(candidate2.Provider.NameSpace) > 0)
				{
					Utils.Swap<LambdaBinder.Candidate>(ref candidate, ref candidate2);
				}
				this.PostError(node, "Duplicate candidate functions in namespaces '{0}' and '{1}'", new object[]
				{
					candidate.Provider.NameSpace,
					candidate2.Provider.NameSpace
				});
			}
			else
			{
				this.PostError(node, "Ambiguous invocation of function '{0}'", new object[] { node.Head.Value });
			}
			return candidate ?? candidates[0];
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002C7B8 File Offset: 0x0002A9B8
		public override void PostVisit(ListNode node)
		{
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002C7BC File Offset: 0x0002A9BC
		public override bool PreVisit(WithNode node)
		{
			node.Local.Accept(this);
			int count = this._rgwith.Count;
			this._rgwith.Add(node);
			node.Body.Accept(this);
			this._rgwith.RemoveAt(count);
			node.SetValue(node.Body);
			return false;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002C812 File Offset: 0x0002AA12
		public override void PostVisit(WithNode node)
		{
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002C814 File Offset: 0x0002AA14
		public override void PostVisit(WithLocalNode node)
		{
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002C818 File Offset: 0x0002AA18
		private bool ValidateType(ExprNode expr, ref ExprTypeKind itemKind)
		{
			ExprTypeKind kind = expr.ExprType.Kind;
			switch (kind)
			{
			case ExprTypeKind.None:
				return false;
			case ExprTypeKind.Error:
				return false;
			default:
				if (kind == itemKind)
				{
					return true;
				}
				switch (itemKind)
				{
				case ExprTypeKind.None:
					itemKind = kind;
					return true;
				case ExprTypeKind.Error:
					itemKind = kind;
					return true;
				default:
				{
					ExprTypeKind exprTypeKind;
					if (!LambdaBinder.CanPromote(true, kind, itemKind, out exprTypeKind))
					{
						return false;
					}
					itemKind = exprTypeKind;
					return true;
				}
				}
				break;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002C880 File Offset: 0x0002AA80
		internal static bool CanPromote(bool precise, ExprTypeKind k1, ExprTypeKind k2, out ExprTypeKind res)
		{
			if (k1 == k2)
			{
				res = k1;
				if (res != ExprTypeKind.Error && res != ExprTypeKind.None)
				{
					return true;
				}
				res = ExprTypeKind.Error;
				return false;
			}
			else
			{
				int num = LambdaBinder.MapKindToIndex(k1);
				int num2 = LambdaBinder.MapKindToIndex(k2);
				if (num < 0 || num2 < 0)
				{
					res = ExprTypeKind.Error;
					return false;
				}
				switch (num | (num2 << 2))
				{
				case 1:
				case 4:
					res = ExprTypeKind.I8;
					return true;
				case 2:
				case 8:
					res = (precise ? ExprTypeKind.R8 : ExprTypeKind.R4);
					return true;
				case 3:
				case 6:
				case 7:
				case 9:
				case 11:
				case 12:
				case 13:
				case 14:
					res = ExprTypeKind.R8;
					return true;
				}
				res = ExprTypeKind.Error;
				return false;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002C924 File Offset: 0x0002AB24
		private static int MapKindToIndex(ExprTypeKind kind)
		{
			switch (kind)
			{
			case ExprTypeKind.I4:
				return 0;
			case ExprTypeKind.I8:
				return 1;
			case ExprTypeKind.R4:
				return 2;
			case ExprTypeKind.R8:
				return 3;
			default:
				return -1;
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002C956 File Offset: 0x0002AB56
		private static T Cast<T>(object a)
		{
			return (T)((object)a);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002CB74 File Offset: 0x0002AD74
		// Note: this type is marked as 'beforefieldinit'.
		static LambdaBinder()
		{
			LambdaBinder.Cmp[] array = new LambdaBinder.Cmp[8];
			array[2] = (object a, object b) => LambdaBinder.Cast<DvBool>(a) == LambdaBinder.Cast<DvBool>(b);
			array[3] = (object a, object b) => LambdaBinder.Cast<DvInt4>(a) == LambdaBinder.Cast<DvInt4>(b);
			array[4] = (object a, object b) => LambdaBinder.Cast<DvInt8>(a) == LambdaBinder.Cast<DvInt8>(b);
			array[5] = (object a, object b) => TypeUtils.Eq(LambdaBinder.Cast<float>(a), LambdaBinder.Cast<float>(b));
			array[6] = (object a, object b) => TypeUtils.Eq(LambdaBinder.Cast<double>(a), LambdaBinder.Cast<double>(b));
			array[7] = (object a, object b) => LambdaBinder.Cast<DvText>(a) == LambdaBinder.Cast<DvText>(b);
			LambdaBinder._fnEqual = array;
			LambdaBinder.Cmp[] array2 = new LambdaBinder.Cmp[8];
			array2[2] = (object a, object b) => LambdaBinder.Cast<DvBool>(a) != LambdaBinder.Cast<DvBool>(b);
			array2[3] = (object a, object b) => LambdaBinder.Cast<DvInt4>(a) != LambdaBinder.Cast<DvInt4>(b);
			array2[4] = (object a, object b) => LambdaBinder.Cast<DvInt8>(a) != LambdaBinder.Cast<DvInt8>(b);
			array2[5] = (object a, object b) => TypeUtils.Ne(LambdaBinder.Cast<float>(a), LambdaBinder.Cast<float>(b));
			array2[6] = (object a, object b) => TypeUtils.Ne(LambdaBinder.Cast<double>(a), LambdaBinder.Cast<double>(b));
			array2[7] = (object a, object b) => LambdaBinder.Cast<DvText>(a) != LambdaBinder.Cast<DvText>(b);
			LambdaBinder._fnNotEqual = array2;
			LambdaBinder.Cmp[] array3 = new LambdaBinder.Cmp[8];
			array3[3] = (object a, object b) => LambdaBinder.Cast<DvInt4>(a) < LambdaBinder.Cast<DvInt4>(b);
			array3[4] = (object a, object b) => LambdaBinder.Cast<DvInt8>(a) < LambdaBinder.Cast<DvInt8>(b);
			array3[5] = (object a, object b) => TypeUtils.Lt(LambdaBinder.Cast<float>(a), LambdaBinder.Cast<float>(b));
			array3[6] = (object a, object b) => TypeUtils.Lt(LambdaBinder.Cast<double>(a), LambdaBinder.Cast<double>(b));
			LambdaBinder._fnLess = array3;
			LambdaBinder.Cmp[] array4 = new LambdaBinder.Cmp[8];
			array4[3] = (object a, object b) => LambdaBinder.Cast<DvInt4>(a) <= LambdaBinder.Cast<DvInt4>(b);
			array4[4] = (object a, object b) => LambdaBinder.Cast<DvInt8>(a) <= LambdaBinder.Cast<DvInt8>(b);
			array4[5] = (object a, object b) => TypeUtils.Le(LambdaBinder.Cast<float>(a), LambdaBinder.Cast<float>(b));
			array4[6] = (object a, object b) => TypeUtils.Le(LambdaBinder.Cast<double>(a), LambdaBinder.Cast<double>(b));
			LambdaBinder._fnLessEqual = array4;
			LambdaBinder.Cmp[] array5 = new LambdaBinder.Cmp[8];
			array5[3] = (object a, object b) => LambdaBinder.Cast<DvInt4>(a) > LambdaBinder.Cast<DvInt4>(b);
			array5[4] = (object a, object b) => LambdaBinder.Cast<DvInt8>(a) > LambdaBinder.Cast<DvInt8>(b);
			array5[5] = (object a, object b) => TypeUtils.Gt(LambdaBinder.Cast<float>(a), LambdaBinder.Cast<float>(b));
			array5[6] = (object a, object b) => TypeUtils.Gt(LambdaBinder.Cast<double>(a), LambdaBinder.Cast<double>(b));
			LambdaBinder._fnGreater = array5;
			LambdaBinder.Cmp[] array6 = new LambdaBinder.Cmp[8];
			array6[3] = (object a, object b) => LambdaBinder.Cast<DvInt4>(a) >= LambdaBinder.Cast<DvInt4>(b);
			array6[4] = (object a, object b) => LambdaBinder.Cast<DvInt8>(a) >= LambdaBinder.Cast<DvInt8>(b);
			array6[5] = (object a, object b) => TypeUtils.Ge(LambdaBinder.Cast<float>(a), LambdaBinder.Cast<float>(b));
			array6[6] = (object a, object b) => TypeUtils.Ge(LambdaBinder.Cast<double>(a), LambdaBinder.Cast<double>(b));
			LambdaBinder._fnGreaterEqual = array6;
		}

		// Token: 0x04000423 RID: 1059
		private List<WithNode> _rgwith;

		// Token: 0x04000424 RID: 1060
		private List<Error> _errors;

		// Token: 0x04000425 RID: 1061
		private LambdaNode _lambda;

		// Token: 0x04000426 RID: 1062
		private readonly IFunctionProvider[] _providers;

		// Token: 0x04000427 RID: 1063
		private readonly Action<string> _printError;

		// Token: 0x04000428 RID: 1064
		private static readonly LambdaBinder.Cmp[] _fnEqual;

		// Token: 0x04000429 RID: 1065
		private static readonly LambdaBinder.Cmp[] _fnNotEqual;

		// Token: 0x0400042A RID: 1066
		private static readonly LambdaBinder.Cmp[] _fnLess;

		// Token: 0x0400042B RID: 1067
		private static readonly LambdaBinder.Cmp[] _fnLessEqual;

		// Token: 0x0400042C RID: 1068
		private static readonly LambdaBinder.Cmp[] _fnGreater;

		// Token: 0x0400042D RID: 1069
		private static readonly LambdaBinder.Cmp[] _fnGreaterEqual;

		// Token: 0x0200018D RID: 397
		private sealed class Candidate
		{
			// Token: 0x06000856 RID: 2134 RVA: 0x0002CF5B File Offset: 0x0002B15B
			public bool MatchesArity(int arity)
			{
				if (!this.IsVariable)
				{
					return arity == this.Kinds.Length;
				}
				return arity >= this.Kinds.Length - 1;
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000857 RID: 2135 RVA: 0x0002CF81 File Offset: 0x0002B181
			public int Arity
			{
				get
				{
					return this.Kinds.Length;
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002CF8B File Offset: 0x0002B18B
			public bool IsIdentity
			{
				get
				{
					return this.Method.ReturnType == typeof(void);
				}
			}

			// Token: 0x06000859 RID: 2137 RVA: 0x0002CFA8 File Offset: 0x0002B1A8
			public static bool TryGetCandidate(CallNode node, IFunctionProvider provider, MethodInfo meth, Action<string> printError, out LambdaBinder.Candidate cand)
			{
				cand = null;
				if (meth == null)
				{
					return false;
				}
				ParameterInfo[] parameters = meth.GetParameters();
				bool flag = parameters.Length == 1 && meth.ReturnType == typeof(void);
				if (!meth.IsStatic || (!meth.IsPublic && !flag))
				{
					printError(string.Format("Error in ExprTransform: Function '{0}' in namespace '{1}' must be static and public", node.Head.Value, provider.NameSpace));
					return false;
				}
				bool flag2 = false;
				ExprTypeKind[] array = new ExprTypeKind[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					Type type = parameters[i].ParameterType;
					if (i == parameters.Length - 1 && !flag && PlatformUtils.IsArrayEx(type))
					{
						flag2 = true;
						type = type.GetElementType();
					}
					ExprType exprType = ExprType.ToExprType(type);
					if (exprType.Kind <= ExprTypeKind.Error || exprType.Kind >= ExprTypeKind._Lim)
					{
						printError(string.Format("Error in ExprTransform: Function '{0}' in namespace '{1}' has invalid parameter type '{2}'", node.Head.Value, provider.NameSpace, type));
						return false;
					}
					array[i] = exprType.Kind;
				}
				ExprTypeKind exprTypeKind;
				if (flag)
				{
					exprTypeKind = array[0];
				}
				else
				{
					exprTypeKind = ExprType.ToExprType(meth.ReturnType).Kind;
					if (exprTypeKind <= ExprTypeKind.Error || exprTypeKind >= ExprTypeKind._Lim)
					{
						printError(string.Format("Error in ExprTransform: Function '{0}' in namespace '{1}' has invalid return type '{2}'", node.Head.Value, provider.NameSpace, meth.ReturnType));
						return false;
					}
				}
				cand = new LambdaBinder.Candidate(provider, meth, array, exprTypeKind, flag2);
				return true;
			}

			// Token: 0x0600085A RID: 2138 RVA: 0x0002D11A File Offset: 0x0002B31A
			private Candidate(IFunctionProvider provider, MethodInfo meth, ExprTypeKind[] kinds, ExprTypeKind kindRet, bool isVar)
			{
				this.Provider = provider;
				this.Method = meth;
				this.Kinds = kinds;
				this.ReturnKind = kindRet;
				this.IsVariable = isVar;
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x0002D148 File Offset: 0x0002B348
			public bool IsApplicable(ExprTypeKind[] kinds, out int bad)
			{
				bad = 0;
				int num = (this.IsVariable ? (this.Kinds.Length - 1) : this.Kinds.Length);
				for (int i = 0; i < num; i++)
				{
					if (!LambdaBinder.CanConvert(kinds[i], this.Kinds[i]))
					{
						bad++;
					}
				}
				if (this.IsVariable)
				{
					ExprTypeKind exprTypeKind = this.Kinds[this.Kinds.Length - 1];
					for (int j = num; j < kinds.Length; j++)
					{
						if (!LambdaBinder.CanConvert(kinds[j], exprTypeKind))
						{
							bad++;
						}
					}
				}
				return bad == 0;
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x0002D1D8 File Offset: 0x0002B3D8
			public int CompareSignatures(LambdaBinder.Candidate other)
			{
				if (this.IsVariable)
				{
					if (!other.IsVariable)
					{
						return 1;
					}
					if (this.Kinds.Length != other.Kinds.Length)
					{
						if (this.Kinds.Length <= other.Kinds.Length)
						{
							return 1;
						}
						return -1;
					}
				}
				else if (other.IsVariable)
				{
					return -1;
				}
				int num = 0;
				for (int i = 0; i < this.Kinds.Length; i++)
				{
					ExprTypeKind exprTypeKind = this.Kinds[i];
					ExprTypeKind exprTypeKind2 = other.Kinds[i];
					if (exprTypeKind != exprTypeKind2)
					{
						if (!LambdaBinder.CanConvert(exprTypeKind, exprTypeKind2))
						{
							return 1;
						}
						num = -1;
					}
				}
				return num;
			}

			// Token: 0x0400044F RID: 1103
			public readonly IFunctionProvider Provider;

			// Token: 0x04000450 RID: 1104
			public readonly MethodInfo Method;

			// Token: 0x04000451 RID: 1105
			public readonly ExprTypeKind[] Kinds;

			// Token: 0x04000452 RID: 1106
			public readonly ExprTypeKind ReturnKind;

			// Token: 0x04000453 RID: 1107
			public readonly bool IsVariable;
		}

		// Token: 0x0200018E RID: 398
		// (Invoke) Token: 0x0600085E RID: 2142
		private delegate DvBool Cmp(object a, object b);
	}
}
