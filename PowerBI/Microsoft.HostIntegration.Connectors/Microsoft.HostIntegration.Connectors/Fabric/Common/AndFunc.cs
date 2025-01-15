using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000406 RID: 1030
	internal class AndFunc : StandaloneFunc
	{
		// Token: 0x060023F2 RID: 9202 RVA: 0x0006E31E File Offset: 0x0006C51E
		private AndFunc()
		{
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x0006E350 File Offset: 0x0006C550
		public override object Invoke(object[] args)
		{
			foreach (object obj in args)
			{
				if (!(bool)obj)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0006E38C File Offset: 0x0006C58C
		internal override object Invoke(IReadablePropertyContext context, FuncArguments args)
		{
			foreach (PropertyExpression propertyExpression in args)
			{
				object obj = propertyExpression.Eval(context);
				if (obj == null || !(bool)obj)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0006E3F8 File Offset: 0x0006C5F8
		public override PropertyFunc Bind(FuncArguments args)
		{
			for (int i = args.Count - 1; i >= 0; i--)
			{
				bool flag;
				if (args.GetLiteralArg<bool>(i, out flag))
				{
					if (!flag)
					{
						return new Literal(false);
					}
					args.RemoveAt(i);
				}
			}
			return base.Collapse(args);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0006E440 File Offset: 0x0006C640
		public override string ToString()
		{
			return "and";
		}

		// Token: 0x0400163D RID: 5693
		public static readonly AndFunc Singleton = new AndFunc();
	}
}
