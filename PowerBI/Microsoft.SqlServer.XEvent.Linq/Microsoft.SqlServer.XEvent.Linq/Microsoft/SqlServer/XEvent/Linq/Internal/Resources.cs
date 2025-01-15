using System;
using System.Resources;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x020000DD RID: 221
	internal static class Resources
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0001CEFC File Offset: 0x0001CEFC
		private static ResourceManager ResManager
		{
			get
			{
				if (Resources.sm_resources == null)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.SqlServer.XEvent.Linq", typeof(Resources).Assembly);
					Resources.sm_resources = resourceManager;
				}
				return Resources.sm_resources;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001CF38 File Offset: 0x0001CF38
		public static string GetString(string name)
		{
			return Resources.ResManager.GetString(name);
		}

		// Token: 0x040002B8 RID: 696
		private static ResourceManager sm_resources;
	}
}
