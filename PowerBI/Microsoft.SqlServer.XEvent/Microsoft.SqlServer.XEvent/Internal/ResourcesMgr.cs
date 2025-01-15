using System;
using System.Globalization;
using System.Resources;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x0200004A RID: 74
	public sealed class ResourcesMgr
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003A74 File Offset: 0x00003A74
		public static ResourceManager ResManager
		{
			get
			{
				if (object.ReferenceEquals(ResourcesMgr.sm_resources, null))
				{
					ResourcesMgr.sm_resources = new ResourceManager("Microsoft.SqlServer.XEvent", typeof(ResourcesMgr).Assembly);
				}
				return ResourcesMgr.sm_resources;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003AD8 File Offset: 0x00003AD8
		public static string GetString(string name, CultureInfo culture)
		{
			return ResourcesMgr.ResManager.GetString(name, culture);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003AB8 File Offset: 0x00003AB8
		public static string GetString(string name)
		{
			return ResourcesMgr.ResManager.GetString(name);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003AF8 File Offset: 0x00003AF8
		public unsafe static char* GetNativeString(string name)
		{
			char* ptr = null;
			string @string = ResourcesMgr.ResManager.GetString(name);
			if (@string != null)
			{
				ptr = (char*)Marshal.StringToHGlobalUni(@string).ToPointer();
			}
			return ptr;
		}

		// Token: 0x0400013B RID: 315
		private static ResourceManager sm_resources;
	}
}
