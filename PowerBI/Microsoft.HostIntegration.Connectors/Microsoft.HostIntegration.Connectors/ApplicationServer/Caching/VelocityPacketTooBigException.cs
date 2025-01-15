using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	internal sealed class VelocityPacketTooBigException : VelocityPacketFormatFatalException
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x00028315 File Offset: 0x00026515
		public VelocityPacketTooBigException()
			: this("Message larger than configured maximum message size.")
		{
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00028322 File Offset: 0x00026522
		public VelocityPacketTooBigException(string message)
			: base(message)
		{
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002832B File Offset: 0x0002652B
		public VelocityPacketTooBigException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00028335 File Offset: 0x00026535
		private VelocityPacketTooBigException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
