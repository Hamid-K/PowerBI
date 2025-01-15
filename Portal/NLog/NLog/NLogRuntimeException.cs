using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NLog
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class NLogRuntimeException : Exception
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00008960 File Offset: 0x00006B60
		public NLogRuntimeException()
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00008968 File Offset: 0x00006B68
		public NLogRuntimeException(string message)
			: base(message)
		{
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00008971 File Offset: 0x00006B71
		[StringFormatMethod("message")]
		public NLogRuntimeException(string message, params object[] messageParameters)
			: base(string.Format(message, messageParameters))
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00008980 File Offset: 0x00006B80
		public NLogRuntimeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000898A File Offset: 0x00006B8A
		protected NLogRuntimeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
