using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000407 RID: 1031
	internal class OrFunc : StandaloneFunc
	{
		// Token: 0x060023F8 RID: 9208 RVA: 0x0006E31E File Offset: 0x0006C51E
		private OrFunc()
		{
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x0006E454 File Offset: 0x0006C654
		public override object Invoke(object[] args)
		{
			foreach (object obj in args)
			{
				if (obj != null && (bool)obj)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x0006E494 File Offset: 0x0006C694
		internal override object Invoke(IReadablePropertyContext context, FuncArguments args)
		{
			foreach (PropertyExpression propertyExpression in args)
			{
				bool flag = (bool)propertyExpression.Eval(context);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0006E4FC File Offset: 0x0006C6FC
		public override PropertyFunc Bind(FuncArguments args)
		{
			for (int i = args.Count - 1; i >= 0; i--)
			{
				bool flag;
				if (args.GetLiteralArg<bool>(i, out flag))
				{
					if (flag)
					{
						return new Literal(true);
					}
					args.RemoveAt(i);
				}
			}
			return base.Collapse(args);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0006E544 File Offset: 0x0006C744
		public override string ToString()
		{
			return "or";
		}

		// Token: 0x0400163E RID: 5694
		public static readonly OrFunc Singleton = new OrFunc();
	}
}
