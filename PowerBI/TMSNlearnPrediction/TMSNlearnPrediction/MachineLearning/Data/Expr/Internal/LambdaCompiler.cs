using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.MachineLearning.CodeGeneration;
using Microsoft.MachineLearning.Internal.Lexer;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x0200018F RID: 399
	internal sealed class LambdaCompiler : IDisposable
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0002D264 File Offset: 0x0002B464
		public static Delegate Compile(out List<Error> errors, DynamicAssembly assem, LambdaNode node)
		{
			Contracts.CheckValue<LambdaNode>(node, "node");
			Delegate @delegate;
			using (LambdaCompiler lambdaCompiler = new LambdaCompiler(assem, node))
			{
				@delegate = lambdaCompiler.Do(out errors);
			}
			return @delegate;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		private LambdaCompiler(DynamicAssembly assem, LambdaNode node)
		{
			this._top = node;
			Type type;
			switch (node.Vars.Length)
			{
			case 1:
				type = typeof(Func<, >);
				break;
			case 2:
				type = typeof(Func<, , >);
				break;
			case 3:
				type = typeof(Func<, , , >);
				break;
			case 4:
				type = typeof(Func<, , , , >);
				break;
			case 5:
				type = typeof(Func<, , , , , >);
				break;
			case 6:
				type = typeof(Func<, , , , , , >);
				break;
			case 7:
				type = typeof(Func<, , , , , , , >);
				break;
			case 8:
				type = typeof(Func<, , , , , , , , >);
				break;
			case 9:
				type = typeof(Func<, , , , , , , , , >);
				break;
			case 10:
				type = typeof(Func<, , , , , , , , , , >);
				break;
			case 11:
				type = typeof(Func<, , , , , , , , , , , >);
				break;
			case 12:
				type = typeof(Func<, , , , , , , , , , , , >);
				break;
			case 13:
				type = typeof(Func<, , , , , , , , , , , , , >);
				break;
			case 14:
				type = typeof(Func<, , , , , , , , , , , , , , >);
				break;
			case 15:
				type = typeof(Func<, , , , , , , , , , , , , , , >);
				break;
			case 16:
				type = typeof(Func<, , , , , , , , , , , , , , , , >);
				break;
			default:
				throw Contracts.Except("Internal error in LambdaCompiler");
			}
			Type[] array = new Type[node.Vars.Length + 1];
			foreach (ParamNode paramNode in node.Vars)
			{
				array[paramNode.Index] = paramNode.Type.RawType;
			}
			array[node.Vars.Length] = node.ResultType.RawType;
			this._delType = type.MakeGenericType(array);
			Array.Copy(array, 0, array, 1, array.Length - 1);
			array[0] = typeof(object);
			this._meth = MethodGenerator.Create(assem, "lambda", typeof(Exec), node.ResultType.RawType, array);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002D4B8 File Offset: 0x0002B6B8
		private Delegate Do(out List<Error> errors)
		{
			LambdaCompiler.Visitor visitor = new LambdaCompiler.Visitor(this._meth);
			this._top.Expr.Accept(visitor);
			errors = visitor.GetErrors();
			if (errors != null)
			{
				return null;
			}
			ILGeneratorExtensions.Ret(this._meth.Il);
			return this._meth.CreateDelegate(this._delType, null);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002D513 File Offset: 0x0002B713
		public void Dispose()
		{
			this._meth.Dispose();
		}

		// Token: 0x04000454 RID: 1108
		public const int MaxParams = 16;

		// Token: 0x04000455 RID: 1109
		private LambdaNode _top;

		// Token: 0x04000456 RID: 1110
		private Type _delType;

		// Token: 0x04000457 RID: 1111
		private MethodGenerator _meth;

		// Token: 0x02000191 RID: 401
		private sealed class Visitor : ExprVisitor
		{
			// Token: 0x06000869 RID: 2153 RVA: 0x0002D530 File Offset: 0x0002B730
			public Visitor(MethodGenerator meth)
			{
				this._meth = meth;
				this._gen = meth.Il;
				BuiltinFunctions instance = BuiltinFunctions.Instance;
				this._opsBL = instance._opsBL;
				this._opsI4 = instance._opsI4;
				this._opsI8 = instance._opsI8;
				this._opsTX = instance._opsTX;
				this._methGetFalseBL = typeof(DvBool).GetMethod("get_False");
				this._methGetTrueBL = typeof(DvBool).GetMethod("get_True");
				this._methGetNABL = typeof(DvBool).GetMethod("get_NA");
				this._methGetRawBitsBL = typeof(DvBool).GetMethod("GetRawBits");
				this._methGetRawBitsI4 = typeof(DvInt4).GetMethod("GetRawBits");
				this._methGetRawBitsI8 = typeof(DvInt8).GetMethod("GetRawBits");
				this._methGetNATX = typeof(DvText).GetMethod("get_NA");
				this._methGetIsNATX = typeof(DvText).GetMethod("get_IsNA");
				this._cacheWith = new List<LambdaCompiler.Visitor.CachedWithLocal>();
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x0002D667 File Offset: 0x0002B867
			public List<Error> GetErrors()
			{
				return this._errors;
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x0002D670 File Offset: 0x0002B870
			private void DoConvert(ExprNode node)
			{
				if (node.NeedsConversion)
				{
					switch (node.SrcKind)
					{
					case ExprTypeKind.I4:
						switch (node.ExprType.Kind)
						{
						case ExprTypeKind.I8:
							this.CallOp<DvInt4, DvInt8>(this._opsI8, "op_Implicit");
							return;
						case ExprTypeKind.R4:
							this.CallOp<DvInt4, float>(this._opsI4, "op_Explicit");
							return;
						case ExprTypeKind.R8:
							this.CallOp<DvInt4, double>(this._opsI4, "op_Explicit");
							return;
						}
						break;
					case ExprTypeKind.I8:
						if (node.IsR8)
						{
							this.CallOp<DvInt8, double>(this._opsI8, "op_Explicit");
							return;
						}
						break;
					case ExprTypeKind.R4:
						if (node.IsR8)
						{
							ILGeneratorExtensions.Conv_R8(this._gen);
							return;
						}
						break;
					default:
						this.PostError(node, "Internal error in implicit conversion");
						break;
					}
					this.PostError(node, "Internal error(2) in implicit conversion");
					return;
				}
				if (node.IsR4)
				{
					ILGeneratorExtensions.Conv_R4(this._gen);
					return;
				}
				if (node.IsR8)
				{
					ILGeneratorExtensions.Conv_R8(this._gen);
				}
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x0002D76E File Offset: 0x0002B96E
			private void PostError(Node node)
			{
				Utils.Add<Error>(ref this._errors, new Error(node.Token, "Code generation error"));
			}

			// Token: 0x0600086D RID: 2157 RVA: 0x0002D78B File Offset: 0x0002B98B
			private void PostError(Node node, string msg)
			{
				Utils.Add<Error>(ref this._errors, new Error(node.Token, msg));
			}

			// Token: 0x0600086E RID: 2158 RVA: 0x0002D7A4 File Offset: 0x0002B9A4
			private void PostError(Node node, string msg, params object[] args)
			{
				Utils.Add<Error>(ref this._errors, new Error(node.Token, msg, args));
			}

			// Token: 0x0600086F RID: 2159 RVA: 0x0002D7C0 File Offset: 0x0002B9C0
			private bool TryUseValue(ExprNode node)
			{
				object exprValue = node.ExprValue;
				if (exprValue == null)
				{
					return false;
				}
				switch (node.ExprType.Kind)
				{
				case ExprTypeKind.Error:
					this.PostError(node);
					break;
				case ExprTypeKind.BL:
					this.GenBL((DvBool)exprValue);
					break;
				case ExprTypeKind.I4:
					this.GenI4((DvInt4)exprValue);
					break;
				case ExprTypeKind.I8:
					this.GenI8((DvInt8)exprValue);
					break;
				case ExprTypeKind.R4:
					ILGeneratorExtensions.Ldc_R4(this._gen, (float)exprValue);
					break;
				case ExprTypeKind.R8:
					ILGeneratorExtensions.Ldc_R8(this._gen, (double)exprValue);
					break;
				case ExprTypeKind.TX:
				{
					DvText dvText = (DvText)exprValue;
					if (dvText.IsNA)
					{
						ILGeneratorExtensions.Call(this._gen, this._methGetNATX);
					}
					else
					{
						ILGeneratorExtensions.Ldstr(this._gen, dvText.ToString());
						this.CallFnc<string, DvText>(new Func<string, DvText>(Exec.ToTX));
					}
					break;
				}
				default:
					this.PostError(node, "Bad ExprType");
					break;
				}
				return true;
			}

			// Token: 0x06000870 RID: 2160 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
			public override void Visit(BoolLitNode node)
			{
				this.GenBL((DvBool)node.ExprValue);
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
			public override void Visit(StrLitNode node)
			{
				DvText dvText = (DvText)node.ExprValue;
				if (dvText.IsNA)
				{
					ILGeneratorExtensions.Call(this._gen, this._methGetNATX);
					return;
				}
				ILGeneratorExtensions.Ldstr(this._gen, dvText.ToString());
				this.CallFnc<string, DvText>(new Func<string, DvText>(Exec.ToTX));
			}

			// Token: 0x06000872 RID: 2162 RVA: 0x0002D944 File Offset: 0x0002BB44
			public override void Visit(NumLitNode node)
			{
				object exprValue = node.ExprValue;
				switch (node.ExprType.Kind)
				{
				case ExprTypeKind.I4:
					this.GenI4((DvInt4)exprValue);
					return;
				case ExprTypeKind.I8:
					this.GenI8((DvInt8)exprValue);
					return;
				case ExprTypeKind.R4:
					ILGeneratorExtensions.Ldc_R4(this._gen, (float)exprValue);
					return;
				case ExprTypeKind.R8:
					ILGeneratorExtensions.Ldc_R8(this._gen, (double)exprValue);
					return;
				default:
					this.PostError(node, "Internal error in numeric literal");
					return;
				}
			}

			// Token: 0x06000873 RID: 2163 RVA: 0x0002D9CC File Offset: 0x0002BBCC
			public override void Visit(IdentNode node)
			{
				if (this.TryUseValue(node))
				{
					return;
				}
				Node referent = node.Referent;
				if (node.Referent == null)
				{
					this.PostError(node, "Unbound name!");
					return;
				}
				NodeKind kind = referent.Kind;
				if (kind != NodeKind.Param)
				{
					if (kind != NodeKind.WithLocal)
					{
						this.PostError(node, "Unbound name!");
						return;
					}
					WithLocalNode asWithLocal = referent.AsWithLocal;
					if (asWithLocal.UseCount <= 1)
					{
						asWithLocal.GenCount++;
						asWithLocal.Value.Accept(this);
					}
					else
					{
						LambdaCompiler.Visitor.CachedWithLocal cachedWithLocal = this._cacheWith[asWithLocal.Index];
						if (cachedWithLocal.Flag != null)
						{
							bool flag = asWithLocal.GenCount > 0;
							Label label = default(Label);
							if (flag)
							{
								label = this._gen.DefineLabel();
								ILGeneratorExtensions.Brtrue(ILGeneratorExtensions.Ldloc(this._gen, cachedWithLocal.Flag), label);
							}
							asWithLocal.GenCount++;
							asWithLocal.Value.Accept(this);
							ILGeneratorExtensions.Stloc(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Stloc(this._gen, cachedWithLocal.Value), 1), cachedWithLocal.Flag);
							if (flag)
							{
								this._gen.MarkLabel(label);
							}
						}
						ILGeneratorExtensions.Ldloc(this._gen, cachedWithLocal.Value);
					}
				}
				else
				{
					ILGeneratorExtensions.Ldarg(this._gen, referent.AsParam.Index + 1);
				}
				this.DoConvert(node);
			}

			// Token: 0x06000874 RID: 2164 RVA: 0x0002DB2A File Offset: 0x0002BD2A
			public override bool PreVisit(UnaryOpNode node)
			{
				return !this.TryUseValue(node);
			}

			// Token: 0x06000875 RID: 2165 RVA: 0x0002DB38 File Offset: 0x0002BD38
			public override void PostVisit(UnaryOpNode node)
			{
				switch (node.Op)
				{
				case UnaryOp.Not:
					this.CallOp<DvBool, DvBool>(this._opsBL, "op_LogicalNot");
					break;
				case UnaryOp.Minus:
					switch (node.SrcKind)
					{
					case ExprTypeKind.I4:
						this.CallOp<DvInt4, DvInt4>(this._opsI4, "op_UnaryNegation");
						break;
					case ExprTypeKind.I8:
						this.CallOp<DvInt8, DvInt8>(this._opsI8, "op_UnaryNegation");
						break;
					case ExprTypeKind.R4:
					case ExprTypeKind.R8:
						ILGeneratorExtensions.Neg(this._gen);
						break;
					default:
						this.PostError(node, "Internal error in unary minus");
						break;
					}
					break;
				default:
					this.PostError(node, "Internal error in unary operator");
					break;
				}
				this.DoConvert(node);
			}

			// Token: 0x06000876 RID: 2166 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
			public override bool PreVisit(BinaryOpNode node)
			{
				if (this.TryUseValue(node))
				{
					return false;
				}
				if (node.ReduceToLeft)
				{
					node.Left.Accept(this);
					this.DoConvert(node);
					return false;
				}
				if (node.ReduceToRight)
				{
					node.Right.Accept(this);
					this.DoConvert(node);
					return false;
				}
				switch (node.Op)
				{
				case BinaryOp.Coalesce:
					this.GenCoalesce(node);
					break;
				case BinaryOp.Or:
				case BinaryOp.And:
					this.GenBoolBinOp(node);
					break;
				case BinaryOp.Add:
				case BinaryOp.Sub:
				case BinaryOp.Mul:
				case BinaryOp.Div:
				case BinaryOp.Mod:
				case BinaryOp.Power:
					this.GenNumBinOp(node);
					break;
				case BinaryOp.Error:
					this.PostError(node);
					break;
				default:
					this.PostError(node, "Internal error in binary operator");
					break;
				}
				this.DoConvert(node);
				return false;
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x0002DCA4 File Offset: 0x0002BEA4
			private void GenBoolBinOp(BinaryOpNode node)
			{
				Label label = this._gen.DefineLabel();
				node.Left.Accept(this);
				ILGeneratorExtensions.Call(ILGeneratorExtensions.Dup(this._gen), this._methGetRawBitsBL);
				if (node.Op == BinaryOp.Or)
				{
					ILGeneratorExtensions.Beq(ILGeneratorExtensions.Ldc_I4(this._gen, 1), label);
				}
				else
				{
					ILGeneratorExtensions.Brfalse(this._gen, label);
				}
				using (MethodGenerator.Temporary temporary = this._meth.AcquireTemporary(typeof(DvBool), false))
				{
					ILGeneratorExtensions.Stloc(this._gen, temporary.Local);
					node.Right.Accept(this);
					ILGeneratorExtensions.Call(ILGeneratorExtensions.Ldloc(this._gen, temporary.Local), this._methGetRawBitsBL);
				}
				if (node.Op == BinaryOp.Or)
				{
					ILGeneratorExtensions.Brfalse(this._gen, label);
				}
				else
				{
					ILGeneratorExtensions.Beq(ILGeneratorExtensions.Ldc_I4(this._gen, 1), label);
				}
				ILGeneratorExtensions.Call(ILGeneratorExtensions.Dup(this._gen), this._methGetRawBitsBL);
				if (node.Op == BinaryOp.Or)
				{
					ILGeneratorExtensions.Beq(ILGeneratorExtensions.Ldc_I4(this._gen, 1), label);
				}
				else
				{
					ILGeneratorExtensions.Brfalse(this._gen, label);
				}
				ILGeneratorExtensions.Call(ILGeneratorExtensions.Pop(this._gen), this._methGetNABL);
				this._gen.MarkLabel(label);
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x0002DE10 File Offset: 0x0002C010
			private void GenNumBinOp(BinaryOpNode node)
			{
				node.Left.Accept(this);
				node.Right.Accept(this);
				if (node.SrcKind == ExprTypeKind.I4)
				{
					switch (node.Op)
					{
					case BinaryOp.Add:
						this.CallBinOp<DvInt4>(this._opsI4, "op_Addition");
						return;
					case BinaryOp.Sub:
						this.CallBinOp<DvInt4>(this._opsI4, "op_Subtraction");
						return;
					case BinaryOp.Mul:
						this.CallBinOp<DvInt4>(this._opsI4, "op_Multiply");
						return;
					case BinaryOp.Div:
						this.CallBinOp<DvInt4>(this._opsI4, "op_Division");
						return;
					case BinaryOp.Mod:
						this.CallBinOp<DvInt4>(this._opsI4, "op_Modulus");
						return;
					case BinaryOp.Power:
						this.CallBin<DvInt4>(new Func<DvInt4, DvInt4, DvInt4>(DvInt4.Pow));
						return;
					default:
						this.PostError(node, "Internal error in numeric binary operator");
						return;
					}
				}
				else if (node.SrcKind == ExprTypeKind.I8)
				{
					switch (node.Op)
					{
					case BinaryOp.Add:
						this.CallBinOp<DvInt8>(this._opsI8, "op_Addition");
						return;
					case BinaryOp.Sub:
						this.CallBinOp<DvInt8>(this._opsI8, "op_Subtraction");
						return;
					case BinaryOp.Mul:
						this.CallBinOp<DvInt8>(this._opsI8, "op_Multiply");
						return;
					case BinaryOp.Div:
						this.CallBinOp<DvInt8>(this._opsI8, "op_Division");
						return;
					case BinaryOp.Mod:
						this.CallBinOp<DvInt8>(this._opsI8, "op_Modulus");
						return;
					case BinaryOp.Power:
						this.CallBin<DvInt8>(new Func<DvInt8, DvInt8, DvInt8>(DvInt8.Pow));
						return;
					default:
						this.PostError(node, "Internal error in numeric binary operator");
						return;
					}
				}
				else if (node.SrcKind == ExprTypeKind.R4)
				{
					switch (node.Op)
					{
					case BinaryOp.Add:
						ILGeneratorExtensions.Add(this._gen);
						return;
					case BinaryOp.Sub:
						ILGeneratorExtensions.Sub(this._gen);
						return;
					case BinaryOp.Mul:
						ILGeneratorExtensions.Mul(this._gen);
						return;
					case BinaryOp.Div:
						ILGeneratorExtensions.Div(this._gen);
						return;
					case BinaryOp.Mod:
						ILGeneratorExtensions.Rem(this._gen);
						return;
					case BinaryOp.Power:
						this.CallBin<float>(new Func<float, float, float>(BuiltinFunctions.Pow));
						return;
					default:
						this.PostError(node, "Internal error in numeric binary operator");
						return;
					}
				}
				else
				{
					switch (node.Op)
					{
					case BinaryOp.Add:
						ILGeneratorExtensions.Add(this._gen);
						return;
					case BinaryOp.Sub:
						ILGeneratorExtensions.Sub(this._gen);
						return;
					case BinaryOp.Mul:
						ILGeneratorExtensions.Mul(this._gen);
						return;
					case BinaryOp.Div:
						ILGeneratorExtensions.Div(this._gen);
						return;
					case BinaryOp.Mod:
						ILGeneratorExtensions.Rem(this._gen);
						return;
					case BinaryOp.Power:
						this.CallBin<double>(new Func<double, double, double>(Math.Pow));
						return;
					default:
						this.PostError(node, "Internal error in numeric binary operator");
						return;
					}
				}
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x0002E0AC File Offset: 0x0002C2AC
			private void GenBL(DvBool value)
			{
				MethodInfo methodInfo;
				if (value.IsFalse)
				{
					methodInfo = this._methGetFalseBL;
				}
				else if (value.IsTrue)
				{
					methodInfo = this._methGetTrueBL;
				}
				else
				{
					methodInfo = this._methGetNABL;
				}
				ILGeneratorExtensions.Call(this._gen, methodInfo);
			}

			// Token: 0x0600087A RID: 2170 RVA: 0x0002E0F1 File Offset: 0x0002C2F1
			private void GenI4(DvInt4 value)
			{
				ILGeneratorExtensions.Ldc_I4(this._gen, value.RawValue);
				this.CallOp<int, DvInt4>(this._opsI4, "op_Implicit");
			}

			// Token: 0x0600087B RID: 2171 RVA: 0x0002E117 File Offset: 0x0002C317
			private void GenI8(DvInt8 value)
			{
				ILGeneratorExtensions.Ldc_I8(this._gen, value.RawValue);
				this.CallOp<long, DvInt8>(this._opsI8, "op_Implicit");
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x0002E140 File Offset: 0x0002C340
			private void CallOp<TSrc, TDst>(Dictionary<string, List<MethodInfo>> ops, string name)
			{
				List<MethodInfo> list = ops[name];
				MethodInfo methodInfo = null;
				foreach (MethodInfo methodInfo2 in list)
				{
					if (!(methodInfo2.ReturnType != typeof(TDst)))
					{
						ParameterInfo[] parameters = methodInfo2.GetParameters();
						if (parameters.Length == 1 && !(parameters[0].ParameterType != typeof(TSrc)))
						{
							methodInfo = methodInfo2;
						}
					}
				}
				ILGeneratorExtensions.Call(this._gen, methodInfo);
			}

			// Token: 0x0600087D RID: 2173 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
			private void CallOp<T1, T2, TDst>(Dictionary<string, List<MethodInfo>> ops, string name)
			{
				List<MethodInfo> list = ops[name];
				MethodInfo methodInfo = null;
				foreach (MethodInfo methodInfo2 in list)
				{
					if (!(methodInfo2.ReturnType != typeof(TDst)))
					{
						ParameterInfo[] parameters = methodInfo2.GetParameters();
						if (parameters.Length == 2 && !(parameters[0].ParameterType != typeof(T1)) && !(parameters[1].ParameterType != typeof(T2)))
						{
							methodInfo = methodInfo2;
						}
					}
				}
				ILGeneratorExtensions.Call(this._gen, methodInfo);
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x0002E298 File Offset: 0x0002C498
			private void CallBinOp<T>(Dictionary<string, List<MethodInfo>> ops, string name)
			{
				this.CallOp<T, T, T>(ops, name);
			}

			// Token: 0x0600087F RID: 2175 RVA: 0x0002E2A2 File Offset: 0x0002C4A2
			private void CallFnc<TSrc, TDst>(Func<TSrc, TDst> fn)
			{
				ILGeneratorExtensions.Call(this._gen, fn.GetMethodInfo());
			}

			// Token: 0x06000880 RID: 2176 RVA: 0x0002E2B6 File Offset: 0x0002C4B6
			private void CallFnc<T1, T2, TDst>(Func<T1, T2, TDst> fn)
			{
				ILGeneratorExtensions.Call(this._gen, fn.GetMethodInfo());
			}

			// Token: 0x06000881 RID: 2177 RVA: 0x0002E2CA File Offset: 0x0002C4CA
			private void CallUna<T>(Func<T, T> fn)
			{
				ILGeneratorExtensions.Call(this._gen, fn.GetMethodInfo());
			}

			// Token: 0x06000882 RID: 2178 RVA: 0x0002E2DE File Offset: 0x0002C4DE
			private void CallBin<T>(Func<T, T, T> fn)
			{
				ILGeneratorExtensions.Call(this._gen, fn.GetMethodInfo());
			}

			// Token: 0x06000883 RID: 2179 RVA: 0x0002E2F4 File Offset: 0x0002C4F4
			private void GenCoalesce(BinaryOpNode node)
			{
				Label label = this._gen.DefineLabel();
				node.Left.Accept(this);
				this.GenBrNa(node.Left, label, true, true);
				ILGeneratorExtensions.Pop(this._gen);
				node.Right.Accept(this);
				this._gen.MarkLabel(label);
			}

			// Token: 0x06000884 RID: 2180 RVA: 0x0002E34C File Offset: 0x0002C54C
			public override void PostVisit(BinaryOpNode node)
			{
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x0002E350 File Offset: 0x0002C550
			public override bool PreVisit(ConditionalNode node)
			{
				if (this.TryUseValue(node))
				{
					return false;
				}
				DvBool? dvBool = (DvBool?)node.Cond.ExprValue;
				if (dvBool != null)
				{
					if (dvBool.Value.IsTrue)
					{
						node.Left.Accept(this);
					}
					else
					{
						if (!dvBool.Value.IsFalse)
						{
							this.PostError(node.Cond, "Internal error in conditional");
							return false;
						}
						node.Right.Accept(this);
					}
				}
				else
				{
					Label label = this._gen.DefineLabel();
					Label label2 = this._gen.DefineLabel();
					Label label3 = this._gen.DefineLabel();
					node.Cond.Accept(this);
					ILGeneratorExtensions.Beq(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Brfalse(ILGeneratorExtensions.Dup(ILGeneratorExtensions.Call(this._gen, this._methGetRawBitsBL)), label2), 1), label3);
					switch (node.ExprType.Kind)
					{
					case ExprTypeKind.BL:
						ILGeneratorExtensions.Call(this._gen, this._methGetNABL);
						break;
					case ExprTypeKind.I4:
						this.GenI4(DvInt4.NA);
						break;
					case ExprTypeKind.I8:
						this.GenI8(DvInt8.NA);
						break;
					case ExprTypeKind.R4:
						ILGeneratorExtensions.Ldc_R4(this._gen, float.NaN);
						break;
					case ExprTypeKind.R8:
						ILGeneratorExtensions.Ldc_R8(this._gen, double.NaN);
						break;
					case ExprTypeKind.TX:
						ILGeneratorExtensions.Call(this._gen, this._methGetNATX);
						break;
					default:
						this.PostError(node, "Internal error in conditional");
						break;
					}
					ILGeneratorExtensions.Br(this._gen, label).MarkLabel(label2);
					ILGeneratorExtensions.Pop(this._gen);
					node.Right.Accept(this);
					ILGeneratorExtensions.Br(this._gen, label).MarkLabel(label3);
					node.Left.Accept(this);
					this._gen.MarkLabel(label);
				}
				this.DoConvert(node);
				return false;
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x0002E538 File Offset: 0x0002C738
			public override void PostVisit(ConditionalNode node)
			{
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x0002E53C File Offset: 0x0002C73C
			public override bool PreVisit(CompareNode node)
			{
				if (this.TryUseValue(node))
				{
					return false;
				}
				ExprTypeKind argTypeKind = node.ArgTypeKind;
				Node[] items = node.Operands.Items;
				if (argTypeKind == ExprTypeKind.TX && items.Length == 2)
				{
					items[0].Accept(this);
					items[1].Accept(this);
					switch (node.Op)
					{
					case CompareOp.Equal:
						this.CallOp<DvText, DvText, DvBool>(this._opsTX, "op_Equality");
						break;
					case CompareOp.NotEqual:
						this.CallOp<DvText, DvText, DvBool>(this._opsTX, "op_Inequality");
						break;
					}
					this.DoConvert(node);
					return false;
				}
				MethodInfo methodInfo = null;
				switch (argTypeKind)
				{
				case ExprTypeKind.BL:
					methodInfo = this._methGetRawBitsBL;
					break;
				case ExprTypeKind.I4:
					methodInfo = this._methGetRawBitsI4;
					break;
				case ExprTypeKind.I8:
					methodInfo = this._methGetRawBitsI8;
					break;
				}
				Label label = this._gen.DefineLabel();
				Label label2 = this._gen.DefineLabel();
				bool flag;
				if (items.Length == 2)
				{
					flag = true;
					ExprNode exprNode;
					bool? flag2 = this.GenRaw(exprNode = items[0].AsExpr, methodInfo);
					ILGeneratorExtensions.Dup(this._gen);
					this.GenBrNaRaw(exprNode, argTypeKind, label2, flag2);
					ILGeneratorExtensions.Pop(this._gen);
					flag2 = this.GenRaw(exprNode = items[1].AsExpr, methodInfo);
					this.GenBrNaRaw(exprNode, argTypeKind, label2, flag2);
					TokKind kind = node.Operands.Delimiters[0].Kind;
					bool flag3 = kind == node.TidStrict;
					switch (argTypeKind)
					{
					case ExprTypeKind.BL:
						this.GenCmpBool(node.Op, flag3);
						break;
					case ExprTypeKind.I4:
					case ExprTypeKind.I8:
						this.GenCmpInt(node.Op, flag3);
						break;
					case ExprTypeKind.R4:
					case ExprTypeKind.R8:
						this.GenCmpFloat(node.Op, flag3);
						break;
					default:
						this.PostError(node, "Compare codegen for this comparison is NYI");
						return false;
					}
					ILGeneratorExtensions.Br(this._gen, label);
				}
				else
				{
					Action<CompareOp, bool, Label> action;
					Type type;
					switch (argTypeKind)
					{
					case ExprTypeKind.BL:
						action = new Action<CompareOp, bool, Label>(this.GenCmpBool);
						type = typeof(byte);
						break;
					case ExprTypeKind.I4:
						action = new Action<CompareOp, bool, Label>(this.GenCmpInt);
						type = typeof(int);
						break;
					case ExprTypeKind.I8:
						action = new Action<CompareOp, bool, Label>(this.GenCmpInt);
						type = typeof(long);
						break;
					case ExprTypeKind.R4:
						action = new Action<CompareOp, bool, Label>(this.GenCmpFloat);
						type = typeof(float);
						break;
					case ExprTypeKind.R8:
						action = new Action<CompareOp, bool, Label>(this.GenCmpFloat);
						type = typeof(double);
						break;
					case ExprTypeKind.TX:
						action = new Action<CompareOp, bool, Label>(this.GenCmpText);
						type = typeof(DvText);
						break;
					default:
						this.PostError(node, "Compare codegen for this comparison is NYI");
						return false;
					}
					Label label3 = this._gen.DefineLabel();
					if (node.Op != CompareOp.NotEqual)
					{
						ExprNode exprNode2 = items[0].AsExpr;
						flag = true;
						bool? flag2 = this.GenRaw(exprNode2, methodInfo);
						ILGeneratorExtensions.Dup(this._gen);
						this.GenBrNaRaw(exprNode2, argTypeKind, label2, flag2);
						ILGeneratorExtensions.Pop(this._gen);
						int num = 1;
						bool flag4;
						for (;;)
						{
							TokKind kind2 = node.Operands.Delimiters[num - 1].Kind;
							flag4 = kind2 == node.TidStrict;
							exprNode2 = items[num].AsExpr;
							flag2 = this.GenRaw(exprNode2, methodInfo);
							if (!this.GenBrNaRaw(exprNode2, argTypeKind, label2, flag2))
							{
								goto IL_0530;
							}
							if (num == items.Length - 1)
							{
								break;
							}
							ILGeneratorExtensions.Dup(this._gen);
							using (MethodGenerator.Temporary temporary = this._meth.AcquireTemporary(type, false))
							{
								ILGeneratorExtensions.Stloc(this._gen, temporary.Local);
								action(node.Op, flag4, label3);
								ILGeneratorExtensions.Ldloc(this._gen, temporary.Local);
							}
							num++;
						}
						action(node.Op, flag4, label3);
					}
					else
					{
						flag = false;
						MethodGenerator.Temporary[] array = new MethodGenerator.Temporary[items.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = this._meth.AcquireTemporary(type, false);
						}
						try
						{
							ExprNode exprNode3 = items[0].AsExpr;
							bool? flag2 = this.GenRaw(exprNode3, methodInfo);
							this.GenBrNaRaw(exprNode3, argTypeKind, label2, flag2);
							ILGeneratorExtensions.Stloc(this._gen, array[0].Local);
							for (int j = 1; j < items.Length; j++)
							{
								exprNode3 = items[j].AsExpr;
								flag2 = this.GenRaw(exprNode3, methodInfo);
								if (!this.GenBrNaRaw(exprNode3, argTypeKind, label2, flag2))
								{
									goto IL_0530;
								}
								ILGeneratorExtensions.Stloc(this._gen, array[j].Local);
								for (int k = 0; k < j; k++)
								{
									ILGeneratorExtensions.Ldloc(ILGeneratorExtensions.Ldloc(this._gen, array[k].Local), array[j].Local);
									action(node.Op, true, label3);
								}
							}
						}
						finally
						{
							int num2 = array.Length;
							while (--num2 >= 0)
							{
								array[num2].Dispose();
							}
						}
					}
					ILGeneratorExtensions.Br(ILGeneratorExtensions.Call(this._gen, this._methGetTrueBL), label);
					IL_0530:
					this._gen.MarkLabel(label3);
					ILGeneratorExtensions.Br(ILGeneratorExtensions.Call(this._gen, this._methGetFalseBL), label);
				}
				this._gen.MarkLabel(label2);
				if (flag)
				{
					ILGeneratorExtensions.Pop(this._gen);
				}
				ILGeneratorExtensions.Call(ILGeneratorExtensions.Pop(this._gen), this._methGetNABL).MarkLabel(label);
				this.DoConvert(node);
				return false;
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x0002EB00 File Offset: 0x0002CD00
			private bool? GenRaw(ExprNode node, MethodInfo methRaw)
			{
				object exprValue = node.ExprValue;
				if (exprValue != null)
				{
					switch (node.ExprType.Kind)
					{
					case ExprTypeKind.BL:
					{
						DvBool dvBool = (DvBool)exprValue;
						ILGeneratorExtensions.Ldc_I4(this._gen, (int)dvBool.RawValue);
						return new bool?(dvBool.IsNA);
					}
					case ExprTypeKind.I4:
					{
						DvInt4 dvInt = (DvInt4)exprValue;
						ILGeneratorExtensions.Ldc_I4(this._gen, dvInt.RawValue);
						return new bool?(dvInt.IsNA);
					}
					case ExprTypeKind.I8:
					{
						DvInt8 dvInt2 = (DvInt8)exprValue;
						ILGeneratorExtensions.Ldc_I8(this._gen, dvInt2.RawValue);
						return new bool?(dvInt2.IsNA);
					}
					case ExprTypeKind.R4:
					{
						float num = (float)exprValue;
						ILGeneratorExtensions.Ldc_R4(this._gen, num);
						return new bool?(TypeUtils.IsNA(num));
					}
					case ExprTypeKind.R8:
					{
						double num2 = (double)exprValue;
						ILGeneratorExtensions.Ldc_R8(this._gen, num2);
						return new bool?(TypeUtils.IsNA(num2));
					}
					case ExprTypeKind.TX:
					{
						DvText dvText = (DvText)exprValue;
						if (dvText.IsNA)
						{
							ILGeneratorExtensions.Call(this._gen, this._methGetNATX);
							return new bool?(true);
						}
						ILGeneratorExtensions.Ldstr(this._gen, dvText.ToString());
						this.CallFnc<string, DvText>(new Func<string, DvText>(Exec.ToTX));
						return new bool?(false);
					}
					}
				}
				node.Accept(this);
				if (methRaw != null)
				{
					ILGeneratorExtensions.Call(this._gen, methRaw);
				}
				return null;
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x0002EC8B File Offset: 0x0002CE8B
			private bool GenBrNaRaw(ExprNode node, ExprTypeKind kind, Label labNa, bool? isNA)
			{
				if (isNA == null)
				{
					this.GenBrNaCore(node, kind, labNa, true, false, true);
					return true;
				}
				if (isNA.Value)
				{
					ILGeneratorExtensions.Br(this._gen, labNa);
					return false;
				}
				return true;
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x0002ECBD File Offset: 0x0002CEBD
			private void GenBrNa(ExprNode node, Label labNa, bool dup = true, bool rev = false)
			{
				this.GenBrNaCore(node, node.ExprType.Kind, labNa, dup, rev, false);
			}

			// Token: 0x0600088B RID: 2187 RVA: 0x0002ECD8 File Offset: 0x0002CED8
			private void GenBrNaCore(ExprNode node, ExprTypeKind kind, Label labNa, bool dup, bool rev, bool raw)
			{
				if (dup)
				{
					ILGeneratorExtensions.Dup(this._gen);
				}
				switch (kind)
				{
				case ExprTypeKind.BL:
					if (!raw)
					{
						ILGeneratorExtensions.Call(this._gen, this._methGetRawBitsBL);
					}
					ILGeneratorExtensions.Ldc_I4(this._gen, 1);
					if (rev)
					{
						ILGeneratorExtensions.Ble_Un(this._gen, labNa);
						return;
					}
					ILGeneratorExtensions.Bgt_Un(this._gen, labNa);
					return;
				case ExprTypeKind.I4:
					if (!raw)
					{
						ILGeneratorExtensions.Call(this._gen, this._methGetRawBitsI4);
					}
					ILGeneratorExtensions.Ldc_I4(this._gen, int.MinValue);
					if (rev)
					{
						ILGeneratorExtensions.Bne_Un(this._gen, labNa);
						return;
					}
					ILGeneratorExtensions.Beq(this._gen, labNa);
					return;
				case ExprTypeKind.I8:
					if (!raw)
					{
						ILGeneratorExtensions.Call(this._gen, this._methGetRawBitsI8);
					}
					ILGeneratorExtensions.Ldc_I8(this._gen, long.MinValue);
					if (rev)
					{
						ILGeneratorExtensions.Bne_Un(this._gen, labNa);
						return;
					}
					ILGeneratorExtensions.Beq(this._gen, labNa);
					return;
				case ExprTypeKind.R4:
				case ExprTypeKind.R8:
					ILGeneratorExtensions.Dup(this._gen);
					if (rev)
					{
						ILGeneratorExtensions.Beq(this._gen, labNa);
						return;
					}
					ILGeneratorExtensions.Bne_Un(this._gen, labNa);
					return;
				case ExprTypeKind.TX:
				{
					using (MethodGenerator.Temporary temporary = this._meth.AcquireRefTemporary(typeof(DvText)))
					{
						ILGeneratorExtensions.Call(ILGeneratorExtensions.Ldloca(ILGeneratorExtensions.Stloc(this._gen, temporary.Local), temporary.Local), this._methGetIsNATX);
					}
					if (rev)
					{
						ILGeneratorExtensions.Brfalse(this._gen, labNa);
						return;
					}
					ILGeneratorExtensions.Brtrue(this._gen, labNa);
					return;
				}
				default:
					this.PostError(node, "Internal error in GenBrNa");
					return;
				}
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x0002EEAC File Offset: 0x0002D0AC
			private void GenCmpBool(CompareOp op, bool isStrict)
			{
				switch (op)
				{
				case CompareOp.Equal:
					ILGeneratorExtensions.Ceq(this._gen);
					break;
				case CompareOp.NotEqual:
					ILGeneratorExtensions.Xor(this._gen);
					break;
				}
				this.CallOp<bool, DvBool>(this._opsBL, "op_Implicit");
			}

			// Token: 0x0600088D RID: 2189 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
			private void GenCmpInt(CompareOp op, bool isStrict)
			{
				switch (op)
				{
				case CompareOp.Equal:
					ILGeneratorExtensions.Ceq(this._gen);
					break;
				case CompareOp.NotEqual:
					ILGeneratorExtensions.Ceq(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Ceq(this._gen), 0));
					break;
				case CompareOp.IncrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Clt(this._gen);
					}
					else
					{
						ILGeneratorExtensions.Ceq(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Cgt(this._gen), 0));
					}
					break;
				case CompareOp.DecrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Cgt(this._gen);
					}
					else
					{
						ILGeneratorExtensions.Ceq(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Clt(this._gen), 0));
					}
					break;
				}
				this.CallOp<bool, DvBool>(this._opsBL, "op_Implicit");
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x0002EFAC File Offset: 0x0002D1AC
			private void GenCmpFloat(CompareOp op, bool isStrict)
			{
				switch (op)
				{
				case CompareOp.Equal:
					ILGeneratorExtensions.Ceq(this._gen);
					break;
				case CompareOp.NotEqual:
					ILGeneratorExtensions.Ceq(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Ceq(this._gen), 0));
					break;
				case CompareOp.IncrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Clt(this._gen);
					}
					else
					{
						ILGeneratorExtensions.Ceq(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Cgt_Un(this._gen), 0));
					}
					break;
				case CompareOp.DecrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Cgt(this._gen);
					}
					else
					{
						ILGeneratorExtensions.Ceq(ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Clt_Un(this._gen), 0));
					}
					break;
				}
				this.CallOp<bool, DvBool>(this._opsBL, "op_Implicit");
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x0002F060 File Offset: 0x0002D260
			private void GenCmpBool(CompareOp op, bool isStrict, Label labFalse)
			{
				switch (op)
				{
				case CompareOp.Equal:
					ILGeneratorExtensions.Bne_Un(this._gen, labFalse);
					return;
				case CompareOp.NotEqual:
					ILGeneratorExtensions.Beq(this._gen, labFalse);
					return;
				default:
					return;
				}
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x0002F09C File Offset: 0x0002D29C
			private void GenCmpText(CompareOp op, bool isStrict, Label labFalse)
			{
				switch (op)
				{
				case CompareOp.Equal:
					this.CallFnc<DvText, DvText, bool>(new Func<DvText, DvText, bool>(DvText.Identical));
					ILGeneratorExtensions.Brfalse(this._gen, labFalse);
					return;
				case CompareOp.NotEqual:
					this.CallFnc<DvText, DvText, bool>(new Func<DvText, DvText, bool>(DvText.Identical));
					ILGeneratorExtensions.Brtrue(this._gen, labFalse);
					return;
				default:
					return;
				}
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x0002F0FC File Offset: 0x0002D2FC
			private void GenCmpInt(CompareOp op, bool isStrict, Label labFalse)
			{
				switch (op)
				{
				case CompareOp.Equal:
					ILGeneratorExtensions.Bne_Un(this._gen, labFalse);
					return;
				case CompareOp.NotEqual:
					ILGeneratorExtensions.Beq(this._gen, labFalse);
					return;
				case CompareOp.IncrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Bge(this._gen, labFalse);
						return;
					}
					ILGeneratorExtensions.Bgt(this._gen, labFalse);
					return;
				case CompareOp.DecrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Ble(this._gen, labFalse);
						return;
					}
					ILGeneratorExtensions.Blt(this._gen, labFalse);
					return;
				default:
					return;
				}
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x0002F17C File Offset: 0x0002D37C
			private void GenCmpFloat(CompareOp op, bool isStrict, Label labFalse)
			{
				switch (op)
				{
				case CompareOp.Equal:
					ILGeneratorExtensions.Bne_Un(this._gen, labFalse);
					return;
				case CompareOp.NotEqual:
					ILGeneratorExtensions.Beq(this._gen, labFalse);
					return;
				case CompareOp.IncrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Bge_Un(this._gen, labFalse);
						return;
					}
					ILGeneratorExtensions.Bgt_Un(this._gen, labFalse);
					return;
				case CompareOp.DecrChain:
					if (isStrict)
					{
						ILGeneratorExtensions.Ble_Un(this._gen, labFalse);
						return;
					}
					ILGeneratorExtensions.Blt_Un(this._gen, labFalse);
					return;
				default:
					return;
				}
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x0002F1FB File Offset: 0x0002D3FB
			public override void PostVisit(CompareNode node)
			{
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x0002F200 File Offset: 0x0002D400
			public override bool PreVisit(CallNode node)
			{
				if (this.TryUseValue(node))
				{
					return false;
				}
				if (node.Method == null)
				{
					this.PostError(node, "Internal error: unknown function: '{0}'", new object[] { node.Head.Value });
					return false;
				}
				MethodInfo method = node.Method;
				ParameterInfo[] parameters = method.GetParameters();
				Type type;
				if (Utils.Size<ParameterInfo>(parameters) > 0 && PlatformUtils.IsArrayEx(type = parameters[parameters.Length - 1].ParameterType))
				{
					type = type.GetElementType();
					Node[] items = node.Args.Items;
					int num = parameters.Length - 1;
					int num2 = node.Args.Items.Length - num;
					for (int i = 0; i < num; i++)
					{
						items[i].Accept(this);
					}
					ILGeneratorExtensions.Newarr(ILGeneratorExtensions.Ldc_I4(this._gen, num2), type);
					for (int j = 0; j < num2; j++)
					{
						ILGeneratorExtensions.Ldc_I4(ILGeneratorExtensions.Dup(this._gen), j);
						items[num + j].Accept(this);
						ILGeneratorExtensions.Stelem(this._gen, type);
					}
					ILGeneratorExtensions.Call(this._gen, node.Method);
				}
				else
				{
					node.Args.Accept(this);
					if (node.Method.ReturnType != typeof(void))
					{
						ILGeneratorExtensions.Call(this._gen, node.Method);
					}
				}
				this.DoConvert(node);
				return false;
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x0002F36E File Offset: 0x0002D56E
			public override void PostVisit(CallNode node)
			{
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x0002F370 File Offset: 0x0002D570
			public override void PostVisit(ListNode node)
			{
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x0002F374 File Offset: 0x0002D574
			public override bool PreVisit(WithNode node)
			{
				WithLocalNode local = node.Local;
				if (local.Value.ExprValue != null || local.UseCount <= 1)
				{
					node.Body.Accept(this);
				}
				else
				{
					int count = this._cacheWith.Count;
					bool flag = true;
					long num = (long)local.UseCount;
					if (num > 128L)
					{
						flag = false;
					}
					else
					{
						int num2 = count;
						while (--num2 >= 0)
						{
							LambdaCompiler.Visitor.CachedWithLocal cachedWithLocal = this._cacheWith[num2];
							if (cachedWithLocal.Flag != null)
							{
								num *= (long)cachedWithLocal.Node.UseCount;
								if (num > 128L)
								{
									flag = false;
									break;
								}
							}
						}
					}
					using (MethodGenerator.Temporary temporary = this._meth.AcquireTemporary(local.Value.ExprType.GetSysType(), false))
					{
						using (MethodGenerator.Temporary temporary2 = (flag ? this._meth.AcquireTemporary(typeof(bool), false) : default(MethodGenerator.Temporary)))
						{
							LocalBuilder local2 = temporary2.Local;
							if (flag)
							{
								ILGeneratorExtensions.Stloc(ILGeneratorExtensions.Ldc_I4(this._gen, 0), local2);
							}
							else
							{
								local.Value.Accept(this);
								ILGeneratorExtensions.Stloc(this._gen, temporary.Local);
							}
							LambdaCompiler.Visitor.CachedWithLocal cachedWithLocal2 = new LambdaCompiler.Visitor.CachedWithLocal(local, temporary.Local, temporary2.Local);
							this._cacheWith.Add(cachedWithLocal2);
							local.Index = count;
							node.Body.Accept(this);
							local.Index = -1;
							this._cacheWith.RemoveAt(count);
						}
					}
				}
				return false;
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x0002F528 File Offset: 0x0002D728
			public override void PostVisit(WithNode node)
			{
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x0002F52A File Offset: 0x0002D72A
			public override bool PreVisit(WithLocalNode node)
			{
				return false;
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x0002F52D File Offset: 0x0002D72D
			public override void PostVisit(WithLocalNode node)
			{
			}

			// Token: 0x04000458 RID: 1112
			private MethodGenerator _meth;

			// Token: 0x04000459 RID: 1113
			private ILGenerator _gen;

			// Token: 0x0400045A RID: 1114
			private List<Error> _errors;

			// Token: 0x0400045B RID: 1115
			private readonly Dictionary<string, List<MethodInfo>> _opsBL;

			// Token: 0x0400045C RID: 1116
			private readonly Dictionary<string, List<MethodInfo>> _opsI4;

			// Token: 0x0400045D RID: 1117
			private readonly Dictionary<string, List<MethodInfo>> _opsI8;

			// Token: 0x0400045E RID: 1118
			private readonly Dictionary<string, List<MethodInfo>> _opsTX;

			// Token: 0x0400045F RID: 1119
			private readonly MethodInfo _methGetFalseBL;

			// Token: 0x04000460 RID: 1120
			private readonly MethodInfo _methGetTrueBL;

			// Token: 0x04000461 RID: 1121
			private readonly MethodInfo _methGetNABL;

			// Token: 0x04000462 RID: 1122
			private readonly MethodInfo _methGetRawBitsBL;

			// Token: 0x04000463 RID: 1123
			private readonly MethodInfo _methGetRawBitsI4;

			// Token: 0x04000464 RID: 1124
			private readonly MethodInfo _methGetRawBitsI8;

			// Token: 0x04000465 RID: 1125
			private readonly MethodInfo _methGetNATX;

			// Token: 0x04000466 RID: 1126
			private readonly MethodInfo _methGetIsNATX;

			// Token: 0x04000467 RID: 1127
			private List<LambdaCompiler.Visitor.CachedWithLocal> _cacheWith;

			// Token: 0x02000192 RID: 402
			private sealed class CachedWithLocal
			{
				// Token: 0x0600089B RID: 2203 RVA: 0x0002F52F File Offset: 0x0002D72F
				public CachedWithLocal(WithLocalNode node, LocalBuilder value, LocalBuilder flag)
				{
					this.Node = node;
					this.Value = value;
					this.Flag = flag;
				}

				// Token: 0x04000468 RID: 1128
				public readonly WithLocalNode Node;

				// Token: 0x04000469 RID: 1129
				public readonly LocalBuilder Value;

				// Token: 0x0400046A RID: 1130
				public readonly LocalBuilder Flag;
			}
		}
	}
}
