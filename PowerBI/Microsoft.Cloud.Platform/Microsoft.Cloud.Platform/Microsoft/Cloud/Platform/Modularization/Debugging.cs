using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000DC RID: 220
	public static class Debugging
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x00015CB0 File Offset: 0x00013EB0
		public static void AddApplicationRoot(ApplicationRoot appRoot)
		{
			if (ExtendedDiagnostics.IsDebuggerAttached())
			{
				List<ApplicationRoot> applicationRoots = Debugging.ApplicationRoots;
				lock (applicationRoots)
				{
					Debugging.ApplicationRoots.Add(appRoot);
				}
			}
		}

		// Token: 0x0400022B RID: 555
		private static List<ApplicationRoot> ApplicationRoots = new List<ApplicationRoot>();
	}
}
