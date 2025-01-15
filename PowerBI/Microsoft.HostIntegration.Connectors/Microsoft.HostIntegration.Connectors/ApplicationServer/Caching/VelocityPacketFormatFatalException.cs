using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200017A RID: 378
	[Serializable]
	internal class VelocityPacketFormatFatalException : VelocityPacketFormatException
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x000282D7 File Offset: 0x000264D7
		public VelocityPacketFormatFatalException()
			: base("The received stream could not be parsed. The transport channel will now be closed.")
		{
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000282E4 File Offset: 0x000264E4
		public VelocityPacketFormatFatalException(string message)
			: base(message)
		{
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000282ED File Offset: 0x000264ED
		public VelocityPacketFormatFatalException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x000282F7 File Offset: 0x000264F7
		protected VelocityPacketFormatFatalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00028301 File Offset: 0x00026501
		internal VelocityPacketFormatFatalException(string format, params object[] args)
			: this(string.Format(CultureInfo.InvariantCulture, format, args))
		{
		}
	}
}
