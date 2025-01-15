using System;
using System.Security;

namespace NLog.Internal
{
	// Token: 0x02000115 RID: 277
	internal static class EnvironmentHelper
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x000244BF File Offset: 0x000226BF
		internal static string NewLine
		{
			get
			{
				return Environment.NewLine;
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000244C8 File Offset: 0x000226C8
		internal static string GetMachineName()
		{
			string text;
			try
			{
				text = Environment.MachineName;
			}
			catch (SecurityException)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000244F8 File Offset: 0x000226F8
		internal static string GetSafeEnvironmentVariable(string name)
		{
			string text;
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable(name);
				if (string.IsNullOrEmpty(environmentVariable))
				{
					text = null;
				}
				else
				{
					text = environmentVariable;
				}
			}
			catch (SecurityException)
			{
				text = null;
			}
			return text;
		}
	}
}
