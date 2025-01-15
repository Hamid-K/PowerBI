using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	internal class VelocityPacketFormatException : VelocityPacketException
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x00028295 File Offset: 0x00026495
		public VelocityPacketFormatException()
			: base("The received stream could not be parsed. The server will ignore this packet and attempt to recover from this exception.")
		{
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000282A2 File Offset: 0x000264A2
		public VelocityPacketFormatException(string message)
			: base(message)
		{
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000282AB File Offset: 0x000264AB
		public VelocityPacketFormatException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000282B5 File Offset: 0x000264B5
		protected VelocityPacketFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000282BF File Offset: 0x000264BF
		internal VelocityPacketFormatException(string format, params object[] args)
			: this(string.Format(CultureInfo.InvariantCulture, format, args))
		{
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000282D3 File Offset: 0x000264D3
		internal override ErrStatus GetErrorStatus()
		{
			return ErrStatus.INVALID_REQUEST_BODY;
		}
	}
}
