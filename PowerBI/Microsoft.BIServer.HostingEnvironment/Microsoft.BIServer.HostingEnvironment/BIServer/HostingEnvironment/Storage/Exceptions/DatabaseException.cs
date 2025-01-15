using System;

namespace Microsoft.BIServer.HostingEnvironment.Storage.Exceptions
{
	// Token: 0x02000026 RID: 38
	public class DatabaseException : Exception
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00004B6B File Offset: 0x00002D6B
		public static void Assert(bool condition, string formatStr, params object[] formatParams)
		{
			if (!condition)
			{
				string text = string.Format(formatStr, formatParams);
				Logger.Error(text, Array.Empty<object>());
				throw new DatabaseException(text);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004B88 File Offset: 0x00002D88
		public DatabaseException(string message)
			: base(message)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004B91 File Offset: 0x00002D91
		public DatabaseException()
		{
		}
	}
}
