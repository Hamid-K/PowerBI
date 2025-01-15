using System;
using System.Reflection;

namespace Microsoft.MachineLearning.Data.Expr
{
	// Token: 0x020000AE RID: 174
	public abstract class FunctionProviderBase
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00013E30 File Offset: 0x00012030
		protected static bool IsNA(object v)
		{
			if (v == null)
			{
				return false;
			}
			Type type = v.GetType();
			if (type == typeof(DvInt4))
			{
				return ((DvInt4)v).IsNA;
			}
			if (type == typeof(DvInt8))
			{
				return ((DvInt8)v).IsNA;
			}
			if (type == typeof(float))
			{
				return TypeUtils.IsNA((float)v);
			}
			if (type == typeof(double))
			{
				return TypeUtils.IsNA((double)v);
			}
			if (type == typeof(DvBool))
			{
				return ((DvBool)v).IsNA;
			}
			return type == typeof(DvText) && ((DvText)v).IsNA;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00013F0C File Offset: 0x0001210C
		protected static object GetNA(Type type)
		{
			if (type == typeof(DvInt4))
			{
				return DvInt4.NA;
			}
			if (type == typeof(DvInt8))
			{
				return DvInt8.NA;
			}
			if (type == typeof(float))
			{
				return float.NaN;
			}
			if (type == typeof(double))
			{
				return double.NaN;
			}
			if (type == typeof(DvBool))
			{
				return DvBool.NA;
			}
			if (type == typeof(DvText))
			{
				return DvText.NA;
			}
			return null;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00013FCC File Offset: 0x000121CC
		protected static MethodInfo[] Ret(params MethodInfo[] funcs)
		{
			return funcs;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00013FCF File Offset: 0x000121CF
		protected static MethodInfo Fn<T1>(Func<T1> fn)
		{
			return fn.GetMethodInfo();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00013FD7 File Offset: 0x000121D7
		protected static MethodInfo Fn<T1, T2>(Func<T1, T2> fn)
		{
			return fn.GetMethodInfo();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00013FDF File Offset: 0x000121DF
		protected static MethodInfo Fn<T1, T2, T3>(Func<T1, T2, T3> fn)
		{
			return fn.GetMethodInfo();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00013FE7 File Offset: 0x000121E7
		protected static MethodInfo Fn<T1, T2, T3, T4>(Func<T1, T2, T3, T4> fn)
		{
			return fn.GetMethodInfo();
		}
	}
}
