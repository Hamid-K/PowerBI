using System;
using System.Reflection;

namespace Microsoft.MachineLearning.Data.Expr
{
	// Token: 0x020000AF RID: 175
	public interface IFunctionProvider
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600033B RID: 827
		string NameSpace { get; }

		// Token: 0x0600033C RID: 828
		MethodInfo[] Lookup(string name);

		// Token: 0x0600033D RID: 829
		object ResolveToConstant(string name, MethodInfo meth, object[] values);
	}
}
