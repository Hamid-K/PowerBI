using System;
using System.Resources;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x02000008 RID: 8
	public sealed class ResMgr
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00001780 File Offset: 0x00001780
		public static ResourceManager ResManager
		{
			get
			{
				if (object.ReferenceEquals(ResMgr.sm_resources, null))
				{
					ResMgr.sm_resources = new ResourceManager("Microsoft.SqlServer.XE.Core", typeof(ResMgr).Assembly);
				}
				return ResMgr.sm_resources;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000017C4 File Offset: 0x000017C4
		public static string GetString(string name)
		{
			return ResMgr.ResManager.GetString(name);
		}

		// Token: 0x0400003D RID: 61
		private static ResourceManager sm_resources;
	}
}
