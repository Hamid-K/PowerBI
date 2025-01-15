using System;
using System.Collections.Generic;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000402 RID: 1026
	internal abstract class StandaloneFunc : PropertyFunc
	{
		// Token: 0x060023E4 RID: 9188
		public abstract object Invoke(object[] args);

		// Token: 0x060023E5 RID: 9189 RVA: 0x0006E23B File Offset: 0x0006C43B
		public override object Invoke(IReadablePropertyContext context, object[] args)
		{
			return this.Invoke(args);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0006E244 File Offset: 0x0006C444
		protected PropertyFunc Collapse(IList<PropertyExpression> args)
		{
			if (args.Count == 1)
			{
				PropertyExpression propertyExpression = args[0];
				args.Clear();
				foreach (PropertyExpression propertyExpression2 in propertyExpression.Children)
				{
					args.Add(propertyExpression2);
				}
				return propertyExpression.Func;
			}
			return this;
		}
	}
}
