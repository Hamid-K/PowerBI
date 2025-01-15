using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004DF RID: 1247
	internal interface IExpressionHostAssemblyHolder
	{
		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x06003ECC RID: 16076
		ObjectType ObjectType { get; }

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x06003ECD RID: 16077
		string ExprHostAssemblyName { get; }

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06003ECE RID: 16078
		// (set) Token: 0x06003ECF RID: 16079
		byte[] CompiledCode { get; set; }

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x06003ED0 RID: 16080
		// (set) Token: 0x06003ED1 RID: 16081
		bool CompiledCodeGeneratedWithRefusedPermissions { get; set; }

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x06003ED2 RID: 16082
		// (set) Token: 0x06003ED3 RID: 16083
		List<string> CodeModules { get; set; }

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x06003ED4 RID: 16084
		// (set) Token: 0x06003ED5 RID: 16085
		List<CodeClass> CodeClasses { get; set; }
	}
}
