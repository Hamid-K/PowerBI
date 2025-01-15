using System;
using System.CodeDom.Compiler;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B1 RID: 177
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class Base
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00016025 File Offset: 0x00014225
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001602D File Offset: 0x0001422D
		public string baseType { get; set; }

		// Token: 0x06000563 RID: 1379 RVA: 0x00016036 File Offset: 0x00014236
		public Base()
			: this("AI.Base", "Base")
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00016048 File Offset: 0x00014248
		protected Base(string fullName, string name)
		{
			this.baseType = "";
		}
	}
}
