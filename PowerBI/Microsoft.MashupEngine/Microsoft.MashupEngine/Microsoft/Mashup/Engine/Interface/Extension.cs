using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200007C RID: 124
	public static class Extension
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000316E File Offset: 0x0000136E
		public static string[] Modules
		{
			get
			{
				return Extension.modules;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00003175 File Offset: 0x00001375
		public static void AddModule(string moduleName)
		{
			Extension.modules = new List<string>(Extension.modules) { moduleName }.ToArray();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00003192 File Offset: 0x00001392
		public static void RemoveModule(string moduleName)
		{
			List<string> list = new List<string>(Extension.modules);
			list.Remove(moduleName);
			Extension.modules = list.ToArray();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000031B0 File Offset: 0x000013B0
		public static bool IsExtensionOnly(string moduleName)
		{
			return Extension.extensionOnlyModules.Contains(moduleName);
		}

		// Token: 0x04000155 RID: 341
		private static string[] modules = new string[] { "Crypto", "Extensibility", "Action", "Delta", "DataSource", "Environment", "OpenApi", "ParallelEvaluation" };

		// Token: 0x04000156 RID: 342
		private static string[] extensionOnlyModules = new string[] { "Crypto", "Extensibility", "ParallelEvaluation", "SqlDatabase" };
	}
}
