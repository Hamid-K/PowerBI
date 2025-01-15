using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	public abstract class VelocityPacketException : Exception
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001E12D File Offset: 0x0001C32D
		protected VelocityPacketException()
		{
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001E135 File Offset: 0x0001C335
		protected VelocityPacketException(string message)
			: base(message)
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001E13E File Offset: 0x0001C33E
		protected VelocityPacketException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001E148 File Offset: 0x0001C348
		protected VelocityPacketException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000BBB RID: 3003
		internal abstract ErrStatus GetErrorStatus();
	}
}
