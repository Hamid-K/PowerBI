using System;

namespace NLog
{
	// Token: 0x02000018 RID: 24
	[Obsolete("Use NestedDiagnosticsContext class instead. Marked obsolete on NLog 2.0")]
	public static class NDC
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000085C7 File Offset: 0x000067C7
		public static string TopMessage
		{
			get
			{
				return NestedDiagnosticsContext.TopMessage;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000085CE File Offset: 0x000067CE
		public static object TopObject
		{
			get
			{
				return NestedDiagnosticsContext.TopObject;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000085D5 File Offset: 0x000067D5
		public static IDisposable Push(string text)
		{
			return NestedDiagnosticsContext.Push(text);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000085DD File Offset: 0x000067DD
		public static string Pop()
		{
			return NestedDiagnosticsContext.Pop();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000085E4 File Offset: 0x000067E4
		public static object PopObject()
		{
			return NestedDiagnosticsContext.PopObject();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000085EB File Offset: 0x000067EB
		public static void Clear()
		{
			NestedDiagnosticsContext.Clear();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000085F2 File Offset: 0x000067F2
		public static string[] GetAllMessages()
		{
			return NestedDiagnosticsContext.GetAllMessages();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000085F9 File Offset: 0x000067F9
		public static object[] GetAllObjects()
		{
			return NestedDiagnosticsContext.GetAllObjects();
		}
	}
}
