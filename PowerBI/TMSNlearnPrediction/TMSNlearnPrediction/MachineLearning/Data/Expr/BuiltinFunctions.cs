using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.Expr
{
	// Token: 0x020000B0 RID: 176
	public sealed class BuiltinFunctions : FunctionProviderBase, IFunctionProvider
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00013FF7 File Offset: 0x000121F7
		public static BuiltinFunctions Instance
		{
			get
			{
				if (BuiltinFunctions._instance == null)
				{
					Interlocked.CompareExchange<BuiltinFunctions>(ref BuiltinFunctions._instance, new BuiltinFunctions(), null);
				}
				return BuiltinFunctions._instance;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001401A File Offset: 0x0001221A
		public string NameSpace
		{
			get
			{
				return "global";
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00014024 File Offset: 0x00012224
		private BuiltinFunctions()
		{
			this._opsBL = BuiltinFunctions.GetOpsMap(typeof(DvBool));
			this._opsI4 = BuiltinFunctions.GetOpsMap(typeof(DvInt4));
			this._opsI8 = BuiltinFunctions.GetOpsMap(typeof(DvInt8));
			this._opsTX = BuiltinFunctions.GetOpsMap(typeof(DvText));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001408C File Offset: 0x0001228C
		private static Dictionary<string, List<MethodInfo>> GetOpsMap(Type type)
		{
			Dictionary<string, List<MethodInfo>> dictionary = new Dictionary<string, List<MethodInfo>>();
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public))
			{
				if (methodInfo.Name.StartsWith("op_"))
				{
					List<MethodInfo> list;
					if (!dictionary.TryGetValue(methodInfo.Name, out list))
					{
						dictionary.Add(methodInfo.Name, list = new List<MethodInfo>());
					}
					list.Add(methodInfo);
				}
			}
			return dictionary;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000140FC File Offset: 0x000122FC
		private MethodInfo Op<TSrc, TDst>(Dictionary<string, List<MethodInfo>> ops, string name)
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
			return methodInfo;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00014190 File Offset: 0x00012390
		private static MethodInfo Id<T>()
		{
			Action<T> action = new Action<T>(BuiltinFunctions.Id<T>);
			return action.GetMethodInfo();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000141B0 File Offset: 0x000123B0
		private static void Id<T>(T src)
		{
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000141B4 File Offset: 0x000123B4
		public MethodInfo[] Lookup(string name)
		{
			switch (name)
			{
			case "pi":
				return FunctionProviderBase.Ret(new MethodInfo[] { FunctionProviderBase.Fn<double>(new Func<double>(BuiltinFunctions.Pi)) });
			case "na":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvInt4>(new Func<DvInt4, DvInt4>(BuiltinFunctions.NA)),
					FunctionProviderBase.Fn<DvInt8, DvInt8>(new Func<DvInt8, DvInt8>(BuiltinFunctions.NA)),
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.NA)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.NA)),
					FunctionProviderBase.Fn<DvBool, DvBool>(new Func<DvBool, DvBool>(BuiltinFunctions.NA)),
					FunctionProviderBase.Fn<DvText, DvText>(new Func<DvText, DvText>(BuiltinFunctions.NA))
				});
			case "default":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvInt4>(new Func<DvInt4, DvInt4>(BuiltinFunctions.Default)),
					FunctionProviderBase.Fn<DvInt8, DvInt8>(new Func<DvInt8, DvInt8>(BuiltinFunctions.Default)),
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Default)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.Default)),
					FunctionProviderBase.Fn<DvBool, DvBool>(new Func<DvBool, DvBool>(BuiltinFunctions.Default)),
					FunctionProviderBase.Fn<DvText, DvText>(new Func<DvText, DvText>(BuiltinFunctions.Default))
				});
			case "abs":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvInt4>(new Func<DvInt4, DvInt4>(DvInt4.Abs)),
					FunctionProviderBase.Fn<DvInt8, DvInt8>(new Func<DvInt8, DvInt8>(DvInt8.Abs)),
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(Math.Abs)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Abs))
				});
			case "sign":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvInt4>(new Func<DvInt4, DvInt4>(DvInt4.Sign)),
					FunctionProviderBase.Fn<DvInt8, DvInt8>(new Func<DvInt8, DvInt8>(DvInt8.Sign)),
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Sign)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.Sign))
				});
			case "exp":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Exp)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Exp))
				});
			case "ln":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Log)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Log))
				});
			case "log":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Log)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Log)),
					FunctionProviderBase.Fn<float, float, float>(new Func<float, float, float>(BuiltinFunctions.Log)),
					FunctionProviderBase.Fn<double, double, double>(new Func<double, double, double>(Math.Log))
				});
			case "deg":
			case "degrees":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Deg)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.Deg))
				});
			case "rad":
			case "radians":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Rad)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.Rad))
				});
			case "sin":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Sin)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(MathUtils.Sin))
				});
			case "sind":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.SinD)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.SinD))
				});
			case "cos":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Cos)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(MathUtils.Cos))
				});
			case "cosd":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.CosD)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.CosD))
				});
			case "tan":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Tan)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Tan))
				});
			case "tand":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.TanD)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.TanD))
				});
			case "asin":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Asin)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Asin))
				});
			case "acos":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Acos)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Acos))
				});
			case "atan":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Atan)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Atan))
				});
			case "atan2":
			case "atanyx":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float, float>(new Func<float, float, float>(BuiltinFunctions.Atan2)),
					FunctionProviderBase.Fn<double, double, double>(new Func<double, double, double>(Math.Atan2))
				});
			case "sinh":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Sinh)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Sinh))
				});
			case "cosh":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Cosh)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Cosh))
				});
			case "tanh":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Tanh)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Tanh))
				});
			case "sqrt":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Sqrt)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Sqrt))
				});
			case "trunc":
			case "truncate":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Truncate)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Truncate))
				});
			case "floor":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Floor)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Floor))
				});
			case "ceil":
			case "ceiling":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Ceiling)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Ceiling))
				});
			case "round":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.Round)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(Math.Round))
				});
			case "min":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvInt4, DvInt4>(new Func<DvInt4, DvInt4, DvInt4>(DvInt4.Min)),
					FunctionProviderBase.Fn<DvInt8, DvInt8, DvInt8>(new Func<DvInt8, DvInt8, DvInt8>(DvInt8.Min)),
					FunctionProviderBase.Fn<float, float, float>(new Func<float, float, float>(Math.Min)),
					FunctionProviderBase.Fn<double, double, double>(new Func<double, double, double>(Math.Min))
				});
			case "max":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvInt4, DvInt4>(new Func<DvInt4, DvInt4, DvInt4>(DvInt4.Max)),
					FunctionProviderBase.Fn<DvInt8, DvInt8, DvInt8>(new Func<DvInt8, DvInt8, DvInt8>(DvInt8.Max)),
					FunctionProviderBase.Fn<float, float, float>(new Func<float, float, float>(Math.Max)),
					FunctionProviderBase.Fn<double, double, double>(new Func<double, double, double>(Math.Max))
				});
			case "len":
				return FunctionProviderBase.Ret(new MethodInfo[] { FunctionProviderBase.Fn<DvText, DvInt4>(new Func<DvText, DvInt4>(BuiltinFunctions.Len)) });
			case "lower":
				return FunctionProviderBase.Ret(new MethodInfo[] { FunctionProviderBase.Fn<DvText, DvText>(new Func<DvText, DvText>(BuiltinFunctions.Lower)) });
			case "upper":
				return FunctionProviderBase.Ret(new MethodInfo[] { FunctionProviderBase.Fn<DvText, DvText>(new Func<DvText, DvText>(BuiltinFunctions.Upper)) });
			case "right":
				return FunctionProviderBase.Ret(new MethodInfo[] { FunctionProviderBase.Fn<DvText, DvInt4, DvText>(new Func<DvText, DvInt4, DvText>(BuiltinFunctions.Right)) });
			case "left":
				return FunctionProviderBase.Ret(new MethodInfo[] { FunctionProviderBase.Fn<DvText, DvInt4, DvText>(new Func<DvText, DvInt4, DvText>(BuiltinFunctions.Left)) });
			case "mid":
				return FunctionProviderBase.Ret(new MethodInfo[] { FunctionProviderBase.Fn<DvText, DvInt4, DvInt4, DvText>(new Func<DvText, DvInt4, DvInt4, DvText>(BuiltinFunctions.Mid)) });
			case "concat":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvText>(new Func<DvText>(BuiltinFunctions.Empty)),
					BuiltinFunctions.Id<DvText>(),
					FunctionProviderBase.Fn<DvText, DvText, DvText>(new Func<DvText, DvText, DvText>(BuiltinFunctions.Concat)),
					FunctionProviderBase.Fn<DvText, DvText, DvText, DvText>(new Func<DvText, DvText, DvText, DvText>(BuiltinFunctions.Concat)),
					FunctionProviderBase.Fn<DvText[], DvText>(new Func<DvText[], DvText>(BuiltinFunctions.Concat))
				});
			case "isna":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvBool>(new Func<DvInt4, DvBool>(BuiltinFunctions.IsNA)),
					FunctionProviderBase.Fn<DvInt8, DvBool>(new Func<DvInt8, DvBool>(BuiltinFunctions.IsNA)),
					FunctionProviderBase.Fn<float, DvBool>(new Func<float, DvBool>(BuiltinFunctions.IsNA)),
					FunctionProviderBase.Fn<double, DvBool>(new Func<double, DvBool>(BuiltinFunctions.IsNA)),
					FunctionProviderBase.Fn<DvBool, DvBool>(new Func<DvBool, DvBool>(BuiltinFunctions.IsNA)),
					FunctionProviderBase.Fn<DvText, DvBool>(new Func<DvText, DvBool>(BuiltinFunctions.IsNA))
				});
			case "bool":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvText, DvBool>(new Func<DvText, DvBool>(BuiltinFunctions.ToBL)),
					BuiltinFunctions.Id<DvBool>()
				});
			case "int":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					this.Op<DvInt8, DvInt4>(this._opsI4, "op_Explicit"),
					this.Op<float, DvInt4>(this._opsI4, "op_Explicit"),
					this.Op<double, DvInt4>(this._opsI4, "op_Explicit"),
					this.Op<DvBool, DvInt4>(this._opsI4, "op_Explicit"),
					FunctionProviderBase.Fn<DvText, DvInt4>(new Func<DvText, DvInt4>(BuiltinFunctions.ToI4)),
					BuiltinFunctions.Id<DvInt4>()
				});
			case "long":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					this.Op<DvInt4, DvInt8>(this._opsI8, "op_Implicit"),
					this.Op<float, DvInt8>(this._opsI8, "op_Explicit"),
					this.Op<double, DvInt8>(this._opsI8, "op_Explicit"),
					this.Op<DvBool, DvInt8>(this._opsI8, "op_Explicit"),
					FunctionProviderBase.Fn<DvText, DvInt8>(new Func<DvText, DvInt8>(BuiltinFunctions.ToI8)),
					BuiltinFunctions.Id<DvInt8>()
				});
			case "float":
			case "single":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					this.Op<DvInt4, float>(this._opsI4, "op_Explicit"),
					this.Op<DvInt8, float>(this._opsI8, "op_Explicit"),
					FunctionProviderBase.Fn<float, float>(new Func<float, float>(BuiltinFunctions.ToR4)),
					FunctionProviderBase.Fn<double, float>(new Func<double, float>(BuiltinFunctions.ToR4)),
					this.Op<DvBool, float>(this._opsBL, "op_Explicit"),
					FunctionProviderBase.Fn<DvText, float>(new Func<DvText, float>(BuiltinFunctions.ToR4))
				});
			case "double":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					this.Op<DvInt4, double>(this._opsI4, "op_Explicit"),
					this.Op<DvInt8, double>(this._opsI8, "op_Explicit"),
					FunctionProviderBase.Fn<float, double>(new Func<float, double>(BuiltinFunctions.ToR8)),
					FunctionProviderBase.Fn<double, double>(new Func<double, double>(BuiltinFunctions.ToR8)),
					this.Op<DvBool, double>(this._opsBL, "op_Explicit"),
					FunctionProviderBase.Fn<DvText, double>(new Func<DvText, double>(BuiltinFunctions.ToR8))
				});
			case "text":
				return FunctionProviderBase.Ret(new MethodInfo[]
				{
					FunctionProviderBase.Fn<DvInt4, DvText>(new Func<DvInt4, DvText>(BuiltinFunctions.ToTX)),
					FunctionProviderBase.Fn<DvInt8, DvText>(new Func<DvInt8, DvText>(BuiltinFunctions.ToTX)),
					FunctionProviderBase.Fn<float, DvText>(new Func<float, DvText>(BuiltinFunctions.ToTX)),
					FunctionProviderBase.Fn<double, DvText>(new Func<double, DvText>(BuiltinFunctions.ToTX)),
					FunctionProviderBase.Fn<DvBool, DvText>(new Func<DvBool, DvText>(BuiltinFunctions.ToTX)),
					BuiltinFunctions.Id<DvText>()
				});
			}
			return null;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00015224 File Offset: 0x00013424
		public object ResolveToConstant(string name, MethodInfo fn, object[] values)
		{
			Contracts.CheckNonEmpty(name, "name");
			Contracts.CheckValue<MethodInfo>(fn, "fn");
			Contracts.CheckParam(Utils.Size<object>(values) > 0, "values", "Expected values to have positive length");
			Contracts.CheckParam(!values.All((object x) => x != null), "values", "Expected values to contain at least one null");
			if (name == null || (!(name == "na") && !(name == "default")))
			{
				for (int i = 0; i < values.Length; i++)
				{
					if (FunctionProviderBase.IsNA(values[i]))
					{
						return FunctionProviderBase.GetNA(fn.ReturnType);
					}
				}
				return null;
			}
			bool flag = name == "na";
			Type returnType = fn.ReturnType;
			if (returnType == typeof(DvInt4))
			{
				return flag ? DvInt4.NA : default(DvInt4);
			}
			if (returnType == typeof(DvInt8))
			{
				return flag ? DvInt8.NA : default(DvInt8);
			}
			if (returnType == typeof(float))
			{
				return flag ? float.NaN : 0f;
			}
			if (returnType == typeof(double))
			{
				return flag ? double.NaN : 0.0;
			}
			if (returnType == typeof(DvBool))
			{
				return flag ? DvBool.NA : default(DvBool);
			}
			if (returnType == typeof(DvText))
			{
				return flag ? DvText.NA : default(DvText);
			}
			return null;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000153FA File Offset: 0x000135FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Pi()
		{
			return 3.141592653589793;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00015405 File Offset: 0x00013605
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvInt4 NA(DvInt4 a)
		{
			return DvInt4.NA;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001540C File Offset: 0x0001360C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvInt8 NA(DvInt8 a)
		{
			return DvInt8.NA;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00015413 File Offset: 0x00013613
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float NA(float a)
		{
			return float.NaN;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001541A File Offset: 0x0001361A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double NA(double a)
		{
			return double.NaN;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00015425 File Offset: 0x00013625
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool NA(DvBool a)
		{
			return DvBool.NA;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001542C File Offset: 0x0001362C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText NA(DvText a)
		{
			return DvText.NA;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00015434 File Offset: 0x00013634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvInt4 Default(DvInt4 a)
		{
			return default(DvInt4);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001544C File Offset: 0x0001364C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvInt8 Default(DvInt8 a)
		{
			return default(DvInt8);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00015462 File Offset: 0x00013662
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Default(float a)
		{
			return 0f;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00015469 File Offset: 0x00013669
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Default(double a)
		{
			return 0.0;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00015474 File Offset: 0x00013674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool Default(DvBool a)
		{
			return default(DvBool);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001548C File Offset: 0x0001368C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Default(DvText a)
		{
			return default(DvText);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000154A2 File Offset: 0x000136A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sign(float a)
		{
			if (a > 0f)
			{
				return 1f;
			}
			if (a >= 0f)
			{
				return a;
			}
			return -1f;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000154C1 File Offset: 0x000136C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Sign(double a)
		{
			if (a > 0.0)
			{
				return 1.0;
			}
			if (a >= 0.0)
			{
				return a;
			}
			return -1.0;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000154F0 File Offset: 0x000136F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Pow(float a, float b)
		{
			return (float)Math.Pow((double)a, (double)b);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000154FC File Offset: 0x000136FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Exp(float a)
		{
			return (float)Math.Exp((double)a);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00015506 File Offset: 0x00013706
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Log(float a)
		{
			return (float)Math.Log((double)a);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00015510 File Offset: 0x00013710
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Log(float a, float b)
		{
			return (float)Math.Log((double)a, (double)b);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001551C File Offset: 0x0001371C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Deg(float a)
		{
			return (float)((double)a * 57.29577951308232);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001552B File Offset: 0x0001372B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Deg(double a)
		{
			return a * 57.29577951308232;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00015538 File Offset: 0x00013738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Rad(float a)
		{
			return (float)((double)a * 0.017453292519943295);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00015547 File Offset: 0x00013747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Rad(double a)
		{
			return a * 0.017453292519943295;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00015554 File Offset: 0x00013754
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sin(float a)
		{
			float num = (float)Math.Sin((double)a);
			if (Math.Abs(num) <= 1f)
			{
				return num;
			}
			return float.NaN;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00015580 File Offset: 0x00013780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SinD(float a)
		{
			float num = (float)Math.Sin((double)a * 0.017453292519943295);
			if (Math.Abs(num) <= 1f)
			{
				return num;
			}
			return float.NaN;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000155B4 File Offset: 0x000137B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double SinD(double a)
		{
			return MathUtils.Sin(a * 0.017453292519943295);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000155C8 File Offset: 0x000137C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cos(float a)
		{
			float num = (float)Math.Cos((double)a);
			if (Math.Abs(num) <= 1f)
			{
				return num;
			}
			return float.NaN;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000155F4 File Offset: 0x000137F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CosD(float a)
		{
			float num = (float)Math.Cos((double)a * 0.017453292519943295);
			if (Math.Abs(num) <= 1f)
			{
				return num;
			}
			return float.NaN;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00015628 File Offset: 0x00013828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double CosD(double a)
		{
			return MathUtils.Cos(a * 0.017453292519943295);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001563A File Offset: 0x0001383A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Tan(float a)
		{
			return (float)Math.Tan((double)a);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00015644 File Offset: 0x00013844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float TanD(float a)
		{
			return (float)Math.Tan((double)a * 0.017453292519943295);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00015658 File Offset: 0x00013858
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double TanD(double a)
		{
			return Math.Tan(a * 0.017453292519943295);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001566A File Offset: 0x0001386A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Asin(float a)
		{
			return (float)Math.Asin((double)a);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00015674 File Offset: 0x00013874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Acos(float a)
		{
			return (float)Math.Acos((double)a);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001567E File Offset: 0x0001387E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Atan(float a)
		{
			return (float)Math.Atan((double)a);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00015688 File Offset: 0x00013888
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Atan2(float a, float b)
		{
			return (float)Math.Atan2((double)a, (double)b);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00015694 File Offset: 0x00013894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sinh(float a)
		{
			return (float)Math.Sinh((double)a);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001569E File Offset: 0x0001389E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cosh(float a)
		{
			return (float)Math.Cosh((double)a);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000156A8 File Offset: 0x000138A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Tanh(float a)
		{
			return (float)Math.Tanh((double)a);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000156B2 File Offset: 0x000138B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sqrt(float a)
		{
			return (float)Math.Sqrt((double)a);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000156BC File Offset: 0x000138BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Truncate(float a)
		{
			return (float)Math.Truncate((double)a);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000156C6 File Offset: 0x000138C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Floor(float a)
		{
			return (float)Math.Floor((double)a);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000156D0 File Offset: 0x000138D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Ceiling(float a)
		{
			return (float)Math.Ceiling((double)a);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000156DA File Offset: 0x000138DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float a)
		{
			return (float)Math.Round((double)a);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000156E4 File Offset: 0x000138E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Lower(DvText a)
		{
			if (!a.HasChars)
			{
				return a;
			}
			return new DvText(a.ToString().ToLowerInvariant());
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00015708 File Offset: 0x00013908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Upper(DvText a)
		{
			if (!a.HasChars)
			{
				return a;
			}
			return new DvText(a.ToString().ToUpperInvariant());
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001572C File Offset: 0x0001392C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Empty()
		{
			return DvText.Empty;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00015734 File Offset: 0x00013934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Concat(DvText a, DvText b)
		{
			if (a.IsNA || b.IsNA)
			{
				return DvText.NA;
			}
			if (a.IsEmpty)
			{
				return b;
			}
			if (b.IsEmpty)
			{
				return a;
			}
			return new DvText(a.ToString() + b.ToString());
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00015794 File Offset: 0x00013994
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Concat(DvText a, DvText b, DvText c)
		{
			if (a.IsNA || b.IsNA || c.IsNA)
			{
				return DvText.NA;
			}
			return new DvText(a.ToString() + b.ToString() + c.ToString());
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000157F4 File Offset: 0x000139F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Concat(DvText[] a)
		{
			int num = 0;
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i].IsNA)
				{
					return DvText.NA;
				}
				num += a[i].Length;
				if (num < 0)
				{
					return DvText.NA;
				}
			}
			if (num == 0)
			{
				return DvText.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			for (int j = 0; j < a.Length; j++)
			{
				a[j].AddToStringBuilder(stringBuilder);
			}
			return new DvText(stringBuilder.ToString());
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00015879 File Offset: 0x00013A79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvInt4 Len(DvText a)
		{
			if (a.IsNA)
			{
				return DvInt4.NA;
			}
			return a.Length;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00015896 File Offset: 0x00013A96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int NormalizeIndex(int i, int len)
		{
			if (i < 0)
			{
				if ((i += len) < 0)
				{
					return 0;
				}
			}
			else if (i > len)
			{
				return len;
			}
			return i;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000158AE File Offset: 0x00013AAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Right(DvText a, DvInt4 min)
		{
			if (min.IsNA)
			{
				return DvText.NA;
			}
			if (!a.HasChars)
			{
				return a;
			}
			return a.SubSpan(BuiltinFunctions.NormalizeIndex(min.RawValue, a.Length));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000158E4 File Offset: 0x00013AE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Left(DvText a, DvInt4 lim)
		{
			if (lim.IsNA)
			{
				return DvText.NA;
			}
			if (!a.HasChars)
			{
				return a;
			}
			return a.SubSpan(0, BuiltinFunctions.NormalizeIndex(lim.RawValue, a.Length));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001591C File Offset: 0x00013B1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText Mid(DvText a, DvInt4 min, DvInt4 lim)
		{
			if (min.IsNA || lim.IsNA)
			{
				return DvText.NA;
			}
			if (!a.HasChars)
			{
				return a;
			}
			int num = BuiltinFunctions.NormalizeIndex(min.RawValue, a.Length);
			int num2 = BuiltinFunctions.NormalizeIndex(lim.RawValue, a.Length);
			if (num >= num2)
			{
				return DvText.Empty;
			}
			return a.SubSpan(num, num2);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00015987 File Offset: 0x00013B87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool IsNA(DvBool a)
		{
			return a.IsNA;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00015995 File Offset: 0x00013B95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool IsNA(DvText a)
		{
			return a.IsNA;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000159A3 File Offset: 0x00013BA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool IsNA(DvInt4 a)
		{
			return a.IsNA;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000159B1 File Offset: 0x00013BB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool IsNA(DvInt8 a)
		{
			return a.IsNA;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000159BF File Offset: 0x00013BBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool IsNA(float a)
		{
			return float.IsNaN(a);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000159CC File Offset: 0x00013BCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvBool IsNA(double a)
		{
			return double.IsNaN(a);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000159DC File Offset: 0x00013BDC
		public static DvBool ToBL(DvText a)
		{
			DvBool dvBool = default(DvBool);
			Conversions.Instance.Convert(ref a, ref dvBool);
			return dvBool;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00015A00 File Offset: 0x00013C00
		public static DvInt4 ToI4(DvText a)
		{
			DvInt4 dvInt = default(DvInt4);
			Conversions.Instance.Convert(ref a, ref dvInt);
			return dvInt;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00015A24 File Offset: 0x00013C24
		public static DvInt8 ToI8(DvText a)
		{
			DvInt8 dvInt = default(DvInt8);
			Conversions.Instance.Convert(ref a, ref dvInt);
			return dvInt;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00015A48 File Offset: 0x00013C48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ToR4(float a)
		{
			return a;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00015A4C File Offset: 0x00013C4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ToR4(double a)
		{
			return (float)a;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00015A50 File Offset: 0x00013C50
		public static float ToR4(DvText a)
		{
			float num = 0f;
			Conversions.Instance.Convert(ref a, ref num);
			return num;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00015A72 File Offset: 0x00013C72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ToR8(float a)
		{
			return (double)a;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00015A76 File Offset: 0x00013C76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ToR8(double a)
		{
			return a;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00015A7C File Offset: 0x00013C7C
		public static double ToR8(DvText a)
		{
			double num = 0.0;
			Conversions.Instance.Convert(ref a, ref num);
			return num;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00015AA4 File Offset: 0x00013CA4
		public static DvText ToTX(DvInt4 src)
		{
			if (!src.IsNA)
			{
				return new DvText(src.RawValue.ToString());
			}
			return DvText.NA;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00015AD4 File Offset: 0x00013CD4
		public static DvText ToTX(DvInt8 src)
		{
			if (!src.IsNA)
			{
				return new DvText(src.RawValue.ToString());
			}
			return DvText.NA;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00015B04 File Offset: 0x00013D04
		public static DvText ToTX(float src)
		{
			if (!TypeUtils.IsNA(src))
			{
				return new DvText(src.ToString("R", CultureInfo.InvariantCulture));
			}
			return DvText.NA;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00015B2A File Offset: 0x00013D2A
		public static DvText ToTX(double src)
		{
			if (!TypeUtils.IsNA(src))
			{
				return new DvText(src.ToString("G17", CultureInfo.InvariantCulture));
			}
			return DvText.NA;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00015B50 File Offset: 0x00013D50
		public static DvText ToTX(DvBool src)
		{
			if (src.IsFalse)
			{
				return new DvText("0");
			}
			if (src.IsTrue)
			{
				return new DvText("1");
			}
			return DvText.NA;
		}

		// Token: 0x04000183 RID: 387
		private static volatile BuiltinFunctions _instance;

		// Token: 0x04000184 RID: 388
		internal readonly Dictionary<string, List<MethodInfo>> _opsBL;

		// Token: 0x04000185 RID: 389
		internal readonly Dictionary<string, List<MethodInfo>> _opsI4;

		// Token: 0x04000186 RID: 390
		internal readonly Dictionary<string, List<MethodInfo>> _opsI8;

		// Token: 0x04000187 RID: 391
		internal readonly Dictionary<string, List<MethodInfo>> _opsTX;
	}
}
