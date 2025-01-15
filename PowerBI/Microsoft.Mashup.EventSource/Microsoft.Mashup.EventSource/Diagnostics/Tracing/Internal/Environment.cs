using System;
using System.Resources;
using Microsoft.Reflection;

namespace Microsoft.Diagnostics.Tracing.Internal
{
	// Token: 0x0200007B RID: 123
	internal static class Environment
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000E588 File Offset: 0x0000C788
		public static int TickCount
		{
			get
			{
				return Environment.TickCount;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000E590 File Offset: 0x0000C790
		public static string GetResourceString(string key, params object[] args)
		{
			string @string = Environment.rm.GetString(key);
			if (@string != null)
			{
				return string.Format(@string, args);
			}
			string text = string.Empty;
			foreach (object obj in args)
			{
				if (text != string.Empty)
				{
					text += ", ";
				}
				text += obj.ToString();
			}
			return key + " (" + text + ")";
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E607 File Offset: 0x0000C807
		public static string GetRuntimeResourceString(string key, params object[] args)
		{
			return Environment.GetResourceString(key, args);
		}

		// Token: 0x0400017A RID: 378
		public static readonly string NewLine = Environment.NewLine;

		// Token: 0x0400017B RID: 379
		private static ResourceManager rm = new ResourceManager("Microsoft.Diagnostics.Tracing.Messages", typeof(Environment).Assembly());
	}
}
