using System;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration.Exceptions
{
	// Token: 0x0200003C RID: 60
	public class ConfigException : Exception
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000830B File Offset: 0x0000650B
		public static void Assert(bool condition, string formatStr, params object[] formatParams)
		{
			if (!condition)
			{
				string text = string.Format(formatStr, formatParams);
				Logger.Error(text, Array.Empty<object>());
				throw new ConfigException(text);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008328 File Offset: 0x00006528
		public ConfigException(string message)
			: base(message)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008331 File Offset: 0x00006531
		public ConfigException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000833B File Offset: 0x0000653B
		public ConfigException()
		{
		}

		// Token: 0x02000064 RID: 100
		public class InvalidSettingValue : ConfigException
		{
			// Token: 0x06000239 RID: 569 RVA: 0x00009070 File Offset: 0x00007270
			public InvalidSettingValue(string message)
				: base(message)
			{
			}
		}

		// Token: 0x02000065 RID: 101
		public class InvalidUrlReservation : ConfigException
		{
			// Token: 0x0600023A RID: 570 RVA: 0x00009070 File Offset: 0x00007270
			public InvalidUrlReservation(string message)
				: base(message)
			{
			}
		}

		// Token: 0x02000066 RID: 102
		public class MissingConfigFileEntry : ConfigException
		{
			// Token: 0x0600023B RID: 571 RVA: 0x00009070 File Offset: 0x00007270
			public MissingConfigFileEntry(string message)
				: base(message)
			{
			}
		}

		// Token: 0x02000067 RID: 103
		public class MissingConfigFile : ConfigException
		{
			// Token: 0x0600023C RID: 572 RVA: 0x00009070 File Offset: 0x00007270
			public MissingConfigFile(string message)
				: base(message)
			{
			}
		}
	}
}
