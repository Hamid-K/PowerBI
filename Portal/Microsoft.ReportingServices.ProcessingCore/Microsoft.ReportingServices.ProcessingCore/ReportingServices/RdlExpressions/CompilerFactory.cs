using System;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200055D RID: 1373
	internal static class CompilerFactory
	{
		// Token: 0x06004A2A RID: 18986 RVA: 0x00139438 File Offset: 0x00137638
		internal static ICompiler CreateCompiler()
		{
			return new NetFullCompiler();
		}
	}
}
