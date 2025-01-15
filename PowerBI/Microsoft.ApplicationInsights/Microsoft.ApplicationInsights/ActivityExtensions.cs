using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x0200001B RID: 27
	internal static class ActivityExtensions
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x0000627A File Offset: 0x0000447A
		public static bool TryRun(Action action)
		{
			if (!ActivityExtensions.isInitialized)
			{
				ActivityExtensions.isAvailable = ActivityExtensions.Initialize();
			}
			if (ActivityExtensions.isAvailable)
			{
				action();
			}
			return ActivityExtensions.isAvailable;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000062A0 File Offset: 0x000044A0
		internal static string GetOperationName(this Activity activity)
		{
			return activity.Tags.FirstOrDefault((KeyValuePair<string, string> tag) => tag.Key == "OperationName").Value;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000062DF File Offset: 0x000044DF
		internal static void SetOperationName(this Activity activity, string operationName)
		{
			activity.AddTag("OperationName", operationName);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000062F0 File Offset: 0x000044F0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool Initialize()
		{
			bool flag;
			try
			{
				Assembly.Load(new AssemblyName("System.Diagnostics.DiagnosticSource, Version=4.0.3.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"));
				flag = true;
			}
			catch (FileNotFoundException)
			{
				flag = false;
			}
			catch (FileLoadException)
			{
				flag = false;
			}
			finally
			{
				ActivityExtensions.isInitialized = true;
			}
			return flag;
		}

		// Token: 0x0400007D RID: 125
		private const string OperationNameTag = "OperationName";

		// Token: 0x0400007E RID: 126
		private static bool isInitialized;

		// Token: 0x0400007F RID: 127
		private static bool isAvailable;
	}
}
