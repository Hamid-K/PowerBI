using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NLog
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class NLogConfigurationException : Exception
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000891C File Offset: 0x00006B1C
		public NLogConfigurationException()
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00008924 File Offset: 0x00006B24
		public NLogConfigurationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000892D File Offset: 0x00006B2D
		[StringFormatMethod("message")]
		public NLogConfigurationException(string message, params object[] messageParameters)
			: base(string.Format(message, messageParameters))
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000893C File Offset: 0x00006B3C
		[StringFormatMethod("message")]
		public NLogConfigurationException(Exception innerException, string message, params object[] messageParameters)
			: base(string.Format(message, messageParameters), innerException)
		{
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000894C File Offset: 0x00006B4C
		public NLogConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00008956 File Offset: 0x00006B56
		protected NLogConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
