using System;
using System.Collections.Generic;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003FF RID: 1023
	internal class FuncArguments : List<PropertyExpression>
	{
		// Token: 0x060023D6 RID: 9174 RVA: 0x0006E05C File Offset: 0x0006C25C
		public bool GetLiteralArg<T>(int index, out T result)
		{
			PropertyExpression propertyExpression = base[index];
			if (propertyExpression.Func is Literal)
			{
				result = (T)((object)propertyExpression.Eval(null));
				return true;
			}
			result = default(T);
			return false;
		}
	}
}
